using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Usefull;

//�H���Ǘ�
public class SyusuiScript : MonoBehaviour
{
    Transform tf;
    Rigidbody rb;

    [SerializeField] private float speed;
    [SerializeField] private Vector2 rowSpeed;
    [SerializeField] private float normalPosY;
    [SerializeField] private float normalPosRange;
    [SerializeField] private float minDis;
    [SerializeField] private float maxDis;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float brake;
    [SerializeField] private float maxBaseDis;
    [SerializeField] private float minBaseDis;
    [SerializeField] private int backTimer;

    private Vector3 moveSpeed;
    private bool isSearch;
    private Transform playerPos;
    private PlayerScript playerScript;
    private Transform basePos;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;
    private Vector3 Row;
    private bool isLeftBase;
    private int timeBuff;

    //�@�\�Ǘ�
    private void SyusuiController()
    {
        Chase();    //�ǐ�
        Move();     //�ړ�
    }
    //�ړ�
    private void Move()
    {
        Vector3 anglesBuff = tf.eulerAngles;    

        //���ʂ̑��x�Z�o
        moveSpeed.x = speed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.y));
        moveSpeed.z = speed * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.y));
        /////////////////

        //���������Ɛ��������̑��x�Z�o
        moveSpeed.x = moveSpeed.x * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
        moveSpeed.z = moveSpeed.z * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
        moveSpeed.y = speed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.x)) * -1;
        /////////////////////////////////

        rb.velocity = moveSpeed;    //���x���
    }
    //�͈͓��Ƀv���C���[��������ǐՂ���
    private void Chase()
    {
        if (playerScript == null)   //�v���C���[�����Ȃ�������ǐՒ�~//
        {
            isSearch = false; 
        }/////////////////////////////////////////////////////////////////
        if (isSearch)
        {
            Aim();        //�v���C���[�̕�������
            KeepDis();  //���������ɕۂ�
        }
        else
        {
           LeftBase();  //���S�ɖ߂�
        }
    }
    //�v���C���[�̂���������擾
    private void Aim()
    {
        playerDis = playerPos.position - tf.position;                                                                                                                                                                           //�������Z�o
        playerDisNormal = playerDis.normalized;                                                                                                                                                                              //�m�[�}���C�Y��
        float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;                                                                                                          //���������p�x�Z�o
        float vertical = Mathf.Atan2(Mathf.Sqrt(playerDisNormal.x * playerDisNormal.x + playerDisNormal.z * playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg; //���������p�x�Z
        vertical -= 90;                                                                                                                                                                                                                    //�l���C��
        Rowring(horizontal, vertical);                                                                                                                                                                                               //��]������
        tf.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);                                                                                                                                                       //�p�x���
    }
    //�v���C���[���痣�ꂸ�߂��������ێ�
    private void KeepDis()
    {
        if (playerDis.magnitude < minDis)   //�߂������猸������///
        {
            if(speed>minSpeed)
            {
                speed -= brake;
            }            
        }//////////////////////////////////////////////////////////////

        if(playerDis.magnitude > maxDis)    //�����������������//////
        {
            if(speed<maxSpeed)
            {
                speed += brake;
            }           
        }//////////////////////////////////////////////////////////////////
    }
    //�v���C���[��ǐՂ��Ă��Ȃ��Ƃ��̋���
    private void NormalOperation()
    {
        //�p�x�Œ�
        float horizontal = Row.y;
        float vertical = 0;
        ///////////

        //���S�_���N�_�ɂ��邮�邳����
        if (tf.position.y < normalPosY - normalPosRange)
        {
            vertical -= rowSpeed.x*20;
        }
        if (tf.position.y > normalPosY + normalPosRange)
        {
            vertical += rowSpeed.x*20;
        }
        horizontal += rowSpeed.y / 10;
        Rowring(horizontal, vertical);
        tf.localEulerAngles = Row;
        //////////////////////////////
    }
    //���_�����萔���ꂽ��߂�
    private void LeftBase()
    {
        Vector2 dis = new Vector2(basePos.position.x - tf.position.x,basePos.position.z-tf.position.z); //���S�Ƃ̋����Z�o
        float dis2=dis.magnitude;                                                                                                  //������float�ɕϊ�

        if (dis2 > maxBaseDis)  //���e�l���������ƈ����Ԃ�////
        {
            if(!isLeftBase)
            {
                isLeftBase = true;
                timeBuff = backTimer * 60;
            }
        }/////////////////////////////////////////////////////////////

        if (dis2 <= minBaseDis) //���S�t�߂܂ŗ���Ƃ܂��L���肾��///
        {
            isLeftBase = false;
        }//////////////////////////////////////////////////////////////////

        if(isLeftBase)  //���S�n�_�֖߂�/////////////////////////////////////////////////////////
        { 
            Vector3 disVector= new Vector3(dis.x, 0, dis.y).normalized;
            float horizontal = Mathf.Atan2(disVector.x, disVector.z) * Mathf.Rad2Deg;

            //���e���Ԃ��z��������Z�������ɕύX
            if(timeBuff > 0)
            {
                Rowring(horizontal, Row.x);
            }
            else
            {
                Row.y = horizontal;
            }
            ////////////////////////////////////////

            tf.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);

            timeBuff--;
        }////////////////////////////////////////////////////////////////////////////////////////
        else
        {
            NormalOperation();  //�ʏ�ړ�
        }
    }
    //��]
    private void Rowring(float horizontal,float vertical)
    {
        //����������]�p�J��/////////////
        if (horizontal - Row.y > 0)
        {
            Row.y += rowSpeed.y;
            if (horizontal - Row.y < 0)
            {
                Row.y = horizontal;
            }
        }
        if (horizontal - Row.y < 0)
        {
            Row.y -= rowSpeed.y;
            if (horizontal - Row.y > 0)
            {
                Row.y = horizontal;
            }
        }
        /////////////////////////////////

        //����������]�p�J��////////////
        if (vertical - Row.x > 0)
        {
            Row.x += rowSpeed.x;
            if (vertical - Row.x < 0)
            {
                Row.x = vertical;
            }
        }
        else if (vertical - Row.x < 0)
        {
            Row.x -= rowSpeed.x;
            if (vertical - Row.x > 0)
            {
                Row.x = vertical;
            }
        }
        ////////////////////////////////
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isSearch = true;
            playerPos = other.transform;
            playerScript = other.GetComponent<PlayerScript>();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isSearch = false;
        }
    }
    // Start is called before the first frame update
    //������
    public void StartSyusui()
    {
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        basePos = GameObject.FindWithTag("AirBase").GetComponent<Transform>();

        Row = tf.localEulerAngles;
        isSearch = false;
    }
    void Start()
    {
        StartSyusui();
    }

    // Update is called once per frame
    void Update()
    {
        SyusuiController();  
    }
}
