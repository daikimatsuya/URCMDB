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

    public void EMPbotController()
    {

    }

    public void CreateEMP()
    {
        GameObject _ = Instantiate(EMP);
        _.transform.SetParent(this.transform); 
        _.transform.localPosition = Vector3.zero;
        _.transform.localEulerAngles = Vector3.zero;
        EMPScript emp = _.GetComponent<EMPScript>();
        emp.StartEMP(in isDeploy);
        empList.Add(emp);
    }
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
            i++;
        }
    }


    public void StartEMPbot()
    {
        empList = new List<EMPScript>();
        CreateEMP();
    }
}
