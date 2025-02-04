using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{

    //くっつけたオブジェクトを回転させる
    public class RollingScript 
    {
        //回転した座標をトランスフォームに入れる
        public void Rolling(Transform tf,float rowSpeed,in string rotationAxis)
        {

            if (rotationAxis=="x")//x軸を回転させる
            {
                float buff = tf.localEulerAngles.x + rowSpeed;
                Vector3 testBuff = new Vector3(buff, 0, 0);

                tf.localEulerAngles = Vector3.zero;
                tf.localEulerAngles = testBuff;//ここの代入がなぜかバグってる
                return;
            }
            if (rotationAxis=="y")//y軸を回転させる
            {
                tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y + rowSpeed, tf.localEulerAngles.z);
                return;
            }
            if (rotationAxis=="z")//z軸を回転させる
            {
                tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y, tf.localEulerAngles.z + rowSpeed);
                return;
            }

        }

    }
}
