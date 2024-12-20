using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//今のところタイトルのミニゲームの管理のちのち奇麗にする
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

    //miniGameScrollScript mgss;

    //ミニゲーム管理
    private void TitleGameController()
    {
        if(Input.GetKeyUp(KeyCode.F)) 
        { 
            isGameStart = true;
        }
        if (startCountBuff<=0)
        {
            isGameStart = true;
        }
        if (isReset)
        {

            if(resetDelay<=0)
            {
                ResetFlags();
                isReset = false;
            }
            resetDelay--;
        }
        ResetTimer();
        startCountBuff--;
    }
    //フラグ関連リセット
    public void ResetFlags()
    {
        isMoveStart = false;
        isMiniPlayerDead = false;
        isGameStart = false;
        isGoalAction = false;

        startCountBuff = (int)(minigameStartCount * 60);
    }
    //タイマーリセット
    private void ResetTimer()
    {
        if(isMiniPlayerDead)
        {
            if(intervalBuff<=0)
            {
                isResetAction = true;
                intervalBuff = (int)(resetInterval * 60);
            }
            else
            {
                intervalBuff--;
            }
        }
    }
    #region　値受け渡し
    public bool GetResetFlag()
    {
        return isReset;
    }
    public void SetResetFlag(bool flag)
    {
        isReset=flag;
        resetDelay=30;//多分あとで消すよくわからない
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
    void Start()
    {
        isReset = false;
        isResetAction = false;

        startCountBuff = (int)(minigameStartCount * 60);
    }

    // Update is called once per frame
    void Update()
    {
        TitleGameController();  
    }
}
