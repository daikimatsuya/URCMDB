using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//インゲームでプレイヤーにくっついているカメラ管理
public class PlayerCameraScript : MonoBehaviour
{
    private Transform tf;
    private Transform playerPos;
    private PlayerScript ps;
    private GameObject player;
    private GameManagerScript gm;
    private GameObject mainCanvas;
    MovieCamera mc;
    MovieFade mf;
    ExplodeCamera ec;

    private float rot;
    private Vector3 cameraRot;


    [SerializeField] private float cameraDeff;
    [SerializeField] private float rotSpeed;
    [SerializeField] private GameObject movieCanvas;

    //カメラ動かす関数
    private void PlayerCameraController()
    {
        if (player == null)
        {
            SearchPlayer();
            cameraRot = transform.localEulerAngles;
            tf.position= ec.ExplodeCameraController(ref cameraRot);
            tf.localEulerAngles = cameraRot;
            return;
        }
        if (mc.GetEnd())
        {
            movieCanvas.SetActive(false);
            mainCanvas.SetActive(true);


            Move();
        }
        else
        {
            mf.MovieFadeController();
            mc.CameraController();
            mainCanvas.SetActive(false);
            movieCanvas.SetActive(true);
        }
    }

    //カメラがプレイヤーの後ろに移動する
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
                if ((Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))) 
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


            tf.position = new Vector3(playerPos.position.x - deff.x, playerPos.position.y - deff.y + 3, playerPos.position.z - deff.z);
        }
    }
    //プレイヤーが居なかったら再取得する
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
    //デグラド変換
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        tf=GetComponent<Transform>();
        mc=GetComponent<MovieCamera>();
        mf = GetComponent<MovieFade>();
        ec=GetComponent<ExplodeCamera>();
        mainCanvas = GameObject.FindWithTag("UICanvas");
        //playerPos=GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCameraController();
    }
}
