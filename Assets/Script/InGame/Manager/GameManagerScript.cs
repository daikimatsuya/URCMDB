using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Usefull;

//�C���Q�[������
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


    //�Q�[���V�X�e���𓮂���
    private void GameManagerController()
    {
        Usefull.GetTriggerScript.AxisUpdate();//�g���K�[�̓��͏����X�V
        Usefull.GetStickScript.AxisUpdate();//�X�e�B�b�N���͏����X�V
        Usefull.GetControllerScript.SearchController();//�R���g���[���[�ڑ��m�F�X�V

        SceneChanges(); //�V�[���ύX
        us.SetIsGameOver(in gameOverFlag);
        InGameController(isPose); //�Q�[���𓮂���
        PoseChange();   //�|�[�Y�ݒ�؂�ւ�

        
    }
    //������������ĂȂ��Ƃ��ɑ��̃X�N���v�g����Ăяo���ꂽ�Ƃ��ɏ���������
    private void AwakeGameManger()
    {
        Application.targetFrameRate = 60;
        GetComponents();    //�R���|�[�l���g�Q�擾
        Usefull.PMSScript.SetPMS(false);
        lm.AwakeListManager();
        lp.AwakeLaunchPoint();
        us.AwakeUIScript();
    }
    private void StartGameManager()
    {
        gameOverFlag = false;
        cm.SetTarget(ts);
        us.StartUIScript();
        us.SetTarget(in targetPos);
        sws.WeatherSetting(cm);
        us.SetWeatherScript(sws);
        TimeCountScript.SetTime(ref breakTimeBuff, breakTime);
        CreateFadeObject();
        PlayerSpawn();
        ts.StartTarget();
        isPose = false;
    }

    //�Q�[���𓮂���
    private void InGameController(in bool isPose)
    {
        PlayerCheck();  //�v���C���[���Q�[���ɂ��邩���m�F
        us.UIController(isPose);  //UI�Ǘ�
        lm.ListManagerController(ps,isPose);   //���X�g�Q�Ǘ�

        if (ts != null)
        {
            ts.TargetController(in isPose);  //�^�[�Q�b�g�Ǘ�
        }
        if (ps != null)
        {
            ps.PlayerController(in isPose);  //�v���C���[�Ǘ�
        }

        lp.LaunchPointController(in isPose); //���ˑ�Ǘ�       
        cm.CameraController(in isPose);  //�J�����Ǘ�
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
    //�v���C���[�̐����m�F�Ɛ���
    private void PlayerCheck()
    {

        if (player!=null)   //�擾�����v���C���[���Q�[�����ɂȂ����Ƃ��m�F////
        {
            return;
        }////////////////////////////////////////////////////////////////////////

        if (Input.GetKeyDown(KeyCode.Space)|| TimeCountScript.TimeCounter(ref respawnTimerBuff)||Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            SetPlayerSpawnFlag();   //�v���C���[�𐶐�����t���O�Ǘ�
        }
        
        if(!playerSpawnFlag)  //�v���C���[�����t���O�m�F//////////////
        {
            return;
        }/////////////////////////////////////////////////////////////////
        if (playerMissile > 0)
        {
            CreateFadeObject(); //�v���C���[�������̉��o����
            PlayerSpawn();  //�v���C���[����
            playerSpawnFlag = false;
        }
        else
        {
            gameOverFlag = true;
        }

    }
    //�v���C���[�����񂾌�Ɉ�莞�Ԍ�Ƀ��X�|�[��������t���O�Z�b�g
    private void SetPlayerSpawnFlag()
    {
        playerSpawnFlag = true;
        if (!ts)
        {
            playerMissile = 0;
            return;
        }
        if (ts.GetBreak())    //�N���A���Ă���/////
        {
            playerMissile = 0;
        }///////////////////////////////////
    }
    //�v���C���[���X�|�[���^�C�}�[���Z�b�g
    private void SetRespawnTimer()
    {
        TimeCountScript.SetTime(ref respawnTimerBuff, respawnTimer);
    }

    //�v���C���[����
    private void PlayerSpawn()
    {
        player = Instantiate(playerPrefab); //�v���C���[����
        ps = player.GetComponent<PlayerScript>();   //�R���|�[�l���g�擾

        ps.SetFadeObject(in activatingFadeObject);
        ps.SetLaunchpad(lp);    //���ˑ�ʒu�����
        ps.StartPlayer();
        cm.SetPlayer(ps);   //�J�����Ƀv���C���[��o�^
        us.SetPlayer(ps);   //UI�Ƀv���C���[��o�^
        playerMissile--;    //�c�@����

        player.transform.SetParent(launchPad.transform);    //�v���C���[�Ɣ��ˑ��e�q�t��

        SetRespawnTimer();  //�v���C���[�����p�^�C�}�[�Z�b�g
    }
    //�J�n���o����
    private void CreateFadeObject()
    {    
        GameObject __ = Instantiate(fadeObjectPrefab);  //�t�F�[�h�I�u�W�F�N�g����
        activatingFadeObject = __;
        __.transform.SetParent(uiTransform);    //UICanvas�ɐe�q�t��
        __.transform.localScale = Vector3.one;  //�X�P�[���C��
        __.transform.localPosition = Vector3.zero;  //���W�C��
        __.transform.localEulerAngles = new Vector3(0, 0, 0);   //�p�x�C��
    }


    private void GetComponents()
    {
        us = GameObject.FindWithTag("UICanvas").GetComponent<UIScript>();
        uiTransform = GameObject.FindWithTag("UICanvas").transform;   //UICanvas�̃g�����X�t�H�[�����擾
        GameObject target = GameObject.FindWithTag("Target");
        targetPos = target.GetComponent<Transform>();
        ts = target.GetComponent<TargetScript>();
        cm = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
        launchPad = GameObject.FindWithTag("LaunchPoint");
        lp = launchPad.GetComponent<LaunchPointScript>();
        sws = GetComponent<SelectWeatherScript>();
        lm = new ListManager();
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
