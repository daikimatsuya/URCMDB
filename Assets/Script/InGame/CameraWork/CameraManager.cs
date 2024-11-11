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


    public  void CameraController()
    {
        mf.MovieFadeController();
        if(isPlayerDead )
        {
            pcs.ExplodeCamera();
        }
        if (player == null)
        {
            isPlayerDead = true;     
        }
        else
        {
            if (mf.GetEffectEnd())
            {
                isPlayerDead= false;
                if (pcs.GetPos().y <= drownPos)
                {
                    watarEffect.SetActive(true);
                }
                else
                {
                    watarEffect.SetActive(false);
                }
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
    //爆破時のフェード管理
    private void ExplodeFadeController()
    {
        if (!isExplodeEffectFade)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                explodeEffectTimeBuff = 0;
            }
            if (TimeCountScript.TimeCounter(ref explodeEffectTimeBuff))
            {
                mf.SetShadeLevel(2);
                TimeCountScript.SetTime(ref explodeFadeTimeBuff, explodeFadeTime);
                isExplodeEffectFade = true;
            }
        }
        else
        {

            if (TimeCountScript.TimeCounter(ref explodeFadeTimeBuff))
            {
                mf.SetShadeLevel(3);
                gm.SetPlayerSpawnFlag();
                isExplodeEffectFade = false;
                TimeCountScript.SetTime(ref explodeEffectTimeBuff, explodeEffectTime);
            }

        }
    }

    //プレイヤー取得用
    public void SetPlayer(PlayerScript player)
    {
        if (pcs == null)
        {
            pcs = GameObject.FindWithTag("GameCamera").GetComponent<PlayerCameraScript>();
        }
        this.player = player;
        pcs.SetPlayer(this.player.transform);
    }
    //初期化がされてないときに他のスクリプトから呼び出されたときに初期化する
    private void InitialSet()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        pcs = GameObject.FindWithTag("GameCamera").GetComponent<PlayerCameraScript>();
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
