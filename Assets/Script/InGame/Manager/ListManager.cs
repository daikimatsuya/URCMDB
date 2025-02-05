using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManager : MonoBehaviour
{
    private MonitorListScript mls;
    private SpeedUpRingListScript surls;
    private FallRockListScript frls;
    private FunListScript fls;
    private FlakListScript flakls;

    //リスト更新
    public void ListManagerController(in PlayerScript ps,in bool isPose)
    {
        mls.MonitorListController(in ps,in isPose);   //ゲーム内モニター管理
        surls.SpeedUpRingListController(in ps,in isPose); //スピードアップリング管理
        frls.RockFallListController(in isPose);  //岩落とす奴管理
        fls.FunListController(in isPose);    //ファン管理
        flakls.FlakListController(in isPose);    //高角砲管理
    }
    //早期初期化
    public void AwakeListManager()
    {
        mls = GameObject.FindWithTag("MonitorList").GetComponent<MonitorListScript>();
        mls.AwakeMonitorList();

        surls = GameObject.FindWithTag("SpeedUpRingList").GetComponent<SpeedUpRingListScript>();
        surls.AwakeSpeedUpRingList();

        frls=GameObject.FindWithTag("RockFallList").GetComponent<FallRockListScript>(); 
        frls.AwakeFallRockList();

        fls=GameObject.FindWithTag("FunList").GetComponent <FunListScript>();
        fls.AwakeFunList();

        flakls=GameObject.FindWithTag("FlakList").GetComponent<FlakListScript>();   
        flakls.AwakeFlakList();
    }

}
