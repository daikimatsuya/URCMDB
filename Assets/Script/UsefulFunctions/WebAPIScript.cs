using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Networking;
using UnityEngine.Networking;
using System;

public class WebAPIScript : MonoBehaviour
{
    private string url = "https://weather.tsukumijima.net/api/forecast?city=130010";

    [System.Serializable]
    public class str
    {
        public string time;
    }

    [System.Serializable]
    public class  webJson
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
    webJson _forecasts;

    [Obsolete]
    private IEnumerator WEBMethod()
    {
        

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();


        if(unityWebRequest.isNetworkError||unityWebRequest.isHttpError)
        {

        }
        else
        {
            var test1 = new webJson();
            test1.publicTime = "pubtest";
            //test1.forecasts[0].chanceOfRain.T06_12 = "test0";
            //test1.forecasts[0].chanceOfRain.T18_24 = "test0";
            //test1._chansOfRain.T00_06 = "test";
            // JSON文字列にシリアライズ
            var test1Json = JsonUtility.ToJson(test1, true);
            // JSON文字列からクラスとしてデシリアライズする
            var newTest1 = JsonUtility.FromJson<webJson>(test1Json);
            var newTest2 = JsonUtility.FromJson<webJson>(unityWebRequest.downloadHandler.text);

            str _strVal = JsonUtility.FromJson<str>(unityWebRequest.downloadHandler.text);
            _forecasts = JsonUtility.FromJson<webJson>(unityWebRequest.downloadHandler.text);

           
            //Debug.Log(_forecasts._chansOfRain.T12_18);
            Debug.Log(unityWebRequest.downloadHandler.text);
            Debug.Log(JsonUtility.FromJson<webJson>(unityWebRequest.downloadHandler.text));
        }
    }

    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        StartCoroutine(WEBMethod());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
