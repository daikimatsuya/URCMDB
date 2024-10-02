using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniPlayerScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float autoRotateSpeed;
    [SerializeField] float rollingTime;


    Transform tf;


    private Vector3 initialPos;
    private Vector3 initialRot;

    private bool isAuto;
    private bool isRolling;
    private bool isMove;



    private void miniPlayerController()
    {
        if (Input.GetKey(KeyCode.F))
        {
            isRolling = false;
        }
        else
        {
            isRolling = true;
        }

        Move();
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

    private void ControllMove()
    {
        float movePow = tf.position.y;
        if (Input.GetKey(KeyCode.W))
        {
            movePow += moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movePow-=moveSpeed;
        }
        tf.position = new Vector3(tf.position.x, movePow, tf.position.z);
    }
    private void RollingDirection()
    {

    }
    private void AutoRotation()
    {
        tf.eulerAngles = new Vector3(tf.eulerAngles.x, tf.eulerAngles.y - autoRotateSpeed, tf.eulerAngles.z);
    }
    private void FixedRotation()
    {
        if (tf.eulerAngles.y <= autoRotateSpeed)
        {
            tf.eulerAngles = new Vector3(tf.eulerAngles.x, 0.0f, tf.eulerAngles.z);
            isMove = true;
        }
        else if (tf.eulerAngles.y + autoRotateSpeed >= 360.0f)
        {
            tf.eulerAngles = new Vector3(tf.eulerAngles.x, 0.0f, tf.eulerAngles.z);
            isMove = true;
        }
        else
        {
            tf.eulerAngles = new Vector3(tf.eulerAngles.x, tf.eulerAngles.y - autoRotateSpeed, tf.eulerAngles.z);
        }
    }
    private void ResetTransfrom()
    {
        tf.position = initialPos;
        tf.eulerAngles = initialRot;
    }
    public void ResetPlayer()
    {
        isRolling = true;
        isMove = false; 
        ResetTransfrom();
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        initialPos = tf.transform.position;
        initialRot = tf.transform.eulerAngles;

        ResetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        miniPlayerController();
    }
}
