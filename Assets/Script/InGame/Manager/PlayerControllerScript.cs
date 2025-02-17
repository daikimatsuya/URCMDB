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

    private PlayerScript ps;

    private GameObject player;
    private bool playerSpawnFlag;
    private bool gameOverFlag;
    private GameObject activatingFadeObject;
    public void StartPlayerController(in LaunchPointScript lp,in Transform uiTransform)
    {
        playerSpawnFlag = true;
        PlayerSpawn(in lp,in uiTransform);
        gameOverFlag = false;
    }
    public void PlayerController(in bool isPose)
    {
        if (ps)
        {
            ps.PlayerController(in isPose);
            return;
        }

    }
    //�v���C���[�̐����m�F�Ɛ���
    public void PlayerCheck()
    {
        if (player != null)   //�v���C���[���Q�[�����ɂ����烊�^�[��///////////
        {
            return;
        }////////////////////////////////////////////////////////////////////////

        if (Input.GetKeyDown(KeyCode.Space) || TimeCountScript.TimeCounter(ref respawnTimerBuff) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            playerSpawnFlag = true;
        }

        if (!playerSpawnFlag)  //�v���C���[�����t���O�m�F//////////////
        {
            return;
        }/////////////////////////////////////////////////////////////////

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
        ps.SetFadeObject(in activatingFadeObject);             //�t�F�[�h�ݒ�
        ps.SetLaunchpad(lp);                                            //���ˑ�ʒu�����
        ps.StartPlayer();                                                   //�v���C���[������
        player.transform.SetParent(lp.GetTransform());      //�v���C���[�Ɣ��ˑ��e�q�t��

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

        CreatePlayer(in lp);
        CreateFadeObject(in uiTransform);
        missileCount--;                                                     //�c�@����
        SetRespawnTimer();                                             //�v���C���[�����p�^�C�}�[�Z�b�g

        playerSpawnFlag = false;
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
    #endregion
}
