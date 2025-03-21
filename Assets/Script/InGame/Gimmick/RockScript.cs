using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//落とした岩管理
public class RockScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    //落下処理
    public void Fall(in float fallSpeed,in bool isPause)
    {
        rb.velocity = Vector3.zero;
        if (isPause)
        {
            return;
        }
        rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);  //落下速度代入
    }
    //岩を破壊する
    public void BreakRock()
    {
        Destroy(this.gameObject);
    }

    #region 値受け渡し
    public float GetPos()
    {
        return tf.transform.position.y;
    }

    #endregion
    //初期化
    public void StartRock()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }
}
