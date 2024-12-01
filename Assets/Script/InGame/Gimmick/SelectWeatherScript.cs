using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SelectWeatherScript : MonoBehaviour
{
    private bool end=false;
    private int chanceOfRain;
    private bool isRain;

    PlayerCameraScript pcs;
    new Light light;

    [SerializeField] private GameObject rain;
    [SerializeField] private int blankRain;
    [SerializeField] private Material skyBox;



    //気候管理
    public void WeatherSetting(CameraManager cm)
    {
        if(!end)
        {
            pcs = cm.GetPlayerCamera();

            chanceOfRain = WebAPIScript.GetIntChanceOfRain();


            if (chanceOfRain == 0)
            {
                chanceOfRain = blankRain;
            }
            if (chanceOfRain >= UnityEngine.Random.Range(1, 100))
            {
                Rainy();
            }
            else
            {
                Sunny();
            }
            end = true;

            return;
        }

    }
    //天気が雨
    private void Rainy()
    {
        Debug.Log("あめ");
        Debug.Log(chanceOfRain);

        isRain = true;
        CreateRain();
        SetSkyBoxMaterial();
    }
    //天気が晴れ
    private void Sunny()
    {
        Debug.Log("はれ");
        Debug.Log(chanceOfRain);
        isRain= false;
    }
    //雨生成
    private  void CreateRain()
    {
        GameObject _ = Instantiate(rain);
        RainScript rs = _.GetComponent<RainScript>();
        rs.SetCameraTransform(pcs.GetTransform());

    }
    //skybox変更
    private void SetSkyBoxMaterial()
    {
        light=GameObject.FindWithTag("Light").GetComponent<Light>();
        light.color = Color.black;
        UnityEngine.RenderSettings.skybox=skyBox;
    }

    public int GetChanceOfRain()
    {
        if (chanceOfRain != 0)
        {
            return chanceOfRain;
        }
        else
        {
            return blankRain;
        }
    }
    public bool GetIsRain()
    {
        return isRain;
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
