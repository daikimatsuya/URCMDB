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
            pcs = cm.GetPlayerCamera(); //�R���|�[�l���g�擾
            chanceOfRain = WebAPIScript.GetIntChanceOfRain();   //�~���m���擾

            if (chanceOfRain == 255)    //�擾�ł��Ȃ�������//////////////////
            {
                chanceOfRain = blankRain;   //���O�ɐݒ肵���l����
            }//////////////////////////////////////////////////////////////////////

            if (chanceOfRain >= UnityEngine.Random.Range(0, 100))   //�����_���ŉJ���~�点��///////
            {
                Rainy();    //�J�ɂ���
            }/////////////////////////////////////////////////////////////////////////////////////////////////
            else
            {
                Sunny();    //����ɂ���
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
        CreateRain();   //�J����
        SetSkyBoxMaterialRain();    //�X�J�C�{�b�N�ƃ��C�g�ύX
    }
    //�V�C������
    private void Sunny()
    {
        Debug.Log("�͂�");
        Debug.Log(chanceOfRain);
        isRain= false;
        SetSkyBoxMaterialSunny();   //�X�J�C�{�b�N�X�ƃ��C�g�ύX
    }
    //�J����
    private  void CreateRain()
    {
        GameObject _ = Instantiate(rain);   //�J����
        RainScript rs = _.GetComponent<RainScript>();   //�R���|�[�l���g�擾
        rs.SetCameraTransform(pcs.GetTransform());  //�g�����X�t�H�[������

    }
    //skybox�ύX
    private void SetSkyBoxMaterialSunny()
    {
        Light = GameObject.FindWithTag("Light").GetComponent<Light>();  //���C�g�擾
        Light.color = Color.white;  //���C�g�J���[�ύX
        UnityEngine.RenderSettings.skybox = sunSky; //�X�J�C�{�b�N�X�ύX
        SetDefaultSkyBox(); //�X�J�C�{�b�N�X�ݒ�ύX
    }
    //skybox�ύX
    private void SetSkyBoxMaterialRain()
    {
        Light = GameObject.FindWithTag("Light").GetComponent<Light>();  //���C�g�擾
        Light.color = Color.black;  //���C�g�J���[�ύX
        UnityEngine.RenderSettings.skybox = rainSky;    //�X�J�C�{�b�N�X�ύX
        SetDefaultSkyBox(); //�X�J�C�{�b�N�X�ݒ�ύX
    }
    //�f�t�H���g�̃X�J�C�{�b�N�X�ݒ�
    private void SetDefaultSkyBox()
    {
        //�ݒ��ύX
        UnityEngine.RenderSettings.subtractiveShadowColor = defaultColor;
        UnityEngine.RenderSettings.ambientMode = AmbientMode.Trilight;
        UnityEngine.RenderSettings.ambientSkyColor = defaultSkyColor;
        UnityEngine.RenderSettings.ambientEquatorColor = defaultEquatorColor;
        UnityEngine.RenderSettings.ambientGroundColor = defaultGroundColor;
        ////////////
    }

    #region �l�󂯓n��
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
