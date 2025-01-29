using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpRingListScript : MonoBehaviour
{
    private List<SpeedUpRingScript> speedUpRingList = new List<SpeedUpRingScript>();
    
    //スピードアップリング管理
    public void SpeedUpRingListController(in PlayerScript ps)
    {
        if (speedUpRingList == null)
        {
            return;
        }
        for (int i = 0; i < speedUpRingList.Count; i++)
        {
            speedUpRingList[i].Off();   //触れたらオフにする
        }
        if (ps != null)
        {
            return;
        }
        for (int i = 0; i < speedUpRingList.Count; i++)
        {
            speedUpRingList[i].ON();    //リセット
        }
    }

    //早期初期化
    public void AwakeSpeedUpRingList()
    {
        int i = 0;
        foreach (Transform children in this.gameObject.transform)
        {
            speedUpRingList.Add(children.GetComponent<SpeedUpRingScript>());
            speedUpRingList[i].StartSpeedUpRing();
            i++;
        }
    }
}
