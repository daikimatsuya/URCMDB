using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorListScript : MonoBehaviour
{
    private List<MonitorScript> monitorList;

    //モニター群管理
    public void MonitorListController(in PlayerControllerScript pcs,in bool isPause)
    {
        if (isPause)
        {
            return;
        }

        for(int i =0;i< monitorList.Count; i++)
        {
            monitorList[i].Move();
        }
        if(pcs.GetPlayer() != null)
        {
            return;
        }
        for (int i = 0; i < monitorList.Count; i++)
        {
            monitorList[i].ResetPos();  //リセット
        }
    }
    //早期初期化
    public void AwakeMonitorList()
    {

        monitorList = new List<MonitorScript>(FindObjectsOfType<MonitorScript>());
        for (int i = 0; i < monitorList.Count; i++)
        {
            monitorList[i].StartMonitor();
        }
    }

}
