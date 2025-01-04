using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//インゲーム内のUIの処理
public class UIScript : MonoBehaviour
{
    private GameManagerScript gm;
    private Transform yawUItf;
    private GameObject gameOverUI;
    private GameOverUIScript goUs;

    private Vector3 playerRot;
    private Vector3 targetPos;
    private TextMeshProUGUI pmsTex;
    private TextMeshProUGUI playerSpeedTex;
    private TextMeshProUGUI playerSpeedBuffTex;
    private bool targetMarkerflag;
    private Camera mainCamera;
    private RectTransform markerCanvas;
    private GameObject targetMarker;

    [SerializeField] private float YawUIMag;
    [SerializeField] private Vector3 gameOverUIPos;
    [SerializeField] private GameObject pms;
    [SerializeField] private GameObject playerSpeed;
    [SerializeField] private GameObject playerSpeedBuff;

    [SerializeField] private GameObject yawUI;
    [SerializeField] private GameObject yawUI2;
    [SerializeField] private GameObject rowImage;
    [SerializeField] private WeatherUIScript weatherUIScript;
    [SerializeField] private SensorUIScript sensorUIScript;

    //UI全般管理関数
    private void UIController()
    {
        GetPlayerRot(); //プレイヤーの角度取得
        YawUIController();  //プレイヤーのX軸の角度表示
        PMSMode();  //PMS表示
        IsGameOver();   //ゲームオーバーのUI表示
        PlayerSpeedUI();    //プレイヤーの速度表示
        TargetMarkerUI();   //ターゲットマーカー表示
        ActiveChecker();    //ゲーム画面に表示するCanvasのフラグ管理
        sensorUIScript.SensorUIController();    //センサーのUI表示
    }
    //プレイヤーが死んでいたら消す
    private void ActiveChecker()
    {
        if (gm.IsPlayerDead())  //プレイヤーが死んでいるときに表示するUI///////////////////////
        {
            targetMarker.SetActive(false);
            pms.SetActive(false);
            playerSpeed.SetActive(false);
            playerSpeedBuff.SetActive(false);
            yawUI.SetActive(false);
            yawUI2.SetActive(false);
            rowImage.SetActive(false);
            weatherUIScript.SetWeatherUIActive(false);
            sensorUIScript.SetSensorActive(false);
        }///////////////////////////////////////////////////////////////////////////////////////////

        else   //プレイヤーが生きているときに表示するUI/////////////////////////////////////////
        {
            if(targetMarkerflag)
            {
                targetMarker.SetActive(true);
            }
            
            pms.SetActive(true);
            playerSpeed.SetActive(true);
            playerSpeedBuff.SetActive(true);
            yawUI.SetActive(true);
            yawUI2.SetActive(true);
            rowImage.SetActive(true);
           weatherUIScript.SetWeatherUIActive(true);
            sensorUIScript.SetSensorActive(true);
        }///////////////////////////////////////////////////////////////////////////////////////////
    }
    //プレイヤーの角度取得
    private void GetPlayerRot()
    {
        playerRot = gm.GetPlayerRot();
    }
    //プレイヤーの向きをUIにいれる
    private void YawUIController()
    {
        yawUItf.localPosition = new Vector3(yawUItf.localPosition.x, playerRot.x*-YawUIMag, 0);
    }
    //ゲームオーバー時のUI管理
    private void IsGameOver()
    {
        if(gm.GetGameOverFlag())    //ゲームオーバーになっているとき////////////////////////////////////////////////////////////////
        {
            
            gameOverUI.transform.localPosition = gameOverUIPos;

            //モード変更///////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                goUs.MoveRetry();   //モードをリトライに
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
               goUs.MoveBackTitle();    //モードをバックタイトルに
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (goUs.GetPos() < -1)
                {
                    gm.Retry(); //リトライ
                }
                else if(goUs.GetPos() > 1)
                {
                    gm.BackTitle(); //タイトルに戻る
                }
            }
            goUs.TargetHpUI();  //ターゲットの残り体力を表示
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    //PMSのオンオフ表示
    private void PMSMode()
    {
        if (gm.GetPMS())
        {
            pmsTex.text = "PMS:ON";
        }
        else
        {
            pmsTex.text = "PMS:OFF";
        }
    }
    //プレイヤーのスピードをUIに表示
    private void PlayerSpeedUI()
    {
        playerSpeedTex.text = (int)gm.GetPlayerSpeed() + "M/S"; //プレイヤーの基本速度表示
        playerSpeedBuffTex.text = (int)gm.GetPlayerSpeedBuff() + "M/S"; //プレイヤーの移動速度を表示

    }
    //ターゲットの位置をUIに表示
    private void TargetMarkerUI()
    {
        //マーカーの座標を算出
        Vector2 pos;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, targetPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(markerCanvas, screenPos, mainCamera, out pos);
        targetMarker.transform.localPosition = pos;
        ///////////////////////
        

        //ターゲットがカメラの画角に収まっているときに表示させる
        if (Vector3.Dot(mainCamera.transform.forward, (targetPos - mainCamera.transform.position)) < 0)
        {
            targetMarkerflag = false;
            targetMarker.SetActive(false);
        }
        else
        {
            targetMarkerflag = true;
            targetMarker.SetActive(true);
        }
        ////////////////////////////////////////////////////////////
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        //canvasPos = GetComponent<RectTransform>();
        mainCamera=GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        gm=GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        yawUItf = GameObject.FindWithTag("YawUI2").GetComponent<Transform>();
        gameOverUI = GameObject.FindWithTag("GameOverUI");
        goUs=gameOverUI.GetComponent<GameOverUIScript>();
        markerCanvas = GameObject.FindWithTag("MarkerCanvas").GetComponent<RectTransform>();
        targetMarker = GameObject.FindWithTag("TargetMarker");

        pmsTex = pms.GetComponent<TextMeshProUGUI>();
        playerSpeedTex = playerSpeed.GetComponent<TextMeshProUGUI>();
        playerSpeedBuffTex = playerSpeedBuff.GetComponent<TextMeshProUGUI>();

        //wus = weatherUI.GetComponent<WeatherUIScript>();
        weatherUIScript.SetWeatherScript(gm.GetWeatherScript());

        targetPos = gm.GetTargetPos();
    }

    // Update is called once per frame
    void Update()
    {
        UIController();
    }
}
