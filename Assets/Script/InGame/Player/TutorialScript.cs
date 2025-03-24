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
    private bool nextSwitch=true;

    [SerializeField] private float shotTutorialCount;
    [SerializeField] private float boostTutorialCount;
    [SerializeField] private float controlleTutorialCount;
    [SerializeField] private float PMSTutorialCount;
    [SerializeField] private float acceTutorialCount;
    [SerializeField] private float quickMoveTutorialCount;

    //射出時のチュートリアル管理
    public void ShotTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        //プレイヤーが発射されていたらチュートリアルが進行
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
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        //プレイヤーがブーストしていたらチュートリアルが進行///
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
    public void ControlleTutorial(in PlayerScript ps, in bool isConectController)
    {
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        //プレイヤーが操作されていたらチュートリアルが進行///
        if (CheckPlayerControlle(in ps, in isConectController)) 
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
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        //PMSがオンだったらチュートリアルが進行
        if (CheckPMS())    
        {
            tutorialCompletion += PMSTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }   
    //加速時のチュートリアル管理
    public void AcceTutorial(in PlayerScript ps, in bool isConectController)
    {
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        //プレイヤーが加速していたらチュートリアルが進行
        if (CheckPlayerAcce(in ps, in isConectController))  
        {
            tutorialCompletion += acceTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }
    }
    //高速旋回時のチュートリアル管理
    public void QuickMoveTutorial(in PlayerScript ps,in bool isConectController)
    {
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        //プレイヤーが高速旋回していたらチュートリアルが進行
        if (CheckQuickMove(in ps, in isConectController)&&CheckPlayerControlle(in ps,in isConectController))    
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
        //進行していたチュートリアルが終了したらリセットをかけて次のチュートリアルにする
        if (tutorialCompletion >= maxCompletion)    
        {
            tutorialCompletion = 0;
            tutorialNumber++;
            nextSwitch = true;
        }
    }
    //プレイヤーが死んだら全部リセットさせる
    public void ResetAll(in PlayerScript ps)
    {
        //プレイヤーが無かったらリセットをかける
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
    public bool CheckPlayerControlle(in PlayerScript ps,in bool isConectController)
    {
        //キーボード入力がなかったらfalseを返す
        if (!isConectController)
        {
            if(!Input.GetKey(KeyCode.LeftArrow)&& !Input.GetKey(KeyCode.RightArrow)&& !Input.GetKey(KeyCode.UpArrow)&& !Input.GetKey(KeyCode.DownArrow)&& !Input.GetKey(KeyCode.A)&& !Input.GetKey(KeyCode.D)&& !Input.GetKey(KeyCode.W)&& !Input.GetKey(KeyCode.S))
            {
                return false;
            }
            return true;
        }

        //Lスティック入力が無かったらfalseを返す
        if (Input.GetAxis("LeftStickX") == 0 && Input.GetAxis("LeftStickY") == 0)   
        {
            return false;
        }

        return true;
    }

    //プレイヤーのブーストを確認
    public bool CheckPlayerBoost(in PlayerScript ps)
    {
        float acceBuff = ps.GetAccelerate();

        //プレイヤーが自身でブーストしていたらtrueを返す
        if (acceBuff>=1) 
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
        return ps.GetIsFire();  //発射フラグをそのまま返す
    }

    //プレイヤーの加速を確認
    public bool CheckPlayerAcce(in PlayerScript ps,in bool isConectController)
    {
        //スペースが押されていなかったらfalseを返す
        if (!isConectController)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                return false;
            }
            return true;
        }

        //Rトリガーが押されていなかったらfalseを返す
        if (Input.GetAxis("RightTrigger")==0)   
        {
            return false;
        }
        
        return true;
        
    }

    //プレイヤーのPMSを確認
    public bool CheckPMS()
    {
        return Usefull.PMSScript.GetPMS();

    }

    //プレイヤークイックムーブを確認
    public bool CheckQuickMove(in PlayerScript ps,in bool isConectController)
    {
        //シフトが押されていなかったらfalseを返す
        if (!isConectController) 
        {
            if (!Input.GetKey(KeyCode.LeftShift)&&!Input.GetKey(KeyCode.RightShift))
            {
                return false;
            }
            return true;
        }

        //レフトトリガーが押されていなかったらfalseを返す
        if (Input.GetAxis("LeftTrigger") == 0)
        {
            return false;
        }
        
        return true;
        
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
    public bool GetNextSwitch()
    {
        return nextSwitch;
    }
    public void SetNextSwitch(bool flag)
    {
        nextSwitch = flag;
    }
    #endregion


}
