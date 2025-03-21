using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//EMPbotを管理する
public class EMPbotScript : MonoBehaviour
{
    [SerializeField] private float empInterval;
    private int intervalBuff;
    [SerializeField] private float chargeTime;
    [SerializeField] private float explodeTime; 
    [SerializeField] private GameObject EMP;
    [SerializeField] private bool isDeploy;
    [SerializeField] private Vector2 tilling;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float chargeSize;
    [SerializeField] private float explodeSize;
    [SerializeField] private float deploySize;

    private CreateMarkerScript cms;

    private List<EMPScript> empList;

    //EMP発生させるやつ管理
    public void EMPbotController()
    {
        cms.Move(this.transform);       //マーカー追従
        cms.Adjustment();                  //マーカー補正

        if (isDeploy)
        {
            return;
        }
        if(TimeCountScript.TimeCounter(ref intervalBuff))
        {
            CreateEMP();                                                                        //EMP生成
            TimeCountScript.SetTime(ref intervalBuff, empInterval);          //インターバルリセット
        }

    }

    //EMP生成
    public void CreateEMP()
    {
        GameObject _ = Instantiate(EMP);                            //オブジェクト生成
        _.transform.SetParent(this.transform);                      //親子付け
        _.transform.localPosition = Vector3.zero;                  //位置のずれ修正
        _.transform.localEulerAngles = Vector3.zero;            //方向のずれ修正
        EMPScript emp = _.GetComponent<EMPScript>();    //EMPスクリプト取得
        emp.SetSize(chargeSize, explodeSize, deploySize);   //サイズセット
        emp.StartEMP(in isDeploy);                                     //EMPスクリプトを初期化
        empList.Add(emp);                                                 //リストに追加


        if (isDeploy)
        {
            return;
        }
        emp.SetTillingOffset(tilling, offset);             //タイリングとオフセットをセット
        emp.SetChargeTime(chargeTime);              //チャージ時間セット
        emp.SetExplodeTime(explodeTime);           //爆破時間セット
    }

    //EMP管理
    public void EMPController(in PlayerScript ps)
    {
        if (ps == null)
        {
            //プレイヤーがいなかったらEMPを消す
            for (int i = 0; i < empList.Count; i++)
            {
                empList[i].Break();                 //削除
                empList.RemoveAt(i);            //リストから除外
            }
            return;
        }

        //EMPを展開していなかったら展開させる
        if (isDeploy && empList.Count == 0)
        {
            CreateEMP();
        }

        //EMPを動かす
        for (int i = 0; i < empList.Count;)
        {
            if (isDeploy)
            {                
                empList[i].Deploy();    //EMP展開
                i++;
                return;
            }

            //チャージが終わったら爆破させる
            if (empList[i].Charge())
            {
                empList[i].Explode();

            }

            //EMPを消す
            if (empList[i].GetBreakFlag())
            {
                empList[i].Break();         //削除
                empList.RemoveAt(i);     //リストから除外
            }
            else
            {
                i++;
            }


        }
    }

    //初期化
    public void StartEMPbot(in PlayerControllerScript pcs)
    {
        empList = new List<EMPScript>();
        cms=GetComponent<CreateMarkerScript>();
        cms.CreateMarker(this.transform, in pcs);
        cms.SetMarkerSize();

        if (isDeploy)
        {
            return;
        }
        TimeCountScript.SetTime(ref intervalBuff, empInterval);
    }
}
