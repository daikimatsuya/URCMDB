using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//くっつけたオブジェクトを回転させる
public class RollingScript : MonoBehaviour
{
    [SerializeField] private float rowSpeed;
    private float rowSpeedBuff;
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;

    Transform tf;
    //回転した座標をトランスフォームに入れる
    private void Rolling()
    {
        if(x)
        {
            tf.localEulerAngles = new Vector3(tf.localEulerAngles.x + rowSpeedBuff, tf.localEulerAngles.y, tf.localEulerAngles.z);
        }
        if(y)
        {
            tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y + rowSpeedBuff, tf.localEulerAngles.z );
        }
        if (z)
        {
            tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y, tf.localEulerAngles.z + rowSpeedBuff);
        }
      
    }
    public void SetRowSpeed(float rowSpeed)
    {
        this.rowSpeedBuff = rowSpeed;
    }
    public float GetRowSpeed()
    {
        return rowSpeed;
    }
    public void ResetRowSpeed()
    {
        rowSpeedBuff = rowSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        rowSpeedBuff = rowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Rolling();
    }
}
