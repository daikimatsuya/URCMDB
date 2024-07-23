using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    private Transform cameraPos;

    private bool moveEnd;


    [SerializeField] private bool isStageSelect;
    [SerializeField] private int stageCount;
    [SerializeField] private int maxStage;

    [SerializeField] private string stage1;

    private void TitleController()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InStageSelect();
        }
        StageSelect();
    }
    private void InStageSelect()
    {
        isStageSelect = true;
    }
    private void StageSelect()
    {
        if (moveEnd&&isStageSelect)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (stageCount > 0)
                {
                    stageCount--;
                }
                else
                {
                    stageCount = maxStage;
                }

            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (stageCount < maxStage)
                {
                    stageCount++;
                }
                else
                {
                    stageCount = 0;
                }
            }

            SceneChange();
        }
    }
    private void SceneChange()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stageCount == 0)
            {
                isStageSelect = false;
                stageCount = 1;
            }
        }
    }
    public bool GetIsStageSelect()
    {
        return isStageSelect;
    }
    public Vector2 GetStageCount()
    {
        return new Vector2(stageCount, maxStage);
    }
    public void SendMoveEnd(bool end)
    {
        moveEnd = end;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        TitleController();
        stageCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        TitleController();
    }
}
