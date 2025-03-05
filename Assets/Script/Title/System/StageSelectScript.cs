using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Usefull;

//�^�C�g���̃X�e�[�W�I�������p
public class StageSelectScript : MonoBehaviour
{
    [SerializeField] private int baseCount;
    [SerializeField] private int maxStage;
    [SerializeField] private string[] stage;
    [SerializeField] private float stageSelectCoolTime;
    [SerializeField] private int coolTimeBuff;
    [SerializeField] private float fadeTime;
    private int fadeTimeBuff;

    private int stageCount;
    private int stageChangeCount;
    private bool fadeStart;
    private bool fadeEnd;

    private StageRotationScript srs;

    //�X�e�[�W�Z���N�g�Ǘ�
    public void SelectController(in bool canStageChange)
    {
        StageSelect(in canStageChange);                          //�I�����Ă�X�e�[�W�Ǘ�
        srs.Move(stageChangeCount, maxStage);              //���f������]������

        if (!fadeStart) 
        {
            return;
        }
        if (TimeCountScript.TimeCounter(ref fadeTimeBuff))
        {
            fadeEnd = true;                 //�t�F�[�h�̏I���`�F�b�N
        }
    }

    //�I���X�e�[�W�����Z���Z
    private void StageSelect(in bool canStageChange)
    {
        if (canStageChange)
        {         
            return;
        }

        if (srs.GetRotateEnd())  
        {
            //�������̉�]
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (Input.GetAxis("LeftStickX") < 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && coolTimeBuff <= 0)  
            {
                stageChangeCount--;             //�X�e�[�W�̉�]�J�E���g��-1

                if (stageCount > 0)              
                {
                    stageCount--;                   //�X�e�[�W�J�E���g���[�P   
                }
                else
                {
                    stageCount = maxStage;  //�X�e�[�W�J�E���g���O�Ȃ�ő�ɂ���
                }
               TimeCountScript.SetTime(ref coolTimeBuff, stageSelectCoolTime);
            }

            //�E�����̉�]
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (Input.GetAxis("LeftStickX")>0||Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && coolTimeBuff <= 0)  
            {
                stageChangeCount++;        //�X�e�[�W�̉�]�J�E���g��+1
                if (stageCount < maxStage)
                {
                    stageCount++;              //�X�e�[�W�J�E���g��+1
                }
                else
                {
                    stageCount = 0;   //�X�e�[�W�J�E���g���ő�l�Ȃ�O�ɂ���
                }
                TimeCountScript.SetTime(ref coolTimeBuff, stageSelectCoolTime);
            }
            coolTimeBuff--;
        }

    }
    //�X�e�[�W�I�����Z�b�g
    public void StageSelectReset()
    {
        stageCount = baseCount;
        stageChangeCount = baseCount;
        srs.ResetRotate(stageChangeCount,maxStage);
    }
 
    #region �l�󂯓n��
    public bool GetFadeEnd()
    {
        return fadeEnd;
    }
    public string GetStage()
    {
        return stage[stageCount];
    }
    public Vector2 GetStageChangeCount()
    {
        return new Vector2(stageChangeCount,maxStage);
    }
    public int GetStageCount()
    {
        return stageCount;
    }
    public void SetFadeFlag(bool flag)
    {
        fadeStart = flag;
    }

    #endregion

    //�R���|�[�l���g�擾
    private void GetComponets()
    {
        srs = GameObject.FindWithTag("stage").GetComponent<StageRotationScript>();
    }

    //�X�e�[�W�I��������
    public void StartStageSelect()
    {
        GetComponets();

        stageCount = baseCount;
        stageChangeCount = baseCount;
        TimeCountScript.SetTime(ref fadeTimeBuff,fadeTime);
        fadeEnd = false;
        srs.StartStageRotation();
    }

}
