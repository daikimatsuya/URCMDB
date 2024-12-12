using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//オブジェクトがライン上を動くようにする
public class MoveOnRailScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float distansMagnification;
    [SerializeField] bool obtainVoluntarilyRail;
    [SerializeField] float rotSpeed;

    private LineRenderer rail;
    private bool moveEnd;
    private int knot;
    private int next;
    private Vector3 targetAngles;
    private Vector3 rotBuff;


    Rigidbody rb;
    Transform tf;

    //動かす
    private void Move()
    {
        if (rail == null)
        {
            return;
        }
        next = knot + 1;
         
        if (rail.positionCount <= next)
        {
            moveEnd = true;
            next = 0;
            rail = null;
        }
        if (!moveEnd)
        {
            if (moveSpeed > Vector3.Distance(rail.GetPosition(next), tf.position) * distansMagnification)
            {

                SetPosAndRot();

                knot++;
            }
            else
            {
                SetPosAndRot();

            }
        }
        
    }
    //値代入する
    private void SetPosAndRot()
    {
        Vector3 targetPos = SetTargetPos(next);
        Rolling();
        rb.velocity = new Vector3(targetPos.normalized.x * moveSpeed, targetPos.normalized.y * moveSpeed, targetPos.normalized.z * moveSpeed);
    }
    //目標地点設定
    private Vector3 SetTargetPos(int next)
    {
        Vector3 targetPos = rail.GetPosition(next) - tf.position;

        float horizontal = Mathf.Atan2(targetPos.normalized.x, targetPos.normalized.z) * Mathf.Rad2Deg;
        float vertical = Mathf.Atan2(Mathf.Sqrt(targetPos.normalized.x * targetPos.normalized.x + targetPos.normalized.z * targetPos.normalized.z), targetPos.normalized.y) * Mathf.Rad2Deg;

        //tf.eulerAngles = new Vector3(tf.eulerAngles.x, horizontal-90, tf.eulerAngles.z);
        //tf.eulerAngles = new Vector3(tf.eulerAngles.x, tf.eulerAngles.y, -(vertical)+90);

        targetAngles = new Vector3(tf.eulerAngles.x + 360, horizontal - 90 + 360, -(vertical) + 90+360);
        //targetAngles = new Vector3(tf.eulerAngles.x, tf.eulerAngles.y, -(vertical) + 90);

        return targetPos;
    }
    //回転させる
    private void Rolling()
    {
      
      
        if(targetAngles.x>rotBuff.x)
        {
            rotBuff.x += rotSpeed;
            if (targetAngles.x < rotBuff.x)
            {
                rotBuff.x = targetAngles.x;
            }
        }
        else if(targetAngles.x < rotBuff.x)
        {
            rotBuff.x -= rotSpeed;
            if (targetAngles.x > rotBuff.x)
            {
                rotBuff.x = targetAngles.x;
            }
        }
        if(targetAngles.y > rotBuff.y)
        {
            rotBuff.y += rotSpeed;
            if (targetAngles.y < rotBuff.y)
            {
                rotBuff.y = targetAngles.y;
            }
        }
        else if (targetAngles.y < rotBuff.y)
        {
            rotBuff.y -= rotSpeed;
            if (targetAngles.y > rotBuff.y)
            {
                rotBuff.y = targetAngles.y;
            }
        }
        if (targetAngles.z > rotBuff.z)
        {
            rotBuff.z += rotSpeed;
            if (targetAngles.z < rotBuff.z)
            {
                rotBuff.z = targetAngles.z;
            }
        }
        else if (targetAngles.z < rotBuff.z)
        {
            rotBuff.z -= rotSpeed;
            if (targetAngles.z > rotBuff.z)
            {
                rotBuff.z = targetAngles.z;
            }
        }
        tf.eulerAngles = rotBuff;
      //  tf.eulerAngles = targetAngles;
    }
    //ラインセット
    public void SetRail(LineRenderer rail)
    {
        this.rail = rail;
        knot = 0;
        moveEnd = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!obtainVoluntarilyRail)
        {
            return;
        }
        if (other.CompareTag("Rail"))
        {
            rail = null;
            rail=other.gameObject.GetComponent<LineRenderer>();
            SetRail(rail);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();

        rb.velocity=new Vector3(moveSpeed,0,0);

        moveEnd = false;
        rotBuff = tf.eulerAngles;
        rotBuff = new Vector3(rotBuff.x + 360, rotBuff.y + 360, rotBuff.z + 360);
        //rail= GetComponent<LineRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
