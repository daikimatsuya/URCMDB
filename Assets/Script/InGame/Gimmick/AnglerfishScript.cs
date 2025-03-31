using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A���R�E�Ǘ�
public class AnglerfishScript : MonoBehaviour
{
    [SerializeField] private LineRenderer rail;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private ChaseControllerScript ccs;
    private MoveOnRailScript mors;
    Rigidbody rb;
    Transform tf;

    private PlayerControllerScript pcs;
    [SerializeField] private bool isChase;

    public void AnglerfishController()
    {
        ChasePlayer();
        Patrol();
    }

    //�v���C���[��ǂ�
    private void ChasePlayer()
    {
        if (!isChase)
        {
            return;
        }
        ccs.Chase(pcs.GetPlayer().GetPlayerPos());
       
    }

    //����
    private void Patrol()
    {
        if (isChase)
        {
            return;
        }
        if(mors.GetRail() == null)
        {
            mors.SetRail(rail);
        }
        mors.Move();
    }

    //������
    public void StartAnglerfish(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();

        ccs=GetComponent<ChaseControllerScript>();
        mors=GetComponent<MoveOnRailScript>();

        ccs.StartChaseController(rb,tf,moveSpeed);
        mors.StartMoveOnRail(rb,tf,moveSpeed);
    }
}
