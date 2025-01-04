using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�C���Q�[�����n�܂������̃J�������[�N�̏���
public class MovieCamera : MonoBehaviour
{
    [System.Serializable]
    public class MovieCameraElement
    {
        [SerializeField] public Vector3 startPosition;
        [SerializeField] public Vector3 startRotation;
        [SerializeField] public Vector3 targetPosition;
        [SerializeField] public Vector3 targetRotation;

        [SerializeField] public float moveTime;
    }

    [SerializeField] private MovieCameraElement[] elements;
    [SerializeField] private int knotNumber;
    private Vector3 targetPos;
    private Vector3 targetRot;
    private int moveTimeBuff;
    [SerializeField] private float fadeoutTime;



    private bool ready;
    private bool isMove;
    private int number;
    private Vector3 posBuff;
    private Vector3 rotBuff;
    private Vector3 posRange;
    private Vector3 rotRange;
    private Vector3 moveSpeed;
    private Vector3 RotSpeed;
    private int shadelevel;
    private bool isSkip;
    private bool isEnd;

    Transform tf;
    //�J�����𓮂����֐�
    public void CameraController()
    {
        Move();    //�ړ����o
        SetNext();  //���̍��W��ݒ�
    }
    //�ړ�������֐�
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //���o�X�L�b�v
            isSkip = true;
        }
        if (isMove) //���o������/////////////////////////////////////////////////////
        {
            if (TimeCountScript.TimeCounter(ref moveTimeBuff))
            {
                //���W�ړ�
                posBuff += moveSpeed;
                rotBuff += RotSpeed;

                SetTransform();//�|�W�V�����ƃ��[�e�[�V�������g�����X�t�H�[���ɑ��
            }
            else
            {
                //�I���t���O�Z�b�g
                ready = false;
                isMove = false;
            }
        }////////////////////////////////////////////////////////////////////////////

    }
    //���Ɉړ����邽�߂ɕK�v�Ȃ��̂���������
    private void SetNext()
    {
        if (!ready) //���o�������o���Ă��Ȃ���////////////////////////////////////////////////////////////////////////////////
        {
            if (knotNumber > number)    //�ݒ肵�����o���ȓ��Ȃ�///////////////////////////////////////
            {
                //�����l�ƖڕW�l��ݒ�
                posBuff = elements[number].startPosition;
                rotBuff = elements[number].startRotation;

                targetPos = elements[number].targetPosition;
                targetRot = elements[number].targetRotation;
                ///////////////////////

                //���o���ԃZ�b�g
                TimeCountScript.SetTime(ref moveTimeBuff, elements[number].moveTime);

                //�ړ����x�Z�o
                posRange = targetPos - posBuff;
                rotRange = targetRot - rotBuff;

                moveSpeed = posRange / moveTimeBuff;
                RotSpeed = rotRange / moveTimeBuff;
                /////////////

                //�t���O�ރZ�b�g
                number++;
                ready = true;
                isMove = true;
                ////////////////

            }///////////////////////////////////////////////////////////////////////////////////////////////

            else   //�ݒ肵���񐔉��o���I�����///
            {
                isEnd = true;   //���o�I���t���O�I��               
            }//////////////////////////////////////

        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    private void SetTransform()
    {
        tf.position = posBuff;
        tf.eulerAngles=rotBuff;
    }
    #region �l�󂯓n��
    public bool GetEnd()
    {
        return  isEnd;
    }
    public bool GetSkip()
    {
        return isSkip;
    }
    public float GetMoveTime()
    {
        return moveTimeBuff;
    }
    public float GetFadeoutTime()
    {
        return fadeoutTime;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        isSkip = false;
        ready = false;
       
        tf= GetComponent<Transform>();
        number = 0;
        SetNext();
        isMove = true;
        isEnd = false;
        fadeoutTime *= 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
