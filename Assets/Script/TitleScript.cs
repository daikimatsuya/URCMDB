using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    private Transform cameraPos;


    private bool moveEnd;
    private bool rotateEnd;
    [SerializeField] private int stageChangeCount;

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
        if (moveEnd&&isStageSelect&&rotateEnd)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                stageChangeCount--;
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
                stageChangeCount++;
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
                stageChangeCount = 1;
            }
        }
    }
    public bool GetIsStageSelect()
    {
        return isStageSelect;
    }
    public Vector2 GetStageCount()
    {
        return new Vector2(stageChangeCount, maxStage);
    }
    public void SendMoveEnd(bool end)
    {
        moveEnd = end;
    }
    public void SendRotateEnd(bool end)
    {
        rotateEnd = end;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        TitleController();
        stageCount = 1;
        stageChangeCount = 1;
        rotateEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        TitleController();
    }
}
