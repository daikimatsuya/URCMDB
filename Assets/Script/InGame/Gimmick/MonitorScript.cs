using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//モニターオブジェクトを管理する
public class MonitorScript : MonoBehaviour
{
    [SerializeField] private float moveY;
    [SerializeField] private float moveSpeed;
    private float PosYBuff;

    private float initialPosY;
    private float initialRotY;
    private bool isInGame;

    Transform tf;
    RollingScript rs;

    //モニターオブジェクト管理
    private void MonitorController()
    {
        Move(); //ちょっと上下に動かす

        if (!isInGame) //インゲームじゃなかったらreturnを返す//////
        {
            return;
        }////////////////////////////////////////////////////////////////

        if (gm.IsPlayerDead())  //プレイヤーが死んでいたら///
        {
            tf.eulerAngles = new Vector3(0, initialRotY, 0);    //角度初期化
            rs.enabled = false; //回転を停止
        }////////////////////////////////////////////////////////

    }

    //上下に動かす
    private void Move()
    {
        //上下に動かす////////
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
        ////////////////////////
        
        tf.position=new Vector3(tf.position.x,initialPosY+PosYBuff,tf.position.z);  //トランスフォームに代入
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
        rs.enabled = true;  //プレイヤーにぶつかったら回す
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        initialPosY = tf.position.y;
        initialRotY = tf.eulerAngles.y;
        rs = GetComponent<RollingScript>();
        rs.enabled = false;
        if (GameObject.FindWithTag("GameController"))
        {
            isInGame = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MonitorController();
    }
}
