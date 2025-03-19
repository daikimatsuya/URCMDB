using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Usefull;

//�C���Q�[������
public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private string stage;
    [SerializeField] private string title;
    [SerializeField] private float breakTime;
    private int breakTimeBuff;

    private Transform targetPos;
    private CameraManager cm;
    private SelectWeatherScript sws;
    private LaunchPointScript lp;
    private GameObject launchPad;
    private UIScript us;
    private TargetScript ts;
    private ListManager lm;
    private Transform uiTransform;
    private PlayerControllerScript pcs;
    private RadialBlurController rbc;

    private bool isPose;


    //�Q�[���V�X�e���𓮂���
    private void GameManagerController()
    {
        Usefull.GetTriggerScript.AxisUpdate();                            //�g���K�[�̓��͏����X�V
        Usefull.GetStickScript.AxisUpdate();                               //�X�e�B�b�N���͏����X�V
        Usefull.GetControllerScript.SearchController();                //�R���g���[���[�ڑ��m�F�X�V

        SceneChanges();                                                          //�V�[���ύX
        us.SetIsGameOver(pcs.GetGameOverFlag());                 //�Q�[���I�[�o�[�t���O�}��

        InGameController(isPose);                                            //�Q�[���𓮂���
        PoseChange();                                                             //�|�[�Y�ݒ�؂�ւ�

        rbc.BlurController();                                                       //���W�J���u���[
       
    }
    //������������ĂȂ��Ƃ��ɑ��̃X�N���v�g����Ăяo���ꂽ�Ƃ��ɏ���������
    private void AwakeGameManger()
    {
        Application.targetFrameRate = 60;
        GetComponents();                                //�R���|�[�l���g�Q�擾
        Usefull.PMSScript.SetPMS(false);
        lm.AwakeListManager(in pcs);
        lp.AwakeLaunchPoint();
        us.AwakeUIScript();
        cm.AwakeCameraManager(in pcs);
    }
    private void StartGameManager()
    {
        rbc.StartShaderController();
        pcs.StartPlayerController(in lp, in uiTransform,in ts,in rbc);
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

    //�Q�[���𓮂���
    private void InGameController(in bool isPose)
    {
        pcs.PlayerCheck();                                                 //�v���C���[���Q�[���ɂ��邩���m�F
        us.UIController(isPose);                                         //UI�Ǘ�
        lm.ListManagerController(isPose);                           //���X�g�Q�Ǘ�

        if (ts != null)
        {
            ts.TargetController(in isPose);                            //�^�[�Q�b�g�Ǘ�
        }
        else
        {
            pcs.SetClear();
        }
        pcs.PlayerController(in isPose);                              //�v���C���[�Ǘ�
        lp.LaunchPointController(in isPose);                       //���ˑ�Ǘ�       
        cm.CameraController(in isPose);                           //�J�����Ǘ�
    }

    //���g���C����Ƃ��ɃV�[�������[�h
    public void Retry()
    {
        Usefull.GetTriggerScript.SetValue();
        SceneManager.LoadScene(stage);
    }
    //�^�C�g���ɖ߂�Ƃ��ɃV�[�������[�h
    public void BackTitle()
    {
        Usefull.GetTriggerScript.SetValue();
        SceneManager.LoadScene(title);
    }
    //�V�[���ύX�p
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
    //�|�[�Y�t���O�؂�ւ�
    private void PoseChange()
    {
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown("joystick button 7"))   //�|�[�Y�t���O�؂�ւ�
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


    //�R���|�[�l���g�Q���擾
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
        rbc = GameObject.FindWithTag("GameCamera").GetComponent<RadialBlurController>();
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
