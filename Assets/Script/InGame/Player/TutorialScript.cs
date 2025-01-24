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

        if (CheckPlayerShot(in ps)) //�v���C���[�����˂���Ă�����`���[�g���A�����i�s////
        {
            tutorialCompletion += shotTutorialCount;
            if(tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }//////////////////////////////////////////////////////////////////////////////////////
    }
    //�u�[�X�g���̃`���[�g���A���Ǘ�
    public void BoostTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        if (CheckPlayerBoost(in ps))    //�v���C���[���u�[�X�g���Ă�����`���[�g���A�����i�s///
        {
            tutorialCompletion += boostTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }////////////////////////////////////////////////////////////////////////////////////////////
    }
    //���쎞�̃`���[�g���A���Ǘ�
    public void ControlleTutorial(in PlayerScript ps, in bool isConectController)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        if (CheckPlayerControlle(in ps, in isConectController)) //�v���C���[�����삳��Ă�����`���[�g���A�����i�s///
        {
            tutorialCompletion += controlleTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }    
    //PMS�̃`���[�g���A���Ǘ�
    public void PMSTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        if (CheckPMS(in ps))    //PMS���I����������`���[�g���A�����i�s///
        {
            tutorialCompletion += PMSTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }//////////////////////////////////////////////////////////////////////
    }   
    //�������̃`���[�g���A���Ǘ�
    public void AcceTutorial(in PlayerScript ps, in bool isConectController)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        if (CheckPlayerAcce(in ps, in isConectController))  //�v���C���[���������Ă�����`���[�g���A�����i�s///////////
        {
            tutorialCompletion += acceTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    //�������񎞂̃`���[�g���A���Ǘ�
    public void QuickMoveTutorial(in PlayerScript ps,in bool isConectController)
    {
        ResetAll(in ps);    //�v���C���[�����񂾂烊�Z�b�g
        ResetTutorial();    //���̃`���[�g���A�����I������玟�ɐi��

        if (CheckQuickMove(in ps, in isConectController)&&CheckPlayerControlle(in ps,in isConectController))    //�v���C���[���������񂵂Ă�����`���[�g���A�����i�s////
        {
            tutorialCompletion += quickMoveTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


    //�`���[�g���A�������������烊�Z�b�g��������
    private void ResetTutorial()
    {
        if (tutorialCompletion >= maxCompletion)    //�i�s���Ă����`���[�g���A�����I�������烊�Z�b�g�������Ď��̃`���[�g���A���ɂ���//
        {
            tutorialCompletion = 0;
            tutorialNumber++;
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    //�v���C���[�����񂾂�S�����Z�b�g������
    public void ResetAll(in PlayerScript ps)
    {
        if (ps == null) //�v���C���[�����������烊�Z�b�g��������//
        {
            isReset = true;
            tutorialNumber = 0;
            tutorialCompletion = 0;
            return;
        }/////////////////////////////////////////////////////////////

        isReset = false;
    }

    #region �`���[�g���A���`�F�b�J�[
    //�v���C���[�̊p�x���r
    public bool CheckPlayerControlle(in PlayerScript ps,in bool isConectController)
    {
        if (!isConectController) //�R���g���[���[���ڑ�����Ă��Ȃ�������false��Ԃ�///////
        {
            return false;
        }/////////////////////////////////////////////////////////////////////////////////////

        if (Input.GetAxis("LeftStickX") == 0 && Input.GetAxis("LeftStickY") == 0)   //L�X�e�B�b�N���͂�����������false��Ԃ�///
        {
            return false;
        }////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        return true;
    }

    //�v���C���[�̃u�[�X�g���m�F
    public bool CheckPlayerBoost(in PlayerScript ps)
    {
        float acceBuff = ps.GetPlayerAcce();

        if(acceBuff>=1) //�v���C���[�����g�Ńu�[�X�g���Ă�����true��Ԃ�////////
        {
            return true;
        }
        else
        {
            return false;
        }/////////////////////////////////////////////////////////////////////////////
    }

    //�v���C���[���ˏo����Ă���̂����m�F
    public bool CheckPlayerShot(in PlayerScript ps)
    {
        return ps.GetIsFire();  //���˃t���O�����̂܂ܕԂ�
    }

    //�v���C���[�̉������m�F
    public bool CheckPlayerAcce(in PlayerScript ps,in bool isConectController)
    {
        if (!isConectController) //�R���g���[���[���ڑ�����Ă��Ȃ�������false��Ԃ�///////
        {
            return false;
        }/////////////////////////////////////////////////////////////////////////////////////

        if (Input.GetAxis("RightTrigger")==0)   //R�g���K�[��������Ă��Ȃ�������false��Ԃ�//
        {
            return false;
        }////////////////////////////////////////////////////////////////////////////////////////////
        
        return true;
        
    }

    //�v���C���[��PMS���m�F
    public bool CheckPMS(in PlayerScript ps)
    {
        return ps.GetPMS();   //PMS�̃t���O�����̂܂ܕԂ�

    }

    //�v���C���[�N�C�b�N���[�u���m�F
    public bool CheckQuickMove(in PlayerScript ps,in bool isConectController)
    {
        if(!isConectController) //�R���g���[���[���ڑ�����Ă��Ȃ�������false��Ԃ�///////
        {
            return false;
        }/////////////////////////////////////////////////////////////////////////////////////

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
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
