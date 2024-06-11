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

    private Vector3 moveSpeed;
    private bool isSearch;
    private Transform playerPos;
    private PlayerScript playerScript;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;
    private Vector3 Row;

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

        Row = tf.localEulerAngles;
        isSearch = false;
    }

    // Update is called once per frame
    void Update()
    {
        SyusuiController();  
    }
}
