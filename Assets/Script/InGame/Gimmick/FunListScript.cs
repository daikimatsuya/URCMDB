using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�t�@�������X�g�ŊǗ�����
public class FunListScript : MonoBehaviour
{
    private List<FunScript> funList;

    //�t�@�������X�g�ŊǗ�
    public void FunListController(in bool isPause)
    {
        if (isPause)
        {
            return;
        }
        for (int i = 0; i < funList.Count; i++)
        {
            funList[i].RotateFun();     //�t�@������
        }
    }
    //����������
    public void StartFunList()
    {
        funList = new List<FunScript>(FindObjectsOfType<FunScript>());
        for (int i = 0; i < funList.Count; i++)
        {
            funList[i].StartFun();
        }
    }
}
