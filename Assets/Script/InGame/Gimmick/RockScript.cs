using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//落とした岩管理
public class RockScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;


    //管理
    //public void RockController()
    //{
    //    if (tf.position.y < breakArea)  //設定Y座標を下回ったら破壊する///
    //    {
    //        BreakRock();
    //    }//////////////////////////////////////////////////////////////////////

    //    Fall(); //落下
    //}
    //落下処理
    public void Fall(in float fallSpeed)
    {
        rb.velocity = Vector3.zero;
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
    public void StartRock()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }
}
