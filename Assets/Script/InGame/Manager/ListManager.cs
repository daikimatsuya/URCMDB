using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ギミック管理リスト管理
public class ListManager
{
    private MonitorListScript mls;
    private SpeedUpRingListScript surls;
    private FallRockListScript frls;
    private FunListScript fls;
    private FlakListScript flakls;
    private DroneListScript dls;

    //リスト更新
    public void ListManagerController(in PlayerScript ps,in bool isPose)
    {
        mls.MonitorListController(in ps,in isPose);              //ゲーム内モニター管理
        surls.SpeedUpRingListController(in ps, in isPose);   //スピードアップリング管理
        frls.RockFallListController(in isPose);                      //岩落とす奴管理
        fls.FunListController(in isPose);                             //ファン管理
        flakls.FlakListController(in isPose);                        //高角砲管理
        dls.DroneListController(in isPose);                         //ドローン管理
    }
    //早期初期化
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
