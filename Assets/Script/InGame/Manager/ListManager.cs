using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�M�~�b�N�Ǘ����X�g�Ǘ�
public class ListManager
{
    private MonitorListScript mls;
    private SpeedUpRingListScript surls;
    private FallRockListScript frls;
    private FunListScript fls;
    private FlakListScript flakls;
    private DroneListScript dls;

    //���X�g�X�V
    public void ListManagerController(in PlayerScript ps,in bool isPose)
    {
        mls.MonitorListController(in ps,in isPose);              //�Q�[�������j�^�[�Ǘ�
        surls.SpeedUpRingListController(in ps, in isPose);   //�X�s�[�h�A�b�v�����O�Ǘ�
        frls.RockFallListController(in isPose);                      //�◎�Ƃ��z�Ǘ�
        fls.FunListController(in isPose);                             //�t�@���Ǘ�
        flakls.FlakListController(in isPose);                        //���p�C�Ǘ�
        dls.DroneListController(in isPose);                         //�h���[���Ǘ�
    }
    //����������
    public void AwakeListManager()
    {
        mls=new MonitorListScript();
        mls.AwakeMonitorList();

        surls =new SpeedUpRingListScript();
        surls.AwakeSpeedUpRingList();

        frls = new FallRockListScript();
        frls.AwakeFallRockList();

        fls = new FunListScript();
        fls.AwakeFunList();

        flakls = new FlakListScript();
        flakls.AwakeFlakList();

        dls = new DroneListScript();
        dls.AwakeDroneList();
    }

}
