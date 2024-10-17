using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間関係の関数をまとめたユーティリティクラス
public static class TimeCountScript 
{
    //カウントダウン用INT
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
    //カウントダウン用FLOAT
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
    //時間セット
    public static void SetTime(ref int timeBuff,float time)
    {
        timeBuff = (int)(time * 60);
    }


}
