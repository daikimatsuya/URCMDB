using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickSwitchListController : MonoBehaviour
{
    List<GimmickSwitch_WeatherScript> gimmickSwitchList_Weather;
    public void StartGimmickSwitchList(in bool isRain)
    {
        gimmickSwitchList_Weather = new List<GimmickSwitch_WeatherScript>(FindObjectsOfType<GimmickSwitch_WeatherScript>());
        for (int i = 0; i < gimmickSwitchList_Weather.Count;)
        {
            gimmickSwitchList_Weather[i].GimmickSwitch(in isRain);
            gimmickSwitchList_Weather.Remove(gimmickSwitchList_Weather [i]);
        }

    }

}
