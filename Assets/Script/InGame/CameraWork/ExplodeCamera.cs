using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCamera : MonoBehaviour
{
    [SerializeField] private Vector3 centerPos;
    [SerializeField] private float directionX;
    [SerializeField] private float distance;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private Vector3 hitCameraPos;
    [SerializeField] private Vector3 hitCameraRot;

    [SerializeField] private Vector3 clearCameraPos;
    [SerializeField] private Vector3 clearCameraRot;

    private Vector3 pos;
    //プレイヤーが爆発したときのカメラの動き
    public Vector3 MissExplodeCamera(ref Vector3 rotation)
    {
        Rotation(rotation);
        rotation = new Vector3(directionX, rotation.y + rotateSpeed, 0);
        return pos;
    }
    //ターゲットにぶつかって爆発した時のカメラ挙動
    public Vector3 HitTargetCamera(ref Vector3 rotation)
    {
        rotation = hitCameraRot;
        return hitCameraPos;
    }
    //クリア時のカメラ
    public Vector3 ClearCamera(ref Vector3 rotation)
    {
        rotation = clearCameraRot;
        return clearCameraPos;
    }

    //爆発時のカメラを回す
    private void Rotation(Vector3 rotation)
    {
        pos.x = -distance * (float)Math.Sin(ToRadianScript.ToRadian(ref rotation.y));
        pos.z = -distance * (float)Math.Cos(ToRadianScript.ToRadian(ref rotation.y));

        pos.x = pos.x * (float)Math.Cos(ToRadianScript.ToRadian(ref rotation.x));
        pos.z = pos.z * (float)Math.Cos(ToRadianScript.ToRadian(ref rotation.x));

        pos.y = -distance * (float)Math.Sin(ToRadianScript.ToRadian(ref rotation.x)) * -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
