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
    private GameObject player;

    private bool isExplodeEffectFade;
    private Transform tf;
    MovieFade mf;
    public  void CameraController()
    {
        if (player == null)
        {
            ExplodeFadeController();

            return;
        }
        if (!pcs.GetEnd())
        {
            mf.MovieFadeController();
        }
        CanvasActive(pcs.GetEnd());
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
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        //pcs = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCameraScript>();
        TimeCountScript.SetTime(ref explodeEffectTimeBuff, explodeEffectTime);
        mainCanvas = GameObject.FindWithTag("UICanvas");
        mf = GetComponent<MovieFade>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
