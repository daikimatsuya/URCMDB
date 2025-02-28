using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AditionalEfectScript : MonoBehaviour
{
    [SerializeField] private string flagName;
    public void AditionalEffectController()
    {

    }
    public void SetActive(in bool flag)
    {
        if (nameof (flag)!=flagName) 
        {
            return;
        }
        this.gameObject.SetActive(flag);
    }
    public void StartAditionalEffect()
    {

    }
}
