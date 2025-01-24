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
        ResetAll(in ps);
        ResetTutorial();
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
        ResetAll(in ps);
        ResetTutorial();
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
    public void ControlleTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);
        ResetTutorial();
        if (CheckPlayerControlle(in ps))
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
        ResetAll(in ps);
        ResetTutorial();
        if (CheckPMS(in ps))
        {
            tutorialCompletion += PMSTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }   
    //�������̃`���[�g���A���Ǘ�
    public void AcceTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);
        ResetTutorial();
        if (CheckPlayerAcce(in ps))
        {
            tutorialCompletion += acceTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }
    //�������񎞂̃`���[�g���A���Ǘ�
    public void QuickMoveTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);
        ResetTutorial();
        if (CheckQuickMove(in ps)&&CheckPlayerControlle(in ps))
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
        if (tutorialCompletion >= maxCompletion)
        {
            tutorialCompletion = 0;
            tutorialNumber++;
        }
    }
    //�v���C���[�����񂾂�S�����Z�b�g������
    public void ResetAll(in PlayerScript ps)
    {
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
    public bool CheckPlayerControlle(in PlayerScript ps)
    {
        if (Input.GetAxis("LeftStickX") != 0 || Input.GetAxis("LeftStickY") != 0) 
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    //�v���C���[�̃u�[�X�g���m�F
    public bool CheckPlayerBoost(in PlayerScript ps)
    {
        float acceBuff = ps.GetPlayerAcce();
        if(acceBuff>=1)
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
        if (ps.GetIsFire())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //�v���C���[�̉������m�F
    public bool CheckPlayerAcce(in PlayerScript ps)
    {
        if(Input.GetAxis("RightTrigger")!=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //�v���C���[��PMS���m�F
    public bool CheckPMS(in PlayerScript ps)
    {
        if (ps.GetPMS())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //�v���C���[�N�C�b�N���[�u���m�F
    public bool CheckQuickMove(in PlayerScript ps)
    {
        if (Input.GetAxis("LeftTrigger") != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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
