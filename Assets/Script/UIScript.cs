using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    private GameManagerScript gm;
    private Transform yawUI;

    private Vector3 playerRot;

    [SerializeField] private float YawUIMag;
    private void UIController()
    {
        GetPlayerRot();

        YawUIController();
    }
    private void GetPlayerRot()
    {
        playerRot = gm.GetPlayerRot();
    }
    private void YawUIController()
    {
        yawUI.localPosition = new Vector3(yawUI.localPosition.x, playerRot.x*-YawUIMag, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        gm=GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        yawUI = GameObject.FindWithTag("YawUI2").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        UIController();
    }
}
