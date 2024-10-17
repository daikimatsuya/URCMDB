using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

//タイトルシーンでのカメラの動き管理
public class TitleCamera : MonoBehaviour
{
    
    [SerializeField] private Vector3 movePos;
    [SerializeField] private float moveTime;
    [SerializeField] private float zoomSpeed;


    private TitleScript ts;

    Transform tf;

    private float time;
    private bool moveEnd;
    private Vector3 firstPos;

    //カメラを動かす
    private void TitleCameraController()
    {
        Move(ts.GetIsStageSelect());
        SceneChangeMove();
    }
    //移動
    private void Move(bool isStageSelect)
    {
        Vector3 dis;

        if (isStageSelect)
        {
            float x = 1 - Mathf.Pow(1 - (time / moveTime), 3);
            if (time < moveTime)
            {           
                dis = movePos - firstPos;
                tf.position = new Vector3(firstPos.x + (dis.x * x), firstPos.y + (dis.y * x),tf.position.z);
                moveEnd = false;
                ts.SendMoveEnd(moveEnd);
                time++;
            }
            else
            {
                tf.position = new Vector3(movePos.x, movePos.y,tf.position.z);
                moveEnd = true;
                ts.SendMoveEnd(moveEnd);
            }                       
        }
        else
        {
            float x = Mathf.Pow((time / moveTime), 3);
            if (time>0)
            {
                dis = movePos - firstPos;
                tf.position = new Vector3(firstPos.x - (dis.x * x), firstPos.y + (dis.y * x), tf.position.z);
                moveEnd = false;
                ts.SendMoveEnd(moveEnd);
                time--;
            }
            else
            {
                tf.position = new Vector3(firstPos.x, firstPos.y,tf.position.z);
                moveEnd = true;
                ts.SendMoveEnd(moveEnd);
            }
        }
    }
    //演出に使う予定
    private void SceneChangeMove()
    {
        if(ts.GetIsSceneChangeModeFlag())
        {
           
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ts=GameObject.FindWithTag("TitleManager").GetComponent<TitleScript>();
        tf=GetComponent<Transform>();
        moveTime = moveTime * 60;
        firstPos = tf.position;  
    }

    // Update is called once per frame
    void Update()
    {
        TitleCameraController();
    }
}
