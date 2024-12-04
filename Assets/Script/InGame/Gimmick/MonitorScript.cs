using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorScript : MonoBehaviour
{
    [SerializeField] private float moveY;
    [SerializeField] private float moveSpeed;
    private float PosYBuff;


    private float initialPosY;
    private float initialRotY;
    private GameManagerScript gm;

    Transform tf;
    RollingScript rs;

    private void MonitorController()
    {

        Move();
        if (gm.IsPlayerDead())
        {
            tf.eulerAngles = new Vector3(0, initialRotY, 0);
            rs.enabled = false;
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
        if (other.CompareTag("SensorChildren"))
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
        initialRotY = tf.eulerAngles.y;
        rs=GetComponent<RollingScript>();
        rs.enabled = false;
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        MonitorController();
    }
}
