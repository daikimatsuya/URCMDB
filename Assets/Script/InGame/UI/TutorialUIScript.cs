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

    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] controller;
    [SerializeField] private GameObject tutorialCompletion;
    private TextMeshProUGUI completion;

    private TutorialScript ts;
    private bool conectController;

    //チュートリアルUI管理
    public void TutorialUIController(in PlayerScript ps)
    {
        CheckController();
        SelectTutorial(in ps);  //UI用情報更新
        ShowUI(conectController);    //UI表示
        ShowCompletion(ts.GetResetFlag());
        if (ts.GetResetFlag())
        {
            ResetTutorial();
        }
    }

    //起動するチュートリアルを選択
    private void SelectTutorial(in PlayerScript ps)
    {
        switch (ts.GetTutorialNum())
        {
            case (int)Tutorial.shot:
                ts.ShotTutorial(in ps);
                break;

            case (int)Tutorial.controll:
                ts.ControlleTutorial(in ps,conectController);
                break;

            case (int)Tutorial.boost:
                ts.BoostTutorial(in ps);
                break;

            case (int)Tutorial.quick:
                ts.QuickMoveTutorial(in ps, conectController);
                break;

            case (int)Tutorial.acce:
                ts.AcceTutorial(in ps, conectController);
                break;

            case (int)Tutorial.pms:
                ts.PMSTutorial(in ps);
                break;

            case (int)Tutorial.finish:
                ts.ResetAll(in ps);
                break;
        }
    }

    //チュートリアル達成度表示
    private void ShowCompletion(in bool showFlag)
    {
        tutorialCompletion.SetActive(!showFlag);
        completion.text = ts.GetCompletion() + "%";
    }

   
    private void ShowUI(in bool isConectController)
    {
        if(isConectController)
        {

            if (controller.Length == 0)
            {
                return;
            }
            if(controller.Length <=ts.GetTutorialNum())
            {
                controller[controller.Length-1].SetActive(true);
                controller[controller.Length-2].SetActive(false);
                return;
            }
            if(ts.GetTutorialNum() == 0)
            {
                controller[controller.Length-1].SetActive(false);
            }
            else
            {
                controller[ts.GetTutorialNum()-1].SetActive(false);
            }
            controller[ts.GetTutorialNum()].SetActive(true);

        }
        else
        {

            if (keyboard.Length == 0)
            {
                return;
            }
            if (keyboard.Length <= ts.GetTutorialNum())
            {
                keyboard[keyboard.Length - 1].SetActive(true);
                keyboard[keyboard.Length - 2].SetActive(false);
                return;
            }
            if (ts.GetTutorialNum() == 0)
            {
                keyboard[keyboard.Length - 1].SetActive(false);
            }
            else
            {
                keyboard[ts.GetTutorialNum() - 1].SetActive(false);
            }
            keyboard[ts.GetTutorialNum()].SetActive(true);

        }
    }

    //コントローラーの接続をチェック
    private void CheckController()
    {
        bool buff = conectController;
        conectController = Usefull.GetControllerScript.GetIsConectic();
        if (buff != conectController)
        {
            ResetTutorial();
        }
    }
    //チュートリアル表示をリセットさせる
    private void ResetTutorial()
    {
        for (int i = 0; i < keyboard.Length; i++)
        {
            keyboard[i].SetActive(false);
        }
        for (int i = 0; i < controller.Length; i++)
        {
            controller[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetTutorial();
        ts=GameObject.FindWithTag("GameController").GetComponent<TutorialScript>();
        completion = tutorialCompletion.GetComponent<TextMeshProUGUI>();
        conectController = Usefull.GetControllerScript.GetIsConectic();

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
