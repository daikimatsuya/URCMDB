using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���Ԋ֌W�̊֐����܂Ƃ߂����[�e�B���e�B�N���X
public static class TimeCountScript 
{
    //�J�E���g�_�E���pINT
    public static bool TimeCounter(ref int timeBuff)
    {
        if (timeBuff <= 0)
        {
            return true;
        }
        else
        {
            timeBuff--;
            return false;
        }
    }
    //�J�E���g�_�E���pFLOAT
    public static bool  TimeCounter(ref float timeBuff)
    {
        if (timeBuff <= 0)
        {
            return true;
        }
        else
        {
            timeBuff--;
            return false;
        }
    }
    //���ԃZ�b�g
    public static void SetTime(ref int timeBuff,float time)
    {
        timeBuff = (int)(time * 60);
    }


}
