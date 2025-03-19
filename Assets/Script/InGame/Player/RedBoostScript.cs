using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

public class RedBoostScript : MonoBehaviour
{
    [SerializeField] GameObject boostEffect;
    [SerializeField] private GameObject redEffect;
    [SerializeField] private float redBoostTime;
    private float redBoostTimeBuff = 0;
    [SerializeField] private float redBoostAcce;
    [SerializeField] private Vector3 fireSize;
    [SerializeField] private Vector3 firePos;

    private float redSpeedBuff;
    private bool isBoost;
    private List<BoostEffectScript> redBoostEffectList = new List<BoostEffectScript>();

    //RedBoost�Ǘ�
    public void RedBoostController(in float playerSpeed,in bool redBustFlag)
    {
        RedBoost(playerSpeed);
        EffectController();
        RedyEffect(redBustFlag);
    }

    //�u�[�X�g�l�Ǘ�
    private void RedBoost(in float playerSpeed)
    {
        if (!TimeCountScript.TimeCounter(ref redBoostTimeBuff))
        {
            redSpeedBuff += playerSpeed * redBoostAcce;
            CreateRedBoostEffect();
        }
        else if (redSpeedBuff == 0)
        {
            return;
        }
        else
        {
            isBoost = false;
            redSpeedBuff -= playerSpeed * redBoostAcce; ;
            if (redSpeedBuff < 0)
            {
                redSpeedBuff = 0;
            }
        }
    }
    //�u�[�X�g�N��
    public void SetFlagOn()
    {
        TimeCountScript.SetTime(ref redBoostTimeBuff, redBoostTime);
        isBoost=true;
    }

    //�u�[�X�g�\�̎��̃G�t�F�N�g�I���I�t�؂�ւ�
    private void RedyEffect(in bool flag)
    {
        redEffect.SetActive(flag);
    }

    //�u�[�X�g�G�t�F�N�g����
    private void CreateRedBoostEffect()
    {
        GameObject _ = Instantiate(boostEffect);                                   //�G�t�F�N�g����
        _.transform.SetParent(this.transform, true);                               //�G�t�F�N�g���v���C���[�Ɛe�q�t��
        _.transform.localPosition = firePos;                                            //�|�W�V�������
        _.transform.localEulerAngles = new Vector3(0, 180, 0);              //�p�x���
        _.transform.localScale = fireSize;                                               //�T�C�Y���
        BoostEffectScript bf = _.GetComponent<BoostEffectScript>();     //�R���|�[�l���g�擾
        bf.SetTime();                                                                           //�������ԃZ�b�g
        redBoostEffectList.Add(bf);                                                       //���X�g�ɓ����
    }

    //�G�t�F�N�g�Ǘ�
    private void EffectController()
    {
        for (int i = 0; i < redBoostEffectList.Count;)
        {
            if (redBoostEffectList[i].IsDelete())  //�j��t���O���I���ɂȂ��Ă�����
            {
                redBoostEffectList[i].Break();                              //�I�u�W�F�N�g���폜
                redBoostEffectList.Remove(redBoostEffectList[i]); //���X�g����폜

            }/////////////////////////////////////////////////////////////////////
            else
            {
                redBoostEffectList[i].CountTime(); //�������Ԃ�����
                i++;
            }
        }
    }

    //������
    public void StartRedBoost()
    {
        redEffect.SetActive(false);
        isBoost = false;
    }

    #region �l�󂯓n��
    public float GetRedBurstSpeed()
    {
        return redSpeedBuff;
    }
    public bool GetIsBoost()
    {
        return isBoost;
    }
    #endregion

}
