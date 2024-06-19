using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpRingScript : MonoBehaviour
{

    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float ringSize;
    [SerializeField] private float offsetTime;  


    private int offsetBuff;
    private bool isGet;

    new CapsuleCollider  collider;
    Transform tf;

    private GameManagerScript gm;

    private void SpeedUpRingController()
    {
        Off();
        ON();
    }
    private void Off()
    {
        if(isGet)
        {
            if(offsetBuff <= 0)
            {
                if (tf.localScale.y > 0)
                {
                    tf.localScale = new Vector3(1, tf.localScale.z - shrinkSpeed, tf.localScale.z - shrinkSpeed);
                }
                else
                {
                    tf.localScale = new Vector3(0, 0, 0);
                }
            }
            offsetBuff--;
        }         
    }
    private void ON()
    {
        if (gm.IsPlayerDead()==true)
        {
            isGet = false;
            tf.localScale = new Vector3(1, ringSize, ringSize);
            collider.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            offsetBuff = (int)(offsetTime * 60);
            isGet = true;
            collider.enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gm=GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        tf = GetComponent<Transform>();
        collider=GetComponent<CapsuleCollider>();
        tf.localScale = new Vector3(1, ringSize, ringSize);
        offsetBuff = (int)(offsetTime * 60);
        isGet = false;
    }

    // Update is called once per frame
    void Update()
    {
        SpeedUpRingController();  
    }
}
