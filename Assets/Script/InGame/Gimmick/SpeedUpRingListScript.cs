using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�X�s�[�h�A�b�v�����O�����X�g�ŊǗ�
public class SpeedUpRingListScript : MonoBehaviour
{
    private List<SpeedUpRingScript> speedUpRingList ;
    
    //�X�s�[�h�A�b�v�����O�Ǘ�
    public void SpeedUpRingListController(in PlayerControllerScript pcs,in bool isPose)
    {
        if(isPose)
        {
            return;
        }
        if (speedUpRingList == null)
        {
            return;
        }
        for (int i = 0; i < speedUpRingList.Count; i++)
        {
            speedUpRingList[i].Off();   //�G�ꂽ��I�t�ɂ���
        }
        if (pcs.GetPlayer() != null)
        {
            return;
        }
        for (int i = 0; i < speedUpRingList.Count; i++)
        {
            speedUpRingList[i].ON();    //���Z�b�g
        }
    }

    //����������
    public void StartSpeedUpRingList(in PlayerControllerScript pcs)
    {
        speedUpRingList = new List<SpeedUpRingScript>(FindObjectsOfType<SpeedUpRingScript>());
        for (int i = 0; i < speedUpRingList.Count; i++)
        {
            speedUpRingList[i].StartSpeedUpRing(in pcs);
        }
    }
}
