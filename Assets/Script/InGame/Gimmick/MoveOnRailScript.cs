using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Usefull;

//オブジェクトがライン上を動くようにする
public class MoveOnRailScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float distansMagnification;
    [SerializeField] bool obtainVoluntarilyRail;
    [SerializeField] float rotSpeed;
    [SerializeField] string railName;

    private LineRenderer rail;
    private bool moveEnd;
    private int knot;
    private int next;
    private Vector3 targetAngles;
    private Vector3 rotBuff;

    Rigidbody rb;
    Transform tf;
    ComplementingRotationScript crs;

    //動かす
    private void Move()
    {
        if (rail == null)   //レールに乗ってなかったらreturnを返す/////
        {
            return;
        }////////////////////////////////////////////////////////////////

        //レールに次の中継地があるかを確認
        next = knot + 1;
        if (rail.positionCount <= next)
        {
            moveEnd = true;
            next = 0;
            rail = null;
        }
        ////////////////////////////////////
        
        if (!moveEnd)
        {
            //中継地点にたどり着いたら次の中継地点を目指す
            if (moveSpeed > Vector3.Distance(rail.GetPosition(next), tf.position) * distansMagnification)
            {
                SetPosAndRot();
                knot++;
            }
            else
            {
                SetPosAndRot();
            }
            //////////////////////////////////////////////////
        }      
    }
    //値代入する
    private void SetPosAndRot()
    {
        Vector3 targetPos = SetTargetPos(next); //目標地点座標設定
        Rolling();  //回転させる
        Accelerate(targetPos);  //加速
    }
    //加速
    private void Accelerate(Vector3 targetPos)
    {
        //回転角を保存
        Vector3 anglesBuff = tf.eulerAngles;
        anglesBuff.y += 90;
        anglesBuff.z *= -1;
        ///////////////
        
        //角度から各方向への速度を算出///////////////////////////////////////////////////////////////////
        Vector3 velocity;

        //平面方向の速度を算出
        velocity.x = moveSpeed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.y));
        velocity.z = moveSpeed * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.y));
        ///////////////////////

        //水平方向と垂直方向の速度を算出
        velocity.x = velocity.x * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.z));
        velocity.z = velocity.z * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.z));
        velocity.y = moveSpeed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.z)) * -1;
        //////////////////////////////////
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////

        rb.velocity = velocity; //リジットボディに代入
    }
    //目標地点設定
    private Vector3 SetTargetPos(int next)
    {
        Vector3 targetPos = rail.GetPosition(next) - tf.position;   //中継地点と現在の差を算出

        //角度を算出
        float horizontal = Mathf.Atan2(targetPos.normalized.x, targetPos.normalized.z) * Mathf.Rad2Deg;
        float vertical = Mathf.Atan2(Mathf.Sqrt(targetPos.normalized.x * targetPos.normalized.x + targetPos.normalized.z * targetPos.normalized.z), targetPos.normalized.y) * Mathf.Rad2Deg;
        /////////////

        targetAngles = new Vector3(tf.eulerAngles.x , horizontal - 90 , -(vertical) + 90);  //角度を代入

        return targetPos;   //座標の差を返す
    }
    //回転させる
    private void Rolling()
    {
        //目標角度まで回転させる
        rotBuff.x += crs.Rotate(rotSpeed, targetAngles.x, rotBuff.x);
        rotBuff.y += crs.Rotate(rotSpeed, targetAngles.y, rotBuff.y);
        rotBuff.z += crs.Rotate(rotSpeed, targetAngles.z, rotBuff.z);
        /////////////////////////

        tf.eulerAngles = rotBuff;   //角度を代入
    }

    //ラインセット
    public void SetRail(in LineRenderer rail)
    {
        this.rail = rail;   //レールセット
        knot = 0;   //中継地点数初期化
        moveEnd = false;    //移動フラグ初期化
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!obtainVoluntarilyRail)
        {
            return;
        }
        if (other.CompareTag(railName))
        {
            rail = null;
            rail=other.gameObject.GetComponent<LineRenderer>();
            SetRail(rail);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        crs=GetComponent<ComplementingRotationScript>();

        rb.velocity=new Vector3(moveSpeed,0,0);

        moveEnd = false;
        rotBuff = tf.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
