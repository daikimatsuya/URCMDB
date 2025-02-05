using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Usefull;

//秋水管理
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
    [SerializeField] private int backTimer;

    private Vector3 moveSpeed;
    private bool isSearch;
    private Transform playerPos;
    private PlayerScript playerScript;
    private Transform basePos;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;
    private Vector3 Row;
    private bool isLeftBase;
    private int timeBuff;

    //機能管理
    private void SyusuiController()
    {
        Chase();    //追跡
        Move();     //移動
    }
    //移動
    private void Move()
    {
        Vector3 anglesBuff = tf.eulerAngles;    

        //平面の速度算出
        moveSpeed.x = speed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.y));
        moveSpeed.z = speed * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.y));
        /////////////////

        //垂直方向と水平方向の速度算出
        moveSpeed.x = moveSpeed.x * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
        moveSpeed.z = moveSpeed.z * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
        moveSpeed.y = speed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.x)) * -1;
        /////////////////////////////////

        rb.velocity = moveSpeed;    //速度代入
    }
    //範囲内にプレイヤーがいたら追跡する
    private void Chase()
    {
        if (playerScript == null)   //プレイヤーがいなかったら追跡停止//
        {
            isSearch = false; 
        }/////////////////////////////////////////////////////////////////
        if (isSearch)
        {
            Aim();        //プレイヤーの方を向く
            KeepDis();  //距離を一定に保つ
        }
        else
        {
           LeftBase();  //中心に戻る
        }
    }
    //プレイヤーのいる方向を取得
    private void Aim()
    {
        playerDis = playerPos.position - tf.position;                                                                                                                                                                           //距離を算出
        playerDisNormal = playerDis.normalized;                                                                                                                                                                              //ノーマライズ化
        float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;                                                                                                          //水平方向角度算出
        float vertical = Mathf.Atan2(Mathf.Sqrt(playerDisNormal.x * playerDisNormal.x + playerDisNormal.z * playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg; //垂直方向角度算
        vertical -= 90;                                                                                                                                                                                                                    //値を修正
        Rowring(horizontal, vertical);                                                                                                                                                                                               //回転させる
        tf.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);                                                                                                                                                       //角度代入
    }
    //プレイヤーから離れず近すぎずを維持
    private void KeepDis()
    {
        if (playerDis.magnitude < minDis)   //近かったら減速する///
        {
            if(speed>minSpeed)
            {
                speed -= brake;
            }            
        }//////////////////////////////////////////////////////////////

        if(playerDis.magnitude > maxDis)    //遠かったら加速する//////
        {
            if(speed<maxSpeed)
            {
                speed += brake;
            }           
        }//////////////////////////////////////////////////////////////////
    }
    //プレイヤーを追跡していないときの挙動
    private void NormalOperation()
    {
        //角度固定
        float horizontal = Row.y;
        float vertical = 0;
        ///////////

        //中心点を起点にぐるぐるさせる
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
        //////////////////////////////
    }
    //拠点から一定数離れたら戻る
    private void LeftBase()
    {
        Vector2 dis = new Vector2(basePos.position.x - tf.position.x,basePos.position.z-tf.position.z); //中心との距離算出
        float dis2=dis.magnitude;                                                                                                  //距離をfloatに変換

        if (dis2 > maxBaseDis)  //許容値よりも離れると引き返す////
        {
            if(!isLeftBase)
            {
                isLeftBase = true;
                timeBuff = backTimer * 60;
            }
        }/////////////////////////////////////////////////////////////

        if (dis2 <= minBaseDis) //中心付近まで来るとまた広がりだす///
        {
            isLeftBase = false;
        }//////////////////////////////////////////////////////////////////

        if(isLeftBase)  //中心地点へ戻る/////////////////////////////////////////////////////////
        { 
            Vector3 disVector= new Vector3(dis.x, 0, dis.y).normalized;
            float horizontal = Mathf.Atan2(disVector.x, disVector.z) * Mathf.Rad2Deg;

            //許容時間を越したら加算から代入に変更
            if(timeBuff > 0)
            {
                Rowring(horizontal, Row.x);
            }
            else
            {
                Row.y = horizontal;
            }
            ////////////////////////////////////////

            tf.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);

            timeBuff--;
        }////////////////////////////////////////////////////////////////////////////////////////
        else
        {
            NormalOperation();  //通常移動
        }
    }
    //回転
    private void Rowring(float horizontal,float vertical)
    {
        //水平方向回転角遷移/////////////
        if (horizontal - Row.y > 0)
        {
            Row.y += rowSpeed.y;
            if (horizontal - Row.y < 0)
            {
                Row.y = horizontal;
            }
        }
        if (horizontal - Row.y < 0)
        {
            Row.y -= rowSpeed.y;
            if (horizontal - Row.y > 0)
            {
                Row.y = horizontal;
            }
        }
        /////////////////////////////////

        //垂直方向回転角遷移////////////
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
        ////////////////////////////////
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
    //初期化
    public void StartSyusui()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        basePos = GameObject.FindWithTag("AirBase").GetComponent<Transform>();

        Row = tf.localEulerAngles;
        isSearch = false;
    }
    void Start()
    {
        StartSyusui();
    }

    // Update is called once per frame
    void Update()
    {
        SyusuiController();  
    }
}
