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




    private void miniPlayerController()
    {
        if(ts.GetGameStartFlag())
        {
            isRolling = false;
        }
        Move();

        if(ts.GetResetFlag())
        {
            ResetPlayer();
        }
    }
    private void Move()
    {
        if (isMove)
        {
            ControllMove();
        }
        else
        {
            if (isRolling)
            {
                AutoRotation();
            }
            else
            {
                FixedRotation();
            }
        }
    }
    private void AutoMove()
    {

    }
    private void ResetPlayer()
    {
        isRolling = true;
        isMove = false;
        ResetTransfrom();
    }

    private void ControllMove()
    {
        Vector3 pos=tf.position;
        if (ts.GetMoveFlag())
        {
            float movePow = tf.position.y;

            if (Input.GetKey(KeyCode.W))
            {
                movePow += moveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movePow -= moveSpeed;
            }
            if (movePow >= maxHigh)
            {
                movePow = maxHigh;
            }
            if (movePow < maxLow)
            {
                movePow = maxLow;
            }
            SetVector3(ref pos, tf.position.x, movePow, tf.position.z);
            tf.position=pos;
        }
    }
    private void RollingDirection()//後程追記する
    {

    }
    private void AutoRotation()
    {
        SetVector3(ref angles, tf.localEulerAngles.x, (tf.localEulerAngles.y - autoRotateSpeed), tf.localEulerAngles.z);
        SetAngles();
    }
    private void FixedRotation()
    {
        angles = tf.localEulerAngles;
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
        SetAngles();
    }
    private void SetAngles()
    {
        tf.localEulerAngles=angles;
    }
    private static void SetVector3(ref Vector3 target,float x,float y,float z)
    {
         target = new Vector3(x, y, z);
    }
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
