using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SensorScript;

//プレイヤーがEMPにあたった時の処理
public class PlayerHitEMPScript : MonoBehaviour
{
    [SerializeField] private float empLevel;
    [SerializeField] private float dicreaseEMP;
    [SerializeField] private float shockPower;
    [SerializeField] private float zonePower;
    [SerializeField] private float maxEMP;

    //影響を受けているか
    public int EMPAfect()
    {

        if(empLevel > 0)
        {
            return -1;
        }
        return 1;
    }

    //影響減少
    public void Dicrease(in bool isHit)
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
    public void HitEMPZone(in bool isHit)
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
    }
}
