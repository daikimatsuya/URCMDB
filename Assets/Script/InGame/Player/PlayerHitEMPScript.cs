using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SensorScript;

//�v���C���[��EMP�ɂ����������̏���
public class PlayerHitEMPScript : MonoBehaviour
{
    [SerializeField] private float empLevel;
    [SerializeField] private float dicreaseEMP;
    [SerializeField] private float shockPower;
    [SerializeField] private float zonePower;
    [SerializeField] private float maxEMP;

    //�e�����󂯂Ă��邩
    public int EMPAfect()
    {

        if(empLevel > 0)
        {
            return -1;
        }
        return 1;
    }

    //�e������
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

    //�g�ɂ�����
    public void HitEMPShock()
    {
        empLevel += shockPower;
    }
    //�G���A�ɓ���
    public void HitEMPZone(in bool isHit)
    {
        if (isHit)
        {
            empLevel += zonePower;
        }
    }
    //������
    public void StartPlayerHitEMP()
    {
        empLevel = 0;
    }
}
