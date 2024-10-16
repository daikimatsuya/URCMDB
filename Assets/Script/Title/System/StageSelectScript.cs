using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//タイトルのステージ選択処理用
public class StageSelectScript : MonoBehaviour
{
    [SerializeField] private int stageCount;
    [SerializeField] private int maxStage;
    [SerializeField] private string[] stage;
    [SerializeField] private float stageSelectCoolTime;
    [SerializeField] private int coolTimeBuff;
    [SerializeField] private float fadeTime;
    private int fadeTimeBuff;


    private bool rotateEnd;
    private int stageChangeCount;
    private bool fadeStart;

    TitleScript ts;

    private void SelectController()
    {
        if (fadeTimeBuff <= 0)
        {
            SceneChange();
        }
        
        if (ts.GetStageSelectFlag())   
        {
            StageSelect();
        }
        else
        {
            StageSelectReset();
        }

        ts.SetStage(stage[stageCount]);

        if (fadeStart)
        {
            fadeTimeBuff--;
        }
    }
    private void StageSelect()
    {
        if (rotateEnd)
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
    private void StageSelectReset()
    {
        stageCount = 1;
        stageChangeCount = 1;
    }

    private void SceneChange()
    {
        SceneManager.LoadScene(stage[stageCount]);
    }

    public void SetRotateEnd(bool flag)
    {
        rotateEnd = flag;
    }
    public string GetStage()
    {
        return stage[stageCount];
    }
    public bool GetRotateEnd() 
    {
        return rotateEnd;
    }
    public Vector2 GetStageCount()
    {
        return new Vector2(stageChangeCount,maxStage);
    }
    public void SetFadeFlag(bool flag)
    {
        fadeStart = flag;
    }
    // Start is called before the first frame update
    void Start()
    {
        ts=GetComponent<TitleScript>();

        stageCount = 1;
        stageChangeCount = 1;
        rotateEnd = true;
        fadeTimeBuff = (int)(fadeTime * 60);
    }

    // Update is called once per frame
    void Update() 
    {
        SelectController();
    }
}
