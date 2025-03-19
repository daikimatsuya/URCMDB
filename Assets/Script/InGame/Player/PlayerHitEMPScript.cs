using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SensorScript;

//�v���C���[��EMP�ɂ����������̏���
public class PlayerHitEMPScript : MonoBehaviour
{
    [SerializeField] private GameObject empEffect;
    [SerializeField] private float empLevel;
    [SerializeField] private float dicreaseEMP;
    [SerializeField] private float shockPower;
    [SerializeField] private float zonePower;
    [SerializeField] private float maxEMP;

    //EMP�֌W�Ǘ�
    public void EMPAffectController(in bool EMPHit)
    {
        Dicrease(in EMPHit);        //EMP�̉e������
        HitEMPZone(in EMPHit);   //EMP�G���A�l����
    }
    //�e�����󂯂Ă��邩
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

    //EMP��Ԃ̃G�t�F�N�g�I���I�t�؂�ւ�
    private void EMPAffect(in bool flag)
    {
        empEffect.SetActive(flag);
    }

    //�e������
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

    //�g�ɂ�����
    public void HitEMPShock()
    {
        empLevel += shockPower;
    }
    //�G���A�ɓ���
    private void HitEMPZone(in bool isHit)
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
        EMPAffect(false);
    }
}
