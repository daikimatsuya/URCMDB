using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public class EmphasisTransformElement
    {
        [SerializeField] public Vector3 pos;
        [SerializeField] public Vector3 rot;
        [SerializeField] public Vector3 scale;
        [SerializeField] public float time;
    }

    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] controller;
    [SerializeField] private GameObject tutorialCompletion;
    private TextMeshProUGUI completion;
    [SerializeField] private EmphasisTransformElement emphasis;
    private EmphasisTransformElement emphasisBuff;

    private TutorialScript ts;

    private bool conectController;

    //チュートリアルUI管理
    public void TutorialUIController(in PlayerScript ps,in bool isConect)
    {
        CheckController(in isConect);              //コントローラーの接続を確認
        SelectTutorial(in ps);                          //UI用情報更新
        ShowUI(conectController);                 //UI表示
        ShowCompletion(ts.GetResetFlag());  //チュートリアル進行度を表示

        if (ts.GetResetFlag())  
        {
            ResetTutorial();    //リセットフラグがオンになったらリセットさせる
        }
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
    }

}
