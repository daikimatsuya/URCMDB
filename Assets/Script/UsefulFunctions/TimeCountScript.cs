using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //時間関係の関数をまとめたユーティリティクラス
    public static class TimeCountScript
    {
        //カウントダウン用INT
        public static bool TimeCounter(ref int timeBuff)
        {
            if (timeBuff <= 0)  //時間が0未満だったらtrueを返す/////////
            {
                return true;
            }//////////////////////////////////////////////////////////////

            else
            {
                timeBuff--; //時間を減らす
                return false;
            }
        }
        //カウントダウン用FLOAT
        public static bool TimeCounter(ref float timeBuff)
        {
            if (timeBuff <= 0) //時間が0未満だったらtrueを返す/////////
            {
                return true;
            }//////////////////////////////////////////////////////////////
            else
            {
                timeBuff--; //時間を減らす
                return false;
            }
        }
        //時間セット(60fps)
        public static void SetTime(ref int timeBuff, float time)
        {
            timeBuff = (int)(time * 60);//設定した時間をフレーム数に変換
        }
        //時間セット2(60fps)
        public static void SetTime(ref float timeBuff, float time)
        {
            timeBuff = time * 60;//設定した時間をフレーム数に変換
        }


    }
}