using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    private MonitorListScript mls;
   
    public void ListManagerController(in PlayerScript ps)
    {
        mls.MonitorListController();
        mls.SetPlayer(in ps);
    }
    public void AwakeListManager()
    {
        mls = GameObject.FindWithTag("MonitorList").GetComponent<MonitorListScript>();
        mls.AwakeMonitorList();
    }

}
