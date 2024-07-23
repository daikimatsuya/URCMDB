using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    private Transform cameraPos;


    private bool moveEnd;
    private bool rotateEnd;
    private int stageChangeCount;
    private bool isSceneChange;
    private bool isPush;

    [SerializeField] private bool isStageSelect;
    [SerializeField] private int stageCount;
    [SerializeField] private int maxStage;

    [SerializeField] private string stage1;

    private void TitleController()
    {
        isPush = false;
        SceneChange();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InStageSelect();
        }
        StageSelect();
    }
    private void InStageSelect()
    {
        if (!isPush)
        {
            isStageSelect = true;
            isPush = true;
        }
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

        }
    }
    private void SceneChange()
    {
        if (!isPush)
        {
            if (moveEnd && isStageSelect && rotateEnd)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (stageCount == 0)
                    {
                        isStageSelect = false;
                        stageCount = 1;
                        stageChangeCount = 1;
                    }
                    else
                    {
                        isSceneChange = true;
                    }
                    isPush = true;
                }
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
    public bool GetIsSceneChange()
    {
        return isSceneChange;
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
