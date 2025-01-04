using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトルのミニゲーム用プレイヤーの処理
public class miniPlayerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float autoRotateSpeed;
    [SerializeField] float rollingTime;
    [SerializeField] float maxHigh;
    [SerializeField] float maxLow;

    TitlegameScript ts;
    Transform tf;

    private Vector3 initialPos;
    private Vector3 initialRot;
    private Vector3 angles;

    private bool isAuto;
    private bool isRolling;
    private bool isMove;

    //ミニプレーヤー管理
    private void miniPlayerController()
    {
        if(ts.GetGameStartFlag())   //ミニゲームが始まったら回転を停止////
        {
            isRolling = false;
        }/////////////////////////////////////////////////////////////////////

        Move(); //移動または回転

        if(ts.GetResetFlag())   //リセットフラグがオンになったらリセット////
        {
            ResetPlayer();
        }//////////////////////////////////////////////////////////////////////
    }
    //移動
    private void Move()
    {
        if (isMove)
        {
            ControllMove(); //横移動と縦操作
        }
        else
        {
            if (isRolling)
            {
                AutoRotation(); //自動回転する
            }
            else
            {
                FixedRotation();    //回転を補正する
            }
        }
    }
    
    //位置初期化
    private void ResetPlayer()
    {
        isRolling = true;
        isMove = false;
        ResetTransfrom();   //位置を初期化
    }

    //プレーヤーが操作して上下に移動
    private void ControllMove()
    {
        Vector3 pos=tf.position;
        if (ts.GetMoveFlag())   //移動フラグがオンになったら操作を受け付ける////////////
        {
            float movePow = tf.position.y;

            //操作で上下移動
            if (Input.GetKey(KeyCode.W))
            {
                movePow += moveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movePow -= moveSpeed;
            }
            ////////////////
            
            //最大値と最小値でクランプする
            if (movePow >= maxHigh)
            {
                movePow = maxHigh;
            }
            if (movePow < maxLow)
            {
                movePow = maxLow;
            }
            ///////////////////////////////
            SetVector3(ref pos, tf.position.x, movePow, tf.position.z);
            tf.position=pos;
        }///////////////////////////////////////////////////////////////////////////////////
    }
    //タイトルの周りをくるくる回る
    private void AutoRotation()
    {
        SetVector3(ref angles, tf.localEulerAngles.x, (tf.localEulerAngles.y - autoRotateSpeed), tf.localEulerAngles.z);
        SetAngles();
    }
    //所定の位置まで回ってから回転終了
    private void FixedRotation()
    {
        angles = tf.localEulerAngles;

        //特定位置までプレイヤーの回転を継続する
        if (tf.eulerAngles.y <= autoRotateSpeed)
        {
            SetVector3(ref angles, tf.localEulerAngles.x, 0.0f, tf.localEulerAngles.z);
            isMove = true;
            ts.SetMoveFlag(true);
        }
        else if (tf.localEulerAngles.y + autoRotateSpeed >= 360.0f)
        {
            SetVector3(ref angles, tf.localEulerAngles.x, 0.0f, tf.localEulerAngles.z);
            isMove = true;
            ts.SetMoveFlag(true);
        }
        else
        {
            SetVector3(ref angles, tf.localEulerAngles.x, (tf.localEulerAngles.y-autoRotateSpeed), tf.localEulerAngles.z);
        }
        ////////////////////////////////////////////////
        SetAngles();
    }
    #region 値受け渡し
    private void SetAngles()
    {
        tf.localEulerAngles=angles;
    }
    private static void SetVector3(ref Vector3 target,float x,float y,float z)
    {
         target = new Vector3(x, y, z);
    }
    #endregion
    //位置初期化
    private void ResetTransfrom()
    {
        tf.position = initialPos;
        tf.eulerAngles = initialRot;
    }


    private void OnCollisionEnter(Collision collision)
    {
        //ts.SetHitFlag(true);
        if (collision.gameObject.CompareTag("Cloud"))
        {
            ts.SetMoveFlag(false);
            ts.SetMiniPlayerDead(true);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            ts.SetResetActionFlag(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Goal"))
        {
            ts.SetGoalActionFlag(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        ts=GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();
        initialPos = tf.transform.position;
        initialRot = tf.transform.localEulerAngles;
        angles = tf.transform.eulerAngles;
        ResetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        miniPlayerController();
    }
}
