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

    private void RotOffset()
    {
        offsetBuff += offsetSpeed;
        ees.SetTillingOffset(tilling, offsetBuff);
    }

    //EMP����
    public void Explode()
    {
        ees.SizeUp();
        ees.Dissolve();
        ees.Edge();
        if (ees.CountDown())
        {
            breakFlag = true;
        }
    }

    //EMP�`���[�W
    public bool Charge()
    {
        if (!isCharge)
        {
            return true;
        }
        if (ees.CountDown())
        {
            ees.SetTime(explodeTime);
            ees.SetMaxSize(explodeSize);
            isCharge = false;
            return true;
        }

        ees.SizeUp();
        ees.Rotation();
        RotOffset();

        return false;
    }

    //EMP�펞�W�J
    public void Deploy()
    {
        ees.SizeUp();
        ees.CountDown();
        RotOffset();
    }

    //�I�u�W�F�N�g�폜
    public void Break()
    {
        ees.Break();
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
