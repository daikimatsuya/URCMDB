using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    private GameManagerScript gm;
    private Transform yawUI;
    private GameObject gameOverUI;
    private GameOverUIScript goUs;
    private RectTransform canvasPos;
    private RectTransform targetMarkerPos;


    private Vector3 playerRot;
    private Vector3 targetPos;

    [SerializeField] private float YawUIMag;
    [SerializeField] private Vector3 gameOverUIPos;
    [SerializeField] private TextMeshProUGUI pmsTex;
    [SerializeField] private TextMeshProUGUI playerSpeed;
    [SerializeField] private TextMeshProUGUI playerSpeedBuff;
    [SerializeField] private GameObject targetMarker;
    private void UIController()
    {
        GetPlayerRot();
        YawUIController();
        PMSMode();
        IsGameOver();
        PlayerSpeedUI();
        TargetMarkerUI();
    }
    private void GetPlayerRot()
    {
        playerRot = gm.GetPlayerRot();
    }
    private void YawUIController()
    {
        yawUI.localPosition = new Vector3(yawUI.localPosition.x, playerRot.x*-YawUIMag, 0);
    }
    private void IsGameOver()
    {
        if(gm.GetGameOverFlag())
        {
            
            gameOverUI.transform.localPosition = gameOverUIPos;
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                goUs.MoveRetry();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
               goUs.MoveBackTitle();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (goUs.GetPos() < -1)
                {
                    gm.Retry();
                }
                else if(goUs.GetPos() > 1)
                {
                    gm.BackTitle();
                }
            }
            goUs.TargetHpUI();
        }
    }
    private void PMSMode()
    {
        if (gm.GetPMS())
        {
            pmsTex.text = "PMS:ON";
        }
        else
        {
            pmsTex.text = "PMS:OFF";
        }
    }
    private void PlayerSpeedUI()
    {
        playerSpeed.text = (int)gm.GetPlayerSpeed() + "M/S";
        playerSpeedBuff.text = (int)gm.GetPlayerSpeedBuff() + "M/S";

    }
    private void TargetMarkerUI()
    {
        targetPos = gm.GetTargetPos();
        Vector2 pos;
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasPos, screenPos, Camera.main, out pos);
        targetMarkerPos.localPosition = new Vector3(pos.x, pos.y, targetMarkerPos.localPosition.z);


        if (Vector3.Dot(Camera.main.transform.forward, (targetPos - Camera.main.transform.position)) < 0)
        {
            targetMarker.SetActive(false);
        }
        else
        {
            targetMarker.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasPos = GetComponent<RectTransform>();

        gm=GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        yawUI = GameObject.FindWithTag("YawUI2").GetComponent<Transform>();
        gameOverUI = GameObject.FindWithTag("GameOverUI");
        goUs=gameOverUI.GetComponent<GameOverUIScript>();
        targetMarkerPos=targetMarker.GetComponent<RectTransform>(); 

       
  
    }

    // Update is called once per frame
    void Update()
    {
        UIController();
    }
}
