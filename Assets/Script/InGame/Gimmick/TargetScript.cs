using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//jóÚWÇ
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

    //^[QbgÇ
    public void TargetController(in bool isPose)
    {
        if(isPose)
        {
            return;
        }
        IsBreak();  //jóÇ
    }

    //ÁÅ³¹é
    private void IsBreak()
    {
        if (!isBreak)
        {
            return;
        }
        if(Usefull.TimeCountScript.TimeCounter(ref brokeTimeBuff))
        {
            CreateFragments();                                         //jÐ¶¬
            Destroy(this.gameObject);                               //IuWFNgí
        }
    }
    //jÐ¶¬
    private void CreateFragments()
    {
        GameObject _ = Instantiate(fragments);
        _.transform.position = this.transform.position;
    }
    //¼ófÉ·µÖ¦
    private void Breakage()
    {
        model.SetActive(false);
        brokenModel.SetActive(true);
    }

    #region ló¯nµ
    //HPnµ
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

    //ú»
    public void StartTarget()
    {
        Usefull.TimeCountScript.SetTime(ref brokeTimeBuff, brokeTime);
        maxHp = hp;
    }

}
