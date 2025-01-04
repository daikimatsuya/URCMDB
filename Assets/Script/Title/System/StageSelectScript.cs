using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//�^�C�g���̃X�e�[�W�I�������p
public class StageSelectScript : MonoBehaviour
{
    [SerializeField] private int stageCount;
    [SerializeField] private int maxStage;
    [SerializeField] private string[] stage;
    [SerializeField] private float stageSelectCoolTime;
    [SerializeField] private int coolTimeBuff;
    [SerializeField] private float fadeTime;
    private int fadeTimeBuff;

    private bool rotateEnd;
    private int stageChangeCount;
    private bool fadeStart;

    TitleScript ts;
    //�X�e�[�W�Z���N�g�Ǘ�
    private void SelectController()
    {
        if (fadeTimeBuff <= 0)  //�t�F�[�h���Ԃ��o�߂�����V�[����ς���///
        {
            ts.SceneChange();
        }////////////////////////////////////////////////////////////////////////

        if (ts.GetStageSelectFlag())   //�X�e�[�W�I����ʂɂȂ�����X�e�[�W�����J�E���g����////////////
        {
            StageSelect();
        }///////////////////////////////////////////////////////////////////////////////////////////////////

        else
        {
            StageSelectReset(); //�J�E���g�����X�e�[�W�������Z�b�g����
        }

        ts.SetStage(stage[stageCount]); //�J�E���g�����X�e�[�W������

        if (fadeStart)
        {
            fadeTimeBuff--; //�t�F�[�h���n�܂�����J�E���g�_�E���J�n
        }
    }
    //�I���X�e�[�W�����Z���Z
    private void StageSelect()
    {
        if (rotateEnd)  //�X�e�[�W�̉�]���I����Ă�����////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && coolTimeBuff == 0))  //�������̃X�e�[�W�؂�ւ��L�[�����܂��͈�莞�Ԉȏ㉟���Ă�����///////////
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

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && coolTimeBuff == 0))  //�E�����̃X�e�[�W�؂�ւ��L�[�����܂��͈�莞�Ԉȏ㉟���Ă�����///////////
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
        stageCount = 0;
    }
 
    #region �l�󂯓n��
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
    public TitleScript GetTitleScript()
    {
        return ts;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ts=GetComponent<TitleScript>();

        stageCount = 0;
        stageChangeCount = 0;
        rotateEnd = true;
        fadeTimeBuff = (int)(fadeTime * 60);
    }

    // Update is called once per frame
    void Update() 
    {
        SelectController();
    }
}
