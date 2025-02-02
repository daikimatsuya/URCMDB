using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunListScript : MonoBehaviour
{
    private List<FunScript> funList = new List<FunScript>();

    public void FunListController()
    {
        if (funList == null)
        {
            return;
        }
        for (int i = 0; i < funList.Count; i++)
        {
            funList[i].RotateFun();   //G‚ê‚½‚çƒIƒt‚É‚·‚é
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
