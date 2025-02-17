using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Usefull;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int missileCount;
    [SerializeField] private float respawnTimer;
    private int respawnTimerBuff;
    [SerializeField] private GameObject fadeObjectPrefab;

    private PlayerScript ps;

    private GameObject player;
    private bool playerSpawnFlag;
    private bool gameOverFlag;
    private GameObject activatingFadeObject;
    public void StartPlayerController(in LaunchPointScript lp,in Transform uiTransform)
    {
        playerSpawnFlag = true;
        PlayerSpawn(in lp,in uiTransform);
        gameOverFlag = false;
    }
    public void PlayerController(in bool isPose)
    {
        if (ps)
        {
            ps.PlayerController(in isPose);
            return;
        }

    }
    //プレイヤーの生存確認と生成
    public void PlayerCheck()
    {
        if (player != null)   //プレイヤーがゲーム内にいたらリターン///////////
        {
            return;
        }////////////////////////////////////////////////////////////////////////

        if (Input.GetKeyDown(KeyCode.Space) || TimeCountScript.TimeCounter(ref respawnTimerBuff) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            playerSpawnFlag = true;
        }

        if (!playerSpawnFlag)  //プレイヤー生成フラグ確認//////////////
        {
            return;
        }/////////////////////////////////////////////////////////////////

    }

    //開始演出生成
    private void CreateFadeObject(in Transform uiTransform)
    {
        GameObject __ = Instantiate(fadeObjectPrefab);           //フェードオブジェクト生成
        activatingFadeObject = __;                                          //フェードオブジェクトを変数に代入
        __.transform.SetParent(uiTransform);                           //UICanvasに親子付け
        __.transform.localScale = Vector3.one;                         //スケール修正
        __.transform.localPosition = Vector3.zero;                     //座標修正
        __.transform.localEulerAngles = new Vector3(0, 0, 0);   //角度修正
    }
    //プレイヤー生成
    private void CreatePlayer(in LaunchPointScript lp)
    {
        player = Instantiate(playerPrefab);                         //プレイヤー生成
        ps = player.GetComponent<PlayerScript>();           //コンポーネント取得
        ps.SetFadeObject(in activatingFadeObject);             //フェード設定
        ps.SetLaunchpad(lp);                                            //発射台位置情報代入
        ps.StartPlayer();                                                   //プレイヤー初期化
        player.transform.SetParent(lp.GetTransform());      //プレイヤーと発射台を親子付け

    }

    //プレイヤーリスポーンタイマーリセット
    private void SetRespawnTimer()
    {
        TimeCountScript.SetTime(ref respawnTimerBuff, respawnTimer);
    }

    //プレイヤーリスポーン
    public void PlayerSpawn(in LaunchPointScript lp,in Transform uiTransform)
    {
        if (!playerSpawnFlag)
        {
            return;
        }
        if (missileCount <= 0)
        {
            gameOverFlag = true;
            return;
        }

        CreatePlayer(in lp);
        CreateFadeObject(in uiTransform);
        missileCount--;                                                     //残機減少
        SetRespawnTimer();                                             //プレイヤー生成用タイマーセット

        playerSpawnFlag = false;
    }

    #region 値受け渡し
    public PlayerScript GetPlayer()
    {
        return ps;
    }
    public bool GetGameOverFlag()
    {
        return gameOverFlag;
    }
    #endregion
}
