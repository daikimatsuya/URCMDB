using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;


namespace Usefull
{

    //WEB����Json�`���œV�C�\����擾���Ċi�[����
    public class WebAPIScript : MonoBehaviour
    {
        private static string url = "https://weather.tsukumijima.net/api/forecast?city=130010";


        #region Json�擾�p�̍\����
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

        //Json����V�C���擾
        public static IEnumerator WEBMethod()
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(url); //URL�悩������擾
            yield return unityWebRequest.SendWebRequest();  //�����擾�ł���܂ő҂�

            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {

            }
            else
            {
                webJson = JsonUtility.FromJson<WebJson>(unityWebRequest.downloadHandler.text);  //�����i�[

                SaveJson(webJson,filename);

                //�m�F�p�f�o�b�O���O
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
                    //�m�F�p�f�o�b�O���O
                    Debug.Log("Read");
                    Debug.Log(webJson.forecasts[0].chanceOfRain.T00_06);
                    Debug.Log(webJson.forecasts[0].chanceOfRain.T06_12);
                    Debug.Log(webJson.forecasts[0].chanceOfRain.T12_18);
                    Debug.Log(webJson.forecasts[0].chanceOfRain.T18_24);
                    //////////////////////
                }
            }
        }
        //������
        public static void Initiaraze()
        {
            if (webJson == null)    //��񂪓����Ă��Ȃ�������擾����/////
            {
                WEBMethod();
            }//////////////////////////////////////////////////////////////////
        }
        //�~���m���𕶎���Ŏ擾
        public static string GetStringChanceOfRain()
        {
            if (webJson == null) //��񂪓����Ă��Ȃ�������null��Ԃ�///
            {
                return null;
            }///////////////////////////////////////////////////////////////

            //���ԑтɂ���ĕʂ̎��Ԃ̍~���m����Ԃ�
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
        //�~���m����int�Ŏ擾
        public static int GetIntChanceOfRain()
        {
            string buff = GetStringChanceOfRain();  //������ō~���m�����i�[

            if (shouldRain)
            {
                return 100;
            }
            if (shouldSun)
            {
                return 0;
            }
            if (buff == null)    //�l�������Ă��Ȃ������炠�肦�Ȃ����l��Ԃ�////
            {
                return 255;
            }//////////////////////////////////////////////////////////////////////

            //�e�~���m���ɉ����Đ�����Ԃ�////////////
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
        //�J�ɂ���
        public static void SetRain()
        {
            shouldRain = true;
            shouldSun = false;
        }
        //����ɂ���
        public static void SetSun()
        {
            shouldSun = true;
            shouldRain = false;
        }
        //�V�C�������ɑ�����
        public static void SetRial()
        {
            shouldSun = false;
            shouldRain = false;
        }

        //�~���m����Json�ŕۑ�����
        private static void SaveJson(WebJson data,string fileName)
        {
            string filePath = Application.dataPath + "/" + "Resources" + "/" + "Json" + "/" + filename;   //�t�@�C���p�X�擾
            string json = JsonUtility.ToJson(data);                                                                               //�ϊ�
            StreamWriter sw = new StreamWriter(filePath, false);                                                        //streamWriter����
            sw.WriteLine(json);                                                                                                         //��������
            sw.Close();                                                                                                                     //�I��
        }
        //�~���m����ۑ�����Json����ǂݎ��
        private static WebJson ReadJson(string fileName)
        {
            string filePath = Application.dataPath + "/" + "Resources" + "/" + "Json" + "/" + filename;   //�t�@�C���p�X�擾
            if (File.Exists(filePath))  //�t�@�C���m�F/////////////////
            {
                StreamReader sr = new StreamReader(filePath);       //steamWirter����
                string json = sr.ReadToEnd();                                  //�ǂݍ���
                sr.Close();                                                             //�I��

                return JsonUtility.FromJson<WebJson>(json);         //�l�����^�[��
            }/////////////////////////////////////////////////////////
            return null;
        }
        void Start()
        {
            StartCoroutine(WEBMethod());
        }

    }
}