using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Usefull
{
    public class GetStickScript
    {
        private static float beforeStickAxisX;
        private static float beforeStickAxisY;

        private static float beforeStickAxisXBuff;
        private static float beforeStickAxisYtBuff;

        //コントローラーのスティックがいつ押されたのかを取得
        static public float GetAxisDown(string XorY)
        {
            bool isGetDown = false;
            float axis = 0;

            if (XorY == "LeftStickX")
            {

                axis = Input.GetAxis("LeftStickX");    //トリガーの値を取得


                if (axis!=0.0f && (beforeStickAxisX <= 0.1f&& beforeStickAxisX >= -0.1f))   //前フレームと比較/////////
                {
                    isGetDown = true;   //今フレーム押されていたらトゥルーに

                }/////////////////////////////////////////////////////////////////////////////////

                beforeStickAxisXBuff = axis;   //今フレームの値を前フレームに代入
            }
            else if (XorY == "LeftStickY")
            {


                axis = Input.GetAxis("LeftStickY");   //トリガーの値を取得


                if (axis!=0.0f && beforeStickAxisY == 0.0f)   //前フレームと比較/////////
                {
                    isGetDown = true;    //今フレーム押されていたらトゥルーに

                }///////////////////////////////////////////////////////////////////////////////////

                beforeStickAxisYtBuff = axis;  //今フレームの値を前フレームに代入
            }

            if(isGetDown)
            {
                return axis;
            }
            else
            {
                return 0;
            }

        }


        //入力情報を更新
        static public void AxisUpdate()
        {
            beforeStickAxisX = beforeStickAxisXBuff;
            beforeStickAxisY = beforeStickAxisYtBuff;
        }

    }
}