using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�C���Q�[���J�n���̃J�������o���̉��o
public class MovieFade : MonoBehaviour
{
    [SerializeField] private GameObject upside;
    [SerializeField] private GameObject downside;
    [SerializeField] private float parfectShadePos;
    [SerializeField] private float movieShadePos;
    [SerializeField] private float openlyPos;
    [SerializeField] private float moveSpeed;

    private int shadeLevel;
    private Vector3 posBuff;
    private bool isShade;
    private bool isEffectEnd;

    private  Transform upTf;
    private Transform downTf;

    //���̃X�N���v�g�𓮂����֐�
    public void MovieFadeController()
    {

        isEffectEnd = false;

        if (shadeLevel == 0)
        {
            Openly();
        }
        else if(shadeLevel == 1)
        {
            MovieShade();
        }
        else if(shadeLevel == 2)
        {
            ParfectShade();
        }
        else{
            isEffectEnd = true;
            Nothing();
        }
        SetPos();
    }
    //���S�ɉ�ʂ����тň͂�
    private void ParfectShade()
    {
        if(posBuff.y > parfectShadePos)
        {
            isShade = false;
            posBuff.y -= moveSpeed;
            if(posBuff.y <= parfectShadePos)
            {
                posBuff.y = parfectShadePos;
                isShade = true;
            }
        }
    }
    //�㉺�ɑт�\������
    private void MovieShade()
    {
        isShade = false;
        if (posBuff.y < movieShadePos)
        {
            posBuff.y += moveSpeed;
            if (posBuff.y >= movieShadePos)
            {
                posBuff.y = movieShadePos;
            }
        }
        else if (posBuff.y > movieShadePos)
        {

            posBuff.y -= moveSpeed;
            if (posBuff.y <= movieShadePos)
            {
                posBuff.y = movieShadePos;
            }
        }
    }
    //���S�ɑт��\���ɂ���
    private void Openly()
    {
        isShade = false;
        if (posBuff.y > parfectShadePos)
        {
            posBuff.y -= moveSpeed;
            if (posBuff.y <= parfectShadePos)
            {
                posBuff.y = parfectShadePos;

                isEffectEnd = true;
            }
        }
    }
    //���o�Ȃ��S�J�ɂ���
    private void Nothing()
    {
        if (upside)
        {
            posBuff.y = openlyPos;
        }
        else
        {
            posBuff.y = -openlyPos;
        }
        isShade = true;
    }
    
    //�т̃��x�����擾
    public void SetShadeLevel(int level)
    {
        shadeLevel = level;
    }
    //���o���I��������ǂ������擾
    public bool GetEffectEnd()
    {
        return isEffectEnd;
    }
    //���S�ɉB��Ă��邩�ǂ������擾
    public bool GetIsShade()
    {
        return isShade;
    }
    //�l���g�����X�t�H�[���ɑ��
    private void SetPos()
    {
        upTf.localPosition = posBuff;
        downTf.localPosition = -posBuff;
    }
    // Start is called before the first frame update
    void Start()
    {
        upTf = upside.GetComponent<Transform>();
        downTf = downside.GetComponent<Transform>();
        shadeLevel = 1;
        posBuff = upTf.localPosition;
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
        //MovieFadeController();
    }
}
