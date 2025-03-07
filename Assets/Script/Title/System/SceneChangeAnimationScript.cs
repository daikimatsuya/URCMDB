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

    TitleScript ts;

    private float padRotBuff;
    private bool isShot;
    private bool isFadeStart;

    //演出管理
    private void AnimationController()
    {
        UpDown();   //発射台が上下する
    }
    //発射台を上下させる
    private void UpDown()
    {
        if (ts.GetIsSceneChangeModeFlag())  //上下移動フラグがオンの時//////////////////////////////////////////////////////////////////////////////////////
        {
            if (!TimeCountScript.TimeCounter(ref rotationTimeBuff))
            {
                padRotBuff += rotationSpeed;    //回転角に速度を足す
                pad.transform.localEulerAngles = new Vector3(padRotBuff, pad.transform.localEulerAngles.y, pad.transform.localEulerAngles.z);   //トランスフォームに代入
            }
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
        isShot=false;
        isFadeStart=false;
    }
    #region 値受け渡し
    public bool GetIsShotFlag()
    {
        return isShot;
    }
    public void SetStartFadeFlag(bool flag)
    {
        isFadeStart = flag;
    }
    public bool GetIsFadeStartFlag()
    {
        return isFadeStart;
    }
    #endregion
    public void StartSceneChangeAnimation()
    {
        ts = GameObject.FindWithTag("TitleManager").GetComponent<TitleScript>();

        padRotBuff = pad.transform.localEulerAngles.x;
        rotationTimeBuff = (int)(rotationTime * 60);
        rotationSpeed = targetRot / rotationTimeBuff;

        ResetFlags();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartSceneChangeAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationController();
    }
}
