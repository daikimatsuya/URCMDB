using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int playerMissile;

    private void GameManagerController()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        if (!GameObject.FindWithTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.Space)&&playerMissile > 0)
            {

                GameObject _ = Instantiate(player);
                

                playerMissile--;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManagerController();
    }
}
