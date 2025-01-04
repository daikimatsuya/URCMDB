using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

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

    [SerializeField] private bool isOpen;
    [SerializeField] private bool isEnd;
    private Vector3 moveSpeedUpper;
    private Vector3 moveSpeedLower;

    //�O���n�b�`�J�Ǘ�
    private void HatchController()
    {
        if(isEnd)   //�ړ��������Ă���return��Ԃ�///
        {
            return;
        }//////////////////////////////////////////////

        if(isOpen)  //�n�b�`���J��////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            upperHatchTF.localPosition = new Vector3(upperHatchTF.localPosition.x - moveSpeedUpper.x, upperHatchTF.localPosition.y - moveSpeedUpper.y, upperHatchTF.localPosition.z - moveSpeedUpper.z);
            lowerHatchTF.localPosition = new Vector3(lowerHatchTF.localPosition.x - moveSpeedLower.x, lowerHatchTF.localPosition.y - moveSpeedLower.y, lowerHatchTF.localPosition.z - moveSpeedLower.z);
        }/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        else   //�n�b�`�����/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            upperHatchTF.localPosition = new Vector3(upperHatchTF.localPosition.x + moveSpeedUpper.x, upperHatchTF.localPosition.y + moveSpeedUpper.y, upperHatchTF.localPosition.z + moveSpeedUpper.z);
            lowerHatchTF.localPosition = new Vector3(lowerHatchTF.localPosition.x + moveSpeedLower.x, lowerHatchTF.localPosition.y + moveSpeedLower.y, lowerHatchTF.localPosition.z + moveSpeedLower.z);
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (TimeCountScript.TimeCounter(ref moveTimeBuff))
        {
            TimeCountScript.SetTime(ref moveTimeBuff, moveTime);
            isEnd = true;
        }
    }
    //�n�b�`�̊J���x�Z�o
    private void SetSpeed()
    {
        //�ړ������Z�o
        Vector3 lowerPosBuff = initialPosLowerHatch - movePosLowerHatch;
        Vector3 upperPosBuff = initialPosUpperHatch - movePosUpperHatch;
        /////////////////
        
        //������O�ɂ��Ȃ�
        if (moveTimeBuff == 0)
        {
            moveTimeBuff = 1;
        }
        //////////////////
        
        //���x���
        moveSpeedLower=new Vector3(lowerPosBuff.x/moveTimeBuff, lowerPosBuff.y/moveTimeBuff, lowerPosBuff.z/moveTimeBuff);
        moveSpeedUpper=new Vector3(upperPosBuff.x/moveTimeBuff, upperPosBuff.y/moveTimeBuff, upperPosBuff.z/moveTimeBuff);
        //////////
    }

    //�J�t���O�Ǘ�
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
        isEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        HatchController();
    }
}
