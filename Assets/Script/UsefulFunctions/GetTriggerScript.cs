using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //�g���K�[�����t���[���ŉ����ꂽ�̂��𔻕�
    public class GetTriggerScript : MonoBehaviour
    {
        private static float beforeTriggerAxisLeft;
        private static float beforeTriggerAxisRight;

        //�R���g���[���[�̃g���K�[���������ꂽ�̂����擾
        static public bool GetAxisDown(string leftOrRight)
        {
            bool isGetDown = false;
            float axis = 0;

            if (leftOrRight == "LeftTrigger")
            {
                axis = Input.GetAxis("LeftTrigger");    //�g���K�[�̒l���擾
                if (axis > 0.0f && beforeTriggerAxisLeft == 0.0f)   //�O�t���[���Ɣ�r/////////
                {
                    isGetDown = true;   //���t���[��������Ă�����g�D���[��

                }/////////////////////////////////////////////////////////////////////////////////

                beforeTriggerAxisLeft = axis;   //���t���[���̒l��O�t���[���ɑ��
            }
            else if (leftOrRight == "RightTrigger")
            {
                axis = Input.GetAxis("RightTrigger");   //�g���K�[�̒l���擾
                if (axis > 0.0f && beforeTriggerAxisRight == 0.0f)   //�O�t���[���Ɣ�r/////////
                {
                    isGetDown = true;    //���t���[��������Ă�����g�D���[��

                }///////////////////////////////////////////////////////////////////////////////////

                beforeTriggerAxisRight = axis;  //���t���[���̒l��O�t���[���ɑ��
            }

            return isGetDown;
        }
    }
}
