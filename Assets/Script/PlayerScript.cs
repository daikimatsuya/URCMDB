using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    [SerializeField]private float speedBuff;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float accelerate;
    [SerializeField] private float burst;
    [SerializeField] private float rowlingSpeedX;
    [SerializeField] private float rowlingSpeedY;

    private Vector2 rowling;
    private Vector3 playerMove;
    private float burstSpeed;


    private void PlayerController()
    {
        Operation();
        Acceleration();
        Move();
    }
    private void Move()
    {
        playerMove.x = speedBuff * (float)Math.Sin(ToRadian( tf.eulerAngles.y));
        playerMove.z = speedBuff * (float)Math.Cos(ToRadian(tf.eulerAngles.y));

        playerMove.x = playerMove.x * (float)Math.Cos(ToRadian(tf.eulerAngles.x));
        playerMove.z = playerMove.z * (float)Math.Cos(ToRadian(tf.eulerAngles.x));

        playerMove.y = speedBuff * (float)Math.Sin(ToRadian(tf.eulerAngles.x ))*-1;
       

        rb.velocity = playerMove;
    }  
    private void Operation()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            rowling.x -= rowlingSpeedY;          
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rowling.x += rowlingSpeedY;         
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rowling.y -= rowlingSpeedX;           
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rowling.y += rowlingSpeedX;
        }
        if (rowling.x <= -90)
        {
            rowling.x = -89;
        }
        if (rowling.x >= 90)
        {
            rowling.x = 89;
        }

        tf.localEulerAngles = new Vector3(rowling.x, rowling.y, tf.localEulerAngles.z);
    }
    private void Acceleration()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            burstSpeed = burst+playerSpeed/3;
        }
        if(Input.GetKey (KeyCode.W))
        {
            playerSpeed += accelerate;
        }
        speedBuff = playerSpeed + burstSpeed;

        burstSpeed-=0.1f;
        if(burstSpeed <= 0)
        {
            burstSpeed = 0;
        }
    }
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
    }
}
