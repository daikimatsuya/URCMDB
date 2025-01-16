using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] controller;

    private int tutorialNumber;

    private void TutorialController()
    {

    }
    public void NextTutorial()
    {
        tutorialNumber++;
    }
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
