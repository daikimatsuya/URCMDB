using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    private Transform tf;
    private Transform playerPos;

    [SerializeField] private float cameraDeff;

    private void PlayerCameraController()
    {
        Move();
    }
    private void Move()
    {
        tf.localRotation=playerPos.localRotation;

        Vector3 deff = Vector3.zero;
        deff.x = cameraDeff * (float)Math.Sin(ToRadian(playerPos.eulerAngles.y));
        deff.z = cameraDeff * (float)Math.Cos(ToRadian(playerPos.eulerAngles.y));

        deff.x = deff.x * (float)Math.Cos(ToRadian(playerPos.eulerAngles.x));
        deff.z = deff.z * (float)Math.Cos(ToRadian(playerPos.eulerAngles.x ));

        deff.y = cameraDeff * (float)Math.Sin(ToRadian(playerPos.eulerAngles.x ))*-1;

        tf.localPosition=new Vector3(playerPos.localPosition.x-deff.x,playerPos.localPosition.y-deff.y,playerPos.localPosition.z-deff.z);
    }
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        playerPos=GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCameraController();
    }
}
