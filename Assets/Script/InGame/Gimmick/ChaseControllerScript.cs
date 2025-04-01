using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Usefull;

//�H���Ǘ�
public class ChaseControllerScript : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    private Vector3 playerPos;
    private Vector3 playerDis;
    private Vector3 Row;
    private bool isLeftBase;
    private float moveSpeed;
    private Rigidbody rb;
    private Transform tf;

    //�v���C���[�̂���������擾
    public void Chase(in Vector3 chaseTargetPos)
    {
        playerDis = chaseTargetPos - this.gameObject.transform.position;       //�������Z�o

        float horizontal = Mathf.Atan2(playerDis.normalized.x, playerDis.normalized.z) * Mathf.Rad2Deg;   
        float vertical = Mathf.Atan2(Mathf.Sqrt(playerDis.normalized.x * playerDis.normalized.x + playerDis.normalized.z * playerDis.normalized.z), playerDis.normalized.y) * Mathf.Rad2Deg; 

        Rowring(horizontal, vertical);                                                                                     //��]������
        this.gameObject.transform.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);        //�p�x���

        Accelerate(new Vector3(tf.eulerAngles.x, horizontal - 90, -(vertical) + 90));
    }

    //����
    private void Accelerate(Vector3 targetPos)
    {
        //��]�p��ۑ�
        Vector3 anglesBuff = tf.eulerAngles;
        anglesBuff.y += 90;
        anglesBuff.z *= -1;


        //�p�x����e�����ւ̑��x���Z�o
        Vector3 velocity;

        //���ʕ����̑��x���Z�o
        velocity.x = moveSpeed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.y));
        velocity.z = moveSpeed * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.y));

        //���������Ɛ��������̑��x���Z�o
        velocity.x = velocity.x * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.z));
        velocity.z = velocity.z * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.z));
        velocity.y = moveSpeed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.z)) * -1;

        rb.velocity = velocity; //���W�b�g�{�f�B�ɑ��
    }

    //��]
    private void Rowring(float horizontal,float vertical)
    {
        Row.x += ComplementingRotationScript.Rotate(rotateSpeed, 0, Row.x);
        Row.y += ComplementingRotationScript.Rotate(rotateSpeed, horizontal-90, Row.y);
        Row.z += ComplementingRotationScript.Rotate(rotateSpeed, -(vertical)+90, Row.z);
    }

    #region �l�󂯓n��
    public void SetRot(Vector3 rot)
    {
        Row = rot;
    }

    #endregion

    //������
    public void StartChaseController(Rigidbody rb,Transform tf,float moveSpeed)
    {
        this.rb = rb;
        this.tf = tf;
        this.moveSpeed = moveSpeed;
    }

}
