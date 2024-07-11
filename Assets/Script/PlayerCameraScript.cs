using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    private Transform tf;
    private Transform playerPos;
    private PlayerScript ps;
    private GameObject player;
    

    private float rot;

    [SerializeField] private float cameraDeff;
    [SerializeField] private float rotSpeed;

    private void PlayerCameraController()
    {
        SearchPlayer();       
        Move();
    }
    private void Move()
    {
        if (playerPos != null)
        {
            tf.localRotation = playerPos.localRotation;

            Vector3 deff = Vector3.zero;

            if (ps.GetControll())
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    if (rot < 10)
                    {
                        rot += rotSpeed;
                    }

                }
                if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
                {
                    if (rot > -10)
                    {
                        rot -= rotSpeed;
                    }

                }
                if ((Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A)) 
                {
                    if (rot > 0)
                    {
                        rot -= rotSpeed;
                    }
                    if (rot < 0)
                    {
                        rot += rotSpeed;
                    }

                }
                if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)&& !Input.GetKey(KeyCode.A)&& !Input.GetKey(KeyCode.D)) 
                {
                    if (rot > 0)
                    {
                        rot -= rotSpeed;
                    }
                    if (rot < 0)
                    {
                        rot += rotSpeed;
                    }

                }

            }
                deff.x = cameraDeff * (float)Math.Sin(ToRadian(playerPos.eulerAngles.y + rot));
                deff.z = cameraDeff * (float)Math.Cos(ToRadian(playerPos.eulerAngles.y + rot));
            

            deff.x = deff.x * (float)Math.Cos(ToRadian(playerPos.eulerAngles.x));
            deff.z = deff.z * (float)Math.Cos(ToRadian(playerPos.eulerAngles.x));

            deff.y = (cameraDeff + 5) * (float)Math.Sin(ToRadian(playerPos.eulerAngles.x)) * -1;


            tf.localPosition = new Vector3(playerPos.localPosition.x - deff.x, playerPos.localPosition.y - deff.y + 3, playerPos.localPosition.z - deff.z);
        }
    }
    private void SearchPlayer()
    {
        if (playerPos == null)
        {
            if (GameObject.FindWithTag("Player"))
            {
                rot = 0;
                player = GameObject.FindWithTag("Player");
                playerPos = player.GetComponent<Transform>();
                ps = player.GetComponent<PlayerScript>();
            }
        }
    }
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        //playerPos=GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCameraController();
    }
}
