using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ファンをリストで管理する
public class FunListScript : MonoBehaviour
{
    private List<FunScript> funList;

    //ファンをリストで管理
    public void FunListController(in bool isPause)
    {
        if (isPause)
        {
            return;
        }
        for (int i = 0; i < funList.Count; i++)
        {
            funList[i].RotateFun();     //ファンを回す
        }
    }
    //早期初期化
    public void StartFunList()
    {
        funList = new List<FunScript>(FindObjectsOfType<FunScript>());
        for (int i = 0; i < funList.Count; i++)
        {
            funList[i].StartFun();
        }
    }
}
