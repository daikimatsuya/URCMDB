using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ドローンをリストで管理する
public class DroneListScript : MonoBehaviour
{
    private List<DroneScript> droneList;

    //ドローンをリストで管理
    public void DroneListController()
    {
        if (droneList == null)
        {
            return;
        }
        for(int i = 0; i < droneList.Count; i++)
        {
            droneList[i].Roll();    //プロペラ回転
        }
    }

    //早期初期化
    public void StartDroneList()
    {
        droneList = new List<DroneScript>(FindObjectsOfType<DroneScript>());
        for (int i = 0; i < droneList.Count; i++)
        {
            droneList[i].StartDrone();
        }
    }
}
