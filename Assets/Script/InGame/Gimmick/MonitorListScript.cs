using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorListScript : MonoBehaviour
{
    private List<MonitorScript> monitorList;

    //���j�^�[�Q�Ǘ�
    public void MonitorListController(in PlayerScript ps,in bool isPose)
    {
        if (isPose)
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
            monitorList[i].ResetPos();  //���Z�b�g
        }
    }
    //����������
    public void AwakeMonitorList()
    {

        monitorList = new List<MonitorScript>(FindObjectsOfType<MonitorScript>());
        for (int i = 0; i < monitorList.Count; i++)
        {
            monitorList[i].StartMonitor();
        }
    }

}
