using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneListScript : MonoBehaviour
{
    private List<DroneScript> droneList=new List<DroneScript>();

    public void DroneListController(in bool isPose)
    {
        if (isPose)
        {
            return;
        }
        if (droneList == null)
        {
            return;
        }
        for(int i = 0; i < droneList.Count; i++)
        {
            droneList[i].Roll();
        }
    }

    public void AwakeDroneList()
    {
        int i = 0;
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
        foreach (GameObject drone in drones)
        {
            droneList.Add(drone.GetComponent<DroneScript>());
            Debug.Log(drone.name);
            droneList[i].StartDrone();
            i++;
        }
    }
}
