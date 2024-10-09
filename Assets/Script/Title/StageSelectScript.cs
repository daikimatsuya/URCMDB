using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScript : MonoBehaviour
{

    TitleScript ts;
    Transform tf;

    private float rotateBuff;
    private bool rotateEnd;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float baseRot;

    private void StageSelectController()
    {
        Move(ts.GetStageCount());
    }
    private void Move(Vector2 stage)
    {
        float rot = (360 / (stage.y+1)) * stage.x;
        if (ts.GetIsStageSelect())
        {
            if (rot > rotateBuff)
            {
                rotateEnd = false;
                ts.SendRotateEnd(rotateEnd);
                rotateBuff += rotateSpeed;
                if (rot < rotateBuff)
                {
                    rotateBuff = rot;
                    rotateEnd = true;
                    ts.SendRotateEnd(rotateEnd);
                }
            }
            else if (rot < rotateBuff)
            {
                rotateEnd = false;
                ts.SendRotateEnd(rotateEnd);
                rotateBuff -= rotateSpeed;
                if (rot > rotateBuff)
                {
                    rotateBuff = rot;
                    rotateEnd = true;
                    ts.SendRotateEnd(rotateEnd);
                }
            }
            else
            {
                rotateEnd = true;
                ts.SendRotateEnd(rotateEnd);
            }
            tf.localEulerAngles = new Vector3(0, rotateBuff, 0);
        }
        else
        {
            tf.localEulerAngles = new Vector3(0, rotateBuff, 0);     
            rotateBuff = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ts = GameObject.FindWithTag("TitleManager").GetComponent<TitleScript>();
        tf = GetComponent<Transform>();
        rotateBuff=tf.localEulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        StageSelectController();
    }
}
