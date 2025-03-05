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
    private bool rotateEnd;
    private int stageChangeCount;
    private bool fadeStart;
    private bool fadeEnd;

    private StageRotationScript srs;

    //ステージセレクト管理
    public void SelectController(in bool canStageChange)
    {

        if (fadeTimeBuff <= 0)  //フェード時間が経過したらシーンを変える///
        {
            fadeEnd = true;
        }////////////////////////////////////////////////////////////////////////

        StageSelect(in canStageChange);
        srs.Move(stageChangeCount, maxStage);
        if (fadeStart)
        {
            fadeTimeBuff--; //フェードが始まったらカウントダウン開始
        }
    }

    //選択ステージ数加算減算
    private void StageSelect(in bool canStageChange)
    {
        if (!canStageChange)
        {
            StageSelectReset(); //カウントしたステージ数をリセットする
            return;
        }

        if (srs.GetRotateEnd())  //ステージの回転が終わっていたら////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || (Input.GetAxis("LeftStickX") < 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && coolTimeBuff <= 0)  //左向きのステージ切り替えキーを一回または一定時間以上押していたら///////////
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

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || (Input.GetAxis("LeftStickX")>0||Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && coolTimeBuff <= 0)  //右向きのステージ切り替えキーを一回または一定時間以上押していたら///////////
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
        stageCount = baseCount;
        stageChangeCount = baseCount;
        srs.ResetRotate(stageChangeCount,maxStage);
    }
 
    #region 値受け渡し
    public bool GetFadeEnd()
    {
        return fadeEnd;
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

    private void GetComponets()
    {
        srs = GameObject.FindWithTag("stage").GetComponent<StageRotationScript>();
    }

    // Start is called before the first frame update
    public void StartStageSelect()
    {
        GetComponets();

        stageCount = baseCount;
        stageChangeCount = baseCount;
        rotateEnd = true;
        TimeCountScript.SetTime(ref fadeTimeBuff,fadeTime);
        fadeEnd = false;

        srs.StartStageRotation();
    }

}
