using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TutorialUIScript : MonoBehaviour
{
    private enum Tutorial: int
    {
        shot,
        controll,
        boost,
        quick,
        acce,
        pms
    }

    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] controller;

    private TutorialScript ts;

    //チュートリアルUI管理
    public void TutorialUIController(in PlayerScript ps)
    {
        SelectTutorial(in ps);  //UI用情報更新
        ShowUI(Usefull.GetControllerScript.GetIsConectic());    //UI表示
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
                ts.ControlleTutorial(in ps);
                break;

            case (int)Tutorial.boost:
                ts.BoostTutorial(in ps);
                break;

            case (int)Tutorial.quick:
                ts.QuickMoveTutorial(in ps);
                break;

            case (int)Tutorial.acce:
                ts.AcceTutorial(in ps);
                break;

            case (int)Tutorial.pms:
                ts.PMSTutorial(in ps);
                break;
        }
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
            if (keyboard.Length < ts.GetTutorialNum())
            {
                keyboard[keyboard.Length].SetActive(true);
            }
            keyboard[ts.GetTutorialNum()].SetActive(true);

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

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
