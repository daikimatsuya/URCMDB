using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.Editor;
using UnityEngine;

//�C���Q�[���J�n���̃J�������o���̉��o
public class MovieFade : MonoBehaviour
{
    [SerializeField] private bool upside;
    [SerializeField] private float parfectShadePos;
    [SerializeField] private float movieShadePos;
    [SerializeField] private float openlyPos;
    [SerializeField] private float moveSpeed;

    private int shadeLevel;

    //���̃X�N���v�g�𓮂����֐�
    private void MovieFadeController()
    {
        if(shadeLevel == 0)
        {
            Openly();
        }
        else if(shadeLevel == 1)
        {
            MovieShade();
        }
        else
        {
            ParfectSgade();
        }
    }
    //���S�ɉ�ʂ����тň͂�
    private void ParfectSgade()
    {

    }
    //�㉺�ɑт�\������
    private void MovieShade()
    {

    }
    //���S�ɑт��\���ɂ���
    private void Openly()
    {

    }
    //�т̃��x�����擾
    public void SetShadeLevel(int level)
    {
        shadeLevel = level;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(upside)
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
