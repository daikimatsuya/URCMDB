using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //��]����I�u�W�F�N�g�̉�]��⊮����
    public class ComplementingRotationScript : MonoBehaviour
    {
        //��]���
        public float Rotate(float rotateSpeed, float targetRot, float objectRot)
        {
            //���݂̊p�x�ƖڕW�p�x�̍����v�Z
            int rot;
            rot = (int)(targetRot - objectRot);
            ///////////////////////////////////

            //���Ε����ɉ�����ق����߂��ꍇ�̍����C��
            if (rot > 180)
            {
                rot = -360 + rot;
            }
            if (rot < -180)
            {
                rot = 360 + rot;
            }
            /////////////////////////////////////////////

            //�p�x���Z////////////////////
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