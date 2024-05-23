using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakBulletScript : MonoBehaviour
{
    Rigidbody rb;

    private Vector3 speed;
    private void BulletController()
    {
        Move();
    }
    private void Move()
    {
        rb.velocity = speed;
    }
    public void GetAcce(Vector3 acce)
    {
        speed = acce;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        BulletController();
    }
}
