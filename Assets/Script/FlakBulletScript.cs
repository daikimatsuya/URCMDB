using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakBulletScript : MonoBehaviour
{
    Rigidbody rb;

    private Vector3 speed;
    [SerializeField] private float deleteTime;
    private void BulletController()
    {
        Move();
        Delete();
    }
    private void Move()
    {
        rb.velocity = speed;
    }
    private void Delete()
    {
        if (deleteTime <= 0)
        {
            Destroy(this.gameObject);
        }
        deleteTime--;
    }
    public void GetAcce(Vector3 acce)
    {
        speed = acce;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        deleteTime = deleteTime * 60;
    }

    // Update is called once per frame
    void Update()
    {
        BulletController();
    }
}
