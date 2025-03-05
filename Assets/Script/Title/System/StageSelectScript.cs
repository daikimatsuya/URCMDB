using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Usefull;

//タイトルのステージ選択処理用
public class StageSelectScript : MonoBehaviour
{
    [SerializeField] private int baseCount;
    [SerializeField] private int maxStage;
    [SerializeField] private string[] stage;
    [SerializeField] private float stageSelectCoolTime;
    [SerializeField] private int coolTimeBuff;
    [SerializeField] private float fadeTime;
    private int fadeTimeBuff;

    private int stageCount;
    private int stageChangeCount;
    private bool fadeStart;
    private bool fadeEnd;

    private StageRotationScript srs;

    //ステージセレクト管理
    public void SelectController(in bool canStageChange)
    {
        StageSelect(in canStageChange);                          //選択してるステージ管理
        srs.Move(stageChangeCount, maxStage);              //モデルを回転させる

        if (!fadeStart) 
        {
            return;
        }
        if (TimeCountScript.TimeCounter(ref fadeTimeBuff))
        {
            fadeEnd = true;                 //フェードの終わりチェック
        }
    }

    //選択ステージ数加算減算
    private void StageSelect(in bool canStageChange)
    {
        if (canStageChange)
        {         
            return;
        }

        if (srs.GetRotateEnd())  
        {
            //左向きの回転
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (Input.GetAxis("LeftStickX") < 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && coolTimeBuff <= 0)  
            {
                stageChangeCount--;             //ステージの回転カウントを-1

                if (stageCount > 0)              
                {
                    stageCount--;                   //ステージカウントをー１   
                }
                else
                {
                    stageCount = maxStage;  //ステージカウントが０なら最大にする
                }
               TimeCountScript.SetTime(ref coolTimeBuff, stageSelectCoolTime);
            }

            //右向きの回転
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (Input.GetAxis("LeftStickX")>0||Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && coolTimeBuff <= 0)  
            {
                stageChangeCount++;        //ステージの回転カウントを+1
                if (stageCount < maxStage)
                {
                    stageCount++;              //ステージカウントを+1
                }
                else
                {
                    stageCount = 0;   //ステージカウントが最大値なら０にする
                }
                TimeCountScript.SetTime(ref coolTimeBuff, stageSelectCoolTime);
            }
            coolTimeBuff--;
        }

    }
    //ステージ選択リセット
    public void StageSelectReset()
    {
        stageCount = baseCount;
        stageChangeCount = baseCount;
        srs.ResetRotate(stageChangeCount,maxStage);
    }
 
    #region 値受け渡し
    public bool GetFadeEnd()
    {
        return fadeEnd;
    }
    public string GetStage()
    {
        return stage[stageCount];
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

    #endregion

    //コンポーネント取得
    private void GetComponets()
    {
        srs = GameObject.FindWithTag("stage").GetComponent<StageRotationScript>();
    }

    //ステージ選択初期化
    public void StartStageSelect()
    {
        GetComponets();

        stageCount = baseCount;
        stageChangeCount = baseCount;
        TimeCountScript.SetTime(ref fadeTimeBuff,fadeTime);
        fadeEnd = false;
        srs.StartStageRotation();
    }

}
