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

    private TitleScript ts;
    Transform tf;

    private float time;
    private bool moveEnd;
    private Vector3 firstPos;

    //カメラを動かす
    private void TitleCameraController()
    {
        Move(ts.GetIsStageSelect());    //上下移動
    }
    //移動
    private void Move(bool isStageSelect)
    {
        Vector3 dis;

        if (isStageSelect)  //ステージセレクトフラグがオンになると下に移動/////////////////////////////////////////////////////
        {
            float x = 1 - Mathf.Pow(1 - (time / moveTime), 3);
            if (time < moveTime)
            {           
                dis = movePos - firstPos;                                                                                             //差を算出
                tf.position = new Vector3(firstPos.x + (dis.x * x), firstPos.y + (dis.y * x),tf.position.z);    //徐々に移動させる
                moveEnd = false;                                                                                                       //moveEndフラグをfalse
                ts.SendMoveEnd(moveEnd);                                                                                        //フラグ代入
                time++;
            }
            else
            {
                tf.position = new Vector3(movePos.x, movePos.y,tf.position.z);  //座標を代入
                moveEnd = true;                                                                    //moveEndフラグをtrue
                ts.SendMoveEnd(moveEnd);                                                    //フラグ代入
            }                       
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        else　//ステージセレクトフラグがオフになると上に移動/////////////////////////////////////////////////////////
        {
            float x = Mathf.Pow((time / moveTime), 3);
            if (time>0)
            {
                dis = movePos - firstPos;                                                                                            //差を算出
                tf.position = new Vector3(firstPos.x - (dis.x * x), firstPos.y + (dis.y * x), tf.position.z);   //徐々に移動させる
                moveEnd = false;                                                                                                      //moveEndフラグをfalse
                ts.SendMoveEnd(moveEnd);                                                                                       //フラグ代入
                time--;
            }
            else
            {
                tf.position = new Vector3(firstPos.x, firstPos.y,tf.position.z);  //座標を代入
                moveEnd = true;                                                               //moveEndフラグをtrue
                ts.SendMoveEnd(moveEnd);                                               //フラグ代入
            }
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    // Start is called before the first frame update
    public void StartTitleCamera()
    {
        ts = GameObject.FindWithTag("TitleManager").GetComponent<TitleScript>();
        tf = GetComponent<Transform>();
        TimeCountScript.SetTime(ref moveTime, moveTime);
        firstPos = tf.position;
    }
    void Start()
    {
        StartTitleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        TitleCameraController();
    }
}
