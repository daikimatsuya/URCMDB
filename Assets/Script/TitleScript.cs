using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    private Transform cameraPos;

    [SerializeField] private bool isStageSelect;
    [SerializeField] private int selectStageCount;

    private void TitleController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InStageSelect();
        }
    }
    private void InStageSelect()
    {
        isStageSelect = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        TitleController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
