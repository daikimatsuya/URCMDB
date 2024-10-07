using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    private Transform cameraPos;
    private TitlegameScript ts;


    private bool moveEnd;
    private bool rotateEnd;
    private int stageChangeCount;
    private bool isSceneChange;
    private bool isPush;
    private int betBuff;
    [SerializeField] private int coolTimeBuff;

    [SerializeField] private bool isStageSelect;
    [SerializeField] private int stageCount;
    [SerializeField] private int maxStage;
    [SerializeField] private float betTime;
    [SerializeField] private float stageSelectCoolTime;

    [SerializeField] private string []stage;

    private void TitleController()
    {
        isPush = false;
        SceneChange();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InStageSelect();
        }
        StageSelect();
        SceneCountDow();
    }
    private void InStageSelect()
    {
        if (!isPush)
        {
            ts.SetResetActionFlag(true);
            isStageSelect = true;
            isPush = true;
        }
    }
    private void StageSelect()
    {
        if (moveEnd&&isStageSelect&&rotateEnd)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && coolTimeBuff == 0)) 
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
                coolTimeBuff = (int)(stageSelectCoolTime * 60);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && coolTimeBuff == 0))
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
                coolTimeBuff = (int)(stageSelectCoolTime * 60);
            }
            coolTimeBuff--;
        }
    }
    private void SceneCountDow()
    {
        if(isSceneChange)
        {
            if (betBuff <= 0)
            {
                SceneManager.LoadScene(stage[stageCount]);
            }
            betBuff--;
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
                        betBuff = (int)(betTime * 60);
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

        ts=GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();

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
