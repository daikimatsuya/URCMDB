using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SyusuiScript : MonoBehaviour
{
    Transform tf;
    Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private Vector2 rowSpeed;
    [SerializeField] private float normalPosY;
    [SerializeField] private float normalPosRange;
    [SerializeField] private float minDis;
    [SerializeField] private float maxDis;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float brake;
    [SerializeField] private float maxBaseDis;
    [SerializeField] private float minBaseDis;

    private Vector3 moveSpeed;
    private bool isSearch;
    private Transform playerPos;
    private PlayerScript playerScript;
    private Transform basePos;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;
    private Vector3 Row;
    private bool isLeftBase;

    private void SyusuiController()
    {
        Chase();
        Move();
    }
    private void Move()
    {
        moveSpeed.x = speed * (float)Math.Sin(ToRadian(tf.eulerAngles.y));
        moveSpeed.z = speed * (float)Math.Cos(ToRadian(tf.eulerAngles.y));

        moveSpeed.x = moveSpeed.x * (float)Math.Cos(ToRadian(tf.eulerAngles.x));
        moveSpeed.z = moveSpeed.z * (float)Math.Cos(ToRadian(tf.eulerAngles.x));

        moveSpeed.y = speed * (float)Math.Sin(ToRadian(tf.eulerAngles.x)) * -1;


        rb.velocity = moveSpeed;
    }
    private void Chase()
    {
        if (playerScript == null)
        {
            isSearch = false;
           
        }
        if (isSearch)
        {
            Aim();
            KeepDis();

        }
        else
        {
           LeftBase();
        }
    }
    private void Aim()
    {
        playerDis = playerPos.position - tf.position;
        playerDisNormal = playerDis.normalized;

        float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;
        float vertical = Mathf.Atan2(Mathf.Sqrt(playerDisNormal.x * playerDisNormal.x + playerDisNormal.z * playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg;

        vertical -= 90;

        Rowring(horizontal, vertical);

        tf.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);
    }
    private void KeepDis()
    {
        if (playerDis.magnitude < minDis)
        {
            if(speed>minSpeed)
            {
                speed -= brake;
            }            
        }
        if(playerDis.magnitude > maxDis)
        {
            if(speed<maxSpeed)
            {
                speed += brake;
            }           
        }
    }
    private void NormalOperation()
    {
        float horizontal = tf.localEulerAngles.y;
        float vertical = 0;
        if (tf.position.y < normalPosY - normalPosRange)
        {
            vertical -= rowSpeed.x*20;
        }
        if (tf.position.y > normalPosY + normalPosRange)
        {
            vertical += rowSpeed.x*20;
        }
        horizontal += rowSpeed.y / 10;
        Rowring(horizontal, vertical);
        tf.localEulerAngles = Row;
    }
    private void LeftBase()
    {
        Vector2 dis = new Vector2(basePos.position.x - tf.position.x,basePos.position.z-tf.position.z);
        float dis2=dis.magnitude;
        if (dis2 > maxBaseDis)
        {
            isLeftBase = true;
        }
        if (dis2 <= minBaseDis)
        {
            isLeftBase = false;
        }
        if(isLeftBase)
        { 
            Vector3 disVector= new Vector3(dis.x, 0, dis.y).normalized;
            float horizontal = Mathf.Atan2(disVector.x, disVector.z) * Mathf.Rad2Deg;

            Rowring(horizontal,Row.x);

            tf.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);
        }
        else
        {
            NormalOperation();
        }
    }
    private void Rowring(float horizontal,float vertical)
    {
        if (horizontal - Row.y > 0)
        {
            Row.y += rowSpeed.y;
            if (horizontal - Row.y < 0)
            {
                Row.y = horizontal;
            }
        }
        else if (horizontal - Row.y < 0)
        {
            Row.y -= rowSpeed.y;
            if (horizontal - Row.y > 0)
            {
                Row.y = horizontal;
            }
        }
        if (vertical - Row.x > 0)
        {
            Row.x += rowSpeed.x;
            if (vertical - Row.x < 0)
            {
                Row.x = vertical;
            }
        }
        else if (vertical - Row.x < 0)
        {
            Row.x -= rowSpeed.x;
            if (vertical - Row.x > 0)
            {
                Row.x = vertical;
            }
        }
    }
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isSearch = true;
            playerPos = other.transform;
            playerScript = other.GetComponent<PlayerScript>();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isSearch = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        basePos=GameObject.FindWithTag("AirBase").GetComponent<Transform>();

        Row = tf.localEulerAngles;
        isSearch = false;
    }

    // Update is called once per frame
    void Update()
    {
        SyusuiController();  
    }
}
