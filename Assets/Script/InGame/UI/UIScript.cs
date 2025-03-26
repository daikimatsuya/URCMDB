using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Usefull;

//�C���Q�[������UI�̏���
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


    //�����ɏ�����
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
    //������
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

    //UI�S�ʊǗ��֐�
    public void UIController(in bool isPose)
    {
        ps=pcs.GetPlayer();
        CheckController();
        if (ps != null)
        {
            GetPlayerRot();                                                                                                                                         //�v���C���[�̊p�x�擾
            YawUIController();                                                                                                                                    //�v���C���[��X���̊p�x�\��
            playerSpeedMeterScript.SetPlayerSpeed((int)ps.GetPlayerSpeedFloat(), (int)ps.GetPlayerSpeedBuffFloat());    //�v���C���[�̃X�s�[�h���[�^�[�\��
            sensorUIScript.SensorUIController();                                                                                                         //�Z���T�[��UI�\��


        }
        TutorialUI(isControllerConect);                                                                                                //�`���[�g���A��UI�𓮂���
        PMSMode();                                                                                                                          //PMS�\��
        IsGameOver(isPose);                                                                                                             //�Q�[���I�[�o�[��UI�\��
        TargetMarkerUI();                                                                                                                 //�^�[�Q�b�g�}�[�J�[�\��
        ActiveChecker(isPose);                                                                                                          //�Q�[����ʂɕ\������Canvas�̃t���O�Ǘ�
        poseKeyUI.ViewPoseMenu(in isControllerConect);                                                                    //�|�[�Y���j���[UI�\��
        playerStateEffectControllerScript.PlayerStateEffectController();                                                 //���ʃG�t�F�N�g�Ǘ�
    }
    //�v���C���[������ł��������
    private void ActiveChecker(in bool isPose)
    {
        if (ps==null||isPose)  //�v���C���[������ł���Ƃ��ɕ\������UI///////////////////////
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

        else   //�v���C���[�������Ă���Ƃ��ɕ\������UI/////////////////////////////////////////
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
    //�v���C���[�̊p�x�擾
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
    //�v���C���[�̌�����UI�ɂ����
    private void YawUIController()
    {
        yawUItf.localPosition = new Vector3(yawUItf.localPosition.x, playerRot.x*-YawUIMag, 0);
    }
    //�Q�[���I�[�o�[����UI�Ǘ�
    private void IsGameOver(in bool isPose)
    {
        if(isGameOver||isPose)    //�Q�[���I�[�o�[�ɂȂ��Ă���Ƃ�////////////////////////////////////////////////////////////////
        {
            
            gameOverUI.transform.localPosition = gameOverUIPos;

            
            if (Input.GetKeyDown(KeyCode.Space) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))//����////////
            {
                if (goUs.GetPos() < -1)
                {
                    retryFlag = true; //���g���C
                }
                else if (goUs.GetPos() > 1)
                {
                    backtitleFlag = true; //�^�C�g���ɖ߂�
                }
            }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //���[�h�ύX///////////////////////////////////////////////////////////////////////////////////
            if (TimeCountScript.TimeCounter(ref gameOverUIIntervalBuff))
            {
                canSelect = true;
            }
            if (canSelect)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Usefull.GetStickScript.GetAxisDown("LeftStickX") < -0.1f)
                {
                    goUs.MoveRetry();   //���[�h�����g���C��
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Usefull.GetStickScript.GetAxisDown("LeftStickX") > 0.1f)
                {
                    goUs.MoveBackTitle();    //���[�h���o�b�N�^�C�g����
                }
            }
            
            ///////////////////////////////////////////////////////////////////////////////////////////////

            goUs.TargetHpUI();  //�^�[�Q�b�g�̎c��̗͂�\��
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        else   //UI���j���[�����Z�b�g������///////////////////////////////////
        {
            gameOverUI.transform.localPosition = initialGameOverPos;
            goUs.ResetPos();
        }///////////////////////////////////////////////////////////////////////
    }
    //PMS�̃I���I�t�\��
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

    //�^�[�Q�b�g�̈ʒu��UI�ɕ\��
    private void TargetMarkerUI()
    {
        if (!targetTransform)
        {
            return;
        }
        //�}�[�J�[�̍��W���Z�o
        Vector2 pos;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, targetTransform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(markerCanvas, screenPos, mainCamera, out pos);
        targetMarker.transform.localPosition = pos;
        ///////////////////////
        

        //�^�[�Q�b�g���J�����̉�p�Ɏ��܂��Ă���Ƃ��ɕ\��������
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

    #region �l�󂯓n��
    public void SetIsGameOver(in bool  isGameOver)
    {
        this.isGameOver = isGameOver;
    }
    //�v���C���[�擾�p
    public void SetPlayer(in PlayerScript ps)
    {
        this.ps = ps;
    }
    //�^�[�Q�b�g�擾�p
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


    //�`���[�g���A��UI�𓮂����悤
    private void TutorialUI(in bool isConect)
    {
        if(tutorialUIScript!=null)
        {
            tutorialUIScript.TutorialUIController(in ps,in isConect);
        }
    }

}
