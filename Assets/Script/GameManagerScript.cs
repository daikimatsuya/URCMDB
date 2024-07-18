using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int playerMissile;
    [SerializeField] private bool PMS;

    private bool playerDead;
    private Vector3 playerRot;
    private void GameManagerController()
    {
        ChangePMS();
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
    private void ChangePMS()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PMS)
            {
                PMS = false;
            }
            else
            {
                PMS = true;
            }
        }
    }
    public bool GetPMS()
    {
        return PMS;
    }
    public bool IsPlayerDead()
    {
        return playerDead;
    }

    public void PlayerRotSet(Vector3 rot)
    {
       playerRot = rot;
    }
    public Vector3 GetPlayerRot()
    {
        return playerRot;
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
