using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUIScript : MonoBehaviour
{
    [SerializeField] private Sprite rain;
    [SerializeField] private Image WeatherIcon;
    [SerializeField] private GameObject chanceOfRain;
    private TextMeshProUGUI chanceOfRainTex;

    
    //�V�C�A�C�R���ύX
    private void SetMaterial()
    {

        WeatherIcon.sprite = rain;
    }
    //�~���m���\��
    private void SetChanceOfRain(int chanceOfRain)
    {
        chanceOfRainTex=this.chanceOfRain.GetComponent<TextMeshProUGUI>();
        chanceOfRainTex.text = chanceOfRain + "%";
    }
    //UI�\���ύX
    public void SetWeatherScript(SelectWeatherScript sws)
    {
        if (sws != null)
        {
            if (sws.GetIsRain())
            {
                SetMaterial();
            }
            SetChanceOfRain(sws.GetChanceOfRain());
        }
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
