using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//EMP管理用スクリプト
public class EMPScript : MonoBehaviour
{
    private ExplodeEffectScript ees;

    [SerializeField] private float chargeSize;
    [SerializeField] private float explodeSize;
    [SerializeField] private float deploySize;
    [SerializeField] private float deployTime;
    [SerializeField] private float firstDissolveValue;
    [SerializeField] private Vector2 offsetSpeed;
    private Vector2 offsetBuff;
    [SerializeField] private Vector2 tilling;

    Rigidbody rb;

    private float explodeTime;
    private bool breakFlag;
    private bool isCharge;

    //EMP爆発
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

    //EMPチャージ
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

        ees.Rotation();
        ees.SetTillingOffset(Vector2.one, Vector2.zero);

        return false;
    }

    //EMP常時展開
    public void Deploy()
    {
        ees.SizeUp();
        ees.CountDown();
        offsetBuff += offsetSpeed;
        ees.SetTillingOffset(tilling, offsetBuff);
    }

    //オブジェクト削除
    public void Break()
    {
        ees.Break();
    }

    //初期化
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

            TimeCountScript.SetTime(ref deployTime, deployTime);

            ees.SetMaxSize(deploySize);
            ees.SetTime(deployTime);

        }
        else
        {
            this.gameObject.layer = LayerMask.NameToLayer("TypeB");

            ees.SetMaxSize(chargeSize);

        }

    }


    #region 値受け渡し
    public void SetChargeTime(float chargeTimeBuff)
    {
        ees.SetTime(chargeTimeBuff);
    }
    public void SetExplodeTime(float explodeTime)
    {
        this.explodeTime = explodeTime;
    }
    public bool GetBreakFlag()
    {
        return breakFlag;   
    }
    #endregion
}
