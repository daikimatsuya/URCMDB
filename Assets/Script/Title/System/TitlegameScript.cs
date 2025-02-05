using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//���̂Ƃ���^�C�g���̃~�j�Q�[���̊Ǘ��̂��̂����ɂ���
public class TitlegameScript : MonoBehaviour
{
    [SerializeField] private float minigameStartCount;
    private int startCountBuff;
    [SerializeField] private float resetInterval;
    private int intervalBuff;

    private bool isReset;
    private bool isMoveStart;
    private bool isHit;
    private bool isResetAction;
    private bool isMiniPlayerDead;
    private bool isGameStart;
    private bool isGoalAction; 
    private int resetDelay;

    //�~�j�Q�[���Ǘ�
    private void TitleGameController()
    {
        //�~�j�Q�[���X�^�[�g�t���O�Ǘ�
        if(Input.GetKeyUp(KeyCode.F)) 
        { 
            isGameStart = true; //�f�o�b�O�p
        }
        if (TimeCountScript.TimeCounter(ref startCountBuff))
        {
            isGameStart = true;
        }
        ////////////////////////////////
        
        if (isReset)    //���Z�b�g�t���O���I���ɂȂ�����///////////
        {
            if(TimeCountScript.TimeCounter(ref resetDelay))   //���Z�b�g�܂ł̎���///
            {
                ResetFlags();   //�t���O�ނ����Z�b�g������
                isReset = false;
            }/////////////////////////////////////////////

        }///////////////////////////////////////////////////////////
        ResetTimer();
    }
    //�t���O�֘A���Z�b�g
    public void ResetFlags()
    {
        //�t���O���Z�b�g
        isMoveStart = false;
        isMiniPlayerDead = false;
        isGameStart = false;
        isGoalAction = false;
        ////////////////

        TimeCountScript.SetTime(ref startCountBuff, minigameStartCount);
    }
    //�^�C�}�[���Z�b�g
    private void ResetTimer()
    {
        if(isMiniPlayerDead)    //�~�j�Q�[���p�v���C���[�����񂾂�/////////
        {
            if(TimeCountScript.TimeCounter(ref intervalBuff))
            {
                isResetAction = true;   //���Z�b�g�p�t���O�N��
                TimeCountScript.SetTime(ref intervalBuff, resetInterval);
            }
        }//////////////////////////////////////////////////////////////////////
    }
    #region�@�l�󂯓n��
    public bool GetResetFlag()
    {
        return isReset;
    }
    public void SetResetFlag(bool flag)
    {
        isReset=flag;
        resetDelay=30;//�������Ƃŏ����悭�킩��Ȃ�
    }
    public void SetMoveFlag(bool flag)
    {
        isMoveStart = flag;
    }
    public bool GetMoveFlag()
    {
        return isMoveStart;
    }
    public void SetHitFlag(bool flag)
    {
        isHit = flag;
    }
    public bool GetResetActionFlag()
    {
        return isResetAction;
    }
    public void SetResetActionFlag(bool flag)
    {
        isResetAction = flag;
    }
    public void SetMiniPlayerDead(bool isDead)
    {
        isMiniPlayerDead = isDead;
    }
    public bool GetGameStartFlag()
    {
        return isGameStart;
    }
    public void SetGoalActionFlag(bool flag)
    {
        isGoalAction = flag;
    }
    public bool GetGoalActionFlag()
    {
        return isGoalAction;
    }
    #endregion
    // Start is called before the first frame update
    public void StartTitleGame()
    {
        isReset = false;
        isResetAction = false;
        startCountBuff = (int)(minigameStartCount * 60);
    }
    void Start()
    {
        StartTitleGame();
    }

    // Update is called once per frame
    void Update()
    {
        TitleGameController();  
    }
}
