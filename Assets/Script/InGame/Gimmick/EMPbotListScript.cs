using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPbotListScript : MonoBehaviour
{
    private List<EMPbotScript> empbotList;


    public void EMPbotListController(in bool isPause,in PlayerControllerScript pcs)
    {
        if (isPause)
        {
            return;
        }
        for(int i = 0; i < empbotList.Count; i++)
        {
            empbotList[i].EMPController(pcs.GetPlayer());
        }
    }

    public void AwakeEMPbotList()
    {
        empbotList = new List<EMPbotScript>(FindObjectsOfType<EMPbotScript>());



        for(int i = 0; i < empbotList.Count; i++)
        {
            empbotList[i].StartEMPbot();
        }
    }
}
