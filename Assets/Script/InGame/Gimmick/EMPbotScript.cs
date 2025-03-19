using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

public class EMPbotScript : MonoBehaviour
{
    [SerializeField] private float empInterval;
    private int intervalBuff;
    [SerializeField] private float chargeTime;
    private float chargeTimeBuff;
    [SerializeField] private float explodeTime;
    private float explodeTimeBuff;  
    [SerializeField] private GameObject EMP;
    [SerializeField] private bool isDeploy;
    [SerializeField] private Vector2 tilling;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float chargeSize;
    [SerializeField] private float explodeSize;
    [SerializeField] private float deploySize;

    private CreateMarkerScript cms;

    private List<EMPScript> empList;

    public void EMPbotController()
    {
        cms.Move(this.transform);
        cms.Adjustment();

        if (isDeploy)
        {
            return;
        }
        if(TimeCountScript.TimeCounter(ref intervalBuff))
        {
            CreateEMP();
            TimeCountScript.SetTime(ref chargeTimeBuff, chargeTime);
            TimeCountScript.SetTime(ref intervalBuff, empInterval);
        }

    }

    //EMP����
    public void CreateEMP()
    {
        GameObject _ = Instantiate(EMP);                            //�I�u�W�F�N�g����
        _.transform.SetParent(this.transform);                      //�e�q�t��
        _.transform.localPosition = Vector3.zero;                  //�ʒu�̂���C��
        _.transform.localEulerAngles = Vector3.zero;            //�����̂���C��
        EMPScript emp = _.GetComponent<EMPScript>();    //EMP�X�N���v�g�擾
        emp.SetSize(chargeSize, explodeSize, deploySize);   //�T�C�Y�Z�b�g
        emp.StartEMP(in isDeploy);                                     //EMP�X�N���v�g��������
        empList.Add(emp);                                                 //���X�g�ɒǉ�


        if (isDeploy)
        {
            return;
        }
        emp.SetTillingOffset(tilling, offset);                          //�^�C�����O�ƃI�t�Z�b�g���Z�b�g
        emp.SetChargeTime(chargeTime);
        emp.SetExplodeTime(explodeTime);
    }

    //EMP�Ǘ�
    public void EMPController(in PlayerScript ps)
    {
        if (ps == null)
        {
            for (int i = 0; i < empList.Count; i++)
            {
                empList[i].Break();
                empList.RemoveAt(i);
            }
            return;
        }

        if (isDeploy && empList.Count == 0)
        {
            CreateEMP();
        }

        for (int i = 0; i < empList.Count;)
        {
            if (isDeploy)
            {                
                empList[i].Deploy();
                i++;
                return;
            }

            if (empList[i].Charge())
            {
                empList[i].Explode();

            }

            if (empList[i].GetBreakFlag())
            {
                empList[i].Break();
                empList.RemoveAt(i);
            }
            else
            {
                i++;
            }


        }
    }

    //������
    public void StartEMPbot(in PlayerControllerScript pcs)
    {
        empList = new List<EMPScript>();
        cms=GetComponent<CreateMarkerScript>();
        cms.CreateMarker(this.transform, in pcs);
        cms.SetMarkerSize();

        if (isDeploy)
        {
            return;
        }
        TimeCountScript.SetTime(ref intervalBuff, empInterval);
    }
}
