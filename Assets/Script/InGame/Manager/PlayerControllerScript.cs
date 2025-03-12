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
    [SerializeField] private GameObject explodePrefab;
    [SerializeField, Range(0f, 0.3f)] private float maxBlurIntensity;
    private float blurIntnsity;
    [SerializeField, Range(0f, 0.2f)] private float boostedBlurIntensity;
    [SerializeField, Range(0f, 0.1f)] private float accelelatedBlurIntensity;
    private float minBlurIntnsity;
    [SerializeField, Range(0f, 0.1f)] private float blurIntensityBrake;


    private PlayerScript ps;

    private GameObject player;
    private bool playerSpawnFlag;
    private bool gameOverFlag;
    private GameObject activatingFadeObject;
    private LaunchPointScript lp;
    Transform uiTransform;
    private Vector3 playerdeadPos;
    private TargetScript ts;
    private ShaderController sc;

    const float epsilon=0.000001f;

    //初期化
    public void StartPlayerController(in LaunchPointScript lp,in Transform uiTransform,in TargetScript ts,in ShaderController sc)
    {
        this.ts = ts;
        this.lp = lp;
        this.sc = sc;
        this.uiTransform = uiTransform;
        playerSpawnFlag = true;
        PlayerSpawn(in this.lp,in this.uiTransform);
        gameOverFlag = false;
        blurIntnsity = 0f;
        minBlurIntnsity = 0f;
    }
    //プレイヤーを管理
    public void PlayerController(in bool isPose)
    {
        BlurController();
        if (ps)
        {
            ps.PlayerController(in isPose);
            playerdeadPos=ps.GetPlayerPos();

            return;
        }
        PlayerSpawn(lp,uiTransform);
    }
    //プレイヤーの生存確認と生成
    public void PlayerCheck()
    {
        if (player != null)   //プレイヤーがゲーム内にいたらリターン///////////
        {
            return;
        }////////////////////////////////////////////////////////////////////////
        if (ts == null)
        {
            missileCount = 0;
        }
        if (ts.GetHp() <= 0)
        {
            missileCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) || TimeCountScript.TimeCounter(ref respawnTimerBuff) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            playerSpawnFlag = true;
        }

        if (!playerSpawnFlag)  //プレイヤー生成フラグ確認//////////////
        {
            return;
        }/////////////////////////////////////////////////////////////////

    }
    //爆発エフェクト生成
    public GameObject CreateExplodeEffect(in Vector3 pos)
    {
        GameObject _ = GameObject.Instantiate(explodePrefab);
        _.transform.position = pos;
        return _;
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
        ps.StartPlayer(in lp,in activatingFadeObject);           //プレイヤー初期化
        player.transform.SetParent(lp.GetTransform());       //プレイヤーと発射台を親子付け
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

        CreateFadeObject(in uiTransform);                         //フェード生成
        CreatePlayer(in lp);                                               //プレイヤー生成
        missileCount--;                                                     //残機減少
        SetRespawnTimer();                                             //プレイヤー生成用タイマーセット

        playerSpawnFlag = false;
    }

    //ブラー管理
    public void BlurController()
    {
        if (ps==null||!ps.GetIsFire())
        {
            blurIntnsity = 0;
            sc.SetBlurIntensity(blurIntnsity);
            return;
        }

        float buff = ps.GetPlayerBoost() / ps.GetMaxBoost();             //ブーストのブラー用の値算出
        float buff2 = 0;
        if (ps.GetMaxRingBoost() > epsilon)
        {
            buff2 = ps.GetRingBoost() / ps.GetMaxRingBoost();         //加速輪のブラー用の値算出
        }
        if (buff2 > 1 - buff)
        {
            buff2 = 1 - buff;
        }
        

        buff += (buff2*buff2);                                                      //調整して合体
        if (buff > 1)
        {
            buff = 1;
        }

        blurIntnsity = (buff * buff) * maxBlurIntensity;                  //ブラーの値を算出

        

        if (Input.GetKey(KeyCode.Space) || (!GetTriggerScript.GetAxisDown("Right") && Input.GetAxis("RightTrigger") != 0)) 
        {
            minBlurIntnsity = accelelatedBlurIntensity;
        }
        else
        {
            if (minBlurIntnsity > 0)
            {
                minBlurIntnsity -= blurIntensityBrake;
                if(minBlurIntnsity < 0)
                {
                    minBlurIntnsity = 0;
                }
            }
        }

        if (blurIntnsity <= minBlurIntnsity)
        {
            blurIntnsity = minBlurIntnsity;
        }
        sc.SetBlurIntensity(blurIntnsity);          //ブラーの値を代入
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
    public Vector3 GetPlayerdeadPos()
    {
        return playerdeadPos;
    }
    public void SetClear()
    {
        missileCount = 0;
    }
    #endregion
}
