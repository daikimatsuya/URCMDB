using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�h���[�������X�g�ŊǗ�����
public class DroneListScript : MonoBehaviour
{
    private List<DroneScript> droneList=new List<DroneScript>();

    //�h���[�������X�g�ŊǗ�
    public void DroneListController()
    {

        if (droneList == null)
        {
            return;
        }
        for(int i = 0; i < droneList.Count; i++)
        {
            droneList[i].Roll();
        }
    }

    //����������
    public void AwakeDroneList()
    {
        int i = 0;
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
        foreach (GameObject drone in drones)
        {
            droneList.Add(drone.GetComponent<DroneScript>());
            droneList[i].StartDrone();
            i++;
        }
    }
}
