using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Usefull;

//タイトルシーンでのカメラの動き管理
public class TitleCamera : MonoBehaviour
{    
    [SerializeField] private Vector3 movePos;
    [SerializeField] private float moveTime;
    [SerializeField] private float zoomSpeed;

    Transform tf;

    private float time;
    private bool moveEnd;
    private Vector3 firstPos;
    private bool isMoveDown;

    //タイトルカメラ挙動管理
    public void CameraController(in bool isStageSelect)
    {
        if (!isStageSelect)
        {
            return;
        }
        FlagCheck();    //移動可能かを確認
        Move();           //カメラ移動
        
    }

    //移動可能フラグチェック
    public void FlagCheck()
    {
        if (!moveEnd) 
        {
            return; //移動中だったらリターン
        }
        if (!Input.GetKeyDown(KeyCode.Space) && !Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            return; //ボタンが押されていなかったらリターン
        }
        if (isMoveDown)
        {
            isMoveDown = false; //下から上に移動
        }
        else
        {
            isMoveDown = true;  //上から下に移動
        }
    }

    //移動
    public void Move()
    {
        Vector3 dis;

        //下へ移動
        if (isMoveDown)  
        {
            float x = 1 - Mathf.Pow(1 - (time / moveTime), 3);
            if (time < moveTime)
            {           
                dis = movePos - firstPos;                                                                                             //差を算出
                tf.position = new Vector3(firstPos.x + (dis.x * x), firstPos.y + (dis.y * x),tf.position.z);    //徐々に移動させる
                moveEnd = false;                                                                                                       //moveEndフラグをfalse

                time++;
            }
            else
            {
                tf.position = new Vector3(movePos.x, movePos.y,tf.position.z);           //座標を代入
                moveEnd = true;                                                                             //moveEndフラグをtrue

            }                       
        }

        //上へ移動
        else　
        {
            float x = Mathf.Pow((time / moveTime), 3);
            if (time>0)
            {
                dis = movePos - firstPos;                                                                                            //差を算出
                tf.position = new Vector3(firstPos.x - (dis.x * x), firstPos.y + (dis.y * x), tf.position.z);   //徐々に移動させる
                moveEnd = false;                                                                                                      //moveEndフラグをfalse

                time--;
            }
            else
            {
                tf.position = new Vector3(firstPos.x, firstPos.y,tf.position.z);               //座標を代入
                moveEnd = true;                                                                            //moveEndフラグをtrue

            }
        }
    }

    #region 値受け渡し

    //移動していいかのフラグチェック
    public bool GetCanShot()
    {
        if (moveEnd && isMoveDown)
        {
            return true;
        }
        return false;
    }
    #endregion

    //タイトルカメラ初期化
    public void StartTitleCamera()
    {
        tf = GetComponent<Transform>();
        TimeCountScript.SetTime(ref moveTime, moveTime);
        firstPos = tf.position;
        isMoveDown = false;
    }

}
