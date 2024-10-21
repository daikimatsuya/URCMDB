using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�C���Q�[�����n�܂������̃J�������[�N�̏���
public class MovieCamera : MonoBehaviour
{
    [SerializeField] private int knotNumber;
    [SerializeField] private Vector3[] targetPosition;
    private Vector3 targetPos;
    [SerializeField] private Vector3[] startPosition;
    [SerializeField] private Vector3[] targetRotation;
    private Vector3 targetRot;
    [SerializeField] private Vector3[] startRotation;
    [SerializeField] private float[] moveTime;
    private int moveTimeBuff;

    private bool ready;
    [SerializeField] private bool isMove;
    private int number;
    private Vector3 posBuff;
    private Vector3 rotBuff;
    private Vector3 posRange;
    private Vector3 rotRange;
    private Vector3 moveSpeed;
    private Vector3 RotSpeed;

    Transform tf;
    //�J�����𓮂����֐�
    public void CameraController()
    {
        Move();    
        SetNext();
    }
    //�ړ�������֐�
    private void Move()
    {
        if (isMove)
        {
            if (moveTimeBuff > 0)
            {
                posBuff += moveSpeed;
                rotBuff += RotSpeed;

                SetTransform();
            }
            else
            {
                ready = false;
                isMove = false;
            }
            moveTimeBuff--;
        }

    }
    //���Ɉړ����邽�߂ɕK�v�Ȃ��̂���������
    private void SetNext()
    {
        if (!ready)
        {
            if (knotNumber > number)
            {
                posBuff = startPosition[number];
                rotBuff = startRotation[number];

                targetPos = targetPosition[number];
                targetRot = targetRotation[number];

                TimeCountScript.SetTime(ref moveTimeBuff, moveTime[number]);

                posRange = targetPos - posBuff;
                rotRange = targetRot - rotBuff;

                moveSpeed = posRange / moveTimeBuff;
                RotSpeed = rotRange / moveTimeBuff;
    
               // SetTransform();

                number++;
                ready = true;
                //isMove = true;
            }
            else
            {

            }
        }
    }
    private void SetTransform()
    {
        tf.position = posBuff;
        tf.eulerAngles=rotBuff;
    }
    // Start is called before the first frame update
    void Start()
    {
        ready = false;
        tf= GetComponent<Transform>();
        number = 0;
        SetNext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
