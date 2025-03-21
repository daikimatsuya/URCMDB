using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//���j�^�[�I�u�W�F�N�g���Ǘ�����
public class MonitorScript : MonoBehaviour
{
    [SerializeField] private float moveY;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    private float PosYBuff;

    private float initialPosY;
    private float initialRotY;
    private bool rotateFlag=false;

    Transform tf;
    RollingScript rs;

    //���Z�b�g������
    public void ResetPos()
    {
        rotateFlag = false;                                             //��]�t���O�I�t
        tf.eulerAngles = new Vector3(0, initialRotY, 0);    //�p�x������
    }
    //�㉺�ɓ�����
    public void Move()
    {
        //�㉺�ɓ�����
        if (PosYBuff < -moveY)
        {
            PosYBuff = -moveY;
            moveSpeed *= -1;
        }
        else if (PosYBuff > moveY)
        {
            PosYBuff = moveY;
            moveSpeed *= -1;
        }
        PosYBuff += moveSpeed;

        tf.position = new Vector3(tf.position.x, initialPosY + PosYBuff, tf.position.z);  //�g�����X�t�H�[���ɑ��
        if (rotateFlag)
        {
            rs.Rolling(tf, rotateSpeed, "y");
        }
    }

    //���j�^�[������
    public void StartMonitor()
    {
        tf = GetComponent<Transform>();
        initialPosY = tf.position.y;
        initialRotY = tf.eulerAngles.y;
        rs=new RollingScript();
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
        rotateFlag = true;
    }

}
