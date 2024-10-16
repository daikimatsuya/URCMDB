using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//時間関係の関数をまとめたユーティリティクラス
public static class TimeCountScript 
{
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
    public static void SetTime(ref int timeBuff,float time)
    {
        timeBuff = (int)(time * 60);
    }


}
