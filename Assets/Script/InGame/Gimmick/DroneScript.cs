using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//ドロン管理
public class DroneScript : MonoBehaviour
{
    [SerializeField] private Transform[] propellerTf;
    [SerializeField] private float propllerRotSpeed;


     
    //プロペラを回す
    public void Roll()
    {
        for (int i = 0; i < propellerTf.Length; i++)
        {
            RollingScript.Rolling(propellerTf[i], propllerRotSpeed, "y");
        } 
    }
    //初期化
    public void StartDrone()
    {
    
    }
}
