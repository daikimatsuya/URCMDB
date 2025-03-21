using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//EMP�Ǘ��p�X�N���v�g
public class EMPScript : MonoBehaviour
{
    private ExplodeEffectScript ees;

    [SerializeField] private float deployTime;
    [SerializeField] private float firstDissolveValue;
    [SerializeField] private Vector2 offsetSpeed;
    private Vector2 offsetBuff;
    [SerializeField] private Vector2 tilling;

    Rigidbody rb;

    private float explodeTime;
    private bool breakFlag;
    private bool isCharge;
    private float chargeSize;
    private float explodeSize;
    private float deploySize;

    //�I�t�Z�b�g�𑫂�
    private void RotOffset()
    {
        offsetBuff += offsetSpeed;                  //���x�����Z
        ees.SetTillingOffset(tilling, offsetBuff); //���f������
    }

    //EMP����
    public void Explode()
    {
        ees.SizeUp();       //�g��
        ees.Dissolve();     //�f�B�]���u������
        ees.Edge();         //�[�̂Ƃ����f�B�]���u������

        //���ԂŔj��t���O���I���ɂ���
        if (ees.CountDown())
        {
            breakFlag = true;
        }
    }

    //EMP�`���[�W
    public bool Charge()
    {
        //�`���[�W���I����Ă��烊�^�[��
        if (!isCharge)
        {
            return true;
        }

        //���ԂŃ`���[�W�I�����Ĕ����Ɉڍs
        if (ees.CountDown())
        {
            ees.SetTime(explodeTime);        //�����̎��Ԃ��Z�b�g
            ees.SetMaxSize(explodeSize);    //�����̍ő�T�C�Y�Z�b�g
            isCharge = false;                      //�`���[�W�̃t���O���I�t�ɂ���
            return true;
        }

        ees.SizeUp();       //�g��
        ees.Rotation();     //��]
        RotOffset();          //�I�t�Z�b�g���

        return false;
    }

    //EMP�펞�W�J
    public void Deploy()
    {
        ees.SizeUp();           //�g��
        ees.CountDown();    //���Ԍ���
        RotOffset();             //�I�t�Z�b�g���
    }

    //�I�u�W�F�N�g�폜
    public void Break()
    {
        ees.Break();    //�폜
    }

    //������
    public void StartEMP(in bool isDeploy)
    {
        ees=GetComponent<ExplodeEffectScript>();
        ees.StartExplodeEffect();
        rb= GetComponent<Rigidbody>();

        offsetBuff = Vector2.zero;
        breakFlag = false;
        isCharge = true;

        if (isDeploy) 
        {
            this.gameObject.layer = LayerMask.NameToLayer("TypeA");
            ees.SetMaxSize(deploySize);
            ees.SetTime(deployTime);

        }
        else
        {
            this.gameObject.layer = LayerMask.NameToLayer("TypeB");
            ees.SetMaxSize(chargeSize);

        }

    }

    #region �l�󂯓n��
    public void SetChargeTime(float chargeTime)
    {
        ees.SetTime(chargeTime);
    }
    public void SetExplodeTime(float explodeTime)
    {
        this.explodeTime = explodeTime;
    }
    public bool GetBreakFlag()
    {
        return breakFlag;   
    }
    public void SetTillingOffset(Vector2 tilling, Vector2 offset)
    {
        this.tilling = tilling;
        offsetSpeed = offset;
    }
    public void SetSize(float chargeSize,float explodeSize,float deploySize)
    {
        this.chargeSize = chargeSize;
        this.explodeSize = explodeSize;
        this.deploySize = deploySize;
    }
    #endregion
}
