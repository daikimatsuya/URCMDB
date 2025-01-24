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
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        if (CheckPlayerShot(in ps)) //プレイヤーが発射されていたらチュートリアルが進行////
        {
            tutorialCompletion += shotTutorialCount;
            if(tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }//////////////////////////////////////////////////////////////////////////////////////
    }
    //ブースト時のチュートリアル管理
    public void BoostTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        if (CheckPlayerBoost(in ps))    //プレイヤーがブーストしていたらチュートリアルが進行///
        {
            tutorialCompletion += boostTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }////////////////////////////////////////////////////////////////////////////////////////////
    }
    //操作時のチュートリアル管理
    public void ControlleTutorial(in PlayerScript ps, in bool isConectController)
    {
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        if (CheckPlayerControlle(in ps, in isConectController)) //プレイヤーが操作されていたらチュートリアルが進行///
        {
            tutorialCompletion += controlleTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }    
    //PMSのチュートリアル管理
    public void PMSTutorial(in PlayerScript ps)
    {
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        if (CheckPMS(in ps))    //PMSがオンだったらチュートリアルが進行///
        {
            tutorialCompletion += PMSTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }//////////////////////////////////////////////////////////////////////
    }   
    //加速時のチュートリアル管理
    public void AcceTutorial(in PlayerScript ps, in bool isConectController)
    {
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        if (CheckPlayerAcce(in ps, in isConectController))  //プレイヤーが加速していたらチュートリアルが進行///////////
        {
            tutorialCompletion += acceTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    //高速旋回時のチュートリアル管理
    public void QuickMoveTutorial(in PlayerScript ps,in bool isConectController)
    {
        ResetAll(in ps);    //プレイヤーが死んだらリセット
        ResetTutorial();    //このチュートリアルが終わったら次に進む

        if (CheckQuickMove(in ps, in isConectController)&&CheckPlayerControlle(in ps,in isConectController))    //プレイヤーが高速旋回していたらチュートリアルが進行////
        {
            tutorialCompletion += quickMoveTutorialCount;
            if (tutorialCompletion > maxCompletion)
            {
                tutorialCompletion = maxCompletion;
            }
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


    //チュートリアルが完了したらリセットをかける
    private void ResetTutorial()
    {
        if (tutorialCompletion >= maxCompletion)    //進行していたチュートリアルが終了したらリセットをかけて次のチュートリアルにする//
        {
            tutorialCompletion = 0;
            tutorialNumber++;
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    //プレイヤーが死んだら全部リセットさせる
    public void ResetAll(in PlayerScript ps)
    {
        if (ps == null) //プレイヤーが無かったらリセットをかける//
        {
            isReset = true;
            tutorialNumber = 0;
            tutorialCompletion = 0;
            return;
        }/////////////////////////////////////////////////////////////

        isReset = false;
    }

    #region チュートリアルチェッカー
    //プレイヤーの角度を比較
    public bool CheckPlayerControlle(in PlayerScript ps,in bool isConectController)
    {
        if (!isConectController) //コントローラーが接続されていなかったらfalseを返す///////
        {
            return false;
        }/////////////////////////////////////////////////////////////////////////////////////

        if (Input.GetAxis("LeftStickX") == 0 && Input.GetAxis("LeftStickY") == 0)   //Lスティック入力が無かったらfalseを返す///
        {
            return false;
        }////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        return true;
    }

    //プレイヤーのブーストを確認
    public bool CheckPlayerBoost(in PlayerScript ps)
    {
        float acceBuff = ps.GetPlayerAcce();

        if(acceBuff>=1) //プレイヤーが自身でブーストしていたらtrueを返す////////
        {
            return true;
        }
        else
        {
            return false;
        }/////////////////////////////////////////////////////////////////////////////
    }

    //プレイヤーが射出されているのかを確認
    public bool CheckPlayerShot(in PlayerScript ps)
    {
        return ps.GetIsFire();  //発射フラグをそのまま返す
    }

    //プレイヤーの加速を確認
    public bool CheckPlayerAcce(in PlayerScript ps,in bool isConectController)
    {
        if (!isConectController) //コントローラーが接続されていなかったらfalseを返す///////
        {
            return false;
        }/////////////////////////////////////////////////////////////////////////////////////

        if (Input.GetAxis("RightTrigger")==0)   //Rトリガーが押されていなかったらfalseを返す//
        {
            return false;
        }////////////////////////////////////////////////////////////////////////////////////////////
        
        return true;
        
    }

    //プレイヤーのPMSを確認
    public bool CheckPMS(in PlayerScript ps)
    {
        return ps.GetPMS();   //PMSのフラグをそのまま返す

    }

    //プレイヤークイックムーブを確認
    public bool CheckQuickMove(in PlayerScript ps,in bool isConectController)
    {
        if(!isConectController) //コントローラーが接続されていなかったらfalseを返す///////
        {
            return false;
        }/////////////////////////////////////////////////////////////////////////////////////

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
