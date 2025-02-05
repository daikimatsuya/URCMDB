using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

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

    //ミニゲーム管理
    private void TitleGameController()
    {
        //ミニゲームスタートフラグ管理
        if(Input.GetKeyUp(KeyCode.F)) 
        { 
            isGameStart = true; //デバッグ用
        }
        if (TimeCountScript.TimeCounter(ref startCountBuff))
        {
            isGameStart = true;
        }
        ////////////////////////////////
        
        if (isReset)    //リセットフラグがオンになったら///////////
        {
            if(TimeCountScript.TimeCounter(ref resetDelay))   //リセットまでの時間///
            {
                ResetFlags();   //フラグ類をリセットさせる
                isReset = false;
            }/////////////////////////////////////////////

        }///////////////////////////////////////////////////////////
        ResetTimer();
    }
    //フラグ関連リセット
    public void ResetFlags()
    {
        //フラグリセット
        isMoveStart = false;
        isMiniPlayerDead = false;
        isGameStart = false;
        isGoalAction = false;
        ////////////////

        TimeCountScript.SetTime(ref startCountBuff, minigameStartCount);
    }
    //タイマーリセット
    private void ResetTimer()
    {
        if(isMiniPlayerDead)    //ミニゲーム用プレイヤーが死んだら/////////
        {
            if(TimeCountScript.TimeCounter(ref intervalBuff))
            {
                isResetAction = true;   //リセット用フラグ起動
                TimeCountScript.SetTime(ref intervalBuff, resetInterval);
            }
        }//////////////////////////////////////////////////////////////////////
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
