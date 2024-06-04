using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakScript : MonoBehaviour
{
    Transform pos;

    private Transform playerPos;
    private PlayerScript playerScript;

    [SerializeField] private Transform body;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject bullet;

    [SerializeField] private bool isCanReach;
    [SerializeField] private float bulletSpeed;


    private Vector3 playerDis;
    private Vector3 playerDisNormal;

    private void FlakController()
    {
        Aim();
    }

    private void Aim()
    {
        if(playerPos != null)
        {
            
            playerDis=playerPos.position-barrel.position;
            float dis = Mathf.Sqrt(playerDis.x * playerDis.x + playerDis.y * playerDis.y + playerDis.z * playerDis.z) / bulletSpeed;

            Vector3 playerSpeed = playerScript.GetPlayerSpeed();
            playerDis = new Vector3((playerDis.x - (playerPos.position.x + (playerSpeed.x * dis) - barrel.position.x)), (playerDis.y - (playerPos.position.y + (playerSpeed.y * dis) - barrel.position.y)), (playerDis.z - (playerPos.position.z + (playerSpeed.z * dis) - barrel.position.z)));

            dis += (Mathf.Sqrt(playerDis.x * playerDis.x + playerDis.y * playerDis.y + playerDis.z * playerDis.z) / bulletSpeed) / 2;
            playerDis = new Vector3((playerPos.position.x + (playerSpeed.x * dis) - barrel.position.x), (playerPos.position.y + (playerSpeed.y * dis) - barrel.position.y), (playerPos.position.z + (playerSpeed.z * dis) - barrel.position.z));

            playerDisNormal =playerDis.normalized;

            float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;
            float vertical = Mathf.Atan2(Mathf.Sqrt( playerDisNormal.x*playerDisNormal.x + playerDisNormal.z*playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg;
          
            body.localEulerAngles=new Vector3(body.localEulerAngles.x,body.localEulerAngles.y,horizontal+180);
            barrel.localEulerAngles = new Vector3((vertical*-1.0f)+90, barrel.localEulerAngles.y, barrel.localEulerAngles.z);
          
        }
        else
        {
            isCanReach = false;
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
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isCanReach = true;
            playerPos = other.transform;
            playerScript = other.GetComponent<PlayerScript>();
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
