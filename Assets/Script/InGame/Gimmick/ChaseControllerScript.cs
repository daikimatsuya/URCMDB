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

    //プレイヤーのいる方向を取得
    public void Chase(in Vector3 chaseTargetPos)
    {
        playerDis = chaseTargetPos - this.gameObject.transform.position;       //距離を算出

        float horizontal = Mathf.Atan2(playerDis.normalized.x, playerDis.normalized.z) * Mathf.Rad2Deg;   
        float vertical = Mathf.Atan2(Mathf.Sqrt(playerDis.normalized.x * playerDis.normalized.x + playerDis.normalized.z * playerDis.normalized.z), playerDis.normalized.y) * Mathf.Rad2Deg; 

        vertical -= 90;                                                                                                          //角度分ずらす
        Rowring(horizontal, vertical);                                                                                     //回転させる
        this.gameObject.transform.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);        //角度代入
    }
   
   
    //回転
    private void Rowring(float horizontal,float vertical)
    {
        Row.x += ComplementingRotationScript.Rotate(rotateSpeed, 0, Row.x);
        Row.y += ComplementingRotationScript.Rotate(rotateSpeed, horizontal, Row.y);
        Row.z += ComplementingRotationScript.Rotate(rotateSpeed, vertical, Row.z);
    }

    #region 値受け渡し


    #endregion

    //初期化
    public void StartChaseController()
    {

    }

}
