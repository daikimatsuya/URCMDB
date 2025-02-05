using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //�R���g���[���[�̐ڑ����m�F
    public class GetControllerScript
    {
        static private bool isControllerConectic;

        //�ڑ����Ă���R���g���[���[���擾
        static public void SearchController()
        {
            var controllerNames = Input.GetJoystickNames(); //�ڑ����Ă���R���g���[���[�̖��O���擾

            if(controllerNames.Length == 0 )
            {
                isControllerConectic = false;   //�z��ɓ����Ă��Ȃ�������false
                return;
            }
            if (controllerNames[0] =="")
            {
                isControllerConectic = false;   //���O�������ĂȂ�������false
            }
            else
            {
                isControllerConectic = true;    //����ȊO��true
            }
        }

        //�R���g���[���[���ڑ�����Ă��邩���擾
        static public bool GetIsConectic()
        {
            return isControllerConectic;
        }
    }
}
