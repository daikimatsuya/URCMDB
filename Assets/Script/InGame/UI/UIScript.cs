using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Usefull;

//インゲーム内のUIの処理
public class UIScript : MonoBehaviour
{
    private Transform yawUItf;
    private GameObject gameOverUI;
    private GameOverUIScript goUs;
    private PlayerScript ps;
    private PlayerControllerScript pcs;

    private Vector3 playerRot;
    private Transform targetTransform;
    private TextMeshProUGUI pmsTex;
    private bool targetMarkerflag;
    private Camera mainCamera;
    private RectTransform markerCanvas;
    private GameObject targetMarker;
    private bool canSelect;
    private bool retryFlag=false;
    private bool backtitleFlag = false;
    private bool isPMS;
    private bool isGameOver;
    private Vector3 initialGameOverPos;
    private bool isControllerConect;

    [SerializeField] private float YawUIMag;
    [SerializeField] private Vector3 gameOverUIPos;
    [SerializeField] private GameObject pms;
    [SerializeField] private GameObject yawUI;
    [SerializeField] private GameObject yawUI2;
    [SerializeField] private GameObject rowImage;
    [SerializeField] private WeatherUIScript weatherUIScript;
    [SerializeField] private SensorUIScript sensorUIScript;
    [SerializeField] private PlayerSpeedMeterScript playerSpeedMeterScript;
    [SerializeField] private float gameOverUIInterval;
    private int gameOverUIIntervalBuff;
    [SerializeField] private TutorialUIScript tutorialUIScript;
    [SerializeField] private PoseMenuScript poseKeyUI;
    [SerializeField] private PlayerStateEffectControllerScript playerStateEffectControllerScript;


    //早期に初期化
    public void AwakeUIScript()
    {
        yawUItf = GameObject.FindWithTag("YawUI2").GetComponent<Transform>();
        pmsTex = pms.GetComponent<TextMeshProUGUI>();
        markerCanvas = GameObject.FindWithTag("MarkerCanvas").GetComponent<RectTransform>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        targetMarker = GameObject.FindWithTag("TargetMarker");
        gameOverUI = GameObject.FindWithTag("GameOverUI");
      

        if(tutorialUIScript != null)
        {
            tutorialUIScript.AwakeTutorialUI();
        }
        
    }
    //初期化
    public void StartUIScript(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;

        playerSpeedMeterScript.StartPlayerSpeedMeterScript();
        goUs = gameOverUI.GetComponent<GameOverUIScript>();
        goUs.StartGameOverUI();
        initialGameOverPos = gameOverUI.transform.localPosition;
        Usefull.TimeCountScript.SetTime(ref gameOverUIIntervalBuff, gameOverUIInterval);
        canSelect = false;
        CheckController();
        poseKeyUI.StartPoseMenu(in isControllerConect);
        playerStateEffectControllerScript.StartPlayerStateEffectController(pcs);
    }

