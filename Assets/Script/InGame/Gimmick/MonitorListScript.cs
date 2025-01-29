using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorListScript : MonoBehaviour
{
    private List<MonitorScript> monitorList = new List<MonitorScript>();

    //モニター群管理
    public void MonitorListController(in PlayerScript ps)
    {
        if (monitorList == null)
        {
            return;
        }
        for(int i =0;i< monitorList.Count; i++)
        {
            monitorList[i].Move();
        }
        if(ps != null)
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
        int i = 0;
        foreach(Transform children in this.gameObject.transform)
        {
            monitorList.Add(children.GetComponent<MonitorScript>());
            monitorList[i].StartMonitor();
            i++;
        }
    }

}
