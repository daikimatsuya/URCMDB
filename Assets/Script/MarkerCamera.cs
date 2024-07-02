using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerCamera : MonoBehaviour
{
    Transform tf;

    private GameObject player;
    private Transform playerPos;

    private void MarkerCameraController()
    {

    }
    private void Move()
    {
        if(playerPos!=null)
        {
            tf.position = new Vector3(playerPos.position.x, tf.position.y, playerPos.position.z);
            tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, playerPos.localEulerAngles.y, tf.localEulerAngles.z);
        }
    }
    private void SearchPlayer()
    {
        if (playerPos == null)
        {
            if (GameObject.FindWithTag("Player"))
            {
                player = GameObject.FindWithTag("Player");
                playerPos = player.GetComponent<Transform>();

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
