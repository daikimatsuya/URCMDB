using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakListScript : MonoBehaviour
{
    private List<FlakScript> flackList = new List<FlakScript>();

    public void FlakListController(in bool isPose)
    {
        if (isPose)
        {
            return;
        }
        if (flackList == null)
        {
            return;
        }
        for (int i = 0; i < flackList.Count; i++)
        {
            if (flackList[i].GetPlayerPos() == null)
            {
                flackList[i].SetTime();
                flackList[i].LineUIDelete();
                return;
            }
            flackList[i].Aim();
            if (flackList[i].GetTime())
            {
                flackList[i].Shot();
            }
        }
    }
    public void AwakeFlakList()
    {
        int i = 0;
        foreach (Transform children in this.gameObject.transform)
        {
            flackList.Add(children.GetComponent<FlakScript>());
            flackList[i].StartFlak();
            i++;
        }
    }
}
