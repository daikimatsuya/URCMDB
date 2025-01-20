using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//チュートリアルを管理
public class TutorialScript : MonoBehaviour
{
    private int tutorialNumber = 0;
    private float tutorialCompletion = 0;
    private float maxCompletion = 100.0f;
    private bool isReset;

    [SerializeField] private float shotTutorialCount;
    [SerializeField] private float boostTutorialCount;
    [SerializeField] private float controlleTutorialCount;
    [SerializeField] private float PMSTutorialCount;
    [SerializeField] private float acceTutorialCount;
    [SerializeField] private float quickMoveTutorialCount;

    //射出時のチュートリアル管理
    public void ShotTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);
        ResetTutorial();
        if (CheckPlayerShot(in ps))
        {
            tutorialCompletion += shotTutorialCount;
            if(tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }
    //ブースト時のチュートリアル管理
    public void BoostTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);
        ResetTutorial();
        if (CheckPlayerBoost(in ps))
        {
            tutorialCompletion += boostTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }
    //操作時のチュートリアル管理
    public void ControlleTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);
        ResetTutorial();
        if (CheckPlayerControlle(in ps))
        {
            tutorialCompletion += controlleTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }    
    //PMSのチュートリアル管理
    public void PMSTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);
        ResetTutorial();
        if (CheckPMS(in ps))
        {
            tutorialCompletion += PMSTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }   
    //加速時のチュートリアル管理
    public void AcceTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);
        ResetTutorial();
        if (CheckPlayerAcce(in ps))
        {
            tutorialCompletion += acceTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }
    //高速旋回時のチュートリアル管理
    public void QuickMoveTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);
        ResetTutorial();
        if (CheckQuickMove(in ps)&&CheckPlayerControlle(in ps))
        {
            tutorialCompletion += quickMoveTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }


    //チュートリアルが完了したらリセットをかける
    private void ResetTutorial()
    {
        if (tutorialCompletion >= maxCompletion)
        {
            tutorialCompletion = 0;
            tutorialNumber++;
        }
    }
    //プレイヤーが死んだら全部リセットさせる
    public void ResetAll(in PlayerScript ps)
    {
        if (ps == null)
        {
            isReset = true;
            tutorialNumber = 0;
            tutorialCompletion = 0;
            return;
        }
        isReset = false;
    }

    #region チュートリアルチェッカー
    //プレイヤーの角度を比較
    public bool CheckPlayerControlle(in PlayerScript ps)
    {
        if (Input.GetAxis("LeftStickX") != 0 || Input.GetAxis("LeftStickY") != 0) 
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    //プレイヤーのブーストを確認
    public bool CheckPlayerBoost(in PlayerScript ps)
    {
        float acceBuff = ps.GetPlayerAcce();
        if(acceBuff>=1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //プレイヤーが射出されているのかを確認
    public bool CheckPlayerShot(in PlayerScript ps)
    {
        if (ps.GetIsFire())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //プレイヤーの加速を確認
    public bool CheckPlayerAcce(in PlayerScript ps)
    {
        if(Input.GetAxis("RightTrigger")!=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //プレイヤーのPMSを確認
    public bool CheckPMS(in PlayerScript ps)
    {
        if (ps.GetPMS())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //プレイヤークイックムーブを確認
    public bool CheckQuickMove(in PlayerScript ps)
    {
        if (Input.GetAxis("LeftTrigger") != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion


    #region 値受け渡し

    public int GetTutorialNum()
    {
        return tutorialNumber;
    }
    public int GetCompletion()
    {
        return (int)tutorialCompletion;
    }
    public bool GetResetFlag()
    {
        return isReset;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
