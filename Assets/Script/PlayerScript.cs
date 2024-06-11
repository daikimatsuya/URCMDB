using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    private LaunchPointScript lp;

    [SerializeField] private float playerHp;
    [SerializeField] private GameObject flame;
    [SerializeField] private float speedBuff;
    [SerializeField] private float firstSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float ringBust;
    [SerializeField] private float accelerate;
    [SerializeField] private float burst;
    [SerializeField] private float rowlingSpeedX;
    [SerializeField] private float rowlingSpeedY;
    [SerializeField] private float fixRowling;

    private Vector2 rowling;
    private Vector3 playerMove;
    private Vector3 playerMoveBuff;
    private float burstSpeed;
    private int effectTimer;
    private bool isFire;
    private bool isControl;
    private float ringSpeed;

    private void PlayerController()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isControl = true;
        }
        Booooooomb();
        Operation();
        Acceleration();
        Move();
       // FlameEffect();
    }
    private void Move()
    {
        if (isFire)
        {
            playerMove.x = speedBuff * (float)Math.Sin(ToRadian(tf.eulerAngles.y));
            playerMove.z = speedBuff * (float)Math.Cos(ToRadian(tf.eulerAngles.y));

            playerMove.x = playerMove.x * (float)Math.Cos(ToRadian(tf.eulerAngles.x));
            playerMove.z = playerMove.z * (float)Math.Cos(ToRadian(tf.eulerAngles.x));

            playerMove.y = speedBuff * (float)Math.Sin(ToRadian(tf.eulerAngles.x)) * -1;


            rb.velocity = playerMove;
            playerMoveBuff = playerMove;
        }
    }  
    private void Operation()
    {
        if (isControl)
        {
            if (Input.GetKey(KeyCode.UpArrow))
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

            if (rowling.x < 10 && rowling.x > 0)
            {
                rowling.x -= fixRowling;
                if (rowling.x <= 0)
                {
                    rowling.x = 0;
                }
            }
            if (rowling.x > -10 && rowling.x < 0)
            {
                rowling.x += fixRowling;
                if (rowling.x >= 0)
                {
                    rowling.x = 0;
                }
            }


           
        }
        else
        {
            if(isFire)
            {
                speedBuff += firstSpeed;
            }
            rowling.x = -lp.GetRowling().x;
            rowling.y = lp.GetRowling().y+180;
          
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isFire = true;
                lp.Shoot();
            }
        }
        tf.localEulerAngles = new Vector3(rowling.x, rowling.y, tf.localEulerAngles.z);
    }
    private void Acceleration()
    {
        if (isFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                burstSpeed = burst + playerSpeed / 3;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                playerSpeed += accelerate;
            }
         

            burstSpeed -= 0.1f;
            ringSpeed -= 0.1f;
            if (burstSpeed <= 0)
            {
                burstSpeed = 0;
            }
            if(ringSpeed <= 0)
            {
                ringSpeed = 0;
            }
        }
        if(isControl)
        {
            speedBuff = playerSpeed + burstSpeed+ringSpeed;
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
            //lp.Bombed();
            //Destroy(this.gameObject);
        }
    }
    public Vector3 GetPlayerSpeed()
    {
        return playerMoveBuff;
    }
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }

    public void OnCollisionEnter(Collision collision)
    {
        playerHp = 0;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LaunchPad")
        {     
            isControl =true;
        }
        if(other.tag == "SpeedUpRing")
        {
            ringSpeed = playerSpeed * 0.3f;
            playerSpeed += playerSpeed * ringBust;
        }
        if (other.tag == "Bullet")
        {
            playerHp = 0;
            Destroy(other.gameObject);
        }
    }
    public bool GetControll()
    {
        return isControl;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        lp=GameObject.FindWithTag("LaunchPoint").GetComponent<LaunchPointScript>();
        effectTimer = 0;
        isFire = false;
        isControl = false;
        ringSpeed = 0;
        tf.position=lp.GetPos();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
    }
}
