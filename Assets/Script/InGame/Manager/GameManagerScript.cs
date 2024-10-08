using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int playerMissile;
    [SerializeField] private bool PMS;
    [SerializeField] private string stage;
    [SerializeField] private string title;

    private PlayerScript ps;
    private Transform targetPos;

    private bool playerDead;
    private Vector3 playerRot;
    private float playerSpeed;
    private float playerSpeedBuff;
    private bool gameOverFlag;
    private bool ClearFlag;

    private void GameManagerController()
    {
        ChangePMS();
        SpawnPlayer();
        if(ClearFlag)
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                BackTitle();
            }
           
        }
    }

    private void SpawnPlayer()
    {
        if (!GameObject.FindWithTag("Player"))
        {
            playerDead = true;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (playerMissile > 0)
                {
                    GameObject _ = Instantiate(player);
                    ps=_.GetComponent<PlayerScript>();
                    playerMissile--;
                }
                else
                {
                    gameOverFlag = true;

                }
            }

        }
        else
        {
            playerDead= false;
            playerSpeed = ps.GetPlayerSpeedFloat();
            playerSpeedBuff = ps.GetPlayerSpeedBuffFloat();
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
    public bool GetGameOverFlag()
    {
        return gameOverFlag;
    }
    public void Retry()
    {
        SceneManager.LoadScene(stage);
    }
    public void BackTitle()
    {
        SceneManager.LoadScene(title);
    }
    public void SetClearFlag()
    {
        ClearFlag = true;
    }
    public bool GetClearFlag()
    {
        return ClearFlag;
    }
    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }
    public float GetPlayerSpeedBuff()
    {
        return playerSpeedBuff;
    }
    public Vector3 GetTargetPos()
    {
        targetPos = GameObject.FindWithTag("Target").GetComponent<Transform>();
        return targetPos.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        PMS = false;
        gameOverFlag= false;
        ClearFlag = false;
        targetPos = GameObject.FindWithTag("Target").GetComponent<Transform>();
        if (!GameObject.FindWithTag("Player"))
        {
            GameObject _ = Instantiate(player);
            ps = _.GetComponent<PlayerScript>();
            playerMissile--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameManagerController();
    }
}
