using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    public class GetControllerScript : MonoBehaviour
    {
        static private bool isControllerConectic;
        static public void SearchController()
        {
            var controllerNames = Input.GetJoystickNames();

            if (controllerNames[0] =="")
            {
                isControllerConectic = false;
            }
            else
            {
                isControllerConectic = true;
            }
        }

        static public bool GetIsConectic()
        {
            return isControllerConectic;
        }
    }
}
