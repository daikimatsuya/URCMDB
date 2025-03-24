using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�`���[�g���A�����Ǘ�
public class TutorialScript : MonoBehaviour
{
    private int tutorialNumber = 0;
    private float tutorialCompletion = 0;
    private float maxCompletion = 100.0f;
    private bool isReset;
    private bool nextSwitch=true;

    [SerializeField] private float shotTutorialCount;
    [SerializeField] private float boostTutorialCount;
    [SerializeField] private float controlleTutorialCount;
    [SerializeField] private float PMSTutorialCount;
    [SerializeField] private float acceTutorialCount;
    [SerializeField] private float quickMoveTutorialCount;

    //�ˏo���̃`���[�g���A���Ǘ�
    public void ShotTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        //�v���C���[�����˂���Ă�����`���[�g���A�����i�s
        if (CheckPlayerShot(in ps)) 
        {
            tutorialCompletion += shotTutorialCount;
            if(tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }
    //�u�[�X�g���̃`���[�g���A���Ǘ�
    public void BoostTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        //�v���C���[���u�[�X�g���Ă�����`���[�g���A�����i�s///
        if (CheckPlayerBoost(in ps))    
        {
            tutorialCompletion += boostTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }
    //���쎞�̃`���[�g���A���Ǘ�
    public void ControlleTutorial(in PlayerScript ps, in bool isConectController)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        //�v���C���[�����삳��Ă�����`���[�g���A�����i�s///
        if (CheckPlayerControlle(in ps, in isConectController)) 
        {
            tutorialCompletion += controlleTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }    
    //PMS�̃`���[�g���A���Ǘ�
    public void PMSTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        //PMS���I����������`���[�g���A�����i�s
        if (CheckPMS())    
        {
            tutorialCompletion += PMSTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }   
    //�������̃`���[�g���A���Ǘ�
    public void AcceTutorial(in PlayerScript ps, in bool isConectController)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        //�v���C���[���������Ă�����`���[�g���A�����i�s
        if (CheckPlayerAcce(in ps, in isConectController))  
        {
            tutorialCompletion += acceTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }
    //�������񎞂̃`���[�g���A���Ǘ�
    public void QuickMoveTutorial(in PlayerScript ps,in bool isConectController)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        //�v���C���[���������񂵂Ă�����`���[�g���A�����i�s
        if (CheckQuickMove(in ps, in isConectController)&&CheckPlayerControlle(in ps,in isConectController))    
        {
            tutorialCompletion += quickMoveTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }


    //�`���[�g���A�������������烊�Z�b�g��������
    private void ResetTutorial()
    {
        //�i�s���Ă����`���[�g���A�����I�������烊�Z�b�g�������Ď��̃`���[�g���A���ɂ���
        if (tutorialCompletion >= maxCompletion)    
        {
            tutorialCompletion = 0;
            tutorialNumber++;
            nextSwitch = true;
        }
    }
    //�v���C���[�����񂾂�S�����Z�b�g������
    public void ResetAll(in PlayerScript ps)
    {
        //�v���C���[�����������烊�Z�b�g��������
        if (ps == null) 
        {
            isReset = true;
            tutorialNumber = 0;
            tutorialCompletion = 0;
            return;
        }

        isReset = false;
    }

    #region �`���[�g���A���`�F�b�J�[
    //�v���C���[�̊p�x���r
    public bool CheckPlayerControlle(in PlayerScript ps,in bool isConectController)
    {
        //�L�[�{�[�h���͂��Ȃ�������false��Ԃ�
        if (!isConectController)
        {
            if(!Input.GetKey(KeyCode.LeftArrow)&& !Input.GetKey(KeyCode.RightArrow)&& !Input.GetKey(KeyCode.UpArrow)&& !Input.GetKey(KeyCode.DownArrow)&& !Input.GetKey(KeyCode.A)&& !Input.GetKey(KeyCode.D)&& !Input.GetKey(KeyCode.W)&& !Input.GetKey(KeyCode.S))
            {
                return false;
            }
            return true;
        }

        //L�X�e�B�b�N���͂�����������false��Ԃ�
        if (Input.GetAxis("LeftStickX") == 0 && Input.GetAxis("LeftStickY") == 0)   
        {
            return false;
        }

        return true;
    }

    //�v���C���[�̃u�[�X�g���m�F
    public bool CheckPlayerBoost(in PlayerScript ps)
    {
        float acceBuff = ps.GetAccelerate();

        //�v���C���[�����g�Ńu�[�X�g���Ă�����true��Ԃ�
        if (acceBuff>=1) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //�v���C���[���ˏo����Ă���̂����m�F
    public bool CheckPlayerShot(in PlayerScript ps)
    {
        return ps.GetIsFire();  //���˃t���O�����̂܂ܕԂ�
    }

    //�v���C���[�̉������m�F
    public bool CheckPlayerAcce(in PlayerScript ps,in bool isConectController)
    {
        //�X�y�[�X��������Ă��Ȃ�������false��Ԃ�
        if (!isConectController)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                return false;
            }
            return true;
        }

        //R�g���K�[��������Ă��Ȃ�������false��Ԃ�
        if (Input.GetAxis("RightTrigger")==0)   
        {
            return false;
        }
        
        return true;
        
    }

    //�v���C���[��PMS���m�F
    public bool CheckPMS()
    {
        return Usefull.PMSScript.GetPMS();

    }

    //�v���C���[�N�C�b�N���[�u���m�F
    public bool CheckQuickMove(in PlayerScript ps,in bool isConectController)
    {
        //�V�t�g��������Ă��Ȃ�������false��Ԃ�
        if (!isConectController) 
        {
            if (!Input.GetKey(KeyCode.LeftShift)&&!Input.GetKey(KeyCode.RightShift))
            {
                return false;
            }
            return true;
        }

        //���t�g�g���K�[��������Ă��Ȃ�������false��Ԃ�
        if (Input.GetAxis("LeftTrigger") == 0)
        {
            return false;
        }
        
        return true;
        
    }
    #endregion


    #region �l�󂯓n��

    public int GetTutorialNum()
    {
        return tutorialNumber;
    }
    public int GetCompletion()
    {
        return (int)tutorialCompletion;
    }
    public bool GetResetFlag()
    {
        return isReset;
    }
    public bool GetNextSwitch()
    {
        return nextSwitch;
    }
    public void SetNextSwitch(bool flag)
    {
        nextSwitch = flag;
    }
    #endregion


}
