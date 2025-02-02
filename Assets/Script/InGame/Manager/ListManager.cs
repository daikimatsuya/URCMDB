using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    private MonitorListScript mls;
    private SpeedUpRingListScript surls;
    private FallRockListScript frls;
   
    //���X�g�X�V
    public void ListManagerController(in PlayerScript ps)
    {
        mls.MonitorListController(in ps);   //�Q�[�������j�^�[�Ǘ�
        surls.SpeedUpRingListController(in ps); //�X�s�[�h�A�b�v�����O�Ǘ�
        frls.RockFallListController();  //�◎�Ƃ��z�Ǘ�
    }
    //����������
    public void AwakeListManager()
    {
        mls = GameObject.FindWithTag("MonitorList").GetComponent<MonitorListScript>();
        mls.AwakeMonitorList();

        surls = GameObject.FindWithTag("SpeedUpRingList").GetComponent<SpeedUpRingListScript>();
        surls.AwakeSpeedUpRingList();

        frls=GameObject.FindWithTag("RockFallList").GetComponent<FallRockListScript>(); 
        frls.AwakeFallRockList();
    }

}
