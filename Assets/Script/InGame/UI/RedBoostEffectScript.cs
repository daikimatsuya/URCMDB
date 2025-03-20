using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RedBoostEffectScript : MonoBehaviour
{
    [SerializeField] private GameObject redBoostEffect;
    public void RedBoostEffectController(in bool flag)
    {
        redBoostEffect.SetActive(flag);
    }
    public void Off()
    {
        redBoostEffect.SetActive(false);
    }
    public void StartRedBoostEffect()
    {

    }
}
