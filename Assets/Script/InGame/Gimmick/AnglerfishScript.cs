using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

//�A���R�E�Ǘ�
public class AnglerfishScript : MonoBehaviour
{
    [SerializeField] private LineRenderer rail;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float viewingAngle;
    [SerializeField] private float viewingLenge;
    [SerializeField] string[] hitTags;


    private ChaseControllerScript ccs;
    private MoveOnRailScript mors;
    Rigidbody rb;
    Transform tf;

    private PlayerControllerScript pcs;
    [SerializeField] private bool isChase;

    //�Ǘ�
    public void AnglerfishController()
    {
        Discover();
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

    //�v���C���[�Ǐ]�؂�ւ�
    private void Discover()
    {
        if (pcs.GetPlayer() == null)
        {
            isChase = false;
            return;
        }

        Vector3 playerDis = pcs.GetPlayer().GetPlayerPos() - tf.position;

        float lengeBuff = viewingLenge * viewingLenge;
        if (playerDis.sqrMagnitude > lengeBuff)
        {
            isChase = false;
            return;
        }

        float horizontal = Mathf.Atan2(playerDis.normalized.x, playerDis.normalized.z) * Mathf.Rad2Deg;                                                                                                          //���������p�x�Z�o
        float vertical = Mathf.Atan2(Mathf.Sqrt(playerDis.normalized.x * playerDis.normalized.x + playerDis.normalized.z * playerDis.normalized.z), playerDis.normalized.y) * Mathf.Rad2Deg;    //���������p�x�Z�o

        horizontal = horizontal - (tf.eulerAngles.y)-90;
        vertical = vertical - tf.eulerAngles.z-90;

        if (vertical > 180)
        {
            vertical = -360 + vertical;
        }
        if (vertical < -180)
        {
            vertical = 360 + vertical;
        }
        if (horizontal > 180)
        {
            horizontal = -360 + horizontal;
        }
        if (horizontal < -180)
        {
            horizontal = 360 + horizontal;
        }

        horizontal = horizontal * horizontal;
        vertical = vertical * vertical;

        float viewBuff=viewingAngle*viewingAngle;

        if (horizontal > viewBuff || vertical> viewBuff)
        {

            return;
        }

        //�v���C���[�Ƃ̊ԂɎՕ��������邩���m�F
        RaycastHit[] hits = Physics.RaycastAll(tf.position, Vector3.Normalize(playerDis), Vector3.Magnitude(playerDis));

        bool shade = false;
        for (int hitNum = 0; hitNum < hits.Length; hitNum++)
        {
            for (int tagNum = 0; tagNum < hitTags.Length; tagNum++)
            {
                if (hits[hitNum].collider.CompareTag(hitTags[tagNum]))
                {
                    shade = true;
                    return;
                }
            }
        }
        if (shade)
        {
            return;
        }

        isChase = true;
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

        isChase = false;
    }
}
