using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int playerMissile;

    private bool playerDead;
    private void GameManagerController()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        if (!GameObject.FindWithTag("Player"))
        {
            playerDead = true;
            if(Input.GetKeyDown(KeyCode.Space)&&playerMissile > 0)
            {
                GameObject _ = Instantiate(player);
                playerMissile--;
            }
        }
        else
        {
            playerDead= false;
        }
    }
    public bool IsPlayerDead()
    {
        return playerDead;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        GameManagerController();
    }
}
