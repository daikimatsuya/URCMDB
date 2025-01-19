using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�`���[�g���A�����Ǘ�
public class TutorialScript : MonoBehaviour
{
    private PlayerScript ps;

    private float tutorialCompletion;
    private float maxCompletion;
    private float playerAcce;
    private Vector3 playerRot;
    private bool isControlled;
    private bool isBoost;
    private bool isShot;
    private bool isAcce;
    private bool isPMS;
    private bool isQuick;

    //�`���[�g���A���Ǘ��p
    public void TutorialContoroller()
    {
        if(ps != null)
        {
            CheckPlayerRot();   //�v���C���[�����삳��Ă���̂����m�F����
            CheckPlayerBoost();  //�v���C���[���u�[�X�g���Ă���̂����m�F����
            CheckPlayerShot();  //�v���C���[���ˏo����Ă��邩���m�F����
            CheckPlayerAcce();  //�v���C���[���������Ă���̂����m�F����
            CheckPMS(); //�v���C���[��PMS���m�F����
        }
        else
        {
            SetPlayer();    //�v���C���[���擾����
            ResetFlags();   //�t���O�ނ����Z�b�g����
            if (ps != null)
            {
                playerRot=ps.GetPlayerRot();    //�l����
                playerAcce = ps.GetPlayerSpeedBuffFloat();  //�l����
            }
        }
    }

    //�v���C���[�̊p�x���r
    private void CheckPlayerRot()
    {
        Vector3 rotBuff=ps.GetPlayerRot();
        if (rotBuff != playerRot)
        {
            isControlled = true;
        }
        else
        {
            isControlled = false;
        }
        playerRot = rotBuff;
    }

    //�v���C���[�̃u�[�X�g���m�F
    private void CheckPlayerBoost()
    {
        float acceBuff = ps.GetPlayerSpeedBuffFloat();
        if(acceBuff!=0)
        {
            isBoost = true;
        }
        else
        {
            isBoost = false;
        }
    }

    //�v���C���[���ˏo����Ă���̂����m�F
    private void CheckPlayerShot()
    {
        if (ps.GetIsFire())
        {
            isShot = true;
        }
        else
        {
            isControlled = false;
        }
    }

    //�v���C���[�̉������m�F
    private void CheckPlayerAcce()
    {
        if(Input.GetAxis("RightTrigger")!=0)
        {
            isAcce = true;
        }
        else
        {
            isAcce = false;
        }
    }

    //�v���C���[��PMS���m�F
    private void CheckPMS()
    {
        if (ps.GetPMS())
        {
            isPMS = true;
        }
        else
        {
            isPMS = false;
        }
    }

    //�v���C���[�N�C�b�N���[�u���m�F
    private void CheckQuickMove()
    {
        if (Input.GetAxis("LeftTrigger") != 0)
        {
            isQuick=true;
        }
        else
        {
            isQuick = false;
        }
    }

    //�t���O���Z�b�g
    private void ResetFlags()
    {
        isBoost=false;
        isControlled=false;
        isShot=false;
    }
    #region �l�󂯓n��

    public bool GetIsControll()
    {
        return isControlled;
    }
    public bool GetIsBoost()
    {
        return isBoost;
    }
    public bool GetIsShot()
    {
        return isShot;
    }
    public bool GetIsAcce()
    {
        return isAcce;
    }
    public bool GetIsPMS()
    {
        return isPMS;
    }
    #endregion

    //�v���C���[�擾�p
    private void SetPlayer()
    {
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();  
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
