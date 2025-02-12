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
        dls.DroneListController();                                     //ドローン管理
    }
    //早期初期化
    public void AwakeListManager()
    {
        mls=GameObject.FindObjectOfType<MonitorListScript>();
        mls.AwakeMonitorList();

        surls = GameObject.FindObjectOfType<SpeedUpRingListScript>();
        surls.AwakeSpeedUpRingList();

        frls = GameObject.FindObjectOfType<FallRockListScript>();
        frls.AwakeFallRockList();

        fls = GameObject.FindObjectOfType<FunListScript>();
        fls.AwakeFunList();

        flakls = GameObject.FindObjectOfType<FlakListScript>();
        flakls.AwakeFlakList();

        dls = GameObject.FindObjectOfType<DroneListScript>();
        dls.AwakeDroneList();
    }

}
