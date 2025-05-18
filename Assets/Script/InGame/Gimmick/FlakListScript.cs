using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//高角砲管理リスト
public class FlakListScript : MonoBehaviour
{
    private List<FlakScript> flakList;
    private PlayerControllerScript pcs;

    //高角砲一括管理
    public void FlakListController(in bool isPause)
    {
        if (flakList == null)  
        {
            return; //オブジェクトがなければリターン
        }

        for (int i = 0; i < flakList.Count; i++)
        {
            if (!isPause)
            {
                if (flakList[i].GetIsAffective()==false)    
                {
                    flakList[i].SetTime();     //プレイヤーが射程内にいなければクールタイムリセット
                }

                flakList[i].Aim(pcs.GetPlayer()); //プレイヤーを補足

                if (flakList[i].GetTime()) 
                {
                    flakList[i].Shot(in pcs);    //クールタイムが終わってたら撃つ
                }
            }
                flakList[i].BulletController(in isPause);   //弾を管理
        }
    }

    //早期初期化
    public void StartFlakList(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;
        flakList = new List<FlakScript>(FindObjectsOfType<FlakScript>());
        for (int i = 0; i < flakList.Count; i++)
        {
            flakList[i].StartFlak(in pcs);
        }
    }
}
