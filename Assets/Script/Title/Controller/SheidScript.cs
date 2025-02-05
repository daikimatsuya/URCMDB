using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトルゲームを初期化するときのミサイルが横からくる動き管理
public class SheidScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float limitPos;
    [SerializeField] private float resetPos;

    Transform tf;
    TitlegameScript ts;

    private Vector3 initialPos;

    private void SheidController()
    {
        if(ts.GetResetActionFlag()) //リセットフラグがオンになったら動かす///
        {
            Move();
        }/////////////////////////////////////////////////////////////////////////
    }
    private void Move()
    {
        tf.position = new Vector3(tf.position.x - moveSpeed, tf.position.y , tf.position.z);

        if(tf.position.x < resetPos && tf.position.x > resetPos - moveSpeed)    //一定座標まで進んだらミニゲームをリセットする////
        {
            ts.SetResetFlag(true);
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if(tf.position.x < limitPos)    //指定した座標まで進んだら座標とフラグをリセットする////
        {
            ResetAction();
        }//////////////////////////////////////////////////////////////////////////////////////////
    }
    private void ResetAction()
    {
        ts.SetResetActionFlag(false);   //リセット用フラグをオフにする
        tf.position = initialPos;   //座標を初期値に変更
    }

    public void StartSheid()
    {
        tf = GetComponent<Transform>();
        ts = GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();
        initialPos = tf.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartSheid();
    }

    // Update is called once per frame
    void Update()
    {
        SheidController();
    }
}
