using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������I�u�W�F�N�g���㉺�ɓ�����
public class ButtobeScript : MonoBehaviour
{
    [SerializeField] private float maxHight;
    [SerializeField] private float moveSpeed;

    Transform tf;

    private float initialPosY;
    private float posBuff;
    private bool isUp;
    //�㉺�Ɉړ�
    private void Move()
    {
        //�㉺�ړ����J��Ԃ�////////////////
        if(isUp)
        {
            posBuff += moveSpeed;

            if(posBuff>maxHight)    //�ő�l�܂ŏオ��ƈړ������؂�ւ�//
            {
                posBuff = maxHight;
                isUp = false;
            }//////////////////////////////////////////////////////////////////
        }
        else
        {
            posBuff -= moveSpeed;

            if(posBuff<-maxHight)   //�Œ�l�܂ŉ�����ƈړ������؂�ւ�//
            {
                posBuff=-maxHight;
                isUp = true;
            }//////////////////////////////////////////////////////////////////
        }
        //////////////////////////////////
        tf.localPosition = new Vector3(tf.localPosition.x, initialPosY + posBuff, tf.localPosition.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        initialPosY=tf.localPosition.y;
        isUp=true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();  
    }
}
