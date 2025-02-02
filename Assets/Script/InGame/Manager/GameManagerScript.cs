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
    [SerializeField] private bool PMS;
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

    private bool gameOverFlag;
    private bool playerSpawnFlag;
    private bool isPose;


    //ゲームシステムを動かす
    private void GameManagerController()
    {
        Usefull.GetTriggerScript.AxisUpdate();//トリガーの入力情報を更新
        Usefull.GetStickScript.AxisUpdate();//スティック入力情報を更新
        Usefull.GetControllerScript.SearchController();//コントローラー接続確認更新
        PlayerCheck();  //プレイヤーがゲームにいるかを確認

        SceneChanges(); //シーン変更
        us.SetIsGameOver(in gameOverFlag);
        us.UIController();  //UI管理

        InGameController(isPose); //ゲームを動かす
       
        

        if (Input.GetKeyDown(KeyCode.Alpha9))   //ちゃんとしたポーズメニュー作るまでのつなぎ
        {
            isPose=true;
        }
    }
    //初期化がされてないときに他のスクリプトから呼び出されたときに初期化する
    private void AwakeGameManger()
    {
        Application.targetFrameRate = 60;
        GetComponents();    //コンポーネント群取得
        Usefull.PMSScript.SetPMS(false);
        lm.AwakeListManager();
        lp.AwakeLaunchPoint();
    }
    private void StartGameManager()
    {
        gameOverFlag = false;
        cm.SetTarget(ts);
        us.SetTarget(in targetPos);
        sws.WeatherSetting(cm);
        us.SetWeatherScript(sws);
        TimeCountScript.SetTime(ref breakTimeBuff, breakTime);
        CreateFadeObject();
        PlayerSpawn();
        ts.StartTarget();
        isPose = false;
    }
    private void InGameController(in bool isPose)
    {
        lm.ListManagerController(ps,isPose);   //リスト群管理
        if (isPose)
        {
            return;
        }
        if (ts != null)
        {
            ts.TargetController();  //ターゲット管理
        }
        if (ps != null)
        {
            ps.PlayerController();  //プレイヤー管理
        }

        lp.LaunchPointController(); //発射台管理       
        cm.CameraController();  //カメラ管理
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
    //プレイヤーの生存確認と生成
    private void PlayerCheck()
    {

        if (player==null)   //取得したプレイヤーがゲーム内にないことを確認/////////////////////////////////////////////////////////////
        {

            if (Input.GetKeyDown(KeyCode.Space)|| TimeCountScript.TimeCounter(ref respawnTimerBuff)||Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
            {
                SetPlayerSpawnFlag();   //プレイヤーを生成するフラグ管理
            }
            
            if(playerSpawnFlag)  //プレイヤー生成フラグ//////////////////////
            {
                if (playerMissile > 0)
                {
                    CreateFadeObject(); //プレイヤー生成時の演出生成
                    PlayerSpawn();  //プレイヤー生成
                    playerSpawnFlag = false;
                }
                else
                {
                    gameOverFlag = true;
                }
            }/////////////////////////////////////////////////////////////////

        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        else
        {

        }
    }
    //プレイヤーが死んだ後に一定時間後にリスポーンさせるフラグセット
    private void SetPlayerSpawnFlag()
    {
        playerSpawnFlag = true;
        if (!ts)
        {
            playerMissile = 0;
            return;
        }
        if (ts.GetBreak())    //クリアしてたら/////
        {
            playerMissile = 0;
        }///////////////////////////////////
    }
    //プレイヤーリスポーンタイマーリセット
    private void SetRespawnTimer()
    {
        TimeCountScript.SetTime(ref respawnTimerBuff, respawnTimer);
    }

    //プレイヤー生成
    private void PlayerSpawn()
    {
        player = Instantiate(playerPrefab); //プレイヤー生成
        ps = player.GetComponent<PlayerScript>();   //コンポーネント取得

        ps.SetFadeObject(in activatingFadeObject);
        ps.SetLaunchpad(lp);    //発射台位置情報代入
        ps.StartPlayer();
        cm.SetPlayer(ps);   //カメラにプレイヤーを登録
        us.SetPlayer(ps);   //UIにプレイヤーを登録
        playerMissile--;    //残機減少

        player.transform.SetParent(launchPad.transform);    //プレイヤーと発射台を親子付け

        SetRespawnTimer();  //プレイヤー生成用タイマーセット
    }
    //開始演出生成
    private void CreateFadeObject()
    {    
        GameObject __ = Instantiate(fadeObjectPrefab);  //フェードオブジェクト生成
        activatingFadeObject = __;
        __.transform.SetParent(uiTransform);    //UICanvasに親子付け
        __.transform.localScale = Vector3.one;  //スケール修正
        __.transform.localPosition = Vector3.zero;  //座標修正
        __.transform.localEulerAngles = new Vector3(0, 0, 0);   //角度修正
    }


    private void GetComponents()
    {
        us = GameObject.FindWithTag("UICanvas").GetComponent<UIScript>();
        uiTransform = GameObject.FindWithTag("UICanvas").transform;   //UICanvasのトランスフォームを取得
        GameObject target = GameObject.FindWithTag("Target");
        targetPos = target.GetComponent<Transform>();
        ts = target.GetComponent<TargetScript>();
        cm = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
        launchPad = GameObject.FindWithTag("LaunchPoint");
        lp = launchPad.GetComponent<LaunchPointScript>();
        sws = GetComponent<SelectWeatherScript>();
        lm = GetComponent<ListManager>();
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
