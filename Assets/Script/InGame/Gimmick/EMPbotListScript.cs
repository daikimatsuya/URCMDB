using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPbotListScript : MonoBehaviour
{
    private List<EMPbotScript> empbotList;

    //EMPbot管理
    public void EMPbotListController(in bool isPause,in PlayerControllerScript pcs)
    {
        if (isPause)
        {
            return;
        }
        for(int i = 0; i < empbotList.Count; i++)
        {
            empbotList[i].EMPbotController();                       //EMP発生させるやつ管理
            empbotList[i].EMPController(pcs.GetPlayer());     //発生させたEMPを管理
        }
    }

    //EMPbot早期初期化
    public void StartEMPbotList(in PlayerControllerScript pcs)
    {
        empbotList = new List<EMPbotScript>(FindObjectsOfType<EMPbotScript>());

        for(int i = 0; i < empbotList.Count; i++)
        {
            empbotList[i].StartEMPbot(in pcs);
        }
    }
}
