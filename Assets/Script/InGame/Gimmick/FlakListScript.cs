using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//高角砲管理リスト
public class FlakListScript : MonoBehaviour
{
    private List<FlakScript> flackList = new List<FlakScript>();

    //高角砲一括管理
    public void FlakListController(in bool isPose)
    {
        if (flackList == null)  //オブジェクトがなければリターン/////
        {
            return;
        }//////////////////////////////////////////////////////////////

        for (int i = 0; i < flackList.Count; i++)
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
        int i = 0;
        foreach (Transform children in GameObject.FindWithTag("FlakList").transform)   //対応ギミックがあれば取得/////
        {
            flackList.Add(children.GetComponent<FlakScript>());     //リストに追加
            flackList[i].StartFlak();                                                 //初期化
            i++;
        }/////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
