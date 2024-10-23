using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�C���Q�[���J�n���̃J�������o���̉��o
public class MovieFade : MonoBehaviour
{
    [SerializeField] private GameObject upside;
    [SerializeField] private GameObject downside;
    [SerializeField] private float parfectShadeRot;
    [SerializeField] private float movieShadeRot;
    [SerializeField] private float openlyRot;
    [SerializeField] private float moveSpeed;

    private int shadeLevel;
    private Vector3 rotBuff;
    private bool isShade;

    private  Transform upTf;
    private Transform downTf;

    //���̃X�N���v�g�𓮂����֐�
    public void MovieFadeController()
    {

        if(shadeLevel == 0)
        {
            Openly();
        }
        else if(shadeLevel == 1)
        {
            MovieShade();
        }
        else if(shadeLevel == 2)
        {
            ParfectSgade();
        }
        else{
            rotBuff = Vector3.zero;
        }
        SetRot();
    }
    //���S�ɉ�ʂ����тň͂�
    private void ParfectSgade()
    {
        if(rotBuff.x < parfectShadeRot)
        {
            isShade = false;
            rotBuff.x += moveSpeed;
            if(rotBuff.x >= parfectShadeRot)
            {
                rotBuff.x = parfectShadeRot;
                isShade = true;
            }
        }
    }
    //�㉺�ɑт�\������
    private void MovieShade()
    {
        if (rotBuff.x < movieShadeRot)
        {
            isShade = false;
            rotBuff.x += moveSpeed;
            if (rotBuff.x >= movieShadeRot)
            {
                rotBuff.x = movieShadeRot;
            }
        }
        else if (rotBuff.x > movieShadeRot)
        {
            isShade = false;
            rotBuff.x -= moveSpeed;
            if (rotBuff.x <= movieShadeRot)
            {
                rotBuff.x = movieShadeRot;
            }
        }
    }
    //���S�ɑт��\���ɂ���
    private void Openly()
    {
        if (rotBuff.x < parfectShadeRot)
        {
            rotBuff.x += moveSpeed;
            if (rotBuff.x >= parfectShadeRot)
            {
                rotBuff.x = parfectShadeRot;
            }
        }
    }
    
    //�т̃��x�����擾
    public void SetShadeLevel(int level)
    {
        shadeLevel = level;
    }
    //�B��Ă��邩�ǂ������擾
    public bool GetShade()
    {
        return isShade;
    }
    //�l���g�����X�t�H�[���ɑ��
    private void SetRot()
    {
        upTf.localEulerAngles = rotBuff;
        downTf.localEulerAngles = -rotBuff;
    }
    // Start is called before the first frame update
    void Start()
    {
        upTf = upside.GetComponent<Transform>();
        downTf = downside.GetComponent<Transform>();
        shadeLevel = 1;
        rotBuff = upTf.localEulerAngles;
        isShade = false;

        if (upside)
        {
            moveSpeed *= 1;
        }
        else
        {
            moveSpeed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovieFadeController();
    }
}
