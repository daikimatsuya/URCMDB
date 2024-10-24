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
        if (tf.position.y < breakArea)
        {
            BreakRock();
        }
        Fall();
    }
    //落下処理
    private void Fall()
    {
        rb.velocity = Vector3.zero;
        rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);
    }
    //岩を破壊する
    private void BreakRock()
    {
        Destroy(this.gameObject);
    }

    //値受け渡しここから
    public void GetBreakArea(float posY)
    {
        breakArea = posY;
    }
    public void GetFallSpeed(float speed)
    {
        fallSpeed= speed;
    }
    //ここまで

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
