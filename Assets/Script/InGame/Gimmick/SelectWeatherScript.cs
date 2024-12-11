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



    //�C��Ǘ�
    public void WeatherSetting(CameraManager cm)
    {
        if(!end)
        {
            pcs = cm.GetPlayerCamera();

            chanceOfRain = WebAPIScript.GetIntChanceOfRain();


            if (chanceOfRain == 255)
            {
                chanceOfRain = blankRain;
            }
            if (chanceOfRain >= UnityEngine.Random.Range(0, 100))
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
    //�V�C���J
    private void Rainy()
    {
        Debug.Log("����");
        Debug.Log(chanceOfRain);

        isRain = true;
        CreateRain();
        SetSkyBoxMaterialRain();
    }
    //�V�C������
    private void Sunny()
    {
        Debug.Log("�͂�");
        Debug.Log(chanceOfRain);
        isRain= false;
        SetSkyBoxMaterialSunny();
    }
    //�J����
    private  void CreateRain()
    {
        GameObject _ = Instantiate(rain);
        RainScript rs = _.GetComponent<RainScript>();
        rs.SetCameraTransform(pcs.GetTransform());

    }

    //skybox�ύX
    private void SetSkyBoxMaterialSunny()
    {
        Light = GameObject.FindWithTag("Light").GetComponent<Light>();
        Light.color = Color.white;
        UnityEngine.RenderSettings.skybox = sunSky;
        SetDefaultSkyBox();
    }
    //skybox�ύX
    private void SetSkyBoxMaterialRain()
    {
        Light = GameObject.FindWithTag("Light").GetComponent<Light>();
        Light.color = Color.black;
        UnityEngine.RenderSettings.skybox = rainSky;
        SetDefaultSkyBox();
    }
    //�f�t�H���g�̃X�J�C�{�b�N�X�ݒ�
    private void SetDefaultSkyBox()
    {
        UnityEngine.RenderSettings.subtractiveShadowColor = defaultColor;
        UnityEngine.RenderSettings.ambientMode = AmbientMode.Trilight;
        UnityEngine.RenderSettings.ambientSkyColor = defaultSkyColor;
        UnityEngine.RenderSettings.ambientEquatorColor = defaultEquatorColor;
        UnityEngine.RenderSettings.ambientGroundColor = defaultGroundColor;
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
