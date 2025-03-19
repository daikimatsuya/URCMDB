using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPbotListScript : MonoBehaviour
{
    private List<EMPbotScript> empbotList;

    //EMPbotä«óù
    public void EMPbotListController(in bool isPause,in PlayerControllerScript pcs)
    {
        if (isPause)
        {
            return;
        }
        for(int i = 0; i < empbotList.Count; i++)
        {
            empbotList[i].EMPbotController();
            empbotList[i].EMPController(pcs.GetPlayer());
        }
    }

    //EMPbotëÅä˙èâä˙âª
    public void AwakeEMPbotList(in PlayerControllerScript pcs)
    {
        empbotList = new List<EMPbotScript>(FindObjectsOfType<EMPbotScript>());

        for(int i = 0; i < empbotList.Count; i++)
        {
            empbotList[i].StartEMPbot(in pcs);
        }
    }
}
