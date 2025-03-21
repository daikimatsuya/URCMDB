using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//EMPbot���Ǘ�����
public class EMPbotScript : MonoBehaviour
{
    [SerializeField] private float empInterval;
    private int intervalBuff;
    [SerializeField] private float chargeTime;
    [SerializeField] private float explodeTime; 
    [SerializeField] private GameObject EMP;
    [SerializeField] private bool isDeploy;
    [SerializeField] private Vector2 tilling;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float chargeSize;
    [SerializeField] private float explodeSize;
    [SerializeField] private float deploySize;

    private CreateMarkerScript cms;

    private List<EMPScript> empList;

    //EMP�����������Ǘ�
    public void EMPbotController()
    {
        cms.Move(this.transform);       //�}�[�J�[�Ǐ]
        cms.Adjustment();                  //�}�[�J�[�␳

        if (isDeploy)
        {
            return;
        }
        if(TimeCountScript.TimeCounter(ref intervalBuff))
        {
            CreateEMP();                                                                        //EMP����
            TimeCountScript.SetTime(ref intervalBuff, empInterval);          //�C���^�[�o�����Z�b�g
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
        emp.SetTillingOffset(tilling, offset);             //�^�C�����O�ƃI�t�Z�b�g���Z�b�g
        emp.SetChargeTime(chargeTime);              //�`���[�W���ԃZ�b�g
        emp.SetExplodeTime(explodeTime);           //���j���ԃZ�b�g
    }

    //EMP�Ǘ�
    public void EMPController(in PlayerScript ps)
    {
        if (ps == null)
        {
            //�v���C���[�����Ȃ�������EMP������
            for (int i = 0; i < empList.Count; i++)
            {
                empList[i].Break();                 //�폜
                empList.RemoveAt(i);            //���X�g���珜�O
            }
            return;
        }

        //EMP��W�J���Ă��Ȃ�������W�J������
        if (isDeploy && empList.Count == 0)
        {
            CreateEMP();
        }

        //EMP�𓮂���
        for (int i = 0; i < empList.Count;)
        {
            if (isDeploy)
            {                
                empList[i].Deploy();    //EMP�W�J
                i++;
                return;
            }

            //�`���[�W���I������甚�j������
            if (empList[i].Charge())
            {
                empList[i].Explode();

            }

            //EMP������
            if (empList[i].GetBreakFlag())
            {
                empList[i].Break();         //�폜
                empList.RemoveAt(i);     //���X�g���珜�O
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
