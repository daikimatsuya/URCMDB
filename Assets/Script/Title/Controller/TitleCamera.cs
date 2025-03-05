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
    private bool isCameraMove;

    public void CameraController(in bool isStageSelect)
    {
        if (!isStageSelect)
        {
            return;
        }
        FlagCheck();
        Move();
        
    }
    public void FlagCheck()
    {
        if (!moveEnd) 
        {
            return;
        }
        if (!Input.GetKeyDown(KeyCode.Space) && !Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            return;
        }
        if (isCameraMove)
        {
            isCameraMove = false;
        }
        else
        {
            isCameraMove = true;
        }
    }
    //�ړ�
    public void Move()
    {
        Vector3 dis;

        if (isCameraMove)  //�X�e�[�W�Z���N�g�t���O���I���ɂȂ�Ɖ��Ɉړ�/////////////////////////////////////////////////////
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
                tf.position = new Vector3(movePos.x, movePos.y,tf.position.z);  //���W����
                moveEnd = true;                                                                    //moveEnd�t���O��true

            }                       
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        else�@//�X�e�[�W�Z���N�g�t���O���I�t�ɂȂ�Ə�Ɉړ�/////////////////////////////////////////////////////////
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
                tf.position = new Vector3(firstPos.x, firstPos.y,tf.position.z);  //���W����
                moveEnd = true;                                                               //moveEnd�t���O��true

            }
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    #region �l�󂯓n��
    public bool GetCanShot()
    {
        if (moveEnd && isCameraMove)
        {
            return true;
        }
        return false;
    }
    #endregion
    // Start is called before the first frame update
    public void StartTitleCamera()
    {
        tf = GetComponent<Transform>();
        TimeCountScript.SetTime(ref moveTime, moveTime);
        firstPos = tf.position;
        isCameraMove = false;
    }

}
