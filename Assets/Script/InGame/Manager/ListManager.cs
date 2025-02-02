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
    public void ListManagerController(in PlayerScript ps)
    {
        mls.MonitorListController(in ps);   //ゲーム内モニター管理
        surls.SpeedUpRingListController(in ps); //スピードアップリング管理
        frls.RockFallListController();  //岩落とす奴管理
        fls.FunListController();    //ファン管理
        flakls.FlakListController();    //高角砲管理
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
