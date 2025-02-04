using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    public class PMSScript
    {
        static bool isPMS;
        static public void SetPMS(in bool pms)
        {
            isPMS = pms;
        }
        static public bool GetPMS()
        {
            return isPMS;
        }
    }
}
