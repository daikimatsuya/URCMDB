using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//高角砲管理リスト
public class FlakListScript : MonoBehaviour
{
    private List<FlakScript> flakList;

    //高角砲一括管理
    public void FlakListController(in bool isPose)
    {
        if (flackList == null)  //オブジェクトがなければリターン/////
        {
            return;
        }//////////////////////////////////////////////////////////////


        for (int i = 0; i < flakList.Count; i++)
        {
            if (!isPose)
            {
                if (flackList[i].GetPlayerPos() == null)    //プレイヤーが射程内にいない////
                {
                    flackList[i].SetTime();               //クールタイムリセット
                    flackList[i].LineUIDelete();        //予測線を削除
                    return;
                }/////////////////////////////////////////////////////////////////////////////

                flackList[i].Aim(); //プレイヤーを補足
                if (flackList[i].GetTime()) //クールタイム確認///
                {
                    flackList[i].Shot();    //射撃
                }/////////////////////////////////////////////////
            }
            flackList[i].BulletController(in isPose);
        }
    }
    public void AwakeFlakList()
    {
        flakList = new List<FlakScript>(FindObjectsOfType<FlakScript>());
        for (int i = 0; i < flakList.Count; i++)
        {
            flakList[i].StartFlak();
        }
    }
}
