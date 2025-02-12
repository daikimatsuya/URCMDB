using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���p�C�Ǘ����X�g
public class FlakListScript : MonoBehaviour
{
    private List<FlakScript> flakList;

    //���p�C�ꊇ�Ǘ�
    public void FlakListController(in bool isPose)
    {
        if (flackList == null)  //�I�u�W�F�N�g���Ȃ���΃��^�[��/////
        {
            return;
        }//////////////////////////////////////////////////////////////


        for (int i = 0; i < flakList.Count; i++)
        {
            if (!isPose)
            {
                if (flackList[i].GetPlayerPos() == null)    //�v���C���[���˒����ɂ��Ȃ�////
                {
                    flackList[i].SetTime();               //�N�[���^�C�����Z�b�g
                    flackList[i].LineUIDelete();        //�\�������폜
                    return;
                }/////////////////////////////////////////////////////////////////////////////

                flackList[i].Aim(); //�v���C���[��⑫
                if (flackList[i].GetTime()) //�N�[���^�C���m�F///
                {
                    flackList[i].Shot();    //�ˌ�
                }/////////////////////////////////////////////////
            }
            flackList[i].BulletController(in isPose);
        }
    }
    public void AwakeFlakList()
    {
        flakList = new List<FlakScript>(FindObjectsOfType<FlakScript>());
        for (int i = 0; i < flakList.Count; i++)
        {
            flakList[i].StartFlak();
        }
    }
}
