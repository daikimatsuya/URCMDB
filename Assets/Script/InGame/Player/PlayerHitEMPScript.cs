using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SensorScript;

//プレイヤーがEMPにあたった時の処理
public class PlayerHitEMPScript : MonoBehaviour
{
    [SerializeField] private GameObject empEffect;
    [SerializeField] private float empLevel;
    [SerializeField] private float dicreaseEMP;
    [SerializeField] private float shockPower;
    [SerializeField] private float zonePower;
    [SerializeField] private float maxEMP;

    //EMP関係管理
    public void EMPAffectController(in bool EMPHit)
    {
        Dicrease(in EMPHit);        //EMPの影響減少
        HitEMPZone(in EMPHit);   //EMPエリア値増加
    }
    //影響を受けているか
    public int EMPAfect()
    {
        if(empLevel > 0)
        {
            EMPAffect(true);
            return -1;
        }
        EMPAffect(false);
        return 1;
    }

    //EMP状態のエフェクトオンオフ切り替え
    private void EMPAffect(in bool flag)
    {
        empEffect.SetActive(flag);
    }

    //影響減少
    private void Dicrease(in bool isHit)
    {
        if (!isHit)
        {
            if (empLevel > maxEMP)
            {
                empLevel = maxEMP;
            }
            if (empLevel > 0)
            {
                empLevel -= dicreaseEMP;
                if (empLevel < 0)
                {
                    empLevel = 0;
                }
            }
        }

    }

    //波にあたる
    public void HitEMPShock()
    {
        empLevel += shockPower;
    }
    //エリアに入る
    private void HitEMPZone(in bool isHit)
    {
        if (isHit)
        {
            empLevel += zonePower;
        }
    }
    //初期化
    public void StartPlayerHitEMP()
    {
        empLevel = 0;
        EMPAffect(false);
    }
}
