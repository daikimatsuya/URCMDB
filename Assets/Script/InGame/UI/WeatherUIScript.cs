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
        chanceOfRainTex=this.chanceOfRain.GetComponent<TextMeshProUGUI>();  //�~���m�����擾
        chanceOfRainTex.text = chanceOfRain + "%";  //�~���m���\��
    }
    //UI�\���ύX
    public void SetWeatherScript(in SelectWeatherScript sws)
    {
        if (sws != null)
        {
            if (sws.GetIsRain())    //�J��������
            {
                SetMaterial();  //�A�C�R���̃}�e���A�����J�}�[�N�ɕύX
            }
            SetChanceOfRain(sws.GetChanceOfRain()); //�~���m���\��
        }
    }
    
    //UI�I���I�t�؂�ւ�
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
