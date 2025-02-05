using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コンパスに写すマーカー管理
public class MarkerCamera : MonoBehaviour
{
    Transform tf;

    private GameObject player;
    private Transform playerPos;

    [SerializeField] private float pos;

    //MakerCamera管理
    private void MarkerCameraController()
    {
        SearchPlayer(); //プレイヤー検索 
        Move();            //移動
    }
    //カメラ移動
    private void Move()
    {
        if(playerPos!=null)
        {
            tf.position = new Vector3(playerPos.position.x, pos, playerPos.position.z);                                    //ポジション代入
            tf.eulerAngles = new Vector3(tf.eulerAngles.x, player.transform.eulerAngles.y, tf.eulerAngles.z);   //角度代入
        }
    }
    //プレイヤー生存確認と取得
    private void SearchPlayer()
    {
        if (playerPos == null)  //プレイヤーが取得できない時////////////////////////////////////////////////
        {
            if (GameObject.FindWithTag("Player"))   //プレイヤーオブジェクト検索/////
            {
                player = GameObject.FindWithTag("Player");           //プレイヤーオブジェクト取得
                playerPos = player.GetComponent<Transform>();   //コンポーネント取得

            }///////////////////////////////////////////////////////////////////////////////

        }////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MarkerCameraController();
    }
}
