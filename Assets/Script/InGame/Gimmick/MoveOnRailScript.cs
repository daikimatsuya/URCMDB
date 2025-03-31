using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Usefull;

//�I�u�W�F�N�g�����C����𓮂��悤�ɂ���
public class MoveOnRailScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float distansMagnification;
    [SerializeField] bool obtainVoluntarilyRail;
    [SerializeField] float rotSpeed;
    [SerializeField] string railName;

    private LineRenderer rail;
    private bool moveEnd;
    private int knot;
    private int next;
    private Vector3 targetAngles;
    private Vector3 rotBuff;

    Rigidbody rb;
    Transform tf;

    //������
    public void Move()
    {
        //���[���ɏ���ĂȂ�������return��Ԃ�
        if (rail == null)   
        {
            return;
        }

        //���[���Ɏ��̒��p�n�����邩���m�F
        next = knot + 1;
        if (rail.positionCount <= next)
        {
            moveEnd = true;
            next = 0;
            rail = null;
        }
        
        if (!moveEnd)
        {
            //���p�n�_�ɂ��ǂ蒅�����玟�̒��p�n�_��ڎw��
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
    //�l�������
    private void SetPosAndRot()
    {
        Vector3 targetPos = SetTargetPos(next); //�ڕW�n�_���W�ݒ�
        Rolling();                                              //��]������
        Accelerate(targetPos);                           //����
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
    //�ڕW�n�_�ݒ�
    private Vector3 SetTargetPos(int next)
    {
        Vector3 targetPos = rail.GetPosition(next) - tf.position;               //���p�n�_�ƌ��݂̍����Z�o

        //�p�x���Z�o
        float horizontal = Mathf.Atan2(targetPos.normalized.x, targetPos.normalized.z) * Mathf.Rad2Deg;
        float vertical = Mathf.Atan2(Mathf.Sqrt(targetPos.normalized.x * targetPos.normalized.x + targetPos.normalized.z * targetPos.normalized.z), targetPos.normalized.y) * Mathf.Rad2Deg;

        targetAngles = new Vector3(tf.eulerAngles.x , horizontal - 90 , -(vertical) + 90);  //�p�x����
        return targetPos;                                                                                              //���W�̍���Ԃ�
    }
    //��]������
    private void Rolling()
    {
        //�ڕW�p�x�܂ŉ�]������
        rotBuff.x += ComplementingRotationScript.Rotate(rotSpeed,0, rotBuff.x);
        rotBuff.y += ComplementingRotationScript.Rotate(rotSpeed, targetAngles.y, rotBuff.y);
        rotBuff.z += ComplementingRotationScript.Rotate(rotSpeed, targetAngles.z, rotBuff.z);

        tf.eulerAngles = rotBuff;   //�p�x����
    }

    //���C���Z�b�g
    public void SetRail(in LineRenderer rail)
    {
        this.rail = rail;         //���[���Z�b�g
        knot = 0;                //���p�n�_��������
        moveEnd = false;    //�ړ��t���O������
    }

    #region �l�󂯓n��
    public LineRenderer GetRail()
    {
        return rail;
    }
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (!obtainVoluntarilyRail)
        {
            return;
        }
        if (other.CompareTag(railName))
        {
            rail = null;
            rail=other.gameObject.GetComponent<LineRenderer>();
            SetRail(rail);
        }
    }
    
    //������
    public void StartMoveOnRail(Rigidbody rb,Transform tf,float moveSpeed)
    {
        this.rb = rb;
        this.tf = tf;
        this.moveSpeed = moveSpeed;

        rb.velocity = new Vector3(moveSpeed, 0, 0);

        moveEnd = false;
        rotBuff = tf.eulerAngles;
    }

}
