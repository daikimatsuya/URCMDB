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

    //�Q�[���V�X�e���𓮂���
    private void GameManagerController()
    {
        Usefull.GetTriggerScript.AxisUpdate();//�g���K�[�̓��͏����X�V
        Usefull.GetStickScript.AxisUpdate();//�X�e�B�b�N���͏����X�V
        Usefull.GetControllerScript.SearchController();//�R���g���[���[�ڑ��m�F
        ChangePMS();    //PMS�Ǘ�
        PlayerCheck();  //�v���C���[���Q�[���ɂ��邩���m�F
        cm.CameraController();  //�J�����Ǘ�
        BreakTimeContoller();   //�N���A��̃^�C�}�[�Ǘ�
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
    //�v���C���[�̐����m�F�Ɛ���
    private void PlayerCheck()
    {

        if (player==null)   //�擾�����v���C���[���Q�[�����ɂȂ����Ƃ��m�F/////////////////////////////////////////////////////////////
        {
            //�t���O�Ǘ�
            playerDead = true;
            isCanShot = false;
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
                    PlayerSpawn();  //�v���C���[����
                    CreateFadeObject(); //�v���C���[�������̉��o����
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
            playerSpeed = ps.GetPlayerSpeedFloat(); //�v���C���[�̑��x�擾
            playerSpeedBuff = ps.GetPlayerSpeedBuffFloat(); //�v���C���[�̈ړ����x�擾
        }
    }
    //�v���C���[�����񂾌�Ɉ�莞�Ԍ�Ƀ��X�|�[��������t���O�Z�b�g
    private void SetPlayerSpawnFlag()
    {
        playerSpawnFlag = true;
        if (isClear)    //�N���A���Ă���/////
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
            InitialSet();
        }///////////////////////////////////////////////////////////////

        player = Instantiate(playerPrefab); //�v���C���[����
        ps = player.GetComponent<PlayerScript>();   //�R���|�[�l���g�擾
        ps.SetLaunchpad(lp);    //���ˑ�ʒu�����
        cm.SetPlayer(ps);   //�J�����Ƀv���C���[��o�^
        playerMissile--;    //�c�@����


        player.transform.SetParent(launchPad.transform);    //�v���C���[�Ɣ��ˑ��e�q�t��

        SetRespawnTimer();  //�v���C���[�����p�^�C�}�[�Z�b�g
    }
    //�N���A���̃^�[�Q�b�g�j��t���O�Ǘ�
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
    //�J�n���o����
    private void CreateFadeObject()
    {
        Transform uiTransform = GameObject.FindWithTag("UICanvas").transform;   //UICanvas�̃g�����X�t�H�[�����擾
        GameObject __ = Instantiate(fadeObjectPrefab);  //�t�F�[�h�I�u�W�F�N�g����
        __.transform.SetParent(uiTransform);    //UICanvas�ɐe�q�t��
        __.transform.localScale = Vector3.one;  //�X�P�[���C��
        __.transform.localPosition = Vector3.zero;  //���W�C��
        __.transform.localEulerAngles = new Vector3(0, 0, 0);   //�p�x�C��
    }
    //PMS�̃I���I�t
    private void ChangePMS()
    {
        if (Input.GetKeyDown(KeyCode.P)||Input.GetKeyDown("joystick button 3"))
        {
            if (PMS)    //PMS�Ǘ�//////////////////
            {
                PMS = false;
            }
            else
            {
                PMS = true;
            }//////////////////////////////////////
        }
    }

    //������������ĂȂ��Ƃ��ɑ��̃X�N���v�g����Ăяo���ꂽ�Ƃ��ɏ���������
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
    #region �l�󂯓n��
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
