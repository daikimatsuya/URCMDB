using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ファンをリストで管理する
public class FunListScript : MonoBehaviour
{
    private List<FunScript> funList = new List<FunScript>();

    //ファンをリストで管理
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
    //早期初期化
    public void AwakeFunList()
    {
        int i = 0;
        foreach (Transform children in GameObject.FindWithTag("FunList").transform)
        {
            funList.Add(children.GetChild(0).GetComponent<FunScript>());
            funList[i].StartFun();
            i++;
        }
    }
}
