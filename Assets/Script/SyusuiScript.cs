using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SyusuiScript : MonoBehaviour
{
    Transform tf;
    Rigidbody rb;

    [SerializeField] private float firstSpeed;
    [SerializeField] private Vector2 rowSpeed;

    private Vector3 moveSpeed;
    private float speed;
    private bool isSearch;
    private Transform playerPos;
    private PlayerScript playerScript;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;

    private void SyusuiController()
    {
        Chase();
        Move();
    }
    private void Move()
    {
        moveSpeed.x = firstSpeed * (float)Math.Sin(ToRadian(tf.eulerAngles.y));
        moveSpeed.z = firstSpeed * (float)Math.Cos(ToRadian(tf.eulerAngles.y));

        moveSpeed.x = moveSpeed.x * (float)Math.Cos(ToRadian(tf.eulerAngles.x));
        moveSpeed.z = moveSpeed.z * (float)Math.Cos(ToRadian(tf.eulerAngles.x));

        moveSpeed.y = firstSpeed * (float)Math.Sin(ToRadian(tf.eulerAngles.x)) * -1;


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
        float z = 180;



        tf.localEulerAngles = new Vector3((vertical*-1)-90, horizontal-180, z);
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

        speed = firstSpeed;
        isSearch = false;
    }

    // Update is called once per frame
    void Update()
    {
        SyusuiController();  
    }
}
