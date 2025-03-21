using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//EMP管理用スクリプト
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

    //オフセットを足す
    private void RotOffset()
    {
        offsetBuff += offsetSpeed;                  //速度分加算
        ees.SetTillingOffset(tilling, offsetBuff); //反映させる
    }

    //EMP爆発
    public void Explode()
    {
        ees.SizeUp();       //拡大
        ees.Dissolve();     //ディゾルブさせる
        ees.Edge();         //端のとこもディゾルブさせる

        //時間で破壊フラグをオンにする
        if (ees.CountDown())
        {
            breakFlag = true;
        }
    }

    //EMPチャージ
    public bool Charge()
    {
        //チャージが終わってたらリターン
        if (!isCharge)
        {
            return true;
        }

        //時間でチャージ終了して爆発に移行
        if (ees.CountDown())
        {
            ees.SetTime(explodeTime);        //爆発の時間をセット
            ees.SetMaxSize(explodeSize);    //爆発の最大サイズセット
            isCharge = false;                      //チャージのフラグをオフにする
            return true;
        }

        ees.SizeUp();       //拡大
        ees.Rotation();     //回転
        RotOffset();          //オフセット代入

        return false;
    }

    //EMP常時展開
    public void Deploy()
    {
        ees.SizeUp();           //拡大
        ees.CountDown();    //時間減少
        RotOffset();             //オフセット代入
    }

    //オブジェクト削除
    public void Break()
    {
        ees.Break();    //削除
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
