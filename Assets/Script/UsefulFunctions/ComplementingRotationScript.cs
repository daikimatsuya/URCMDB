using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //回転するオブジェクトの回転を補完する
    public class ComplementingRotationScript
    {
        //回転補間
        static public float Rotate(float rotateSpeed, float targetRot, float objectRot)
        {
            //現在の角度と目標角度の差を計算
            int rot;
            rot = (int)(targetRot - objectRot);
            ///////////////////////////////////

            //反対方向に回ったほうが近い場合の差を修正
            if (rot > 180)
            {
                rot = -360 + rot;
            }
            if (rot < -180)
            {
                rot = 360 + rot;
            }
            /////////////////////////////////////////////

            //角度加算////////////////////
            if (rot == 0)
            {
                return 0;
            }

            if (rot < 0)
            {
                if (rot > rotateSpeed)
                {
                    return rot;
                }
                return -rotateSpeed;
            }

            if (rot > rotateSpeed)
            {
                return rotateSpeed;
            }
            return rot;
            /////////////////////////////
        }

    }
}