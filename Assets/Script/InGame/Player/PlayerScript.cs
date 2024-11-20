using System;

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//インゲームのプレイヤーの処理
public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;


    private LaunchPointScript lp;
    private GameManagerScript gm;
    private GameObject dust;

    
    [SerializeField] private float time;
    [SerializeField] private float playerHp;
    [SerializeField] private GameObject boostEffect;
    [SerializeField] private GameObject explode;
    [SerializeField] private float speedBuff;
    [SerializeField] private float firstSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float ringBust;
    [SerializeField] private float accelerate;
    [SerializeField] private float burst;
    [SerializeField] private float rowlingSpeedX;
    [SerializeField] private float rowlingSpeedY;
    [SerializeField] private float fixRowling;
    [SerializeField] private float speedCut;
    

    private Vector2 rowling;
    private Vector3 playerMove;
    private Vector3 playerMoveBuff;
    private float burstSpeed;
    //private int effectTimer;
    private bool isFire;
    private bool isControl;
    private float ringSpeed;
    private bool PMS;
    private List<BoostEffectScript> boostEffectList = new List<BoostEffectScript>();
    //private bool isInStage;
    //[SerializeField] private bool RockOned;

    //プレイヤー管理関数
    private void PlayerController()
    {
        if (gm.GetCanShotFlag())
        {
            lp.SetStart(true);
            //RockOned = false;

            SpeedControllDebager();//デバッグ用

            Booooooomb();
            Operation();
            Acceleration();
            Move();
            CountDown();
            EffectController();

           
        }
        gm.PlayerRotSet(rowling);
    }
    //速度を足してトランスフォームのバッファに入れる
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
    //プレイヤーの操作で向いてる方向を変える
    private void Operation()
    {
        if (isControl)
        {
            if(!Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    rowling.x -= rowlingSpeedY/speedCut;
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    rowling.x += rowlingSpeedY/speedCut;
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    rowling.y -= rowlingSpeedX/speedCut;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    rowling.y += rowlingSpeedX/speedCut;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    rowling.x -= rowlingSpeedY;
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    rowling.x += rowlingSpeedY;
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    rowling.y -= rowlingSpeedX;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    rowling.y += rowlingSpeedX;
                }
            }
            
            if (rowling.x <= -90)
            {
                rowling.x = -89;
            }
            if (rowling.x >= 90)
            {
                rowling.x = 89;
            }

            if (PMS)
            {
                if (rowling.x < 15 && rowling.x > 0)
                {
                    rowling.x -= fixRowling;
                    if (rowling.x <= 0)
                    {
                        rowling.x = 0;
                    }
                }
                if (rowling.x > -15 && rowling.x < 0)
                {
                    rowling.x += fixRowling;
                    if (rowling.x >= 0)
                    {
                        rowling.x = 0;
                    }
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
            rowling.y = lp.GetRowling().y + 180;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFire = true;
                lp.Shoot();
            }
        }
        PMS=gm.GetPMS();

        tf.localEulerAngles = new Vector3(rowling.x, rowling.y, tf.localEulerAngles.z);
    }
    //加減速処理
    private void Acceleration()
    {
        if (isFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                burstSpeed = burst + playerSpeed / 3;
                CreateBoostEffect();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                playerSpeed += accelerate;
            }

            burstSpeed -= playerSpeed / 300;
            ringSpeed -= playerSpeed * 0.003f;
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
    //デバッグで速度調節する（後で消す
    private void SpeedControllDebager()
    {
        if(Input.GetKey(KeyCode.Alpha0))
        {
            playerSpeed = 0;
            speedBuff = 0;
        }
        if ((Input.GetKey(KeyCode.Alpha1)))
        {
            playerSpeed++;
        }
        if ((Input.GetKey(KeyCode.Alpha2)))
        {
            playerSpeed--;
        }
    }

    //体力が無くなったら爆破して消す
    private void Booooooomb()
    {
        if (playerHp <= 0)
        {
            lp.Bombed();
            GameObject _ = Instantiate(explode);
            _.transform.position = tf.position;
            Destroy(this.gameObject);
        }
    }
    //ブースト時に火花が散るエフェクト生成
    private void CreateBoostEffect()
    {
        GameObject _ = Instantiate(boostEffect);
        _.transform.SetParent(tf.transform, true);
        _.transform.localPosition=Vector3.zero;
        _.transform.localEulerAngles = new Vector3(0, 180, 0);
        BoostEffectScript bf=_.GetComponent<BoostEffectScript>();
        bf.SetTime();
        boostEffectList.Add(bf);
    }
    private void EffectController()
    {
        if (boostEffectList != null)
        {
            for (int i = 0; i < boostEffectList.Count;)
            {
                if (boostEffectList[i].IsDelete())
                {
                    boostEffectList[i].Break();
                    boostEffectList.Remove(boostEffectList[i]);
                }
                else
                {
                    boostEffectList[i].CountTime();
                    i++;
                }
            }
        }
    }
    //プレイヤーの爆発までのカウントダウン
    private void CountDown()
    {
        if (isControl)
        {
            if (TimeCountScript.TimeCounter(ref time))
            {
                playerHp = 0;
            }

        }
    }
    #region　値受け渡し
    public Vector3 GetPlayerSpeed()
    {
        return playerMoveBuff;
    }
    public float GetPlayerSpeedBuffFloat()
    {
        return speedBuff;
    }
    public float GetPlayerSpeedFloat()
    {
        return playerSpeed;
    }
    public Vector3 GetPlayerRot()
    {
        return tf.eulerAngles;
    }
    public bool GetControll()
    {
        return isControl;
    }
    #endregion
    //狙われているかのチェック
    public void IsLock()
    {
        //RockOned = true;
    }
    //デグをラッドに変換
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
        if (other.CompareTag("LaunchPad"))
        {     
            isControl =true;
            dust.SetActive(true);
        }
        if(other.CompareTag("SpeedUpRing"))
        {
            ringSpeed = playerSpeed * 0.3f;
            playerSpeed += playerSpeed * ringBust;
        }
        if (other.CompareTag("Bullet"))
        {
            playerHp = 0;
            other.GetComponent<FlakBulletScript>().Delete();            
        }
        if( other.CompareTag("Rock"))
        {
            playerHp = 0;
        }
        if( other.CompareTag("stage"))
        {
            //isInStage = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("stage"))
        {
            //isInStage = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        PMS = false;
        time = time * 60;

        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();


        lp=GameObject.FindWithTag("LaunchPoint").GetComponent<LaunchPointScript>();
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        dust = GameObject.FindWithTag("PlayerDust");
        dust.SetActive(false);
        //effectTimer = 0;
        isFire = false;
        isControl = false;
        ringSpeed = 0;
        tf.position=lp.GetPos();

        rowling.x = -lp.GetRowling().x;
        rowling.y = lp.GetRowling().y + 180;
        tf.localEulerAngles = new Vector3(rowling.x, rowling.y, tf.localEulerAngles.z);
        lp.SetStart(false);
        gm.SetIsHitTarget(false);
        //isInStage = true;
        //RockOned = false;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
    }
}
