using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPbotScript : MonoBehaviour
{
    [SerializeField] private float empInterval;
    private int intervalBuff;
    [SerializeField] private float chargeTime;
    private float chargeTimeBuff;
    [SerializeField] private GameObject EMP;
    [SerializeField] private bool isDeploy;

    private List<EMPScript> empList;

    public void CreateEMP()
    {
        GameObject _ = Instantiate(EMP);
        _.transform.SetParent(this.transform); 
        _.transform.localPosition = Vector3.zero;
        EMPScript emp = _.GetComponent<EMPScript>();
        emp.StartEMP(in isDeploy);
        empList.Add(emp);
    }
    public void EMPController()
    {
        for (int i = 0; i < empList.Count; i++)
        {
            empList[i].Explode();
        }
    }
    public void Charge()
    {
        for (int i = 0; i < empList.Count; i++)
        {
            empList[i].Charge();
        }
    }
    public void Deploy()
    {
        for(int i = 0;i < empList.Count; i++)
        {
            empList[i].Deploy();
        }
    }

    public void StartEMPbot()
    {
        empList = new List<EMPScript>();
    }
}
