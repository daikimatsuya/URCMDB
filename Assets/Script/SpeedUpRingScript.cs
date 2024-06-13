using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpRingScript : MonoBehaviour
{
    [SerializeField] private float offTime;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float ringSize;

    private int timeBuff;

    new CapsuleCollider  collider;
    Transform tf;

    private void SpeedUpRingController()
    {
        Off();
    }
    private void Off()
    {
        if(timeBuff>0)
        {
            if (tf.localScale.y > 0)
            {
                tf.localScale = new Vector3(1, tf.localScale.z - shrinkSpeed, tf.localScale.z - shrinkSpeed);
            }
            else
            {
                tf.localScale = new Vector3(0, 0, 0);
            }
            

            timeBuff--;
            if(timeBuff <= 0)
            {
                tf.localScale=new Vector3(1, ringSize, ringSize); 
                collider.enabled = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            timeBuff = (int)(offTime * 60);
            collider.enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        collider=GetComponent<CapsuleCollider>();
        timeBuff = 0;
        tf.localScale = new Vector3(1, ringSize, ringSize);
    }

    // Update is called once per frame
    void Update()
    {
        SpeedUpRingController();  
    }
}
