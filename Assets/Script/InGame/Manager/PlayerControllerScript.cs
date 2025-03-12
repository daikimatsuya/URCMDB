using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Usefull;

public class PlayerControllerScript : MonoBehaviour
{


    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private int missileCount;
    [SerializeField] private float respawnTimer;
    private int respawnTimerBuff;
    [SerializeField] private GameObject fadeObjectPrefab;
    [SerializeField] private GameObject explodePrefab;
    [SerializeField, Range(0f, 0.3f)] private float maxBlurIntensity;
    private float blurIntnsity;
    [SerializeField, Range(0f, 0.2f)] private float boostedBlurIntensity;
    [SerializeField, Range(0f, 0.1f)] private float accelelatedBlurIntensity;
    private float minBlurIntnsity;
    [SerializeField, Range(0f, 0.1f)] private float blurIntensityBrake;


    private PlayerScript ps;

    private GameObject player;
    private bool playerSpawnFlag;
    private bool gameOverFlag;
    private GameObject activatingFadeObject;
    private LaunchPointScript lp;
    Transform uiTransform;
    private Vector3 playerdeadPos;
    private TargetScript ts;
    private ShaderController sc;

    const float epsilon=0.000001f;

    //������
    public void StartPlayerController(in LaunchPointScript lp,in Transform uiTransform,in TargetScript ts,in ShaderController sc)
    {
        this.ts = ts;
        this.lp = lp;
        this.sc = sc;
        this.uiTransform = uiTransform;
        playerSpawnFlag = true;
        PlayerSpawn(in this.lp,in this.uiTransform);
        gameOverFlag = false;
        blurIntnsity = 0f;
        minBlurIntnsity = 0f;
    }
    //�v���C���[���Ǘ�
    public void PlayerController(in bool isPose)
    {
        BlurController();
        if (ps)
        {
            ps.PlayerController(in isPose);
            playerdeadPos=ps.GetPlayerPos();

            return;
        }
        PlayerSpawn(lp,uiTransform);
    }
    //�v���C���[�̐����m�F�Ɛ���
    public void PlayerCheck()
    {
        if (player != null)   //�v���C���[���Q�[�����ɂ����烊�^�[��///////////
        {
            return;
        }////////////////////////////////////////////////////////////////////////
        if (ts == null)
        {
            missileCount = 0;
        }
        if (ts.GetHp() <= 0)
        {
            missileCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) || TimeCountScript.TimeCounter(ref respawnTimerBuff) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            playerSpawnFlag = true;
        }

        if (!playerSpawnFlag)  //�v���C���[�����t���O�m�F//////////////
        {
            return;
        }/////////////////////////////////////////////////////////////////

    }
    //�����G�t�F�N�g����
    public GameObject CreateExplodeEffect(in Vector3 pos)
    {
        GameObject _ = GameObject.Instantiate(explodePrefab);
        _.transform.position = pos;
        return _;
    }

    //�J�n���o����
    private void CreateFadeObject(in Transform uiTransform)
    {
        GameObject __ = Instantiate(fadeObjectPrefab);           //�t�F�[�h�I�u�W�F�N�g����
        activatingFadeObject = __;                                          //�t�F�[�h�I�u�W�F�N�g��ϐ��ɑ��
        __.transform.SetParent(uiTransform);                           //UICanvas�ɐe�q�t��
        __.transform.localScale = Vector3.one;                         //�X�P�[���C��
        __.transform.localPosition = Vector3.zero;                     //���W�C��
        __.transform.localEulerAngles = new Vector3(0, 0, 0);   //�p�x�C��
    }
    //�v���C���[����
    private void CreatePlayer(in LaunchPointScript lp)
    {
        player = Instantiate(playerPrefab);                         //�v���C���[����
        ps = player.GetComponent<PlayerScript>();           //�R���|�[�l���g�擾
        ps.StartPlayer(in lp,in activatingFadeObject);           //�v���C���[������
        player.transform.SetParent(lp.GetTransform());       //�v���C���[�Ɣ��ˑ��e�q�t��
    }

    //�v���C���[���X�|�[���^�C�}�[���Z�b�g
    private void SetRespawnTimer()
    {
        TimeCountScript.SetTime(ref respawnTimerBuff, respawnTimer);
    }

    //�v���C���[���X�|�[��
    public void PlayerSpawn(in LaunchPointScript lp,in Transform uiTransform)
    {
        if (!playerSpawnFlag)
        {
            return;
        }
        if (missileCount <= 0)
        {
            gameOverFlag = true;
            return;
        }

        CreateFadeObject(in uiTransform);                         //�t�F�[�h����
        CreatePlayer(in lp);                                               //�v���C���[����
        missileCount--;                                                     //�c�@����
        SetRespawnTimer();                                             //�v���C���[�����p�^�C�}�[�Z�b�g

        playerSpawnFlag = false;
    }

    //�u���[�Ǘ�
    public void BlurController()
    {
        if (ps==null||!ps.GetIsFire())
        {
            blurIntnsity = 0;
            sc.SetBlurIntensity(blurIntnsity);
            return;
        }

        float buff = ps.GetPlayerBoost() / ps.GetMaxBoost();             //�u�[�X�g�̃u���[�p�̒l�Z�o
        float buff2 = 0;
        if (ps.GetMaxRingBoost() > epsilon)
        {
            buff2 = ps.GetRingBoost() / ps.GetMaxRingBoost();         //�����ւ̃u���[�p�̒l�Z�o
        }
        if (buff2 > 1 - buff)
        {
            buff2 = 1 - buff;
        }
        

        buff += (buff2*buff2);                                                      //�������č���
        if (buff > 1)
        {
            buff = 1;
        }

        blurIntnsity = (buff * buff) * maxBlurIntensity;                  //�u���[�̒l���Z�o

        

        if (Input.GetKey(KeyCode.Space) || (!GetTriggerScript.GetAxisDown("Right") && Input.GetAxis("RightTrigger") != 0)) 
        {
            minBlurIntnsity = accelelatedBlurIntensity;
        }
        else
        {
            if (minBlurIntnsity > 0)
            {
                minBlurIntnsity -= blurIntensityBrake;
                if(minBlurIntnsity < 0)
                {
                    minBlurIntnsity = 0;
                }
            }
        }

        if (blurIntnsity <= minBlurIntnsity)
        {
            blurIntnsity = minBlurIntnsity;
        }
        sc.SetBlurIntensity(blurIntnsity);          //�u���[�̒l����
    }

    #region �l�󂯓n��
    public PlayerScript GetPlayer()
    {
        return ps;
    }
    public bool GetGameOverFlag()
    {
        return gameOverFlag;
    }
    public Vector3 GetPlayerdeadPos()
    {
        return playerdeadPos;
    }
    public void SetClear()
    {
        missileCount = 0;
    }
    #endregion
}
