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
    [SerializeField] private string testScene;

    private string stage;
    private bool isCanMoveCamera;
    private bool isShoot;

    [SerializeField] private float betTime;

    public Light Light { get; set; }


    //フラグ関連処理
    private void Shoot()
    {
        if (sss.GetFadeEnd())
        {
            ChangeStage();
        }
        if (!tc.GetCanShot())
        {
            return;
        }

        stage=sss.GetStage();

        if (stage == "")    //選択されているステージがタイトルだったらタイトル画面に戻す////
        {
            if(Input.GetKeyDown(KeyCode.Space) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
            {
                isCanMoveCamera = true;
                ResetTitle();        //タイトルに戻る
            }

            return;
        }////////////////////////////////////////////////////////////////////////////////////////

        isCanMoveCamera = false;
        if (Input.GetKeyDown(KeyCode.Space) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            isShoot = true;             //ステージチェンジ演出開始

        }

    }

    //タイトル画面に戻す用フラグセット
    private void ResetTitle()
    {
        ts.SetResetFlag(true);           //タイトルのゲームをリセット
        sss.StageSelectReset();         //ステージ選択リセット
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
    private void ChangeStage()
    {
        Usefull.GetTriggerScript.SetValue();
        SceneManager.LoadScene(stage);
    }



    //タイトル管理
    private void TitleController()
    {
        Usefull.GetTriggerScript.AxisUpdate();              //トリガーの入力情報を更新
        Usefull.GetControllerScript.SearchController();  //コントローラーが接続されているかを確認

        SetWeather();                                                              //天候を操る
        Shoot();                                                                      //ステージに発射
        sss.SelectController(SelectFlag());                                 //ステージセレクト  
        scas.UpDown( tc.GetCanShot());                                   //発射台の上下管理
        scmas.Shoot(in isShoot,scas.GetEndDown());                 //プレイヤー発射管理
        tc.CameraController(in isCanMoveCamera);                    //カメラ移動管理

        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeTestScene();
        }
    }

    //テストシーンへの移動　後々消す
    private void ChangeTestScene()
    {
        SceneManager.LoadScene(testScene);
    }

    //発射アニメーション用フラグ
    private bool SelectFlag()
    {
        if (!tc.GetCanShot()||isShoot)
        {
            return true;
        }
        return false;
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

    //タイトル初期化
    private void StartTitle()
    {
        Application.targetFrameRate = 60;
        GetComponets();
        sss.StartStageSelect();
        scas.StartSceneChangeAnimation();
        scmas.StartSceneChandeMissleAnimation();
        tc.StartTitleCamera();

        isCanMoveCamera = true;
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
