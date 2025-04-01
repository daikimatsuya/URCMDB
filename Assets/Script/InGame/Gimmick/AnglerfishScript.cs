using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

//アンコウ管理
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

    //管理
    public void AnglerfishController()
    {
        Discover();
        ChasePlayer();
        Patrol();
    }

    //プレイヤーを追う
    private void ChasePlayer()
    {
        if (!isChase)
        {
            return;
        }
        ccs.Chase(pcs.GetPlayer().GetPlayerPos());
       
    }

    //巡回
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

    //プレイヤー追従切り替え
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

        float horizontal = Mathf.Atan2(playerDis.normalized.x, playerDis.normalized.z) * Mathf.Rad2Deg;                                                                                                          //水平方向角度算出
        float vertical = Mathf.Atan2(Mathf.Sqrt(playerDis.normalized.x * playerDis.normalized.x + playerDis.normalized.z * playerDis.normalized.z), playerDis.normalized.y) * Mathf.Rad2Deg;    //垂直方向角度算出

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

        //プレイヤーとの間に遮蔽物があるかを確認
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

    //初期化
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
