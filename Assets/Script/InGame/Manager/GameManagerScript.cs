using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Usefull;

//インゲームを回す
public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject fadeObjectPrefab;
    [SerializeField] private int playerMissile;
    [SerializeField] private string stage;
    [SerializeField] private string title;
    [SerializeField] private float breakTime;
    private int breakTimeBuff;
    [SerializeField] private float respawnTimer;
    private int respawnTimerBuff;

    private PlayerScript ps;
    private GameObject activatingFadeObject;
    private Transform targetPos;
    private CameraManager cm;
    private GameObject player;
    private SelectWeatherScript sws;
    private LaunchPointScript lp;
    private GameObject launchPad;
    private UIScript us;
    private TargetScript ts;
    private ListManager lm;
    private Transform uiTransform;
    private PlayerControllerScript pcs;


    private bool playerSpawnFlag;
    private bool isPose;


    //ゲームシステムを動かす
    private void GameManagerController()
    {
        Usefull.GetTriggerScript.AxisUpdate();                            //トリガーの入力情報を更新
        Usefull.GetStickScript.AxisUpdate();                               //スティック入力情報を更新
        Usefull.GetControllerScript.SearchController();                //コントローラー接続確認更新

        SceneChanges();                                                          //シーン変更
        us.SetIsGameOver(pcs.GetGameOverFlag());                 //ゲームオーバーフラグ挿入
        InGameController(isPose);                                            //ゲームを動かす
        PoseChange();                                                             //ポーズ設定切り替え
       
    }
    //初期化がされてないときに他のスクリプトから呼び出されたときに初期化する
    private void AwakeGameManger()
    {
        Application.targetFrameRate = 60;
        GetComponents();                                //コンポーネント群取得
        Usefull.PMSScript.SetPMS(false);
        lm.AwakeListManager();
        lp.AwakeLaunchPoint();
        us.AwakeUIScript();
        cm.AwakeCameraManager(in pcs);
    }
    private void StartGameManager()
    {

        pcs.StartPlayerController(in lp, in uiTransform);
        cm.StartCameraManager();
        cm.SetTarget(ts);
        us.StartUIScript(in pcs);
        us.SetTarget(in targetPos);
        sws.WeatherSetting(cm);
        us.SetWeatherScript(sws);
        TimeCountScript.SetTime(ref breakTimeBuff, breakTime);
        ts.StartTarget();
        isPose = false;
        
    }

    //ゲームを動かす
    private void InGameController(in bool isPose)
    {
        pcs.PlayerCheck();                                                 //プレイヤーがゲームにいるかを確認
        us.UIController(isPose);                                         //UI管理
        lm.ListManagerController(pcs.GetPlayer(),isPose);   //リスト群管理

        if (ts != null)
        {
            ts.TargetController(in isPose);                            //ターゲット管理
        }
        pcs.PlayerController(in isPose);                              //プレイヤー管理
        lp.LaunchPointController(in isPose);                       //発射台管理       
        cm.CameraController(in isPose);                           //カメラ管理
    }

    //リトライするときにシーンをロード
    public void Retry()
    {
        Usefull.GetTriggerScript.SetValue();
        SceneManager.LoadScene(stage);
    }
    //タイトルに戻るときにシーンをロード
    public void BackTitle()
    {
        Usefull.GetTriggerScript.SetValue();
        SceneManager.LoadScene(title);
    }
    //シーン変更用
    private void SceneChanges()
    {
        if(us.GetRetryFlag())
        {
            Retry();
        }
        if(us.GetBacktitleFlag())
        {
            BackTitle();
        }
    }
    //ポーズフラグ切り替え
    private void PoseChange()
    {
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown("joystick button 7"))   //ポーズフラグ切り替え
        {
            if (isPose)
            {
                isPose = false;
            }
            else
            {
                isPose = true;
            }

        }
    }


    //コンポーネント群を取得
    private void GetComponents()
    {
        us = GameObject.FindWithTag("UICanvas").GetComponent<UIScript>();
        uiTransform = GameObject.FindWithTag("UICanvas").transform;   
        GameObject target = GameObject.FindWithTag("Target");
        targetPos = target.GetComponent<Transform>();
        ts = target.GetComponent<TargetScript>();
        cm = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
        launchPad = GameObject.FindWithTag("LaunchPoint");
        lp = launchPad.GetComponent<LaunchPointScript>();
        sws = GetComponent<SelectWeatherScript>();
        lm = new ListManager();
        pcs=GetComponent<PlayerControllerScript>();
    }

    private void Awake()
    {
        AwakeGameManger();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        GameManagerController();
    }
}
