using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //トリガーが今フレームで押されたのかを判別
    public class GetTriggerScript : MonoBehaviour
    {
        private static float beforeTriggerAxisLeft;
        private static float beforeTriggerAxisRight;

        //コントローラーのトリガーがいつ押されたのかを取得
        static public bool GetAxisDown(string leftOrRight)
        {
            bool isGetDown = false;
            float axis = 0;

            if (leftOrRight == "LeftTrigger")
            {
                axis = Input.GetAxis("LeftTrigger");    //トリガーの値を取得
                if (axis > 0.0f && beforeTriggerAxisLeft == 0.0f)   //前フレームと比較/////////
                {
                    isGetDown = true;   //今フレーム押されていたらトゥルーに

                }/////////////////////////////////////////////////////////////////////////////////

                beforeTriggerAxisLeft = axis;   //今フレームの値を前フレームに代入
            }
            else if (leftOrRight == "RightTrigger")
            {
                axis = Input.GetAxis("RightTrigger");   //トリガーの値を取得
                if (axis > 0.0f && beforeTriggerAxisRight == 0.0f)   //前フレームと比較/////////
                {
                    isGetDown = true;    //今フレーム押されていたらトゥルーに

                }///////////////////////////////////////////////////////////////////////////////////

                beforeTriggerAxisRight = axis;  //今フレームの値を前フレームに代入
            }

            return isGetDown;
        }
    }
}
