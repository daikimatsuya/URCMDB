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

        //�R���g���[���[�̃X�e�B�b�N���������ꂽ�̂����擾
        static public float GetAxisDown(string XorY)
        {
            bool isGetDown = false;
            float axis = 0;

            if (XorY == "LeftStickX")
            {

                axis = Input.GetAxis("LeftStickX");    //�g���K�[�̒l���擾


                if (axis!=0.0f && (beforeStickAxisX <= 0.1f&& beforeStickAxisX >= -0.1f))   //�O�t���[���Ɣ�r/////////
                {
                    isGetDown = true;   //���t���[��������Ă�����g�D���[��

                }/////////////////////////////////////////////////////////////////////////////////

                beforeStickAxisXBuff = axis;   //���t���[���̒l��O�t���[���ɑ��
            }
            else if (XorY == "LeftStickY")
            {


                axis = Input.GetAxis("LeftStickY");   //�g���K�[�̒l���擾


                if (axis!=0.0f && beforeStickAxisY == 0.0f)   //�O�t���[���Ɣ�r/////////
                {
                    isGetDown = true;    //���t���[��������Ă�����g�D���[��

                }///////////////////////////////////////////////////////////////////////////////////

                beforeStickAxisYtBuff = axis;  //���t���[���̒l��O�t���[���ɑ��
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


        //���͏����X�V
        static public void AxisUpdate()
        {
            beforeStickAxisX = beforeStickAxisXBuff;
            beforeStickAxisY = beforeStickAxisYtBuff;
        }

    }
}