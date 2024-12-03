using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorScript : MonoBehaviour
{
    [SerializeField] private float moveY;
    [SerializeField] private float moveSpeed;
    private float PosYBuff;


    private float initialPosY;

    Transform tf;
    RollingScript rs;

    private void MonitorController()
    {
        if (!rs.enabled)
        {
            Move();
        }
        else
        {

        }
    }
    private void Move()
    {
        if(PosYBuff<-moveY)
        {
            PosYBuff = -moveY;
            moveSpeed *= -1;
        }
        else if(PosYBuff>moveY) 
        {
            PosYBuff=moveY;
            moveSpeed *= -1;
        }
        PosYBuff += moveSpeed;
        tf.position=new Vector3(tf.position.x,initialPosY+PosYBuff,tf.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stage"))
        {
            return;
        }
        rs.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        initialPosY = tf.position.y;
        rs=GetComponent<RollingScript>();
        rs.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MonitorController();
    }
}
