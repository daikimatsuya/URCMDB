using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

public class DroneScript : MonoBehaviour
{
    [SerializeField] private Transform[] propellerTf;
    [SerializeField] private float propllerRotSpeed;

    private RollingScript rs; 
     
    public void Roll()
    {
        for (int i = 0; i < propellerTf.Length; i++)
        {
            rs.Rolling(propellerTf[i], propllerRotSpeed, "y");
        } 
    }
    public void StartDrone()
    {
        rs=new RollingScript();
    }
}
