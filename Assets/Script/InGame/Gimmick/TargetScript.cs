using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//破壊目標管理
public class TargetScript : MonoBehaviour
{
    [SerializeField] private float hp;
    private float maxHp;
    [SerializeField] private float  explodeTime;
    [SerializeField] private float brokePercent;
    [SerializeField] private float brokeTime;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject brokenModel;
    [SerializeField] private GameObject fragments;
    private int brokeTimeBuff;
    private int explodeTimeBuff;

    private bool isHit;
    private bool isBreak;

    //ターゲット管理
    public void TargetController(in bool isPose)
    {
        if(isPose)
        {
            return;
        }
        IsBreak();  //破壊管理
    }

    //消滅させる
    private void IsBreak()
    {
        if (!isBreak)
        {
            return;
        }
        if(Usefull.TimeCountScript.TimeCounter(ref brokeTimeBuff))
        {

            GameObject _=Instantiate(fragments);
            _.transform.position=this.transform.position;
            Destroy(this.gameObject);                               //オブジェクト削除
        }
    }

    private void Breakage()
    {
        model.SetActive(false);
        brokenModel.SetActive(true);
    }

    #region 値受け渡し
    //HP渡し
    public float GetHp()
    {
        return hp;
    }
    public Vector3 GetPos()
    {
        return this.transform.position;
    }
    public bool GetHit()
    {
        return isHit;
    }
    public bool GetBreak()
    {
        return isBreak;
    }
    #endregion
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript ps=collision.gameObject.GetComponent<PlayerScript>();
            hp-=(int)(ps.GetPlayerSpeedBuffFloat()/10);

            if (hp / maxHp * 100 < brokePercent)
            {
                Breakage();
            }
            if (hp <= 0)
            {
                isBreak = true;
                return;
            }

            isHit = true;
        }
    }

    //初期化
    public void StartTarget()
    {
        Usefull.TimeCountScript.SetTime(ref brokeTimeBuff, brokeTime);
        maxHp = hp;
    }

}
