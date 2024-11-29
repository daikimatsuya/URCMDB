using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private PlayerScript ps;
    private Transform targetPos;
    private CameraManager cm;
    private GameObject player;
    private SelectWeatherScript sws;

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


    //ゲームシステムを動かす
    private void GameManagerController()
    {
        ChangePMS();
        PlayerCheck();
        cm.CameraController();
        BreakTimeContoller();
        sws.WeatherSetting(cm);
    }
    //リトライするときにシーンをロード
    public void Retry()
    {
        SceneManager.LoadScene(stage);
    }
    //タイトルに戻るときにシーンをロード
    public void BackTitle()
    {
        SceneManager.LoadScene(title);
    }
    //プレイヤーの生存確認と生成
    private void PlayerCheck()
    {
        if (player==null)
        {
            playerDead = true;
            isCanShot = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerSpawnFlag = true;
            }
            
            if(playerSpawnFlag)
            {
                if (playerMissile > 0)
                {
                    PlayerSpawn();
                    CreateFadeObject();
                    playerSpawnFlag = false;
                }
                else
                {
                    gameOverFlag = true;
                }
            }

        }
        else
        {
            playerDead= false;
            playerSpeed = ps.GetPlayerSpeedFloat();
            playerSpeedBuff = ps.GetPlayerSpeedBuffFloat();
        }
    }
    //プレイヤー生成
    private void PlayerSpawn()
    {
        if (cm == null)
        {
            InitialSet();
        }
        player = Instantiate(playerPrefab);
        ps = player.GetComponent<PlayerScript>();
        cm.SetPlayer(ps);
        playerMissile--;

    }
    private void BreakTimeContoller()
    {
        if (isClear)
        {
            if (breakTimeBuff <= 0)
            {
                isTargetBreak = true;
                playerMissile = 0;
            }
            breakTimeBuff--;
        }
    }
    //開始演出生成
    private void CreateFadeObject()
    {
        Transform uiTransform = GameObject.FindWithTag("UICanvas").transform;
        GameObject __ = Instantiate(fadeObjectPrefab);
        __.transform.SetParent(uiTransform);
        __.transform.localScale = Vector3.one;
        __.transform.localPosition = Vector3.zero;
        __.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    //PMSのオンオフ
    private void ChangePMS()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PMS)
            {
                PMS = false;
            }
            else
            {
                PMS = true;
            }
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
    public void SetPlayerSpawnFlag()
    {
        playerSpawnFlag = true;
    }
    #endregion
    private void Awake()
    {
        InitialSet();
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
