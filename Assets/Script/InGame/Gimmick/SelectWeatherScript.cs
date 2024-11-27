using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWeatherScript : MonoBehaviour
{
    private bool end=false;
    private int chanceOfRain;

    PlayerCameraScript pcs;

    [SerializeField] private GameObject rain;

    public void WeatherSetting(CameraManager cm)
    {
        if(!end)
        {
            pcs = cm.GetPlayerCamera();
            CreateRain();
            return;
        }

    }
    private  void CreateRain()
    {

        chanceOfRain= WebAPIScript.GetIntChanceOfRain();
        if (chanceOfRain >= UnityEngine.Random.Range(1, 100))
        {
            Debug.Log("‚ ‚ß");
            Debug.Log(chanceOfRain);
            GameObject _=Instantiate(rain);
            RainScript rs = _.GetComponent<RainScript>();
           rs.SetCameraTransform(pcs.GetTransform());
            
        }
        else
        {
            Debug.Log("‚Í‚ê");
            Debug.Log(chanceOfRain);
        }
        end = true;
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
