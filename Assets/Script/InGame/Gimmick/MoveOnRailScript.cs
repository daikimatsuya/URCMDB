using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//オブジェクトがライン上を動くようにする
public class MoveOnRailScript : MonoBehaviour
{
    [SerializeField] float speed;

   [SerializeField] float distansMagnification;
    private LineRenderer rail;
    private bool moveEnd;
    private int knot;
    private int next;

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
        if (!moveEnd)
        {
            if (rail.positionCount <= next)
            {
                moveEnd = true;
                next = 0;

            }


            if (speed > Vector3.Distance(rail.GetPosition(next), tf.position)*distansMagnification)
            {

                Vector3 targetPos = SetTargetPos(next);
                rb.velocity = new Vector3(targetPos.normalized.x * speed, targetPos.normalized.y * speed, targetPos.normalized.z * speed);

                knot++;
            }
            else
            {

                Vector3 targetPos = SetTargetPos(next);
                rb.velocity = new Vector3(targetPos.normalized.x * speed, targetPos.normalized.y * speed, targetPos.normalized.z * speed);
            }

        }
        else
        {
            rail = null;
        }
    }
    //目標地点設定
    private Vector3 SetTargetPos(int next)
    {
        Vector3 targetPos = rail.GetPosition(next) - tf.position;

        float horizontal = Mathf.Atan2(targetPos.normalized.x, targetPos.normalized.z) * Mathf.Rad2Deg;
        float vertical = Mathf.Atan2(Mathf.Sqrt(targetPos.normalized.x * targetPos.normalized.x + targetPos.normalized.z * targetPos.normalized.z), targetPos.normalized.y) * Mathf.Rad2Deg;

        tf.eulerAngles = new Vector3(tf.eulerAngles.x, tf.eulerAngles.y, horizontal);
        tf.eulerAngles = new Vector3(vertical, tf.eulerAngles.y, tf.eulerAngles.z);

        return targetPos;
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
        if (other.CompareTag("Finish"))
        {
            rail=other.gameObject.GetComponent<LineRenderer>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();

        rb.velocity=new Vector3(1,0,0);

        moveEnd = false;
        //rail= GetComponent<LineRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
