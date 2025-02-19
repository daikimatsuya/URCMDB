using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//インゲーム中のカメラ管理
public class CameraManager : MonoBehaviour
{
    [SerializeField] private float explodeEffectTime;
    private int explodeEffectTimeBuff;
    [SerializeField] private float explodeFadeTime;
    private int explodeFadeTimeBuff;
    [SerializeField] private GameObject movieCanvas;
    [SerializeField] private GameObject watarEffect;
    [SerializeField] private float drownPos;

    private PlayerCameraScript pcs;
    private GameObject mainCanvas;
    private MovieFade mf;
    private PlayerScript ps;
    private TargetScript ts;
    private PlayerControllerScript playerController;
  
    private bool isPlayerDead;
    private bool isTargetBreak;

    //カメラ管理
    public  void CameraController(in bool isPose)
    {
        ps = playerController.GetPlayer();
        mf.MovieFadeController();   //カメラのフェード

        if (isPlayerDead )  //プレイヤーが爆発したか////////////////
        {
            ExplodeCameraController();  //爆発時のカメラの動き
        }//////////////////////////////////////////////////////////////


        if (ps == null) //プレイヤーオブジェクトがない時///////
        {
            isPlayerDead = true;   
            watarEffect.SetActive(false);   //水中のエフェクト停止
        }/////////////////////////////////////////////////////////////

        else   //プレイヤーオブジェクトがあるとき//////////////////////////////////////////////////////
        {
            SetWaterEffect();   //水中のカメラ演出管理
            if (mf.GetEffectEnd())  //フェード演出が終わったら////////////////
            {
                isPlayerDead= false;

                if (ps.GetControll())   //プレイヤーが操作できるとき////
                {
                    
                    pcs.FollowPlayerInShoot(in isPose,in ps);  //プレイヤーを追従
                }/////////////////////////////////////////////////////////////

                else   //プレイヤーが発射台に固定されてる時///
                {
                    pcs.FollowPlayerInSet(in ps);
                }////////////////////////////////////////////////

            }///////////////////////////////////////////////////////////////////////

            else   //フェード演出/////////////
            {
                pcs.MovieCut(); //ムービー中の演出
            }/////////////////////////////////

            CanvasActive(mf.GetEffectEnd());    //演出時のCanvas切り替え
        }
    }
    //爆発時のカメラ
    private void ExplodeCameraController()
    {
        if (ts)
        {
            isTargetBreak = ts.GetBreak();
        }
        if (isTargetBreak) //目標が破壊されたとき////////////////////
        {
            pcs.ClearCamera();
            return;
        }///////////////////////////////////////////////////

        if (ts.GetHit()) //目標にあたった時/////////////
        {

            pcs.HitExplodeCamera();
            return;
        }///////////////////////////////////////////////
        pcs.MissExplodeCamera();    
    }
    //水に入った時の演出管理
    private void SetWaterEffect()
    {
        if (pcs.GetPos().y <= drownPos)
        {
            //指定した座標の下に入ると画面が青くなる
            watarEffect.SetActive(true);
        }
        else
        {
            //上がると戻る
            watarEffect.SetActive(false);
        }
    }
    //開始演出からゲーム画面へのキャンバスのオンオフ管理
    private void CanvasActive(bool flag)
    {
        if (flag)
        {
            //ゲーム画面オン
            movieCanvas.SetActive(false);
            mainCanvas.SetActive(true);
        }
        else
        {
            //演出画面オン
            mainCanvas.SetActive(false);
            movieCanvas.SetActive(true);
        }
    }

    #region 値受け渡し
    public PlayerCameraScript GetPlayerCamera()
    {
        if (pcs == null)
        {
            pcs = GameObject.FindWithTag("GameCamera").GetComponent<PlayerCameraScript>();
        }
        return pcs;
    }
    public void SetTarget(in TargetScript target)
    {
        ts = target;
    }

    #endregion
    public void AwakeCameraManager(in PlayerControllerScript player)
    {
        GameObject _ = GameObject.FindWithTag("GameCamera");
        pcs = _.GetComponent<PlayerCameraScript>();
        pcs.AwakePlayerCamera();
        mainCanvas = GameObject.FindWithTag("UICanvas");
        mf = GetComponent<MovieFade>();
        playerController = player;
    }
    //初期化がされてないときに他のスクリプトから呼び出されたときに初期化する
    public void StartCameraManager()
    {
        mf.SetShadeLevel(1);
        pcs.SetMF(mf);
        ps = playerController.GetPlayer();
        pcs.SetPlayer(ps.transform,ps);
        TimeCountScript.SetTime(ref explodeEffectTimeBuff, explodeEffectTime);
        mainCanvas.SetActive(false);
        watarEffect.SetActive(false);
    }

}