    //UI全般管理関数
    public void UIController(in bool isPose)
    {
        ps=pcs.GetPlayer();
        CheckController();
        if (ps != null)
        {
            GetPlayerRot();                                                                                                                                         //プレイヤーの角度取得
            YawUIController();                                                                                                                                    //プレイヤーのX軸の角度表示
            playerSpeedMeterScript.SetPlayerSpeed((int)ps.GetPlayerSpeedFloat(), (int)ps.GetPlayerSpeedBuffFloat());    //プレイヤーのスピードメーター表示
            sensorUIScript.SensorUIController();                                                                                                         //センサーのUI表示


        }
        TutorialUI(isControllerConect);                                                                                                //チュートリアルUIを動かす
        PMSMode();                                                                                                                          //PMS表示
        IsGameOver(isPose);                                                                                                             //ゲームオーバーのUI表示
        TargetMarkerUI();                                                                                                                 //ターゲットマーカー表示
        ActiveChecker(isPose);                                                                                                          //ゲーム画面に表示するCanvasのフラグ管理
        poseKeyUI.ViewPoseMenu(in isControllerConect);                                                                    //ポーズメニューUI表示
        playerStateEffectControllerScript.PlayerStateEffectController();                                                 //効果エフェクト管理
    }
    //プレイヤーが死んでいたら消す
    private void ActiveChecker(in bool isPose)
    {
        if (ps==null||isPose)  //プレイヤーが死んでいるときに表示するUI///////////////////////
        {
            targetMarker.SetActive(false);
            pms.SetActive(false);
            playerSpeedMeterScript.SetSpeedMeterActive(false);
            yawUI.SetActive(false);
            yawUI2.SetActive(false);
            rowImage.SetActive(false);
            weatherUIScript.SetWeatherUIActive(false);
            sensorUIScript.SetSensorActive(false);
            poseKeyUI.SetPoseActive(false);
        }///////////////////////////////////////////////////////////////////////////////////////////

        else   //プレイヤーが生きているときに表示するUI/////////////////////////////////////////
        {
            if(targetMarkerflag)
            {
                targetMarker.SetActive(true);
            }     
            pms.SetActive(true);
            playerSpeedMeterScript.SetSpeedMeterActive(true);
            yawUI.SetActive(true);
            yawUI2.SetActive(true);
            rowImage.SetActive(true);
           weatherUIScript.SetWeatherUIActive(true);
            sensorUIScript.SetSensorActive(true);
            poseKeyUI.SetPoseActive(true);
        }///////////////////////////////////////////////////////////////////////////////////////////
    }
    //プレイヤーの角度取得
    private void GetPlayerRot()
    {
        playerRot = ps.GetPlayerRot();
        if (playerRot.x > 270)
        {
            playerRot.x -= 360;
        }
        if (playerRot.x <= -270)
        {
            playerRot.x += 360;
        }
    }
    //プレイヤーの向きをUIにいれる
    private void YawUIController()
    {
        yawUItf.localPosition = new Vector3(yawUItf.localPosition.x, playerRot.x*-YawUIMag, 0);
    }
    //ゲームオーバー時のUI管理
    private void IsGameOver(in bool isPose)
    {
        if(isGameOver||isPose)    //ゲームオーバーになっているとき////////////////////////////////////////////////////////////////
        {
            
            gameOverUI.transform.localPosition = gameOverUIPos;

            
            if (Input.GetKeyDown(KeyCode.Space) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))//決定////////
            {
                if (goUs.GetPos() < -1)
                {
                    retryFlag = true; //リトライ
                }
                else if (goUs.GetPos() > 1)
                {
                    backtitleFlag = true; //タイトルに戻る
                }
            }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //モード変更///////////////////////////////////////////////////////////////////////////////////
            if (TimeCountScript.TimeCounter(ref gameOverUIIntervalBuff))
            {
                canSelect = true;
            }
            if (canSelect)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Usefull.GetStickScript.GetAxisDown("LeftStickX") < -0.1f)
                {
                    goUs.MoveRetry();   //モードをリトライに
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Usefull.GetStickScript.GetAxisDown("LeftStickX") > 0.1f)
                {
                    goUs.MoveBackTitle();    //モードをバックタイトルに
                }
            }
            
            ///////////////////////////////////////////////////////////////////////////////////////////////

            goUs.TargetHpUI();  //ターゲットの残り体力を表示
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        else   //UIメニューをリセットさせる///////////////////////////////////
        {
            gameOverUI.transform.localPosition = initialGameOverPos;
            goUs.ResetPos();
        }///////////////////////////////////////////////////////////////////////
    }
    //PMSのオンオフ表示
    private void PMSMode()
    {
        isPMS = PMSScript.GetPMS();
        if (isPMS)
        {
            pmsTex.text = "PMS:ON";
        }
        else
        {
            pmsTex.text = "PMS:OFF";
        }
    }

    //ターゲットの位置をUIに表示
    private void TargetMarkerUI()
    {
        if (!targetTransform)
        {
            return;
        }
        //マーカーの座標を算出
        Vector2 pos;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, targetTransform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(markerCanvas, screenPos, mainCamera, out pos);
        targetMarker.transform.localPosition = pos;
        ///////////////////////
        

        //ターゲットがカメラの画角に収まっているときに表示させる
        if (Vector3.Dot(mainCamera.transform.forward, (targetTransform.position - mainCamera.transform.position)) < 0)
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
    private void CheckController()
    {
        isControllerConect=Usefull.GetControllerScript.GetIsConectic();
    }

    #region 値受け渡し
    public void SetIsGameOver(in bool  isGameOver)
    {
        this.isGameOver = isGameOver;
    }
    //プレイヤー取得用
    public void SetPlayer(in PlayerScript ps)
    {
        this.ps = ps;
    }
    //ターゲット取得用
    public void SetTarget(in Transform targetPos)
    {
        this.targetTransform = targetPos;
    }
    public bool GetRetryFlag()
    {
        return retryFlag;
    }
    public bool GetBacktitleFlag()
    {
        return backtitleFlag;
    }
    public void SetWeatherScript(in SelectWeatherScript sws)
    {
        weatherUIScript.SetWeatherScript(sws);
    }

    #endregion


    //チュートリアルUIを動かすよう
    private void TutorialUI(in bool isConect)
    {
        if(tutorialUIScript!=null)
        {
            tutorialUIScript.TutorialUIController(in ps,in isConect);
        }
    }

}
