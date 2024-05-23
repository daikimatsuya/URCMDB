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

    private Vector3 playerDis;

    private void FlakController()
    {
        Aim();
    }

    private void Aim()
    {
        if(playerPos != null)
        {
            playerDis=playerPos.position-barrel.position;
            Vector3 playerDisNormal=playerDis.normalized;

            float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;
            float vertical = Mathf.Atan2(Mathf.Sqrt( playerDisNormal.x*playerDisNormal.x + playerDisNormal.z*playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg;
          
            body.localEulerAngles=new Vector3(body.localEulerAngles.x,body.localEulerAngles.y,horizontal+180);
            barrel.localEulerAngles = new Vector3((vertical*-1.0f)+90, barrel.localEulerAngles.y, barrel.localEulerAngles.z);
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
        FlakController();
    }
}
