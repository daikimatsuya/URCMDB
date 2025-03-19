using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RedBoostEffectScript : MonoBehaviour
{
    [SerializeField] private GameObject redBoostEffect;
    public void RedBoostEffectController(in bool flag)
    {

    }
    private void On()
    {
        redBoostEffect.SetActive(true);
    }
    public void Off()
    {
        redBoostEffect.SetActive(false);
    }
    public void StartRedBoostEffect()
    {

    }
}
