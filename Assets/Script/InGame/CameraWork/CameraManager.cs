using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameManagerScript gm;
    private MovieFade mf;
    private PlayerScript player;
  

    private bool isExplodeEffectFade;
    private bool isPlayerDead;

    //カメラ管理
    public  void CameraController()
    {
        mf.MovieFadeController();
        if(isPlayerDead )
        {
            ExplodeCameraController();
        }
        if (player == null)
        {
            isPlayerDead = true;     
            watarEffect.SetActive(false);
        }
        else
        {
            SetWaterEffect();
            if (mf.GetEffectEnd())
            {
                isPlayerDead= false;

                if (player.GetControll())
                {
                    pcs.FollowPlayerInShoot();
                }
                else
                {
                    pcs.FollowPlayerInSet();
                }
            }
            else
            {
                pcs.MovieCut();
            }
            CanvasActive(mf.GetEffectEnd());
        }
    }
    //爆発時のカメラ
    private void ExplodeCameraController()
    {
        if(gm.GetIsHitTarget())
        {
            if (gm.GetTargetDead())
            {
                pcs.ClearCamera();
                return;
            }
            pcs.HitExplodeCamera();
            return;
        }
        pcs.MissExplodeCamera();
    }
    //水に入った時の演出管理
    private void SetWaterEffect()
    {
        if (pcs.GetPos().y <= drownPos)
        {
            watarEffect.SetActive(true);
        }
        else
        {
            watarEffect.SetActive(false);
        }
    }
    //開始演出からゲーム画面へのキャンバスのオンオフ管理
    private void CanvasActive(bool flag)
    {
        if (flag)
        {
            movieCanvas.SetActive(false);
            mainCanvas.SetActive(true);
        }
        else
        {
            mainCanvas.SetActive(false);
            movieCanvas.SetActive(true);
        }
    }


    public PlayerCameraScript GetPlayerCamera()
    {
        return pcs;
    }

    //プレイヤー取得用
    public void SetPlayer(PlayerScript player)
    {
        if (pcs == null)
        {
            pcs = GameObject.FindWithTag("GameCamera").GetComponent<PlayerCameraScript>();
        }

        this.player = player;
        pcs.SetPlayer(this.player.transform,this.player);
    }

    //初期化がされてないときに他のスクリプトから呼び出されたときに初期化する
    private void InitialSet()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        GameObject _ = GameObject.FindWithTag("GameCamera");
        pcs = _.GetComponent<PlayerCameraScript>();
        mf = GetComponent<MovieFade>();
        mf.SetShadeLevel(1);
        pcs.SetMF(mf);
        //pcs.SetMoviewFade(mf);
        TimeCountScript.SetTime(ref explodeEffectTimeBuff, explodeEffectTime);
        mainCanvas = GameObject.FindWithTag("UICanvas");
        mainCanvas.SetActive(false);
        watarEffect.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

        InitialSet();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
