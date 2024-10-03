using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoclScript : MonoBehaviour
{



    Rigidbody rb;
    Transform tf;

    float breakArea;
    float fallSpeed;

    private void RockController()
    {
        if (tf.position.y < breakArea)
        {
            BreakRock();
        }
        Fall();
    }
    private void Fall()
    {
        rb.velocity = Vector3.zero;
        rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);
    }
    private void BreakRock()
    {
        Destroy(this.gameObject);
    }


    public void GetBreakArea(float posY)
    {
        breakArea = posY;
    }
    public void GetFallSpeed(float speed)
    {
        fallSpeed= speed;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RockController();
    }
}
