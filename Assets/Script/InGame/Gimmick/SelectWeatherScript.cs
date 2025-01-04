using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;

public class SelectWeatherScript : MonoBehaviour
{
    private bool end=false;
    private int chanceOfRain;
    private bool isRain;

    PlayerCameraScript pcs;
    [SerializeField] private GameObject rain;
    [SerializeField] private int blankRain;
    [SerializeField] private Material rainSky;
    [SerializeField] private Material sunSky;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color defaultSkyColor;
    [SerializeField] private Color defaultEquatorColor;
    [SerializeField] private Color defaultGroundColor;

    public Light Light { get; set; }

    //気候管理
    public void WeatherSetting(CameraManager cm)
    {
        if(!end)
        {
            pcs = cm.GetPlayerCamera(); //コンポーネント取得
            chanceOfRain = WebAPIScript.GetIntChanceOfRain();   //降水確率取得

            if (chanceOfRain == 255)    //取得できなかったら//////////////////
            {
                chanceOfRain = blankRain;   //事前に設定した値を代入
            }//////////////////////////////////////////////////////////////////////

            if (chanceOfRain >= UnityEngine.Random.Range(0, 100))   //ランダムで雨を降らせる///////
            {
                Rainy();    //雨にする
            }/////////////////////////////////////////////////////////////////////////////////////////////////
            else
            {
                Sunny();    //晴れにする
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
        CreateRain();   //雨生成
        SetSkyBoxMaterialRain();    //スカイボックとライト変更
    }
    //天気が晴れ
    private void Sunny()
    {
        Debug.Log("はれ");
        Debug.Log(chanceOfRain);
        isRain= false;
        SetSkyBoxMaterialSunny();   //スカイボックスとライト変更
    }
    //雨生成
    private  void CreateRain()
    {
        GameObject _ = Instantiate(rain);   //雨生成
        RainScript rs = _.GetComponent<RainScript>();   //コンポーネント取得
        rs.SetCameraTransform(pcs.GetTransform());  //トランスフォームを代入

    }
    //skybox変更
    private void SetSkyBoxMaterialSunny()
    {
        Light = GameObject.FindWithTag("Light").GetComponent<Light>();  //ライト取得
        Light.color = Color.white;  //ライトカラー変更
        UnityEngine.RenderSettings.skybox = sunSky; //スカイボックス変更
        SetDefaultSkyBox(); //スカイボックス設定変更
    }
    //skybox変更
    private void SetSkyBoxMaterialRain()
    {
        Light = GameObject.FindWithTag("Light").GetComponent<Light>();  //ライト取得
        Light.color = Color.black;  //ライトカラー変更
        UnityEngine.RenderSettings.skybox = rainSky;    //スカイボックス変更
        SetDefaultSkyBox(); //スカイボックス設定変更
    }
    //デフォルトのスカイボックス設定
    private void SetDefaultSkyBox()
    {
        //設定を変更
        UnityEngine.RenderSettings.subtractiveShadowColor = defaultColor;
        UnityEngine.RenderSettings.ambientMode = AmbientMode.Trilight;
        UnityEngine.RenderSettings.ambientSkyColor = defaultSkyColor;
        UnityEngine.RenderSettings.ambientEquatorColor = defaultEquatorColor;
        UnityEngine.RenderSettings.ambientGroundColor = defaultGroundColor;
        ////////////
    }

    #region 値受け渡し
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
