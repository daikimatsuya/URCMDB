using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlegameScript : MonoBehaviour
{
    private bool isReset;
    private bool isMoveStart;
    private bool isHit;
    private void TitleGameController()
    {
        if (isReset)
        {
            isReset = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReset = true;
            ResetFlags();
        }
    }
    private void ResetFlags()
    {
        isMoveStart = false;
    }
    public bool GetResetFlag()
    {
        return isReset;
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
    // Start is called before the first frame update
    void Start()
    {
        isReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        TitleGameController();  
    }
}
