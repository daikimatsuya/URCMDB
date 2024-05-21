using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YawUIScript : MonoBehaviour
{
    Transform pos;

    private Transform playerPos;
    private GameObject player;

    [SerializeField] float yaw;
    

    // Start is called before the first frame update
    private void YawUIController()
    {
        SearchPlayer();
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        YawUIController();
    }
}
