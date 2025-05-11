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
    private SeaUrchinListScript suls;
    private AnglerFishListScript afls;
    private WhirlpoolsListScript wls;

    //�C���Q�[���̃��X�g�X�V
    public void ListManagerControllerInGame(in bool isPause)
    {
        mls.MonitorListController(in pcs,in isPause);            //�Q�[�������j�^�[�Ǘ�
        surls.SpeedUpRingListController(in pcs, in isPause); //�X�s�[�h�A�b�v�����O�Ǘ�
        frls.RockFallListController(in isPause);                     //�◎�Ƃ��z�Ǘ�
        fls.FunListController(in isPause);                             //�t�@���Ǘ�
        flakls.FlakListController(in isPause);                        //���p�C�Ǘ�
        dls.DroneListController();                                      //�h���[���Ǘ�
        eel.ExplodeEffectListController(in pcs);                   //�����G�t�F�N�g�Ǘ�
        ebls.EMPbotListController(in isPause, in pcs);          //EMPbot�Ǘ�
        suls.SeaUrchinListController(in isPause);                //���ɊǗ�
        afls.AnglerFishListController(in isPause);                //�A���R�E�Ǘ�
        wls.WhirlpoolsListController(in isPause);               //�Q���Ǘ�
    }

    //�^�C�g���̃��X�g�X�V
    public void ListManagerControllerInTitle()
    {

    }


    //����������(�C���Q�[��)
    public void AwakeListManagerInGame(in PlayerControllerScript pcs)
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
        ebls.AwakeEMPbotList(in pcs);

        suls=GameObject.FindObjectOfType<SeaUrchinListScript>();
        suls.AwakeSeaUrchinList();

        afls=GameObject.FindObjectOfType<AnglerFishListScript>();
        afls.AwakeAnglerFishList(in pcs);

        wls=GameObject.FindObjectOfType<WhirlpoolsListScript>();
        wls.AwakeWhirlpoolsList();

    }

    //����������(�^�C�g��)
    public void AwakeListManagerInTitle()
    {

    }

}
