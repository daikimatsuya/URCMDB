using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A���R�E�Ǘ�
public class AnglerfishScript : MonoBehaviour
{
    [SerializeField] private LineRenderer rail;

    private ChaseControllerScript ccs;
    private MoveOnRailScript mors;
    Rigidbody rb;

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

        ccs=GetComponent<ChaseControllerScript>();
        mors=GetComponent<MoveOnRailScript>();

        ccs.StartChaseController();
        mors.StartMoveOnRail(rb);
    }
}
