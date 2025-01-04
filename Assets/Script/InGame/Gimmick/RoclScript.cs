using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//落とした岩管理
public class RoclScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    float breakArea;
    float fallSpeed;

    //管理
    private void RockController()
    {
        if (tf.position.y < breakArea)  //設定Y座標を下回ったら破壊する///
        {
            BreakRock();
        }//////////////////////////////////////////////////////////////////////

        Fall(); //落下
    }
    //落下処理
    private void Fall()
    {
        rb.velocity = Vector3.zero;
        rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);  //落下速度代入
    }
    //岩を破壊する
    private void BreakRock()
    {
        Destroy(this.gameObject);
    }

    #region 値受け渡し
    public void SetBreakArea(float posY)
    {
        breakArea = posY;
    }
    public void SetFallSpeed(float speed)
    {
        fallSpeed= speed;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RockController();
    }
}
