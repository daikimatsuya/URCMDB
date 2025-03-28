using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnRailListScript : MonoBehaviour
{
    private List<MoveOnRailScript> moveOnRailList;

    //レールに沿うオブジェクト管理
    public void MoveOnRailListController(in bool isPause)
    {
        if (isPause)
        {
            return;
        }
        for (int i = 0; i < moveOnRailList.Count; i++)
        {
            moveOnRailList[i].Move();
        }
    }

    //初期化
    public void StartMoveOnRailList()
    {
        moveOnRailList = new List<MoveOnRailScript>(FindObjectsOfType<MoveOnRailScript>());

        for (int i = 0; i < moveOnRailList.Count; i++) 
        {
            moveOnRailList[i].StartMoveOnRail();
        }
    }
}
