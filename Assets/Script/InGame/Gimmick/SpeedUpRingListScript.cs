using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpRingListScript : MonoBehaviour
{
    private List<SpeedUpRingScript> speedUpRingList = new List<SpeedUpRingScript>();
    
    //�X�s�[�h�A�b�v�����O�Ǘ�
    public void SpeedUpRingListController(in PlayerScript ps)
    {
        if (speedUpRingList == null)
        {
            return;
        }
        for (int i = 0; i < speedUpRingList.Count; i++)
        {
            speedUpRingList[i].Off();   //�G�ꂽ��I�t�ɂ���
        }
        if (ps != null)
        {
            return;
        }
        for (int i = 0; i < speedUpRingList.Count; i++)
        {
            speedUpRingList[i].ON();    //���Z�b�g
        }
    }

    //����������
    public void AwakeSpeedUpRingList()
    {
        int i = 0;
        foreach (Transform children in this.gameObject.transform)
        {
            speedUpRingList.Add(children.GetComponent<SpeedUpRingScript>());
            speedUpRingList[i].StartSpeedUpRing();
            i++;
        }
    }
}
