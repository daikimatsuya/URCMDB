using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//モニターをリストで管理
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
            monitorList[i].Move();      //移動させる
        }

        //プレイヤーが死んだらリセット
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
    public void StartMonitorList()
    {

        monitorList = new List<MonitorScript>(FindObjectsOfType<MonitorScript>());
        for (int i = 0; i < monitorList.Count; i++)
        {
            monitorList[i].StartMonitor();
        }
    }

}
