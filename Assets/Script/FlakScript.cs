using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakScript : MonoBehaviour
{
    Transform pos;

    private Transform playerPos;

    [SerializeField] private Transform body;
    [SerializeField] private Transform barrel;


    [SerializeField] private bool isCanReach;

    private void FlakController()
    {
        Aim();
    }

    private void Aim()
    {
        if(playerPos != null)
        {

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isCanReach = true;
            playerPos = other.transform;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isCanReach = false;
            playerPos = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isCanReach=false;      
        pos=GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
