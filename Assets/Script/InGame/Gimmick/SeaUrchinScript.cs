using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class SeaUrchinScript : MonoBehaviour
{
    private MoveOnRailScript mors;

    Rigidbody rb;
    Transform tf;

    [SerializeField] private LineRenderer rail;
    [SerializeField] private float moveSpeed;

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
        tf = GetComponent<Transform>();

        mors = GetComponent<MoveOnRailScript>();
        mors.StartMoveOnRail(rb,tf,moveSpeed);
    }
}
