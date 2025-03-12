using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//EMP�Ǘ��p�X�N���v�g
public class EMPScript : MonoBehaviour
{
    private ExplodeEffectScript ees;

    [SerializeField] private float chargeSize;
    [SerializeField] private float explodeSize;
    [SerializeField] private float deploySize;
    [SerializeField] private float explodeTime;
    [SerializeField] private float chargeTime;
    [SerializeField] private float deployTime;
    [SerializeField] private float firstDissolveValue;
    [SerializeField] private Vector2 offsetSpeed;
    private Vector2 offsetBuff;
    [SerializeField] private Vector2 tilling;

    Rigidbody rb;

    //EMP����
    public void Explode()
    {
        ees.SizeUp();
        ees.Dissolve();
        ees.Edge();
        if (ees.CountDown())
        {
            ees.Break();
        }
    }

    //EMP�`���[�W
    public bool Charge()
    {
        if (ees.CountDown())
        {
            ees.SetTime(explodeTime);
            ees.SetMaxSize(explodeSize);
            return true;
        }

        ees.Rotation();
        ees.SetTillingOffset(Vector2.zero, Vector2.zero);

        return false;
    }

    //EMP�펞�W�J
    public void Deploy()
    {
        ees.SizeUp();
        ees.CountDown();

        offsetBuff += offsetSpeed;
        ees.SetTillingOffset(tilling, offsetBuff);
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

        if (isDeploy) 
        {
            rb.mass = 3.0f;

            TimeCountScript.SetTime(ref deployTime, deployTime);

            ees.SetMaxSize(deploySize);
            ees.SetTime(deployTime);
        }
        else
        {
            rb.mass = 0.0f;

            TimeCountScript.SetTime(ref chargeTime, chargeTime);
            TimeCountScript.SetTime(ref explodeTime, explodeTime);

            ees.SetMaxSize(chargeSize);
            ees.SetTime(chargeTime);
        }

    }


}
