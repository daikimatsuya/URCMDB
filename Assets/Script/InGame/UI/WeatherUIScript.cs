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

    
    //天気アイコン変更
    private void SetMaterial()
    {
        WeatherIcon.sprite = rain;
    }
    //降水確率表示
    private void SetChanceOfRain(int chanceOfRain)
    {
        chanceOfRainTex=this.chanceOfRain.GetComponent<TextMeshProUGUI>();  //降水確率を取得
        chanceOfRainTex.text = chanceOfRain + "%";  //降水確率表示
    }
    //UI表示変更
    public void SetWeatherScript(in SelectWeatherScript sws)
    {
        if (sws != null)
        {
            if (sws.GetIsRain())    //雨だったら
            {
                SetMaterial();  //アイコンのマテリアルを雨マークに変更
            }
            SetChanceOfRain(sws.GetChanceOfRain()); //降水確率表示
        }
    }
    
    //UIオンオフ切り替え
    public void SetWeatherUIActive(bool flag)
    {
        this.gameObject.SetActive(flag);
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
