using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//オブジェクトがライン上を動くようにする
public class MoveOnRailScript : MonoBehaviour
{
    [SerializeField] float speed;

    private LineRenderer rail;
    private bool moveEnd;
    private int knot;
    private int next;

    Rigidbody rb;
    Transform tf;

    //動かす
    private void Move()
    {
        if (moveEnd)
        {
            return;
        }
        next = knot + 1;
        if (rail.positionCount < next)
        {
            moveEnd = true;
        }

       Vector3 targetPos= SetTargetPos();


        if (speed > Vector3.Distance(rail.GetPosition(next), tf.position))
        {
            //float speed_= speed- Vector3.Distance(rail.GetPosition(next), tf.position);

            //rb.velocity = new Vector3(targetPos.normalized.x * speed_, targetPos.normalized.y * speed_, targetPos.normalized.z * speed_);

            knot++;
        }
        else
        {
            rb.velocity = new Vector3(targetPos.normalized.x * speed, targetPos.normalized.y * speed, targetPos.normalized.z * speed);
        }
    }
    //目標地点設定
    private Vector3 SetTargetPos()
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
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
