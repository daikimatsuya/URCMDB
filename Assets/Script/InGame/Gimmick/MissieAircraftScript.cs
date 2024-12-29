using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissieAircraftScript : MonoBehaviour
{
    [SerializeField] private GameObject upperHatch;
    [SerializeField] private Vector3 movePosUpperHatch;
    [SerializeField] private GameObject lowerHatch;
    [SerializeField] private Vector3 movePosLowerHatch;
    private Vector3 initialPosUpperHatch;
    private Vector3 initialPosLowerHatch;
    [SerializeField] private float moveTime;
    private int moveTimeBuff;

    Transform upperHatchTF;
    Transform lowerHatchTF;

    private bool isOpen;
    private bool isEnd;
    private Vector3 moveSpeedUpper;
    private Vector3 moveSpeedLower;

    private void HatchController()
    {
        if(isEnd)
        {
            return;
        }

        if(isOpen)
        {
            //upperHatchTF.localPosition=new Vector3(initialPosUpperHatch.x+(moveSpeedUpper.x*))
        }
        else
        {

        }
        if (TimeCountScript.TimeCounter(ref moveTimeBuff))
        {
            isEnd = true;
        }
    }
    private void SetSpeed()
    {
        Vector3 lowerPosBuff = initialPosLowerHatch - movePosLowerHatch;
        Vector3 upperPosBuff = initialPosUpperHatch - movePosUpperHatch;

        if (moveTimeBuff == 0)
        {
            moveTimeBuff = 1;
        }
        moveSpeedLower=new Vector3(lowerPosBuff.x/moveTimeBuff, lowerPosBuff.y/moveTimeBuff, lowerPosBuff.z/moveTimeBuff);
        moveSpeedUpper=new Vector3(upperPosBuff.x/moveTimeBuff, upperPosBuff.y/moveTimeBuff, upperPosBuff.z/moveTimeBuff);
    }
    public void SetFlag(bool flag)
    {
        isOpen = flag;
        TimeCountScript.SetTime(ref moveTimeBuff, moveTime);
        isEnd = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        upperHatchTF = upperHatch.GetComponent<Transform>();
        lowerHatchTF = lowerHatch.GetComponent<Transform>();
        initialPosUpperHatch=upperHatchTF.localPosition;
        initialPosLowerHatch=lowerHatchTF.localPosition;

        TimeCountScript.SetTime(ref moveTimeBuff, moveTime);
        SetSpeed();
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
