using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRockListScript : MonoBehaviour
{
    private List<RockFallScript> rockFallList = new List<RockFallScript>();

    //岩生成オブジェクト管理
    public void RockFallListController(in bool isPose)
    {
        if (rockFallList == null)
        {
            return;
        }
        for (int i = 0; i < rockFallList.Count; i++)
        {
            rockFallList[i].ListController(in isPose);

            if (!isPose &&rockFallList[i].GetInterval())
            {
                rockFallList[i].SpawnRock();
                rockFallList[i].SetTime();
            }

        }
    }
    public void AwakeFallRockList()
    {
        int i = 0;
        foreach (Transform children in this.gameObject.transform)
        {
            rockFallList.Add(children.GetComponent<RockFallScript>());
            rockFallList[i].StartRockFall();
            i++;
        }
    }
}
