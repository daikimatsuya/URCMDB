using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//岩落とすやつをリストで管理
public class FallRockListScript : MonoBehaviour
{
    private List<RockFallScript> rockFallList;

    //岩生成オブジェクト管理
    public void RockFallListController(in bool isPause)
    {

        for (int i = 0; i < rockFallList.Count; i++)
        {
            rockFallList[i].RockController(in isPause); //落とした岩を管理

            if (!isPause &&rockFallList[i].GetInterval())
            {
                rockFallList[i].SpawnRock();    //岩を生成
                rockFallList[i].SetTime();         //クールタイム設定
            }

        }
    }
    //早期初期化
    public void AwakeFallRockList()
    {
        rockFallList = new List<RockFallScript>(FindObjectsOfType<RockFallScript>());
        for (int i = 0; i < rockFallList.Count; i++)
        {
            rockFallList[i].StartRockFall();
        }
    }
}
