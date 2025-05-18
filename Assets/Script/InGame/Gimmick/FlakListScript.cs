using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���p�C�Ǘ����X�g
public class FlakListScript : MonoBehaviour
{
    private List<FlakScript> flakList;
    private PlayerControllerScript pcs;

    //���p�C�ꊇ�Ǘ�
    public void FlakListController(in bool isPause)
    {
        if (flakList == null)  
        {
            return; //�I�u�W�F�N�g���Ȃ���΃��^�[��
        }

        for (int i = 0; i < flakList.Count; i++)
        {
            if (!isPause)
            {
                if (flakList[i].GetIsAffective()==false)    
                {
                    flakList[i].SetTime();     //�v���C���[���˒����ɂ��Ȃ���΃N�[���^�C�����Z�b�g
                }

                flakList[i].Aim(pcs.GetPlayer()); //�v���C���[��⑫

                if (flakList[i].GetTime()) 
                {
                    flakList[i].Shot(in pcs);    //�N�[���^�C�����I����Ă��猂��
                }
            }
                flakList[i].BulletController(in isPause);   //�e���Ǘ�
        }
    }

    //����������
    public void StartFlakList(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;
        flakList = new List<FlakScript>(FindObjectsOfType<FlakScript>());
        for (int i = 0; i < flakList.Count; i++)
        {
            flakList[i].StartFlak(in pcs);
        }
    }
}
