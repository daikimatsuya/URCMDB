using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Usefull;

//���̂Ƃ���C���Q�[���֘A�̃f�[�^�̂����◠���d���S�ʂ̂��̂����ɂ���
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
    private GameObject afs;
    private Transform targetPos;
    private CameraManager cm;
    private GameObject player;
    private SelectWeatherScript sws;
    private LaunchPointScript lp;
    private GameObject launchPad;
    private UIScript us;
    private TargetScript ts;

    private bool playerDead;
    private bool gameOverFlag;
    private bool playerSpawnFlag;
    private bool isHitTarget;
    private bool isTargetBreak;
    private bool isTutorial;
    private Transform uiTransform;

    //�Q�[���V�X�e���𓮂���
    private void GameManagerController()
    {
        Usefull.GetTriggerScript.AxisUpdate();//�g���K�[�̓��͏����X�V
        Usefull.GetStickScript.AxisUpdate();//�X�e�B�b�N���͏����X�V
        Usefull.GetControllerScript.SearchController();//�R���g���[���[�ڑ��m�F�X�V
        PlayerCheck();  //�v���C���[���Q�[���ɂ��邩���m�F
        cm.CameraController();  //�J�����Ǘ�
        SceneChanges(); //�V�[���ύX
        us.SetIsGameOver(in gameOverFlag);
        us.UIController();  //UI�Ǘ�

        if (Input.GetKeyDown(KeyCode.Alpha9))   //�����Ƃ����|�[�Y���j���[���܂ł̂Ȃ�
        {
            BackTitle();
        }
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
    //�v���C���[�̐����m�F�Ɛ���
    private void PlayerCheck()
    {

        if (player==null)   //�擾�����v���C���[���Q�[�����ɂȂ����Ƃ��m�F/////////////////////////////////////////////////////////////
        {
            //�t���O�Ǘ�
            playerDead = true;
            /////////////
            ///
            if (Input.GetKeyDown(KeyCode.Space)|| TimeCountScript.TimeCounter(ref respawnTimerBuff)||Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
            {
                SetPlayerSpawnFlag();   //�v���C���[�𐶐�����t���O�Ǘ�
            }
            
            if(playerSpawnFlag)  //�v���C���[�����t���O//////////////////////
            {
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
            }/////////////////////////////////////////////////////////////////

        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        else
        {
            playerDead= false;
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
        if (cm == null)     //CameraManager������������擾����//
        {
            AwakeGameManger();
        }///////////////////////////////////////////////////////////////

        player = Instantiate(playerPrefab); //�v���C���[����
        ps = player.GetComponent<PlayerScript>();   //�R���|�[�l���g�擾

        ps.SetFadeObject(in afs);
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
        afs = __;
        __.transform.SetParent(uiTransform);    //UICanvas�ɐe�q�t��
        __.transform.localScale = Vector3.one;  //�X�P�[���C��
        __.transform.localPosition = Vector3.zero;  //���W�C��
        __.transform.localEulerAngles = new Vector3(0, 0, 0);   //�p�x�C��
    }


    //������������ĂȂ��Ƃ��ɑ��̃X�N���v�g����Ăяo���ꂽ�Ƃ��ɏ���������
    private void AwakeGameManger()
    {
        Application.targetFrameRate = 60;
        Usefull.PMSScript.SetPMS(false);
        us = GameObject.FindWithTag("UICanvas").GetComponent<UIScript>();
        uiTransform = GameObject.FindWithTag("UICanvas").transform;   //UICanvas�̃g�����X�t�H�[�����擾

    }
    private void StartGameManager()
    {
        gameOverFlag = false;

        sws = GetComponent<SelectWeatherScript>();
        GameObject target = GameObject.FindWithTag("Target");
        targetPos = target.GetComponent<Transform>();
        ts = target.GetComponent<TargetScript>();
        cm = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
        cm.SetTarget(ts);
        launchPad = GameObject.FindWithTag("LaunchPoint");
        lp = launchPad.GetComponent<LaunchPointScript>();

        us.SetTarget(in targetPos);
        sws.WeatherSetting(cm);
        us.SetWeatherScript(sws);
        TimeCountScript.SetTime(ref breakTimeBuff, breakTime);

        CreateFadeObject();
        PlayerSpawn();

    }
    #region �l�󂯓n��

    public bool GetPMS()
    {
        return PMS;
    }
    public bool IsPlayerDead()
    {
        return playerDead;
    }
    #endregion
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
