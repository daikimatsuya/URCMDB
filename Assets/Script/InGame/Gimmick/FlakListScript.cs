using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���p�C�Ǘ����X�g
public class FlakListScript : MonoBehaviour
{
    private List<FlakScript> flakList;
    private PlayerControllerScript pcs;

    //���p�C�ꊇ�Ǘ�
    public void FlakListController(in bool isPose)
    {
        if (flakList == null)  //�I�u�W�F�N�g���Ȃ���΃��^�[��/////
        {
            return;
        }//////////////////////////////////////////////////////////////


        for (int i = 0; i < flakList.Count; i++)
        {
            if (!isPose)
            {
                if (flakList[i].GetIsAffective()==false)    //�v���C���[���˒����ɂ��Ȃ�////
                {
                    flakList[i].SetTime();               //�N�[���^�C�����Z�b�g
                }/////////////////////////////////////////////////////////////////////////////

                flakList[i].Aim(pcs.GetPlayer()); //�v���C���[��⑫
                if (flakList[i].GetTime()) //�N�[���^�C���m�F///
                {
                    flakList[i].Shot(in pcs);    //�ˌ�
                }/////////////////////////////////////////////////
            }
                flakList[i].BulletController(in isPose);
        }
    }
    public void AwakeFlakList(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;
        flakList = new List<FlakScript>(FindObjectsOfType<FlakScript>());
        for (int i = 0; i < flakList.Count; i++)
        {
            flakList[i].StartFlak(in pcs);
        }
    }
}
