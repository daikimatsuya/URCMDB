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

    private PlayerCameraScript pcs;
    private GameObject mainCanvas;
    private GameManagerScript gm;
    private MovieFade mf;
    private GameObject player;

    private bool isExplodeEffectFade;


    public  void CameraController()
    {
        mf.MovieFadeController();
        if (player == null)
        {
            pcs.ExplodeCamera();
        }
        else
        {
            if (mf.GetEffectEnd())
            {
                pcs.FollowPlayer();
            }
            else
            {
                pcs.MovieCut();
            }
            CanvasActive(mf.GetEffectEnd());
        }
    }
    //キャンバスのオンオフ管理
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
    public void SetPlayer(GameObject player)
    {
        this.player = player;
        pcs.SetPlayer(this.player.transform);
    }
    // Start is called before the first frame update
    void Start()
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

    }

    // Update is called once per frame
    void Update()
    {

    }
}
