using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunListScript : MonoBehaviour
{
    private List<FunScript> funList = new List<FunScript>();

    public void FunListController(in bool isPose)
    {
        if (isPose)
        {
            return;
        }
        if (funList == null)
        {
            return;
        }
        for (int i = 0; i < funList.Count; i++)
        {
            funList[i].RotateFun(); 
        }
    }
    public void AwakeFunList()
    {
        int i = 0;
        foreach (Transform children in this.gameObject.transform)
        {
            funList.Add(children.GetChild(0).GetComponent<FunScript>());
            funList[i].StartFun();
            i++;
        }
    }
}
