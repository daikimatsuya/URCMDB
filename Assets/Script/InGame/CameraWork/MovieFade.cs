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

        //�㉺�̑т̉��o�̈ʒu����
        if (shadeLevel == 0)
        {
            Openly();   //���S�ɊJ��
        }
        else if(shadeLevel == 1)
        {
            MovieShade();   //�㉺�ɑт݂����Ɏc��
        }
        else if(shadeLevel == 2)
        {
            ParfectShade(); //���S�ɉ�ʂ𕢂�
        }
        else
        {
            isEffectEnd = true;
            Nothing();  //���o�I��
        }
        ///////////////////////////
        SetPos();
    }
    //���S�ɉ�ʂ����тň͂�
    private void ParfectShade()
    {
        //���o�p�̃I�u�W�F�N�g�𒆐S�ɋ߂Â���
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
        ///////////////////////////////////////
    }
    //�㉺�ɑт�\������
    private void MovieShade()
    {
        isShade = false;

        //���ɂ���Ă������ɂ��炷
        if (posBuff.y < movieShadePos)
        {
            posBuff.y += moveSpeed;
            if (posBuff.y >= movieShadePos)
            {
                posBuff.y = movieShadePos;
            }
        }
        ////////////
     
        //��ɂ���Ă����牺�ɂ��炷
        else if (posBuff.y > movieShadePos)
        {

            posBuff.y -= moveSpeed;
            if (posBuff.y <= movieShadePos)
            {
                posBuff.y = movieShadePos;
            }
        }
        ///////////
    }
    //���S�ɑт��\���ɂ���
    private void Openly()
    {
        isShade = false;

        //��ʊO�Ɍ������Ă��炷
        if (posBuff.y > parfectShadePos)
        {
            posBuff.y -= moveSpeed;
            if (posBuff.y <= parfectShadePos)
            {
                posBuff.y = parfectShadePos;

                isEffectEnd = true;
            }
        }
        ////////////////////////
    }
    //���o�Ȃ��S�J�ɂ���
    private void Nothing()
    {
        //�l���
        if (upside)
        {
            posBuff.y = openlyPos;
        }
        else
        {
            posBuff.y = -openlyPos;
        }
        ////////
        isShade = true;
    }

    //�l���g�����X�t�H�[���ɑ��
    private void SetPos()
    {
        upTf.localPosition = posBuff;
        downTf.localPosition = -posBuff;
    }

    #region �l�󂯓n��
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
    #endregion

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
