using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeatherScript : MonoBehaviour
{
    private bool end;
    public void WeatherSetting()
    {
        if(end)
        {
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
