using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaUrchinListScript : MonoBehaviour
{
    private List<SeaUrchinScript> seaUrchinList;

    //���[���ɉ����I�u�W�F�N�g�Ǘ�
    public void SeaUrchinListController(in bool isPause)
    {
        if (isPause)
        {
            return;
        }
        for (int i = 0; i < seaUrchinList.Count; i++)
        {
            seaUrchinList[i].Move();
        }
    }

    //������
    public void StartSeaUrchinList()
    {
        seaUrchinList = new List<SeaUrchinScript>(FindObjectsOfType<SeaUrchinScript>());

        for (int i = 0; i < seaUrchinList.Count; i++) 
        {
            seaUrchinList[i].StartSeaUrchin();
        }
    }
}
