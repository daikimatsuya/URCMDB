using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //PMSの値をシーンを超えて保存する
    public class PMSScript
    {
        static bool isPMS;
        //PMS保存
        static public void SetPMS(in bool pms)
        {
            isPMS = pms;
        }
        //PMS取得
        static public bool GetPMS()
        {
            return isPMS;
        }
    }
}
