using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���j�^�[�����X�g�ŊǗ�
public class MonitorListScript : MonoBehaviour
{
    private List<MonitorScript> monitorList;

    //���j�^�[�Q�Ǘ�
    public void MonitorListController(in PlayerControllerScript pcs,in bool isPause)
    {
        if (isPause)
        {
            return;
        }

        for(int i =0;i< monitorList.Count; i++)
        {
            monitorList[i].Move();      //�ړ�������
        }

        //�v���C���[�����񂾂烊�Z�b�g
        if(pcs.GetPlayer() != null)
        {
            return;
        }
        for (int i = 0; i < monitorList.Count; i++)
        {
            monitorList[i].ResetPos();  //���Z�b�g
        }
    }
    //����������
    public void StartMonitorList()
    {

        monitorList = new List<MonitorScript>(FindObjectsOfType<MonitorScript>());
        for (int i = 0; i < monitorList.Count; i++)
        {
            monitorList[i].StartMonitor();
        }
    }

}
