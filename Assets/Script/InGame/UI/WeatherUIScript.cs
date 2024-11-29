using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUIScript : MonoBehaviour
{
    [SerializeField] private static Sprite rain;
    [SerializeField] private static Image WeatherIcon;

    private SelectWeatherScript sws;
    private int chanceOfRain;
    private bool isRain;
    public static void WeatherUIController()
    {
        
    }
    private void SetMaterial()
    {
        WeatherIcon = GetComponent<Image>();
        WeatherIcon.sprite = rain;
    }
    private void SetChanceOfRain()
    {

    }
    public void SetWeatherScript(SelectWeatherScript sws)
    {
        this.sws = sws;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
