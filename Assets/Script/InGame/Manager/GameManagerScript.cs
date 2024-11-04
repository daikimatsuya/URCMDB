using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//���̂Ƃ���C���Q�[���֘A�̃f�[�^�̂����◠���d���S�ʂ̂��̂����ɂ���
public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject fadeObjectPrefab;
    [SerializeField] private int playerMissile;
    [SerializeField] private bool PMS;
    [SerializeField] private string stage;
    [SerializeField] private string title;

    private PlayerScript ps;
    private Transform targetPos;
    private CameraManager cm;
    private GameObject player;

    private bool playerDead;
    private Vector3 playerRot;
    private float playerSpeed;
    private float playerSpeedBuff;
    private bool gameOverFlag;
    private bool ClearFlag;
    private bool isCanShot;
    private bool playerSpawnFlag;

    //�Q�[���V�X�e���𓮂���
    private void GameManagerController()
    {
        ChangePMS();
        PlayerCheck();
        cm.CameraController();
        if(ClearFlag)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                BackTitle();
            }
           
        }
    }
    //���g���C����Ƃ��ɃV�[�������[�h
    public void Retry()
    {
        SceneManager.LoadScene(stage);
    }
    //�^�C�g���ɖ߂�Ƃ��ɃV�[�������[�h
    public void BackTitle()
    {
        SceneManager.LoadScene(title);
    }
    //�v���C���[�̐����m�F�Ɛ���
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
    //�v���C���[����
    private void PlayerSpawn()
    {
        if (cm == null)
        {
            InitialSet();
        }
        player = Instantiate(playerPrefab);
        ps = player.GetComponent<PlayerScript>();
        cm.SetPlayer(player);
        playerMissile--;

    }
    //�J�n���o����
    private void CreateFadeObject()
    {
        Transform uiTransform = GameObject.FindWithTag("UICanvas").transform;
        GameObject __ = Instantiate(fadeObjectPrefab);
        __.transform.SetParent(uiTransform);
        __.transform.localScale = Vector3.one;
        __.transform.localPosition = Vector3.zero;
        __.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
    //PMS�̃I���I�t
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
    //������������ĂȂ��Ƃ��ɑ��̃X�N���v�g����Ăяo���ꂽ�Ƃ��ɏ���������
    private void InitialSet()
    {
        Application.targetFrameRate = 60;
        PMS = false;
        gameOverFlag = false;
        ClearFlag = false;
        targetPos = GameObject.FindWithTag("Target").GetComponent<Transform>();
        cm = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();

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

    public void SetClearFlag()
    {
        ClearFlag = true;
    }
    public bool GetClearFlag()
    {
        return ClearFlag;
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
    // Start is called before the first frame update
    void Start()
    {
        InitialSet();
    }

    // Update is called once per frame
    void Update()
    {
        GameManagerController();
    }
}
