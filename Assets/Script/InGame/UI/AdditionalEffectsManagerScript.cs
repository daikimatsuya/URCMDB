using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalEffectsManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] additionalEffects;

    private List<AditionalEfectScript> effectList;
    private string[] effectName;
    private bool[] flag;

    public void AdditionalEffectsManagerController(in bool flag)
    {
        for (int i = 0;i< effectList.Count; i++)
        {
            effectList[i].SetActive(flag);
        }
    }
    public void StartAdditionalEffectsManager()
    {
        flag=new bool[additionalEffects.Length];
        for (int i = 0; i < additionalEffects.Length; i++)
        {
            effectList.Add(effectList[i].GetComponent<AditionalEfectScript>());
        }
    }

    #regionÅ@íléÛÇØìnÇµ
   
    public string GetNameNum(int num)
    {
        return effectName[num];
    }      
    public void SetFlag(bool flag,int num)
    {
        this.flag[num]  = flag;
    }
    #endregion
}
