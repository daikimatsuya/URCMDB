using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Usefull;

//今のところインゲーム関連のデータのやり取りや裏方仕事全般のちのち奇麗にする
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
    private Transform targetPos;
    private CameraManager cm;
    private GameObject player;
    private SelectWeatherScript sws;
    private LaunchPointScript lp;
    private GameObject launchPad;
    

    private bool playerDead;
    private Vector3 playerRot;
    private float playerSpeed;
    private float playerSpeedBuff;
    private bool gameOverFlag;
    private bool isClear;
    private bool isCanShot;
    private bool playerSpawnFlag;
    private bool isHitTarget;
    private bool isTargetBreak;
    private bool isTutorial;

    //ゲームシステムを動かす
    private void GameManagerController()
    {
        Usefull.GetTriggerScript.AxisUpdate();//トリガーの入力情報を更新
        Usefull.GetStickScript.AxisUpdate();//スティック入力情報を更新
        Usefull.GetControllerScript.SearchController();//コントローラー接続確認
        ChangePMS();    //PMS管理
        PlayerCheck();  //プレイヤーがゲームにいるかを確認
        cm.CameraController();  //カメラ管理
        BreakTimeContoller();   //クリア後のタイマー管理
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
    //プレイヤーの生存確認と生成
    private void PlayerCheck()
    {

        if (player==null)   //取得したプレイヤーがゲーム内にないことを確認/////////////////////////////////////////////////////////////
        {
            //フラグ管理
            playerDead = true;
            isCanShot = false;
            /////////////
            ///
            if (Input.GetKeyDown(KeyCode.Space)|| TimeCountScript.TimeCounter(ref respawnTimerBuff)||Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
            {
                SetPlayerSpawnFlag();   //プレイヤーを生成するフラグ管理
            }
            
            if(playerSpawnFlag)  //プレイヤー生成フラグ//////////////////////
            {
                if (playerMissile > 0)
                {
                    PlayerSpawn();  //プレイヤー生成
                    CreateFadeObject(); //プレイヤー生成時の演出生成
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
            playerDead= false;
            playerSpeed = ps.GetPlayerSpeedFloat(); //プレイヤーの速度取得
            playerSpeedBuff = ps.GetPlayerSpeedBuffFloat(); //プレイヤーの移動速度取得
        }
    }
    //プレイヤーが死んだ後に一定時間後にリスポーンさせるフラグセット
    private void SetPlayerSpawnFlag()
    {
        playerSpawnFlag = true;
        if (isClear)    //クリアしてたら/////
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
        if (cm == null)     //CameraManagerが無かったら取得する//
        {
            InitialSet();
        }///////////////////////////////////////////////////////////////

        player = Instantiate(playerPrefab); //プレイヤー生成
        ps = player.GetComponent<PlayerScript>();   //コンポーネント取得
        ps.SetLaunchpad(lp);    //発射台位置情報代入
        cm.SetPlayer(ps);   //カメラにプレイヤーを登録
        playerMissile--;    //残機減少


        player.transform.SetParent(launchPad.transform);    //プレイヤーと発射台を親子付け

        SetRespawnTimer();  //プレイヤー生成用タイマーセット
    }
    //クリア時のターゲット破壊フラグ管理
    private void BreakTimeContoller()
    {
        if (!isClear)
        {
            return;
        }

        if (TimeCountScript.TimeCounter(ref breakTimeBuff))
        {
            isTargetBreak = true;
            playerMissile = 0;
        }
        
    }
    //開始演出生成
    private void CreateFadeObject()
    {
        Transform uiTransform = GameObject.FindWithTag("UICanvas").transform;   //UICanvasのトランスフォームを取得
        GameObject __ = Instantiate(fadeObjectPrefab);  //フェードオブジェクト生成
        __.transform.SetParent(uiTransform);    //UICanvasに親子付け
        __.transform.localScale = Vector3.one;  //スケール修正
        __.transform.localPosition = Vector3.zero;  //座標修正
        __.transform.localEulerAngles = new Vector3(0, 0, 0);   //角度修正
    }
    //PMSのオンオフ
    private void ChangePMS()
    {
        if (Input.GetKeyDown(KeyCode.P)||Input.GetKeyDown("joystick button 3"))
        {
            if (PMS)    //PMS管理//////////////////
            {
                PMS = false;
            }
            else
            {
                PMS = true;
            }//////////////////////////////////////
        }
    }

    //初期化がされてないときに他のスクリプトから呼び出されたときに初期化する
    private void InitialSet()
    {
        Application.targetFrameRate = 60;
        PMS = false;
        gameOverFlag = false;
        isClear = false;

        sws = GetComponent<SelectWeatherScript>();
        targetPos = GameObject.FindWithTag("Target").GetComponent<Transform>();
        cm = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
        launchPad = GameObject.FindWithTag("LaunchPoint");
        lp = launchPad.GetComponent<LaunchPointScript>();

        TimeCountScript.SetTime(ref breakTimeBuff, breakTime);

        PlayerSpawn();

    }
    #region 値受け渡し
    public SelectWeatherScript GetWeatherScript()
    {
        return sws;
    }
    public bool GetPMS()
    {
        return PMS;
    }
    public bool IsPlayerDead()
    {
        return playerDead;
    }

    public void PlayerRotSet(Vector3 rot)
    {
       playerRot = rot;
    }
    public Vector3 GetPlayerRot()
    {
        return playerRot;
    }
    public bool GetGameOverFlag()
    {
        return gameOverFlag;
    }
    public bool GetTargetBreakFlag()
    {
        return isTargetBreak;
    }

    public void SetClearFlag()
    {
        isClear = true;
    }
    public void SetIsHitTarget(bool flag)
    {
        isHitTarget = flag;
    }
    public bool GetIsHitTarget()
    {
        return isHitTarget;
    }
    public bool GetTargetDead()
    {
        return isClear;
    }
    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }
    public float GetPlayerSpeedBuff()
    {
        return playerSpeedBuff;
    }
    public Vector3 GetTargetPos()
    {
        targetPos = GameObject.FindWithTag("Target").GetComponent<Transform>();
        return targetPos.position;
    }
    public bool GetCanShotFlag()
    {
        return isCanShot;
    }
    public void SetGameStartFlag(bool start)
    {
        isCanShot = start;
    }

    #endregion
    private void Awake()
    {
        InitialSet();
        sws.WeatherSetting(cm);
    }
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        GameManagerController();
    }
}
