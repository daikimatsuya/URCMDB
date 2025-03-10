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
    private ExplodeEffectListScript eel;
    private PlayerControllerScript pcs;
    private EMPbotListScript ebls;

    //���X�g�X�V
    public void ListManagerController(in bool isPose)
    {
        mls.MonitorListController(in pcs,in isPose);              //�Q�[�������j�^�[�Ǘ�
        surls.SpeedUpRingListController(in pcs, in isPose);   //�X�s�[�h�A�b�v�����O�Ǘ�
        frls.RockFallListController(in isPose);                      //�◎�Ƃ��z�Ǘ�
        fls.FunListController(in isPose);                             //�t�@���Ǘ�
        flakls.FlakListController(in isPose);                        //���p�C�Ǘ�
        dls.DroneListController();                                     //�h���[���Ǘ�
        eel.ExplodeEffectListController(in pcs);                  //�����G�t�F�N�g�Ǘ�
    }
    //����������
    public void AwakeListManager(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;

        mls=GameObject.FindObjectOfType<MonitorListScript>();
        mls.AwakeMonitorList();

        surls = GameObject.FindObjectOfType<SpeedUpRingListScript>();
        surls.AwakeSpeedUpRingList(in pcs);

        frls = GameObject.FindObjectOfType<FallRockListScript>();
        frls.AwakeFallRockList();

        fls = GameObject.FindObjectOfType<FunListScript>();
        fls.AwakeFunList();

        flakls = GameObject.FindObjectOfType<FlakListScript>();
        flakls.AwakeFlakList(in pcs);

        dls = GameObject.FindObjectOfType<DroneListScript>();
        dls.AwakeDroneList();

        eel=GameObject.FindObjectOfType<ExplodeEffectListScript>();
        eel.AwakeExplodeEffectList();

        ebls = GameObject.FindObjectOfType<EMPbotListScript>();
        ebls.AwakeEMPbotList();
    }

}
