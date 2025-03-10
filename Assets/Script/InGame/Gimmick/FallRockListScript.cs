using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRockListScript : MonoBehaviour
{
    private List<RockFallScript> rockFallList;

    //�␶���I�u�W�F�N�g�Ǘ�
    public void RockFallListController(in bool isPause)
    {

        for (int i = 0; i < rockFallList.Count; i++)
        {
            rockFallList[i].RockController(in isPause);

            if (!isPause &&rockFallList[i].GetInterval())
            {
                rockFallList[i].SpawnRock();
                rockFallList[i].SetTime();
            }

        }
    }
    public void AwakeFallRockList()
    {
        rockFallList = new List<RockFallScript>(FindObjectsOfType<RockFallScript>());
        for (int i = 0; i < rockFallList.Count; i++)
        {
            rockFallList[i].StartRockFall();
        }
    }
}
