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
