using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Usefull;

//H…ŠÇ—
public class ChaseControllerScript : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;

    private Vector3 playerPos;
    private Vector3 playerDis;
    private Vector3 Row;
    private bool isLeftBase;

    //ƒvƒŒƒCƒ„[‚Ì‚¢‚é•ûŒü‚ğæ“¾
    public void Chase(in Vector3 chaseTargetPos)
    {
        playerDis = chaseTargetPos - this.gameObject.transform.position;       //‹——£‚ğZo

        float horizontal = Mathf.Atan2(playerDis.normalized.x, playerDis.normalized.z) * Mathf.Rad2Deg;   
        float vertical = Mathf.Atan2(Mathf.Sqrt(playerDis.normalized.x * playerDis.normalized.x + playerDis.normalized.z * playerDis.normalized.z), playerDis.normalized.y) * Mathf.Rad2Deg; 

        vertical -= 90;                                                                                                          //Šp“x•ª‚¸‚ç‚·
        Rowring(horizontal, vertical);                                                                                     //‰ñ“]‚³‚¹‚é
        this.gameObject.transform.localEulerAngles = new Vector3(Row.x, Row.y, Row.z);        //Šp“x‘ã“ü
    }
   
   
    //‰ñ“]
    private void Rowring(float horizontal,float vertical)
    {
        Row.x += ComplementingRotationScript.Rotate(rotateSpeed, 0, Row.x);
        Row.y += ComplementingRotationScript.Rotate(rotateSpeed, horizontal, Row.y);
        Row.z += ComplementingRotationScript.Rotate(rotateSpeed, vertical, Row.z);
    }

    #region ’ló‚¯“n‚µ


    #endregion

    //‰Šú‰»
    public void StartChaseController()
    {

    }

}
