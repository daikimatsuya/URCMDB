using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //���Ԋ֌W�̊֐����܂Ƃ߂����[�e�B���e�B�N���X
    public static class TimeCountScript
    {
        //�J�E���g�_�E���pINT
        public static bool TimeCounter(ref int timeBuff)
        {
            if (timeBuff <= 0)  //���Ԃ�0������������true��Ԃ�/////////
            {
                return true;
            }//////////////////////////////////////////////////////////////

            else
            {
                timeBuff--; //���Ԃ����炷
                return false;
            }
        }
        //�J�E���g�_�E���pFLOAT
        public static bool TimeCounter(ref float timeBuff)
        {
            if (timeBuff <= 0) //���Ԃ�0������������true��Ԃ�/////////
            {
                return true;
            }//////////////////////////////////////////////////////////////
            else
            {
                timeBuff--; //���Ԃ����炷
                return false;
            }
        }
        //���ԃZ�b�g(60fps)
        public static void SetTime(ref int timeBuff, float time)
        {
            timeBuff = (int)(time * 60);//�ݒ肵�����Ԃ��t���[�����ɕϊ�
        }
        //���ԃZ�b�g2(60fps)
        public static void SetTime(ref float timeBuff, float time)
        {
            timeBuff = time * 60;//�ݒ肵�����Ԃ��t���[�����ɕϊ�
        }


    }
}