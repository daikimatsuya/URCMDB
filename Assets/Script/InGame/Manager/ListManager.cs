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
    private ExplodeEffectListScript eel;
    private PlayerControllerScript pcs;
    private EMPbotListScript ebls;
    private SeaUrchinListScript suls;
    private AnglerFishListScript afls;
    private WhirlpoolsListScript wls;

    //インゲームのリスト更新
    public void ListManagerControllerInGame(in bool isPause)
    {
        mls.MonitorListController(in pcs,in isPause);            //ゲーム内モニター管理
        surls.SpeedUpRingListController(in pcs, in isPause); //スピードアップリング管理
        frls.RockFallListController(in isPause);                     //岩落とす奴管理
        fls.FunListController(in isPause);                             //ファン管理
        flakls.FlakListController(in isPause);                        //高角砲管理
        dls.DroneListController();                                      //ドローン管理
        eel.ExplodeEffectListController(in pcs);                   //爆発エフェクト管理
        ebls.EMPbotListController(in isPause, in pcs);          //EMPbot管理
        suls.SeaUrchinListController(in isPause);                //うに管理
        afls.AnglerFishListController(in isPause);                //アンコウ管理
        wls.WhirlpoolsListController(in isPause);               //渦潮管理
    }

    //タイトルのリスト更新
    public void ListManagerControllerInTitle()
    {

    }


    //早期初期化(インゲーム)
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

    //早期初期化(タイトル)
    public void AwakeListManagerInTitle()
    {

    }

}
