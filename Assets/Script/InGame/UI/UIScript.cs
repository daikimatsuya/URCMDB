using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//�C���Q�[������UI�̏���
public class UIScript : MonoBehaviour
{
    private GameManagerScript gm;
    private Transform yawUItf;
    private GameObject gameOverUI;
    private GameOverUIScript goUs;

    private Vector3 playerRot;
    private Vector3 targetPos;
    private TextMeshProUGUI pmsTex;
    private bool targetMarkerflag;
    private Camera mainCamera;
    private RectTransform markerCanvas;
    private GameObject targetMarker;

    [SerializeField] private float YawUIMag;
    [SerializeField] private Vector3 gameOverUIPos;
    [SerializeField] private GameObject pms;
    [SerializeField] private GameObject yawUI;
    [SerializeField] private GameObject yawUI2;
    [SerializeField] private GameObject rowImage;
    [SerializeField] private WeatherUIScript weatherUIScript;
    [SerializeField] private SensorUIScript sensorUIScript;
    [SerializeField] private PlayerSpeedMeterScript playerSpeedMeterScript;

    //UI�S�ʊǗ��֐�
    private void UIController()
    {
        GetPlayerRot(); //�v���C���[�̊p�x�擾
        YawUIController();  //�v���C���[��X���̊p�x�\��
        PMSMode();  //PMS�\��
        IsGameOver();   //�Q�[���I�[�o�[��UI�\��
        //PlayerSpeedUI();    //�v���C���[�̑��x�\��
        TargetMarkerUI();   //�^�[�Q�b�g�}�[�J�[�\��
        ActiveChecker();    //�Q�[����ʂɕ\������Canvas�̃t���O�Ǘ�
        sensorUIScript.SensorUIController();    //�Z���T�[��UI�\��
       playerSpeedMeterScript.SetPlayerSpeed((int)gm.GetPlayerSpeed(),(int)gm.GetPlayerSpeedBuff());    //�v���C���[�̃X�s�[�h���[�^�[�\��
    }
    //�v���C���[������ł��������
    private void ActiveChecker()
    {
        if (gm.IsPlayerDead())  //�v���C���[������ł���Ƃ��ɕ\������UI///////////////////////
        {
            targetMarker.SetActive(false);
            pms.SetActive(false);
            playerSpeedMeterScript.SetSpeedMeterActive(false);
            yawUI.SetActive(false);
            yawUI2.SetActive(false);
            rowImage.SetActive(false);
            weatherUIScript.SetWeatherUIActive(false);
            sensorUIScript.SetSensorActive(false);
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
        }///////////////////////////////////////////////////////////////////////////////////////////
    }
    //�v���C���[�̊p�x�擾
    private void GetPlayerRot()
    {
        playerRot = gm.GetPlayerRot();
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
    private void IsGameOver()
    {
        if(gm.GetGameOverFlag())    //�Q�[���I�[�o�[�ɂȂ��Ă���Ƃ�////////////////////////////////////////////////////////////////
        {
            
            gameOverUI.transform.localPosition = gameOverUIPos;

            //���[�h�ύX///////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                goUs.MoveRetry();   //���[�h�����g���C��
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
               goUs.MoveBackTitle();    //���[�h���o�b�N�^�C�g����
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (goUs.GetPos() < -1)
                {
                    gm.Retry(); //���g���C
                }
                else if(goUs.GetPos() > 1)
                {
                    gm.BackTitle(); //�^�C�g���ɖ߂�
                }
            }
            goUs.TargetHpUI();  //�^�[�Q�b�g�̎c��̗͂�\��
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    //PMS�̃I���I�t�\��
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

    //�^�[�Q�b�g�̈ʒu��UI�ɕ\��
    private void TargetMarkerUI()
    {
        //�}�[�J�[�̍��W���Z�o
        Vector2 pos;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, targetPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(markerCanvas, screenPos, mainCamera, out pos);
        targetMarker.transform.localPosition = pos;
        ///////////////////////
        

        //�^�[�Q�b�g���J�����̉�p�Ɏ��܂��Ă���Ƃ��ɕ\��������
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
