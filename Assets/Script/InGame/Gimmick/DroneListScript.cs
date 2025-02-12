using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�h���[�������X�g�ŊǗ�����
public class DroneListScript : MonoBehaviour
{
    private List<DroneScript> droneList;

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
        droneList = new List<DroneScript>(FindObjectsOfType<DroneScript>());
        for (int i = 0; i < droneList.Count; i++)
        {
            droneList[i].StartDrone();
        }
    }
}
