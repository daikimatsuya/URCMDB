using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//ステージセレクトからインゲームへの演出管理
public class SceneChangeAnimationScript : MonoBehaviour
{
    [SerializeField] private GameObject missile;
    [SerializeField] private GameObject pad;
    [SerializeField] private float targetRot;
    private float rotationSpeed;
    [SerializeField] private float rotationTime;
    private float rotationTimeBuff;
    [SerializeField] private float missileMovepeed;

    private float padRotBuff;
    private bool isFadeStart;
    private bool isEnd;

    //発射台を上下させる
    public void UpDown(in bool isDown)
    {
        isEnd=false;
        if (isDown)  //上下移動フラグがオンの時//////////////////////////////////////////////////////////////////////////////////////
        {
            if (!TimeCountScript.TimeCounter(ref rotationTimeBuff))
            {
                padRotBuff += rotationSpeed;    //回転角に速度を足す
                pad.transform.localEulerAngles = new Vector3(padRotBuff, pad.transform.localEulerAngles.y, pad.transform.localEulerAngles.z);   //トランスフォームに代入
            }
            else
            {
                isEnd = true;
            }
     
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Z
        else   //上下移動フラグがオフの時
        {
            if (rotationTimeBuff < (int)(rotationTime * 60))
            {
                padRotBuff -= rotationSpeed;    //回転角に速度を減算
                pad.transform.localEulerAngles = new Vector3(padRotBuff, pad.transform.localEulerAngles.y, pad.transform.localEulerAngles.z);  //トランスフォームに代入

                rotationTimeBuff++;
            }
        }
    }
    //フラグリセット
    private void ResetFlags()
    {
        isFadeStart = false;
    }
    #region 値受け渡し
    public void SetStartFadeFlag(bool flag)
    {
        isFadeStart = flag;
    }
    public bool GetIsFadeStartFlag()
    {
        return isFadeStart;
    }
    public bool GetEndDown()
    {
        return isEnd;
    }
    #endregion
    public void StartSceneChangeAnimation()
    {
        padRotBuff = pad.transform.localEulerAngles.x;
        rotationTimeBuff = (int)(rotationTime * 60);
        rotationSpeed = targetRot / rotationTimeBuff;
        isEnd = false;
        ResetFlags();
    }

}
