using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{
    [SerializeField] private Vector3 firstPos;
    [SerializeField] private Vector3 movePos;
    [SerializeField] private float moveTime;


    private TitleScript ts;

    Transform tf;

    private float time;
    private bool moveEnd;

    private void TitleCameraController()
    {
        Move(ts.GetIsStageSelect());
    }
    private void Move(bool isStageSelect)
    {
        Vector3 dis;

        if (isStageSelect)
        {
            float x = 1 - Mathf.Pow(1 - (time / moveTime), 3);
            if (time < moveTime)
            {           
                dis = movePos - firstPos;
                tf.position = new Vector3(firstPos.x + (dis.x * x), firstPos.y + (dis.y * x), firstPos.z + (dis.z * x));
                moveEnd = false;
                ts.SendMoveEnd(moveEnd);
                time++;
            }
            else
            {
                tf.position = movePos;
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
                tf.position = new Vector3(firstPos.x - (dis.x * x), firstPos.y + (dis.y * x), firstPos.z - (dis.z * x));
                moveEnd = false;
                ts.SendMoveEnd(moveEnd);
                time--;
            }
            else
            {
                tf.position = firstPos;
                moveEnd = true;
                ts.SendMoveEnd(moveEnd);
            }
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
        Move(ts.GetIsStageSelect());
    }
}
