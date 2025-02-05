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

    private TitleScript ts;
    Transform tf;

    private float time;
    private bool moveEnd;
    private Vector3 firstPos;

    //�J�����𓮂���
    private void TitleCameraController()
    {
        Move(ts.GetIsStageSelect());    //�㉺�ړ�
    }
    //�ړ�
    private void Move(bool isStageSelect)
    {
        Vector3 dis;

        if (isStageSelect)  //�X�e�[�W�Z���N�g�t���O���I���ɂȂ�Ɖ��Ɉړ�/////////////////////////////////////////////////////
        {
            float x = 1 - Mathf.Pow(1 - (time / moveTime), 3);
            if (time < moveTime)
            {           
                dis = movePos - firstPos;                                                                                             //�����Z�o
                tf.position = new Vector3(firstPos.x + (dis.x * x), firstPos.y + (dis.y * x),tf.position.z);    //���X�Ɉړ�������
                moveEnd = false;                                                                                                       //moveEnd�t���O��false
                ts.SendMoveEnd(moveEnd);                                                                                        //�t���O���
                time++;
            }
            else
            {
                tf.position = new Vector3(movePos.x, movePos.y,tf.position.z);  //���W����
                moveEnd = true;                                                                    //moveEnd�t���O��true
                ts.SendMoveEnd(moveEnd);                                                    //�t���O���
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
                ts.SendMoveEnd(moveEnd);                                                                                       //�t���O���
                time--;
            }
            else
            {
                tf.position = new Vector3(firstPos.x, firstPos.y,tf.position.z);  //���W����
                moveEnd = true;                                                               //moveEnd�t���O��true
                ts.SendMoveEnd(moveEnd);                                               //�t���O���
            }
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    // Start is called before the first frame update
    public void StartTitleCamera()
    {
        ts = GameObject.FindWithTag("TitleManager").GetComponent<TitleScript>();
        tf = GetComponent<Transform>();
        TimeCountScript.SetTime(ref moveTime, moveTime);
        firstPos = tf.position;
    }
    void Start()
    {
        StartTitleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        TitleCameraController();
    }
}
