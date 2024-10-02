using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlegameScript : MonoBehaviour
{
    private bool isReset;
    private bool isMoveStart;
    private void TitleGameController()
    {
        if (isReset)
        {
            isReset = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReset = true;
        }
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
