using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Usefull;

//チュートリアル用UI表示管理
public class TutorialUIScript : MonoBehaviour
{
    private enum Tutorial: int
    {
        shot,
        controll,
        boost,
        quick,
        acce,
        pms,
        finish
    }

    [System.Serializable]
    public class EmphasisTransformElement
    {
        [SerializeField] public Vector3 pos;
        [SerializeField] public Vector3 rot;
        [SerializeField] public Vector3 scale;
        [SerializeField] public float time;
    }

    public class DoubleVector3
    {
        public double x;
        public double y;
        public double z;
    }

    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] controller;
    [SerializeField] private GameObject tutorialCompletion;
    private TextMeshProUGUI completion;
    [SerializeField] private EmphasisTransformElement emphasis;
    private EmphasisTransformElement emphasisBuff;
    [SerializeField] private RectTransform canvasTransform;
    private DoubleVector3 initialPos;
    private DoubleVector3 initialRot;
    private DoubleVector3 initialScale;

    private TutorialScript ts;

    private bool conectController;

    //チュートリアルUI管理
    public void TutorialUIController(in PlayerScript ps,in bool isConect)
    {
        CheckController(in isConect);              //コントローラーの接続を確認
        SelectTutorial(in ps);                          //UI用情報更新
        SetEmphasis();
        EmphasisTransition();
        ShowUI(conectController);                 //UI表示
        ShowCompletion(ts.GetResetFlag());  //チュートリアル進行度を表示


        if (ts.GetResetFlag())  
        {
            ResetTutorial();    //リセットフラグがオンになったらリセットさせる
        }
    }

    //強調表示から通常表示への遷移
    private void EmphasisTransition()
    {
        if(TimeCountScript.TimeCounter(ref emphasisBuff.time))
        {
            return;
        }
        float t = emphasisBuff.time / emphasis.time;
        t = 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));


        canvasTransform.position = initialPos + new Vector3(emphasisBuff.pos.x * t, emphasisBuff.pos.y * t, emphasisBuff.pos.z * t);
        canvasTransform.localEulerAngles = initialRot + new Vector3(emphasisBuff.rot.x * t, emphasisBuff.rot.y * t, emphasisBuff.rot.z * t);
        canvasTransform.localScale = initialScale + new Vector3(emphasisBuff.scale.x * t, emphasisBuff.scale.y * t, emphasisBuff.scale.z * t);
    }

    //強調表示の値初期化
    private void SetEmphasis()
    {
        if (!ts.GetNextSwitch())
        {
            return;
        }
        SetEmphasisTransformElement(emphasisBuff, emphasis);
        ts.SetNextSwitch(false);     
    }

    private void SetEmphasisTransformElement(EmphasisTransformElement buff,EmphasisTransformElement emphasis)
    {
        buff.pos=emphasis.pos;
        buff.rot=emphasis.rot;
        buff.scale=emphasis.scale;
        buff.time=emphasis.time;
    }

    //起動するチュートリアルを選択
    private void SelectTutorial(in PlayerScript ps)
    {
        switch (ts.GetTutorialNum())
        {
            case (int)Tutorial.shot:
                ts.ShotTutorial(in ps);                                         //発射チュートリアル実行
                break;

            case (int)Tutorial.controll:
                ts.ControlleTutorial(in ps,conectController);         //操作チュートリアル進行
                break;

            case (int)Tutorial.boost:
                ts.BoostTutorial(in ps);                                      //ブーストチュートリアル進行
                break;

            case (int)Tutorial.quick:
                ts.QuickMoveTutorial(in ps, conectController);    //高速旋回チュートリアル進行
                break;

            case (int)Tutorial.acce:
                ts.AcceTutorial(in ps, conectController);             //加速チュートリアル進行
                break;

            case (int)Tutorial.pms:
                ts.PMSTutorial(in ps);                                      //PMSチュートリアル進行
                break;

            case (int)Tutorial.finish:
                ts.ResetAll(in ps);                                           //チュートリアルが終了したらリセット
                break;
        }
    }

    //チュートリアル達成度表示
    private void ShowCompletion(in bool showFlag)
    {
        tutorialCompletion.SetActive(!showFlag);        //進行度の表示
        completion.text = ts.GetCompletion() + "%";  //進行度の値変更
    }

    //UIを表示
    private void ShowUI(in bool isConectController)
    {
        //コントローラー用
        if (isConectController) 
        {
            if (controller.Length == 0) 
            {
                return;     //コントローラー用ＵＩ画像が準備されていなかったらリターンさせる
            }
            if(controller.Length <=ts.GetTutorialNum()) 
            {
                //チュートリアル用ＵＩ画像が足りなかったら範囲内の最後を表示
                controller[controller.Length-1].SetActive(true);                        //最後のやつを表示
                controller[controller.Length-2].SetActive(false);                       //最後の一つ前を非表示
                return;
            }
            if(ts.GetTutorialNum() != 0) 
            {
                controller[ts.GetTutorialNum() - 1].SetActive(false); //現在の一つ前のＵＩを非表示にする
            }
            controller[ts.GetTutorialNum()].SetActive(true);    //現在のＵＩを表示する
        }

        //キーボード用
        else
        {
            if (keyboard.Length == 0)   
            {
                return;//キーボード用ＵＩ画像が準備されていなかったらリターンさせる/////
            }
            if (keyboard.Length <= ts.GetTutorialNum()) 
            {
                //チュートリアル用ＵＩ画像が足りなかったら範囲内の最後を表示
                keyboard[keyboard.Length - 1].SetActive(true);                          //最後のやつを表示
                keyboard[keyboard.Length - 2].SetActive(false);                         //最後の一つ前を非表示
                return;
            }
            if (ts.GetTutorialNum() != 0)   
            {
                keyboard[ts.GetTutorialNum() - 1].SetActive(false); //現在の一つ前のＵＩを非表示にする
            }
            keyboard[ts.GetTutorialNum()].SetActive(true);    //現在のＵＩを表示する
        }
    }

    //コントローラーの接続をチェック
    private void CheckController(in bool isConect)
    {
        bool buff = conectController;
        conectController = isConect; //コントローラーの接続情報取得

        if (buff != conectController) 
        {   
            ResetTutorial();     //抜かれた瞬間と接続した瞬間に表示ＵＩをリセットする
        }
    }
    //チュートリアル表示をリセットさせる
    private void ResetTutorial()
    {
        //準備されているＵＩの数非表示を回す
        for (int i = 0; i < keyboard.Length; i++)
        {
            keyboard[i].SetActive(false);
        }
        for (int i = 0; i < controller.Length; i++)
        {
            controller[i].SetActive(false);
        }
    }

    //早期初期化
    public void AwakeTutorialUI()
    {
        ResetTutorial();
        ts = GameObject.FindWithTag("GameController").GetComponent<TutorialScript>();
        completion = tutorialCompletion.GetComponent<TextMeshProUGUI>();
        conectController = Usefull.GetControllerScript.GetIsConectic();


        initialPos = new Vector3 (canvasTransform.position.x, canvasTransform.position.y, canvasTransform.position.z);
        initialRot = new Vector3(canvasTransform.localEulerAngles.x, canvasTransform.localEulerAngles.y, canvasTransform.localEulerAngles.z);
        initialScale = new Vector3(canvasTransform.localScale.x, canvasTransform.localScale.y, canvasTransform.localScale.z);

        emphasisBuff = new EmphasisTransformElement();


        emphasis.pos = emphasis.pos - initialPos;
        emphasis.rot = emphasis.rot - initialRot;
        emphasis.scale = emphasis.scale - initialScale;

        emphasis.pos = new Vector3(emphasis.pos.x / emphasis.time, emphasis.pos.y / emphasis.time, emphasis.pos.z / emphasis.time);
        emphasis.rot = new Vector3(emphasis.rot.x / emphasis.time, emphasis.rot.y / emphasis.time, emphasis.rot.z / emphasis.time);
        emphasis.scale = new Vector3(emphasis.scale.x / emphasis.time, emphasis.scale.y / emphasis.time, emphasis.scale.z / emphasis.time);

        SetEmphasis();
    }

}
