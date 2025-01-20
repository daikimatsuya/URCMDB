using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TutorialUIScript : MonoBehaviour
{
    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] controller;


    private TutorialScript ts;

    public void TutorialUIController(in PlayerScript ps)
    {
        SelectTutorial();
        ShowUI(Usefull.GetControllerScript.GetIsConectic());
    }

    private void SelectTutorial()
    {

    }

    private void ShotUI(in PlayerScript ps)
    {
        ts.ShotTutorial(in ps);
    }
    private void BoostUI(in PlayerScript ps)
    {
        ts.BoostTutorial(in ps);
    }
    private void ControlleUI(in PlayerScript ps)
    {
        ts.ControlleTutorial(in ps);
    }
    private void PMSUI(in PlayerScript ps)
    {
        ts.PMSTutorial(in ps);
    }
    private void AcceUI(in PlayerScript ps)
    {
        ts.AcceTutorial(in ps);
    }
    private void QuickMoveUI(in PlayerScript ps)
    {
        ts.QuickMoveTutorial(in ps);
    }
    private void ShowUI(in bool isConectController)
    {
        if(isConectController)
        {
            controller[ts.GetTutorialNum()].SetActive(true);

        }
        else
        {
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
