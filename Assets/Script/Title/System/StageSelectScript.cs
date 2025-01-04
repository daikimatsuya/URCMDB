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
        if (fadeTimeBuff <= 0)  //フェード時間が経過したらシーンを変える///
        {
            ts.SceneChange();
        }////////////////////////////////////////////////////////////////////////

        if (ts.GetStageSelectFlag())   //ステージ選択画面になったらステージ数をカウントする////////////
        {
            StageSelect();
        }///////////////////////////////////////////////////////////////////////////////////////////////////

        else
        {
            StageSelectReset(); //カウントしたステージ数をリセットする
        }

        ts.SetStage(stage[stageCount]); //カウントしたステージ数を代入

        if (fadeStart)
        {
            fadeTimeBuff--; //フェードが始まったらカウントダウン開始
        }
    }
    //選択ステージ数加算減算
    private void StageSelect()
    {
        if (rotateEnd)  //ステージの回転が終わっていたら////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && coolTimeBuff == 0))  //左向きのステージ切り替えキーを一回または一定時間以上押していたら///////////
            {
                stageChangeCount--; //ステージの回転カウントを-1
                if (stageCount > 0) //選択しているステージが１以上なら//////
                {
                    stageCount--;   //ステージカウントを-1
                }////////////////////////////////////////////////////////////////
                else
                {
                    stageCount = maxStage;  //ステージカウントを最大値に
                }
               TimeCountScript.SetTime(ref coolTimeBuff, stageSelectCoolTime);
            }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && coolTimeBuff == 0))  //右向きのステージ切り替えキーを一回または一定時間以上押していたら///////////
            {
                stageChangeCount++; //ステージの回転カウントを+1
                if (stageCount < maxStage)//選択しているステージが最大値未満なら//////
                {
                    stageCount++; //ステージカウントを-1
                }////////////////////////////////////////////////////////////////////////////
                else
                {
                    stageCount = 0;   //ステージカウントを0に
                }
                TimeCountScript.SetTime(ref coolTimeBuff, stageSelectCoolTime);
            }/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            coolTimeBuff--;
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
    //ステージ選択リセット
    private void StageSelectReset()
    {
        stageCount = 0;
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
