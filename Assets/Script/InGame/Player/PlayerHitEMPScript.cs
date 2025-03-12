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
    private bool isHit;
    private int d = 0;
    private int h = 0;

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
    public void Dicrease(ref bool isHit)
    {
        d++;
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
            isHit = false;
        }
        else {
            isHit = false;
        }
    }

    //�g�ɂ�����
    public void HitEMPShock()
    {
        empLevel += shockPower;
    }
    //�G���A�ɓ���
    public void HitEMPZone(ref bool isHit)
    {
        h++;
        if (!isHit)
        {
            empLevel += zonePower;
            isHit = true;
        }
    }
    //������
    public void StartPlayerHitEMP()
    {
        empLevel = 0;
    }
}
