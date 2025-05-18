using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickSwitch_WeatherScript : MonoBehaviour
{
    [SerializeField] private bool onlySun;
    [SerializeField] private bool onlyRain;
    public void GimmickSwitch(in bool isRain)
    {
        if (!isRain&&onlyRain)
        {
            this.gameObject.SetActive(false);
        }
        if (isRain && onlySun)
        {
            this .gameObject.SetActive(false);
        }
    }
}
