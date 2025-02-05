using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //トリガーが今フレームで押されたのかを判別
    public class GetTriggerScript
    {
        private static float beforeTriggerAxisLeft;
        private static float beforeTriggerAxisRight;
        private static float beforeTriggerAxisLeftBuff;
        private static float beforeTriggerAxisRightBuff;
        private static float intervalLeft=0.1f;
        private static float intervalLeftBuff;
        private static float intervalRight=0.1f;
        private static float intervalRightBuff;

        //コントローラーのトリガーがいつ押されたのかを取得
        static public bool GetAxisDown(string leftOrRight)
        {
            bool isGetDown = false;
            float axis = 0;

            if (leftOrRight == "LeftTrigger")
            {
                if(!TimeCountScript.TimeCounter(ref intervalLeftBuff))
                {
                    axis = 1.0f;
                }
                else
                {
                    axis = Input.GetAxis("LeftTrigger");    //トリガーの値を取得
                }
               
                if (axis > 0.0f && beforeTriggerAxisLeft == 0.0f)   //前フレームと比較/////////
                {
                    isGetDown = true;   //今フレーム押されていたらトゥルーに

                }/////////////////////////////////////////////////////////////////////////////////

                beforeTriggerAxisLeftBuff = axis;   //今フレームの値を前フレームに代入
            }
            else if (leftOrRight == "RightTrigger")
            {
                if(!TimeCountScript.TimeCounter(ref intervalRightBuff))
                {
                    axis = 1.0f;
                }
                else
                {
                    axis = Input.GetAxis("RightTrigger");   //トリガーの値を取得
                }
                
                if (axis > 0.0f && beforeTriggerAxisRight == 0.0f)   //前フレームと比較/////////
                {
                    isGetDown = true;    //今フレーム押されていたらトゥルーに

                }///////////////////////////////////////////////////////////////////////////////////

                beforeTriggerAxisRightBuff = axis;  //今フレームの値を前フレームに代入
            }

            return isGetDown;
        }

        //入力情報を更新
        static public void AxisUpdate()
        {
            beforeTriggerAxisLeft = beforeTriggerAxisLeftBuff;
            beforeTriggerAxisRight= beforeTriggerAxisRightBuff;
        }

        //シーンを跨ぐ際に値を保存する
        static public void SetValue()
        {
            if (Input.GetAxis("LeftTrigger")!=0)
            {
                TimeCountScript.SetTime(ref intervalLeftBuff, intervalLeft);
            }
            if (Input.GetAxis("RightTrigger")!=0)
            {
                TimeCountScript.SetTime(ref intervalRightBuff, intervalRight);
            }

        }

    }
}
