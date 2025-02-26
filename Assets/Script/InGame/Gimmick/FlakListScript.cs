using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//高角砲管理リスト
public class FlakListScript : MonoBehaviour
{
    private List<FlakScript> flakList;
    private PlayerControllerScript pcs;

    //高角砲一括管理
    public void FlakListController(in bool isPose)
    {
        if (flakList == null)  //オブジェクトがなければリターン/////
        {
            return;
        }//////////////////////////////////////////////////////////////


        for (int i = 0; i < flakList.Count; i++)
        {
            if (!isPose)
            {
                if (flakList[i].GetIsAffective()==false)    //プレイヤーが射程内にいない////
                {
                    flakList[i].SetTime();               //クールタイムリセット
                }/////////////////////////////////////////////////////////////////////////////

                flakList[i].Aim(pcs.GetPlayer()); //プレイヤーを補足
                if (flakList[i].GetTime()) //クールタイム確認///
                {
                    flakList[i].Shot(in pcs);    //射撃
                }/////////////////////////////////////////////////
            }
                flakList[i].BulletController(in isPose);
        }
    }
    public void AwakeFlakList(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;
        flakList = new List<FlakScript>(FindObjectsOfType<FlakScript>());
        for (int i = 0; i < flakList.Count; i++)
        {
            flakList[i].StartFlak(in pcs);
        }
    }
}
