using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //コントローラーの接続を確認
    public class GetControllerScript
    {
        static private bool isControllerConectic;

        //接続しているコントローラーを取得
        static public void SearchController()
        {
            var controllerNames = Input.GetJoystickNames(); //接続しているコントローラーの名前を取得

            if(controllerNames.Length == 0 )
            {
                isControllerConectic = false;   //配列に入っていなかったらfalse
                return;
            }
            if (controllerNames[0] =="")
            {
                isControllerConectic = false;   //名前が入ってなかったらfalse
            }
            else
            {
                isControllerConectic = true;    //それ以外はtrue
            }
        }

        //コントローラーが接続されているかを取得
        static public bool GetIsConectic()
        {
            return isControllerConectic;
        }
    }
}
