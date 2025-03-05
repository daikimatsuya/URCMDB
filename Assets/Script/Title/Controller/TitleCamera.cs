using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Usefull;

//�^�C�g���V�[���ł̃J�����̓����Ǘ�
public class TitleCamera : MonoBehaviour
{    
    [SerializeField] private Vector3 movePos;
    [SerializeField] private float moveTime;
    [SerializeField] private float zoomSpeed;

    Transform tf;

    private float time;
    private bool moveEnd;
    private Vector3 firstPos;
    private bool isMoveDown;

    //�^�C�g���J���������Ǘ�
    public void CameraController(in bool isStageSelect)
    {
        if (!isStageSelect)
        {
            return;
        }
        FlagCheck();    //�ړ��\�����m�F
        Move();           //�J�����ړ�
        
    }

    //�ړ��\�t���O�`�F�b�N
    public void FlagCheck()
    {
        if (!moveEnd) 
        {
            return; //�ړ����������烊�^�[��
        }
        if (!Input.GetKeyDown(KeyCode.Space) && !Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            return; //�{�^����������Ă��Ȃ������烊�^�[��
        }
        if (isMoveDown)
        {
            isMoveDown = false; //�������Ɉړ�
        }
        else
        {
            isMoveDown = true;  //�ォ�牺�Ɉړ�
        }
    }

    //�ړ�
    public void Move()
    {
        Vector3 dis;

        //���ֈړ�
        if (isMoveDown)  
        {
            float x = 1 - Mathf.Pow(1 - (time / moveTime), 3);
            if (time < moveTime)
            {           
                dis = movePos - firstPos;                                                                                             //�����Z�o
                tf.position = new Vector3(firstPos.x + (dis.x * x), firstPos.y + (dis.y * x),tf.position.z);    //���X�Ɉړ�������
                moveEnd = false;                                                                                                       //moveEnd�t���O��false

                time++;
            }
            else
            {
                tf.position = new Vector3(movePos.x, movePos.y,tf.position.z);           //���W����
                moveEnd = true;                                                                             //moveEnd�t���O��true

            }                       
        }

        //��ֈړ�
        else�@
        {
            float x = Mathf.Pow((time / moveTime), 3);
            if (time>0)
            {
                dis = movePos - firstPos;                                                                                            //�����Z�o
                tf.position = new Vector3(firstPos.x - (dis.x * x), firstPos.y + (dis.y * x), tf.position.z);   //���X�Ɉړ�������
                moveEnd = false;                                                                                                      //moveEnd�t���O��false

                time--;
            }
            else
            {
                tf.position = new Vector3(firstPos.x, firstPos.y,tf.position.z);               //���W����
                moveEnd = true;                                                                            //moveEnd�t���O��true

            }
        }
    }

    #region �l�󂯓n��

    //�ړ����Ă������̃t���O�`�F�b�N
    public bool GetCanShot()
    {
        if (moveEnd && isMoveDown)
        {
            return true;
        }
        return false;
    }
    #endregion

    //�^�C�g���J����������
    public void StartTitleCamera()
    {
        tf = GetComponent<Transform>();
        TimeCountScript.SetTime(ref moveTime, moveTime);
        firstPos = tf.position;
        isMoveDown = false;
    }

}
