using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlegameScript : MonoBehaviour
{
    [SerializeField] private float resetInterval;

    private bool isReset;
    private bool isMoveStart;
    private bool isHit;
    private bool isResetAction;
    private bool isMiniPlayerDead;

    private int intervalBuff;
    private int resetDelay;

    private void TitleGameController()
    {
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
    }
    private void ResetFlags()
    {
        isMoveStart = false;
        isMiniPlayerDead = false;
    }
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
    public bool GetResetFlag()
    {
        return isReset;
    }
    public void SetResetFlag(bool flag)
    {
        isReset=flag;
        resetDelay=1;
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
   
    // Start is called before the first frame update
    void Start()
    {
        isReset = false;
        isResetAction = false;
    }

    // Update is called once per frame
    void Update()
    {
        TitleGameController();  
    }
}
