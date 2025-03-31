using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class SeaUrchinScript : MonoBehaviour
{
    private MoveOnRailScript mors;

    Rigidbody rb;

    [SerializeField] private LineRenderer rail;

    //ˆÚ“®
    public void Move()
    {
        if (mors.GetRail()==null)
        {
            mors.SetRail(rail);
        }
        mors.Move();
    }
    public void SetStop()
    {
        rb.velocity = Vector3.zero;
    }
    public void StartSeaUrchin()
    {
        rb = GetComponent<Rigidbody>();

        mors = GetComponent<MoveOnRailScript>();
        mors.StartMoveOnRail(rb);
    }
}
