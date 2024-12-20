using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public class FlakScript : MonoBehaviour
{
    Transform pos;
    Transform linePos;
    LineUIScript lineScript;

    private Transform playerPos;
    private PlayerScript playerScript;
    private LineUIScript lineUI;
    private MarkerScript ms; 

    [SerializeField] private bool autShotSwitch;

    [SerializeField] private Transform body;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject marker;

    //[SerializeField] private bool isCanReach;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shotInterval;
    [SerializeField] private float range;

    [SerializeField] private float setWarning;
    [SerializeField] private float setVoid;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;
    private int intervalBuff;


    private void FlakController()
    {
        Aim();
    }
    private void Aim()
    {

        if(playerPos != null)
        {


            playerScript.IsLock();
            
            playerDis=playerPos.position-barrel.position;
            lineUI.SetLine(barrel.position,playerDis,intervalBuff);
            Vector3 playerSpeed= playerScript.GetPlayerSpeed();
            float a = Vector3.Dot(playerSpeed, playerSpeed) - (bulletSpeed*bulletSpeed);
            float b = Vector3.Dot(playerDis, playerSpeed) * 2;
            float c = Vector3.Dot(playerDis, playerDis);

            float discriminant = (b * b) - (4 * a * c);
            if(discriminant < 0)
            {
                return;
            }
            float t1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            float t2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

            float t = new float[] { t1, t2 }.Where(t => t > 0).Max();

            playerDis = new Vector3((playerPos.position.x + (playerSpeed.x * t) - barrel.position.x), (playerPos.position.y + (playerSpeed.y * t) - barrel.position.y), (playerPos.position.z + (playerSpeed.z * t) - barrel.position.z));
            playerDisNormal = playerDis.normalized;
            float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;
            float vertical = Mathf.Atan2(Mathf.Sqrt( playerDisNormal.x*playerDisNormal.x + playerDisNormal.z*playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg;
          
            body.localEulerAngles=new Vector3(body.localEulerAngles.x,body.localEulerAngles.y,horizontal+180);
            barrel.localEulerAngles = new Vector3((vertical*-1.0f)+90, barrel.localEulerAngles.y, barrel.localEulerAngles.z);


            
            if (intervalBuff <= 0)
            {
                if(autShotSwitch)
                {
                    
                    Shot();
                    intervalBuff = (int)(shotInterval * 60);
                }
                
            }
            if (lineUI.GetShade())
            {
                intervalBuff++;
                lineUI.SetVoid();
            }
            else
            {
                SetLineColor();
                intervalBuff--;
            }
            
        }
        else
        {
            //isCanReach = false;
            intervalBuff = (int)(shotInterval * 60);

            if(lineUI != null)
            {
                lineUI.Death();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shot();
        }
    }
    private void Shot()
    {
        GameObject _=Instantiate(bullet);
        _.transform.localPosition = new Vector3(bulletPoint.position.x, bulletPoint.position.y, bulletPoint.position.z);
        _.transform.localEulerAngles = new Vector3(-barrel.localEulerAngles.x, body.localEulerAngles.z + 180, 0);
        FlakBulletScript fb=_.GetComponent<FlakBulletScript>();
        fb.GetAcce(new Vector3(playerDisNormal.x*bulletSpeed,playerDisNormal.y*bulletSpeed,playerDisNormal.z*bulletSpeed));
    }
    private void SetLineColor()
    {
        if (intervalBuff > (shotInterval-setWarning-setVoid) * 60) 
        {
            lineUI.SetVoid();
        }
        else if(intervalBuff > (setWarning) * 60) 
        {
            lineUI.SetRed();
        }
        else
        {
            lineUI.SetWarning();
        }
    }
    private void CreateMarker()
    {
        GameObject _ = Instantiate(marker);
        ms = _.GetComponent<MarkerScript>();
        ms.Move(pos.position);
     
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //isCanReach = true;
            playerPos = other.transform;
            playerScript = other.GetComponent<PlayerScript>();
            GameObject _ = Instantiate(line);
            lineUI = _.GetComponent<LineUIScript>();
            _.transform.position = barrel.position;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            intervalBuff = (int)(shotInterval * 60);
            //isCanReach = false;
            playerPos = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //isCanReach=false;      
        pos=GetComponent<Transform>();
        linePos = line.GetComponent<Transform>();
        lineScript=line.GetComponent<LineUIScript>();
        intervalBuff = (int)(shotInterval * 60);

        CreateMarker();
    }

    // Update is called once per frame
    void Update()
    {
        FlakController();
    }
}
