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
    private bool rotateEnd;
    private int stageChangeCount;
    private bool fadeStart;
    private bool fadeEnd;

    private StageRotationScript srs;

    //�X�e�[�W�Z���N�g�Ǘ�
    public void SelectController(in bool canStageChange)
    {

        if (fadeTimeBuff <= 0)  //�t�F�[�h���Ԃ��o�߂�����V�[����ς���///
        {
            fadeEnd = true;
        }////////////////////////////////////////////////////////////////////////

        StageSelect(in canStageChange);
        srs.Move(stageChangeCount, maxStage);
        if (fadeStart)
        {
            fadeTimeBuff--; //�t�F�[�h���n�܂�����J�E���g�_�E���J�n
        }
    }

    //�I���X�e�[�W�����Z���Z
    private void StageSelect(in bool canStageChange)
    {
        if (!canStageChange)
        {
            StageSelectReset(); //�J�E���g�����X�e�[�W�������Z�b�g����
            return;
        }

        if (srs.GetRotateEnd())  //�X�e�[�W�̉�]���I����Ă�����////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (Input.GetAxis("LeftStickX") < 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && coolTimeBuff <= 0)  //�������̃X�e�[�W�؂�ւ��L�[�����܂��͈�莞�Ԉȏ㉟���Ă�����///////////
            {
                stageChangeCount--; //�X�e�[�W�̉�]�J�E���g��-1
                if (stageCount > 0) //�I�����Ă���X�e�[�W���P�ȏ�Ȃ�//////
                {
                    stageCount--;   //�X�e�[�W�J�E���g��-1
                }////////////////////////////////////////////////////////////////
                else
                {
                    stageCount = maxStage;  //�X�e�[�W�J�E���g���ő�l��
                }
               TimeCountScript.SetTime(ref coolTimeBuff, stageSelectCoolTime);
            }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (Input.GetAxis("LeftStickX")>0||Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && coolTimeBuff <= 0)  //�E�����̃X�e�[�W�؂�ւ��L�[�����܂��͈�莞�Ԉȏ㉟���Ă�����///////////
            {
                stageChangeCount++; //�X�e�[�W�̉�]�J�E���g��+1
                if (stageCount < maxStage)//�I�����Ă���X�e�[�W���ő�l�����Ȃ�//////
                {
                    stageCount++; //�X�e�[�W�J�E���g��-1
                }////////////////////////////////////////////////////////////////////////////
                else
                {
                    stageCount = 0;   //�X�e�[�W�J�E���g��0��
                }
                TimeCountScript.SetTime(ref coolTimeBuff, stageSelectCoolTime);
            }/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            coolTimeBuff--;
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
    //�X�e�[�W�I�����Z�b�g
    private void StageSelectReset()
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
    public void SetRotateEnd(bool flag)
    {
        rotateEnd = flag;
    }
    public string GetStage()
    {
        return stage[stageCount];
    }
    public bool GetRotateEnd() 
    {
        return rotateEnd;
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

    private void GetComponets()
    {
        srs = GameObject.FindWithTag("stage").GetComponent<StageRotationScript>();
    }

    // Start is called before the first frame update
    public void StartStageSelect()
    {
        GetComponets();

        stageCount = baseCount;
        stageChangeCount = baseCount;
        rotateEnd = true;
        TimeCountScript.SetTime(ref fadeTimeBuff,fadeTime);
        fadeEnd = false;

        srs.StartStageRotation();
    }

}
