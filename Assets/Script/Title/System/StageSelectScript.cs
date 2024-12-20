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
    //ステージセレクト管理
    private void SelectController()
    {
        if (fadeTimeBuff <= 0)
        {
            ts.SceneChange();
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
    //選択ステージ数加算減算
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
    //ステージ選択リセット
    private void StageSelectReset()
    {
        stageCount = 0;
        //stageChangeCount = 0;
    }
 
    #region 値受け渡し
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
    public Vector2 GetStageChangeCount()
    {
        return new Vector2(stageChangeCount,maxStage);
    }
    public int GetStageCount()
    {
        return stageCount;
    }
    public void SetFadeFlag(bool flag)
    {
        fadeStart = flag;
    }
    public TitleScript GetTitleScript()
    {
        return ts;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ts=GetComponent<TitleScript>();

        stageCount = 0;
        stageChangeCount = 0;
        rotateEnd = true;
        fadeTimeBuff = (int)(fadeTime * 60);
    }

    // Update is called once per frame
    void Update() 
    {
        SelectController();
    }
}
