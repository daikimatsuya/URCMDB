using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�◎�Ƃ�������X�g�ŊǗ�
public class FallRockListScript : MonoBehaviour
{
    private List<RockFallScript> rockFallList;

    //�␶���I�u�W�F�N�g�Ǘ�
    public void RockFallListController(in bool isPause)
    {

        for (int i = 0; i < rockFallList.Count; i++)
        {
            rockFallList[i].RockController(in isPause); //���Ƃ�������Ǘ�

            if (!isPause &&rockFallList[i].GetInterval())
            {
                rockFallList[i].SpawnRock();    //��𐶐�
                rockFallList[i].SetTime();         //�N�[���^�C���ݒ�
            }

        }
    }
    //����������
    public void AwakeFallRockList()
    {
        rockFallList = new List<RockFallScript>(FindObjectsOfType<RockFallScript>());
        for (int i = 0; i < rockFallList.Count; i++)
        {
            rockFallList[i].StartRockFall();
        }
    }
}
