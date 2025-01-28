using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//���j�^�[�I�u�W�F�N�g���Ǘ�����
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

    //���j�^�[�I�u�W�F�N�g�Ǘ�
    private void MonitorController()
    {
        Move(); //������Ə㉺�ɓ�����

        if (!isInGame) //�C���Q�[������Ȃ�������return��Ԃ�//////
        {
            return;
        }////////////////////////////////////////////////////////////////

        if (gm.IsPlayerDead())  //�v���C���[������ł�����///
        {
            tf.eulerAngles = new Vector3(0, initialRotY, 0);    //�p�x������
            rs.enabled = false; //��]���~
        }////////////////////////////////////////////////////////

    }

    //�㉺�ɓ�����
    private void Move()
    {
        //�㉺�ɓ�����////////
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
        
        tf.position=new Vector3(tf.position.x,initialPosY+PosYBuff,tf.position.z);  //�g�����X�t�H�[���ɑ��
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
        rs.enabled = true;  //�v���C���[�ɂԂ��������
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
