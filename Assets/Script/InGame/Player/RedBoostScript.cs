using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

public class RedBoostScript : MonoBehaviour
{
    [SerializeField] GameObject boostEffect;
    [SerializeField] private GameObject redEffect;
    [SerializeField] private float redBoostTime;
    private float redBoostTimeBuff = 0;
    [SerializeField] private float redBoostAcce;
    [SerializeField] private Vector3 fireSize;
    [SerializeField] private Vector3 firePos;

    private float redSpeedBuff;
    private bool isBoost;
    private List<BoostEffectScript> redBoostEffectList = new List<BoostEffectScript>();

    //RedBoost管理
    public void RedBoostController(in float playerSpeed,in bool redBustFlag)
    {
        RedBoost(playerSpeed);
        EffectController();
        RedyEffect(redBustFlag);
    }

    //ブースト値管理
    private void RedBoost(in float playerSpeed)
    {
        if (!TimeCountScript.TimeCounter(ref redBoostTimeBuff))
        {
            redSpeedBuff += playerSpeed * redBoostAcce;
            CreateRedBoostEffect();
        }
        else if (redSpeedBuff == 0)
        {
            return;
        }
        else
        {
            isBoost = false;
            redSpeedBuff -= playerSpeed * redBoostAcce; ;
            if (redSpeedBuff < 0)
            {
                redSpeedBuff = 0;
            }
        }
    }
    //ブースト起動
    public void SetFlagOn()
    {
        TimeCountScript.SetTime(ref redBoostTimeBuff, redBoostTime);
        isBoost=true;
    }

    //ブースト可能の時のエフェクトオンオフ切り替え
    private void RedyEffect(in bool flag)
    {
        redEffect.SetActive(flag);
    }

    //ブーストエフェクト生成
    private void CreateRedBoostEffect()
    {
        GameObject _ = Instantiate(boostEffect);                                   //エフェクト生成
        _.transform.SetParent(this.transform, true);                               //エフェクトをプレイヤーと親子付け
        _.transform.localPosition = firePos;                                            //ポジション代入
        _.transform.localEulerAngles = new Vector3(0, 180, 0);              //角度代入
        _.transform.localScale = fireSize;                                               //サイズ代入
        BoostEffectScript bf = _.GetComponent<BoostEffectScript>();     //コンポーネント取得
        bf.SetTime();                                                                           //生存時間セット
        redBoostEffectList.Add(bf);                                                       //リストに入れる
    }

    //エフェクト管理
    private void EffectController()
    {
        for (int i = 0; i < redBoostEffectList.Count;)
        {
            if (redBoostEffectList[i].IsDelete())  //破壊フラグがオンになっていたら
            {
                redBoostEffectList[i].Break();                              //オブジェクトを削除
                redBoostEffectList.Remove(redBoostEffectList[i]); //リストから削除

            }/////////////////////////////////////////////////////////////////////
            else
            {
                redBoostEffectList[i].CountTime(); //生存時間を現象
                i++;
            }
        }
    }

    //初期化
    public void StartRedBoost()
    {
        redEffect.SetActive(false);
        isBoost = false;
    }

    #region 値受け渡し
    public float GetRedBurstSpeed()
    {
        return redSpeedBuff;
    }
    public bool GetIsBoost()
    {
        return isBoost;
    }
    #endregion

}
