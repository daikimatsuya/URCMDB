using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Y�[������悤�ȓ������Ǘ�
public class ZoomScript : MonoBehaviour
{
    [SerializeField] private bool setInitialPos;//�����l�ɒl�𑫂��Ĉʒu�����炵�Ă��珉���l�܂œ�����
    [SerializeField] private bool zoomIn;
    [SerializeField] private bool zoomOut;

    [SerializeField] private float zoomLength;
    private float lengthBuff;
    [SerializeField] private float zoomTime;
    private int timeBuff;

    Transform tf;

    private float PosBuffZ;
    private Vector3 initialPos;
    //�Y�[���C���A�E�g�Ǘ�
    private void ZoomController()
    {
        if(zoomIn)
        {
            ZoomIn();   //�Y�[���C�����ۂ�������������
        }
        if(zoomOut)
        {
            ZoomOUT();  //�Y�[���A�E�g���ۂ�������������
        }
    }
    //�Y�[���C���̂悤�ȓ�����������
    private void ZoomIn()
    {
        if (setInitialPos)  //�����l�܂œ������ꍇ///////////////////////////////////////////////////
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ -= lengthBuff; //���W���Z
            }
            else
            {
                zoomIn = false; //�ړ��I��
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ + zoomLength);   //���W���
        }////////////////////////////////////////////////////////////////////////////////////////////

        else   //�����l���瓮�����ꍇ//////////////////////////////////////////////////////////////
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ -= lengthBuff; //���W���Z
            }
            else
            {
                zoomIn = false; //�ړ��I��
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ);   //���W���
        }///////////////////////////////////////////////////////////////////////////////////////////
    }
    //�Y�[���A�E�g�̂悤�ȓ�����������
    private void ZoomOUT()
    {
        if (setInitialPos)  //�����l�܂œ������ꍇ///////////////////////////////////////////////////
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ += lengthBuff; //���W���Z
            }
            else
            {
                zoomIn = false; //�ړ��I��
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ - zoomLength);   //���W���
        }////////////////////////////////////////////////////////////////////////////////////////////

        else   //�����l���瓮�����ꍇ//////////////////////////////////////////////////////////////
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ += lengthBuff; //���W���Z
            }
            else
            {
                zoomIn = false; //�ړ��I��
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ);   //���W���
        }///////////////////////////////////////////////////////////////////////////////////////////
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        initialPos = tf.position;
        PosBuffZ = initialPos.z;

        timeBuff = (int)(zoomTime * 60);
        lengthBuff= zoomLength /timeBuff;
    }

    // Update is called once per frame
    void Update()
    {
        ZoomController();
    }
}
