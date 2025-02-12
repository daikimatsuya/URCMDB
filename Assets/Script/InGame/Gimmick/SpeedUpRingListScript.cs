using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スピードアップリングをリストで管理
public class SpeedUpRingListScript : MonoBehaviour
{
    private List<SpeedUpRingScript> speedUpRingList ;
    
    //スピードアップリング管理
    public void SpeedUpRingListController(in PlayerScript ps,in bool isPose)
    {
        if(isPose)
        {
            return;
        }
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
        speedUpRingList = new List<SpeedUpRingScript>(FindObjectsOfType<SpeedUpRingScript>());
        for (int i = 0; i < speedUpRingList.Count; i++)
        {
            speedUpRingList[i].StartSpeedUpRing();
        }
    }
}
