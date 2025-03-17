using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

public class EMPbotScript : MonoBehaviour
{
    [SerializeField] private float empInterval;
    private int intervalBuff;
    [SerializeField] private float chargeTime;
    private float chargeTimeBuff;
    [SerializeField] private float explodeTime;
    private float explodeTimeBuff;  
    [SerializeField] private GameObject EMP;
    [SerializeField] private bool isDeploy;

    private List<EMPScript> empList;

    public void EMPbotController()
    {
        if (isDeploy)
        {
            return;
        }
        if(TimeCountScript.TimeCounter(ref intervalBuff))
        {
            CreateEMP();
            TimeCountScript.SetTime(ref chargeTimeBuff, chargeTime);
            TimeCountScript.SetTime(ref intervalBuff, empInterval);
        }

    }

    //EMP生成
    public void CreateEMP()
    {
        GameObject _ = Instantiate(EMP);                            //オブジェクト生成
        _.transform.SetParent(this.transform);                      //親子付け
        _.transform.localPosition = Vector3.zero;                  //位置のずれ修正
        _.transform.localEulerAngles = Vector3.zero;            //方向のずれ修正
        EMPScript emp = _.GetComponent<EMPScript>();    //EMPスクリプト取得
        emp.StartEMP(in isDeploy);                                     //EMPスクリプトを初期化
        empList.Add(emp);                                                 //リストに追加
        if (isDeploy)
        {
            return;
        }
        emp.SetChargeTime(chargeTime);
        emp.SetExplodeTime(explodeTime);
    }

    //EMP管理
    public void EMPController(in PlayerScript ps)
    {
        if (ps == null)
        {
            for (int i = 0; i < empList.Count; i++)
            {
                empList[i].Break();
                empList.RemoveAt(i);
            }
            return;
        }

        if (isDeploy && empList.Count == 0)
        {
            CreateEMP();
        }

        for (int i = 0; i < empList.Count;)
        {
            if (isDeploy)
            {                
                empList[i].Deploy();
                i++;
                return;
            }

            if (empList[i].Charge())
            {
                empList[i].Explode();

            }

            if (empList[i].GetBreakFlag())
            {
                empList[i].Break();
                empList.RemoveAt(i);
            }
            else
            {
                i++;
            }


        }
    }

    //初期化
    public void StartEMPbot()
    {
        empList = new List<EMPScript>();
        if (isDeploy)
        {
            return;
        }
        TimeCountScript.SetTime(ref intervalBuff, empInterval);
    }
}
