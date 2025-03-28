using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class SeaUrchinScript : MonoBehaviour
{
    private MoveOnRailScript mors;
    public void Move()
    {
        mors.Move();
    }
    public void StartSeaUrchin()
    {
        mors = GetComponent<MoveOnRailScript>();
        mors.StartMoveOnRail();
    }
}
