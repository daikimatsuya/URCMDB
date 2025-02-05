using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;


namespace Usefull
{

    //WEBからJson形式で天気予報を取得して格納する
    public class WebAPIScript : MonoBehaviour
    {
        private static string url = "https://weather.tsukumijima.net/api/forecast?city=130010";


        #region Json取得用の構造体
        [System.Serializable]
        public class WebJson
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

        private static string filename = "chanceOfRain.json";
        private static bool shouldRain=false;
        private static bool shouldSun=false;

        //Jsonから天気情報取得
        public static IEnumerator WEBMethod()
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(url); //URL先から情報を取得
            yield return unityWebRequest.SendWebRequest();  //情報を取得できるまで待つ

            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {

            }
            else
            {
                webJson = JsonUtility.FromJson<WebJson>(unityWebRequest.downloadHandler.text);  //情報を格納

                SaveJson(webJson,filename);

                //確認用デバッグログ
                Debug.Log(webJson.forecasts[0].chanceOfRain.T00_06);
                Debug.Log(webJson.forecasts[0].chanceOfRain.T06_12);
                Debug.Log(webJson.forecasts[0].chanceOfRain.T12_18);
                Debug.Log(webJson.forecasts[0].chanceOfRain.T18_24);
                //////////////////////
            }

            if (webJson == null)
            {
                webJson = ReadJson(filename);
                if(webJson != null)
                {
                    //確認用デバッグログ
                    Debug.Log("Read");
                    Debug.Log(webJson.forecasts[0].chanceOfRain.T00_06);
                    Debug.Log(webJson.forecasts[0].chanceOfRain.T06_12);
                    Debug.Log(webJson.forecasts[0].chanceOfRain.T12_18);
                    Debug.Log(webJson.forecasts[0].chanceOfRain.T18_24);
                    //////////////////////
                }
            }
        }
        //初期化
        public static void Initiaraze()
        {
            if (webJson == null)    //情報が入っていなかったら取得する/////
            {
                WEBMethod();
            }//////////////////////////////////////////////////////////////////
        }
        //降水確率を文字列で取得
        public static string GetStringChanceOfRain()
        {
            if (webJson == null) //情報が入っていなかったらnullを返す///
            {
                return null;
            }///////////////////////////////////////////////////////////////

            //時間帯によって別の時間の降水確率を返す
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
            ///////////////////////////////////////////
        }
        //降水確率をintで取得
        public static int GetIntChanceOfRain()
        {
            string buff = GetStringChanceOfRain();  //文字列で降水確率を格納

            if (shouldRain)
            {
                return 100;
            }
            if (shouldSun)
            {
                return 0;
            }
            if (buff == null)    //値が入っていなかったらありえない数値を返す////
            {
                return 255;
            }//////////////////////////////////////////////////////////////////////

            //各降水確率に応じて数字を返す////////////
            if (buff == "0%")
            {
                return 0;
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
            //////////////////////////////////////////
        }
        //雨にする
        public static void SetRain()
        {
            shouldRain = true;
            shouldSun = false;
        }
        //晴れにする
        public static void SetSun()
        {
            shouldSun = true;
            shouldRain = false;
        }
        //天気が現実に即する
        public static void SetRial()
        {
            shouldSun = false;
            shouldRain = false;
        }

        //降水確率をJsonで保存する
        private static void SaveJson(WebJson data,string fileName)
        {
            string filePath = Application.dataPath + "/" + "Resources" + "/" + "Json" + "/" + filename;   //ファイルパス取得
            string json = JsonUtility.ToJson(data);                                                                               //変換
            StreamWriter sw = new StreamWriter(filePath, false);                                                        //streamWriter生成
            sw.WriteLine(json);                                                                                                         //書き込み
            sw.Close();                                                                                                                     //終了
        }
        //降水確率を保存したJsonから読み取る
        private static WebJson ReadJson(string fileName)
        {
            string filePath = Application.dataPath + "/" + "Resources" + "/" + "Json" + "/" + filename;   //ファイルパス取得
            if (File.Exists(filePath))  //ファイル確認/////////////////
            {
                StreamReader sr = new StreamReader(filePath);       //steamWirter生成
                string json = sr.ReadToEnd();                                  //読み込み
                sr.Close();                                                             //終了

                return JsonUtility.FromJson<WebJson>(json);         //値をリターン
            }/////////////////////////////////////////////////////////
            return null;
        }
        void Start()
        {
            StartCoroutine(WEBMethod());
        }

    }
}