using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //PMS‚Ì’l‚ğƒV[ƒ“‚ğ’´‚¦‚Ä•Û‘¶‚·‚é
    public class PMSScript
    {
        static bool isPMS;
        //PMS•Û‘¶
        static public void SetPMS(in bool pms)
        {
            isPMS = pms;
        }
        //PMSæ“¾
        static public bool GetPMS()
        {
            return isPMS;
        }
    }
}
