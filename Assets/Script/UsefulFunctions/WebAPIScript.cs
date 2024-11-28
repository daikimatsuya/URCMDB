using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Networking;
using UnityEngine.Networking;
using System;

//WEBからJson形式で天気予報を取得して格納する
public class WebAPIScript : MonoBehaviour
{
    private static string url = "https://weather.tsukumijima.net/api/forecast?city=130010";


    #region Json取得用の構造体
    [System.Serializable]
    public class  WebJson
    {
        public string publicTime;
        public string publicTimeFormatted;
        public string publishingOffice;
        public string title;
        public string link;
        public Description description;
        public Forecasts[] forecasts;
        public Location location;
        public Copyright copyright;


        
        [System.Serializable]
        public struct Description
        {
            public string publicTime;
            public string publicTimeFormatted;
            public string headlineText;
            public string bodyText;
        }
        [System.Serializable]
        public struct Forecasts
        {
            public string date;
            public string dateLabel;
            public string telop;
            public Detail detail;
            public Temperature temperature;
            public chanceOfRain chanceOfRain;
            public Image image;

        }
        [System.Serializable]
        public struct Detail
        {
            public string weather;
            public string wind;
            public string wave;
        }
        [System.Serializable]
        public struct Temperature
        {
            public Min min;
            public Max max;

        }
        [System.Serializable]
        public struct Min
        {
            public string celsius;
            public string fahrenheit;
        }
        [System.Serializable]
        public struct Max
        {
            public string celsius;
            public string fahrenheit;
        }

        [System.Serializable]
        public struct ChansOfRain
        {
            public string T00_06;
            public string T06_12;
            public string T12_18;
            public string T18_24;
        }
        [System.Serializable]
        public struct Image
        {
            public string title;
            public string url;
            public string width;
            public string height;
        }
        [System.Serializable]
        public struct Location
        {
            public string area;
            public string prefecture;
            public string district;
            public string city;
        }
        [System.Serializable]
        public struct Copyright
        {
            public string title;
            public string link;
            public Image image;
            public Provider[] provider;
        }


        [System.Serializable]
        public struct Provider
        {
            public string link;
            public string name;
            public string note;
        }

        [System.Serializable]
        public struct chanceOfRain
        {
            public string T00_06;
            public string T06_12;
            public string T12_18;
            public string T18_24;
        }
    }
    #endregion
    static WebJson webJson;

    //Jsonから天気情報取得

    public static IEnumerator WEBMethod()
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        if(unityWebRequest.result!=UnityWebRequest.Result.Success)
        {
        }
        else
        {
            webJson = JsonUtility.FromJson<WebJson>(unityWebRequest.downloadHandler.text);
            Debug.Log(webJson.forecasts[0].chanceOfRain.T00_06);
            Debug.Log(webJson.forecasts[0].chanceOfRain.T06_12);
            Debug.Log(webJson.forecasts[0].chanceOfRain.T12_18);
            Debug.Log(webJson.forecasts[0].chanceOfRain.T18_24);
        }
    }
    public static void Initiaraze()
    {
        if (webJson == null)
        {
             WEBMethod();
        }
    }
    public static string GetStringChanceOfRain()
    {
        if(webJson == null)
        {
            return null;
        }
        if (webJson.forecasts[0].chanceOfRain.T00_06 != "--%")
        {
            return webJson.forecasts[0].chanceOfRain.T00_06;
        }
        if (webJson.forecasts[0].chanceOfRain.T06_12 != "--%")
        {
            return webJson.forecasts[0].chanceOfRain.T06_12;
        }
        if (webJson.forecasts[0].chanceOfRain.T12_18 != "--%")
        {
            return webJson.forecasts[0].chanceOfRain.T12_18;
        }
        return webJson.forecasts[0].chanceOfRain.T18_24;
    }
    public static int GetIntChanceOfRain()
    {
        string buff = GetStringChanceOfRain();
        if(buff == null)
        {
            return 0;
        }
        if (buff == "0%")
        {
            return 1;
        }
        if (buff == "10%")
        {
            return 10;
        }
        if (buff == "20%")
        {
            return 20;
        }
        if (buff == "30%")
        {
            return 30;
        }
        if (buff == "40%")
        {
            return 40;
        }
        if (buff == "50%")
        {
            return 50;
        }
        if (buff == "60%")
        {
            return 60;
        }
        if (buff == "70%")
        {
            return 70;
        }
        if (buff == "80%")
        {
            return 80;
        }
        if (buff == "90%")
        {
            return 90;
        }
        return 99;
    }
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(WEBMethod());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
