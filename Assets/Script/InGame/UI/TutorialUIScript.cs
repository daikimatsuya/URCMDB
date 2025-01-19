using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TutorialUIScript : MonoBehaviour
{
    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] controller;

    private int tutorialNumber;
    private TutorialScript ts;

    public void TutorialUIController()
    {
        ts.TutorialContoroller();   //情報更新


    }

    private void ShotTutorial()
    {

    }
    private void ControlleTutorial()
    {

    }
    private void BoostTutorial()
    {

    }
    private void NextTutorial()
    {
        tutorialNumber++;
    }

    //チュートリアル表示をリセットさせる
    private void ResetTutorial()
    {
        tutorialNumber = 0;
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
