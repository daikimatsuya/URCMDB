using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���p�C�Ǘ����X�g
public class FlakListScript : MonoBehaviour
{
    private List<FlakScript> flackList = new List<FlakScript>();

    //���p�C�ꊇ�Ǘ�
    public void FlakListController(in bool isPose)
    {
        if (flackList == null)  //�I�u�W�F�N�g���Ȃ���΃��^�[��/////
        {
            return;
        }//////////////////////////////////////////////////////////////

        for (int i = 0; i < flackList.Count; i++)
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
        int i = 0;
        foreach (Transform children in GameObject.FindWithTag("FlakList").transform)   //�Ή��M�~�b�N������Ύ擾/////
        {
            flackList.Add(children.GetComponent<FlakScript>());     //���X�g�ɒǉ�
            flackList[i].StartFlak();                                                 //������
            i++;
        }/////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
