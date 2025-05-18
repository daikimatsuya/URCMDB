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
    public void StartListManagerInGame(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;

        mls=GameObject.FindObjectOfType<MonitorListScript>();
        mls.StartMonitorList();

        surls = GameObject.FindObjectOfType<SpeedUpRingListScript>();
        surls.StartSpeedUpRingList(in pcs);

        frls = GameObject.FindObjectOfType<FallRockListScript>();
        frls.StartFallRockList();

        fls = GameObject.FindObjectOfType<FunListScript>();
        fls.StartFunList();

        flakls = GameObject.FindObjectOfType<FlakListScript>();
        flakls.StartFlakList(in pcs);

        dls = GameObject.FindObjectOfType<DroneListScript>();
        dls.StartDroneList();

        eel=GameObject.FindObjectOfType<ExplodeEffectListScript>();
        eel.StartExplodeEffectList();

        ebls = GameObject.FindObjectOfType<EMPbotListScript>();
        ebls.StartEMPbotList(in pcs);

        suls=GameObject.FindObjectOfType<SeaUrchinListScript>();
        suls.StartSeaUrchinList();

        afls=GameObject.FindObjectOfType<AnglerFishListScript>();
        afls.StartAnglerFishList(in pcs);

        wls=GameObject.FindObjectOfType<WhirlpoolsListScript>();
        wls.StartWhirlpoolsList();

    }

    //早期初期化(タイトル)
    public void AwakeListManagerInTitle()
    {

    }

}
