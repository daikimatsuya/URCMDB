using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�h�����Ǘ�
public class DroneScript : MonoBehaviour
{
    [SerializeField] private Transform[] propellerTf;
    [SerializeField] private float propllerRotSpeed;


     
    //�v���y������
    public void Roll()
    {
        for (int i = 0; i < propellerTf.Length; i++)
        {
            RollingScript.Rolling(propellerTf[i], propllerRotSpeed, "y");
        } 
    }
    //������
    public void StartDrone()
    {
    
    }
}
