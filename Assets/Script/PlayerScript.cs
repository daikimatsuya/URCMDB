using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    [SerializeField] private float playerHp;
    [SerializeField] private GameObject flame;
    [SerializeField] private float speedBuff;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float accelerate;
    [SerializeField] private float burst;
    [SerializeField] private float rowlingSpeedX;
    [SerializeField] private float rowlingSpeedY;
    [SerializeField] private float fixRowling;

    private Vector2 rowling;
    private Vector3 playerMove;
    private float burstSpeed;
    private int effectTimer;


    private void PlayerController()
    {
        Booooooomb();
        Operation();
        Acceleration();
        Move();
       // FlameEffect();
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

        if (rowling.x < 10&&rowling.x>0)
        {
            rowling.x -= fixRowling;
            if(rowling.x <= 0)
            {
                rowling.x = 0;
            }
        }
        if (rowling.x > -10 && rowling.x < 0)
        {
            rowling.x +=fixRowling;
            if( rowling.x >= 0)
            {
                rowling.x = 0;
            }
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
    private void FlameEffect()
    {
        if (effectTimer <= 0)
        {
            GameObject _ = Instantiate(flame);
            _.transform.position = new Vector3(tf.localPosition.x, tf.localPosition.y , tf.localPosition.z);

            effectTimer = 2;

        }
        effectTimer--;
    }
    private void Booooooomb()
    {
        if (playerHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        playerHp = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        effectTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
    }
}
