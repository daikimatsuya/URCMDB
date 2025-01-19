using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//チュートリアルを管理
public class TutorialScript : MonoBehaviour
{
    private PlayerScript ps;

    private float tutorialCompletion;
    private float maxCompletion;
    private float playerAcce;
    private Vector3 playerRot;
    private bool isControlled;
    private bool isBoost;
    private bool isShot;
    private bool isAcce;
    private bool isPMS;
    private bool isQuick;

    //チュートリアル管理用
    public void TutorialContoroller()
    {
        if(ps != null)
        {
            CheckPlayerRot();   //プレイヤーが操作されているのかを確認する
            CheckPlayerBoost();  //プレイヤーがブーストしているのかを確認する
            CheckPlayerShot();  //プレイヤーが射出されているかを確認する
            CheckPlayerAcce();  //プレイヤーが加速しているのかを確認する
            CheckPMS(); //プレイヤーのPMSを確認する
        }
        else
        {
            SetPlayer();    //プレイヤーを取得する
            ResetFlags();   //フラグ類をリセットする
            if (ps != null)
            {
                playerRot=ps.GetPlayerRot();    //値を代入
                playerAcce = ps.GetPlayerSpeedBuffFloat();  //値を代入
            }
        }
    }

    //プレイヤーの角度を比較
    private void CheckPlayerRot()
    {
        Vector3 rotBuff=ps.GetPlayerRot();
        if (rotBuff != playerRot)
        {
            isControlled = true;
        }
        else
        {
            isControlled = false;
        }
        playerRot = rotBuff;
    }

    //プレイヤーのブーストを確認
    private void CheckPlayerBoost()
    {
        float acceBuff = ps.GetPlayerSpeedBuffFloat();
        if(acceBuff!=0)
        {
            isBoost = true;
        }
        else
        {
            isBoost = false;
        }
    }

    //プレイヤーが射出されているのかを確認
    private void CheckPlayerShot()
    {
        if (ps.GetIsFire())
        {
            isShot = true;
        }
        else
        {
            isControlled = false;
        }
    }

    //プレイヤーの加速を確認
    private void CheckPlayerAcce()
    {
        if(Input.GetAxis("RightTrigger")!=0)
        {
            isAcce = true;
        }
        else
        {
            isAcce = false;
        }
    }

    //プレイヤーのPMSを確認
    private void CheckPMS()
    {
        if (ps.GetPMS())
        {
            isPMS = true;
        }
        else
        {
            isPMS = false;
        }
    }

    //プレイヤークイックムーブを確認
    private void CheckQuickMove()
    {
        if (Input.GetAxis("LeftTrigger") != 0)
        {
            isQuick=true;
        }
        else
        {
            isQuick = false;
        }
    }

    //フラグリセット
    private void ResetFlags()
    {
        isBoost=false;
        isControlled=false;
        isShot=false;
    }
    #region 値受け渡し

    public bool GetIsControll()
    {
        return isControlled;
    }
    public bool GetIsBoost()
    {
        return isBoost;
    }
    public bool GetIsShot()
    {
        return isShot;
    }
    public bool GetIsAcce()
    {
        return isAcce;
    }
    public bool GetIsPMS()
    {
        return isPMS;
    }
    #endregion

    //プレイヤー取得用
    private void SetPlayer()
    {
        ps = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();  
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
