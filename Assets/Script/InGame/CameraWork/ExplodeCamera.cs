using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

public class ExplodeCamera : MonoBehaviour
{
    [SerializeField] private float directionX;
    [SerializeField] private float distance;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 hitCameraPos;
    [SerializeField] private Vector3 hitCameraRot;
    [SerializeField] private Vector3 clearCameraPos;
    [SerializeField] private Vector3 clearCameraRot;

    private Vector3 pos;
    //プレイヤーが壁とかで爆発したときのカメラの動き
    public Vector3 MissExplodeCamera(ref Vector3 rotation,in Vector3 playerPos)
    {
        Rotation(rotation,in playerPos);                                                      //回転させる
        rotation = new Vector3(directionX, rotation.y + rotateSpeed, 0);      //必要な値だけ代入
        return pos;
    }
    //ターゲットにぶつかって爆発した時のカメラ挙動
    public Vector3 HitTargetCamera(ref Vector3 rotation)
    {
        rotation = hitCameraRot;    //角度に決めた値代入
        return hitCameraPos;
    }
    //クリア時のカメラ
    public Vector3 ClearCamera(ref Vector3 rotation)
    {
        rotation = clearCameraRot;    //角度に決めた値代入
        return clearCameraPos;
    }

    //爆発時のカメラを回す
    private void Rotation(Vector3 rotation,in Vector3 playerPos)
    {
        //平面で値を出す
        pos.x = -distance * (float)Math.Sin(ToRadianScript.ToRadian(ref rotation.y));
        pos.z = -distance * (float)Math.Cos(ToRadianScript.ToRadian(ref rotation.y));

        //水平方向と垂直方向で値を出す
        pos.x = pos.x * (float)Math.Cos(ToRadianScript.ToRadian(ref rotation.x));
        pos.z = pos.z * (float)Math.Cos(ToRadianScript.ToRadian(ref rotation.x));

        pos.y = -distance * (float)Math.Sin(ToRadianScript.ToRadian(ref rotation.x)) * -1;

        pos += playerPos;
    }

}
