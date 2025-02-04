using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{

    //���������I�u�W�F�N�g����]������
    public class RollingScript 
    {
        //��]�������W���g�����X�t�H�[���ɓ����
        public void Rolling(Transform tf,float rowSpeed,in string rotationAxis)
        {

            if (rotationAxis=="x")//x������]������
            {
                float buff = tf.localEulerAngles.x + rowSpeed;
                Vector3 testBuff = new Vector3(buff, 0, 0);

                tf.localEulerAngles = Vector3.zero;
                tf.localEulerAngles = testBuff;//�����̑�����Ȃ����o�O���Ă�
                return;
            }
            if (rotationAxis=="y")//y������]������
            {
                tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y + rowSpeed, tf.localEulerAngles.z);
                return;
            }
            if (rotationAxis=="z")//z������]������
            {
                tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y, tf.localEulerAngles.z + rowSpeed);
                return;
            }

        }

    }
}
