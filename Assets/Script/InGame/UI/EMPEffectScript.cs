using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPEffectScript : MonoBehaviour
{
    [SerializeField] private GameObject EMPEffect;
    public void EMPEffectController(in bool flag)
    {
        EMPEffect.SetActive(flag);
    }

    public void Off()
    {
        EMPEffect.SetActive(false);
    }
    public void StartEMPEffect()
    {

    }
}
