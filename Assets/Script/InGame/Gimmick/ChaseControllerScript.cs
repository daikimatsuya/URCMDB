using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Usefull;

//秋水管理
public class ChaseControllerScript : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    private Vector3 playerPos;
    private Vector3 playerDis;
    private Vector3 Row;
    private bool isLeftBase;
    private float moveSpeed;
    private Rigidbody rb;
    private Transform tf;

    //プレイヤーのいる方向を取得
    public void Chase(in Vector3 chaseTargetPos)
    {
        playerDis = chaseTargetPos - this.gameObject.transform.position;       //距離を算出

        float horizontal = Mathf.Atan2(playerDis.normalized.x, playerDis.normalized.z) * Mathf.Rad2Deg;   
        float vertical = Mathf.Atan2(Mathf.Sqrt(playerDis.normalized.x * playerDis.normalized.x + playerDis.normalized.z * playerDis.normalized.z), playerDis.normalized.y) * Mathf.Rad2Deg; 

        Rowring(horizontal, vertical);                                                                                     //回転させる
        this.gameObject.transform.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);        //角度代入

        Accelerate(new Vector3(tf.eulerAngles.x, horizontal - 90, -(vertical) + 90));
    }

    //加速
    private void Accelerate(Vector3 targetPos)
    {
        //回転角を保存
        Vector3 anglesBuff = tf.eulerAngles;
        anglesBuff.y += 90;
        anglesBuff.z *= -1;


        //角度から各方向への速度を算出
        Vector3 velocity;

        //平面方向の速度を算出
        velocity.x = moveSpeed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.y));
        velocity.z = moveSpeed * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.y));

        //水平方向と垂直方向の速度を算出
        velocity.x = velocity.x * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.z));
        velocity.z = velocity.z * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.z));
        velocity.y = moveSpeed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.z)) * -1;

        rb.velocity = velocity; //リジットボディに代入
    }

    //回転
    private void Rowring(float horizontal,float vertical)
    {
        Row.x += ComplementingRotationScript.Rotate(rotateSpeed, 0, Row.x);
        Row.y += ComplementingRotationScript.Rotate(rotateSpeed, horizontal-90, Row.y);
        Row.z += ComplementingRotationScript.Rotate(rotateSpeed, -(vertical)+90, Row.z);
    }

    #region 値受け渡し
    public void SetRot(Vector3 rot)
    {
        Row = rot;
    }

    #endregion

    //初期化
    public void StartChaseController(Rigidbody rb,Transform tf,float moveSpeed)
    {
        this.rb = rb;
        this.tf = tf;
        this.moveSpeed = moveSpeed;
    }

}
