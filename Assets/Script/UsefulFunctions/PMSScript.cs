using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //PMS�̒l���V�[���𒴂��ĕۑ�����
    public class PMSScript
    {
        static bool isPMS;
        //PMS�ۑ�
        static public void SetPMS(in bool pms)
        {
            isPMS = pms;
        }
        //PMS�擾
        static public bool GetPMS()
        {
            return isPMS;
        }
    }
}
