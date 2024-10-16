using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            tf.position = new Vector3(tf.position.x, movePow, tf.position.z);
        }
    }
    private void RollingDirection()
    {

    }
    private void AutoRotation()
    {
        tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y - autoRotateSpeed, tf.localEulerAngles.z);
    }
    private void FixedRotation()
    {
        if (tf.eulerAngles.y <= autoRotateSpeed)
        {
            tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, 0.0f, tf.localEulerAngles.z);
            isMove = true;
            ts.SetMoveFlag(true);
        }
        else if (tf.localEulerAngles.y + autoRotateSpeed >= 360.0f)
        {
            tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, 0.0f, tf.localEulerAngles.z);
            isMove = true;
            ts.SetMoveFlag(true);
        }
        else
        {
            tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y - autoRotateSpeed, tf.localEulerAngles.z);
        }
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

        ResetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        miniPlayerController();
    }
}
