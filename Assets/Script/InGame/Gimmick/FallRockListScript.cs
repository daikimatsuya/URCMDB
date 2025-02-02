using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRockListScript : MonoBehaviour
{
    private List<RockFallScript> rockFallList = new List<RockFallScript>();

    //�␶���I�u�W�F�N�g�Ǘ�
    public void RockFallListController()
    {
        if (rockFallList == null)
        {
            return;
        }
        for (int i = 0; i < rockFallList.Count; i++)
        {
            if (rockFallList[i].GetInterval())
            {
                rockFallList[i].SpawnRock();
                rockFallList[i].SetTime();
            }
            rockFallList[i].ListController();   
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
