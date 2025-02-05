using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //�g���K�[�����t���[���ŉ����ꂽ�̂��𔻕�
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

        //�R���g���[���[�̃g���K�[���������ꂽ�̂����擾
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
                    axis = Input.GetAxis("LeftTrigger");    //�g���K�[�̒l���擾
                }
               
                if (axis > 0.0f && beforeTriggerAxisLeft == 0.0f)   //�O�t���[���Ɣ�r/////////
                {
                    isGetDown = true;   //���t���[��������Ă�����g�D���[��

                }/////////////////////////////////////////////////////////////////////////////////

                beforeTriggerAxisLeftBuff = axis;   //���t���[���̒l��O�t���[���ɑ��
            }
            else if (leftOrRight == "RightTrigger")
            {
                if(!TimeCountScript.TimeCounter(ref intervalRightBuff))
                {
                    axis = 1.0f;
                }
                else
                {
                    axis = Input.GetAxis("RightTrigger");   //�g���K�[�̒l���擾
                }
                
                if (axis > 0.0f && beforeTriggerAxisRight == 0.0f)   //�O�t���[���Ɣ�r/////////
                {
                    isGetDown = true;    //���t���[��������Ă�����g�D���[��

                }///////////////////////////////////////////////////////////////////////////////////

                beforeTriggerAxisRightBuff = axis;  //���t���[���̒l��O�t���[���ɑ��
            }

            return isGetDown;
        }

        //���͏����X�V
        static public void AxisUpdate()
        {
            beforeTriggerAxisLeft = beforeTriggerAxisLeftBuff;
            beforeTriggerAxisRight= beforeTriggerAxisRightBuff;
        }

        //�V�[�����ׂ��ۂɒl��ۑ�����
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
