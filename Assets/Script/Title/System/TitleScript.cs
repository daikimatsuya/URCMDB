using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

//タイトルを回す
public class TitleScript : MonoBehaviour
{
    private TitlegameScript ts;
    private StageSelectScript sss;
    private SceneChangeAnimationScript scas;
    private SceneChangeMissleActionScript scmas;
    private TitleCamera tc;

    private bool isSceneChangeMode;
    private string stage;
    private bool isShoot;

    [SerializeField] private float betTime;

    public Light Light { get; set; }


    //フラグ関連処理
    private void Shoot()
    {
        if (!tc.GetCanShot())
        {
            return;
        }

        stage=sss.GetStage();

        if (stage == "")    //選択されているステージがタイトルだったらタイトル画面に戻す////
        {
            if(Input.GetKeyDown(KeyCode.Space) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
            {
                ResetTitle();   //タイトルに戻る
                return;
            }
            isSceneChangeMode = false;
            return;
        }////////////////////////////////////////////////////////////////////////////////////////
        else
        {
            isSceneChangeMode = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            isShoot = true; //ステージチェンジ演出開始
        }
        if (!sss.GetFadeEnd())
        {
            return;
        }
        ChangeStage();
    }
    //タイトル画面に戻す
    private void ResetTitle()
    {
        //ステージセレクトモード用のフラグを初期化
        isSceneChangeMode=false;
        ts.SetResetFlag(true);
        /////////////////////////////////////////////
    }
    //天気を人為的に変える
    private void SetWeather()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Usefull.WebAPIScript.SetRain();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Usefull.WebAPIScript.SetSun();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Usefull.WebAPIScript.SetRial();
        }
    }
    //シーンチェンジ
    public void ChangeStage()
    {
        Usefull.GetTriggerScript.SetValue();
        SceneManager.LoadScene(stage);
    }

    #region 値受け渡し

    public bool GetIsSceneChangeModeFlag()
    {
        return isSceneChangeMode;
    }
    #endregion

    //タイトル管理
    private void TitleController()
    {
        Usefull.GetTriggerScript.AxisUpdate();              //トリガーの入力情報を更新
        Usefull.GetControllerScript.SearchController();  //コントローラーが接続されているかを確認

        SetWeather();                                                              //天候を操る
        Shoot();                                                                      //ステージに発射
        sss.SelectController(tc.GetCanShot());                                    //ステージセレクト  
        scas.UpDown(in isSceneChangeMode);                          //発射台の上下管理
        scmas.Shoot(in isShoot);                                              //プレイヤー発射管理
        tc.CameraController(in isShoot);
    }

    //コンポーネント取得
    private void GetComponets()
    {
        ts = GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();
        Light = GameObject.FindWithTag("Light").GetComponent<Light>();
        Light.color = Color.white;
        sss = GetComponent<StageSelectScript>();
        scas=GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();
        scmas=GameObject.FindWithTag("titlePlayerModel").GetComponent<SceneChangeMissleActionScript>();
        tc = GameObject.FindWithTag("MainCamera").GetComponent<TitleCamera>();
    }
    private void StartTitle()
    {
        Application.targetFrameRate = 60;
        GetComponets();
        sss.StartStageSelect();
        scas.StartSceneChangeAnimation();
        scmas.StartSceneChandeMissleAnimation();
        tc.StartTitleCamera();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartTitle();
    }

    // Update is called once per frame
    void Update()
    {
        TitleController();
    }
}
