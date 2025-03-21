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
    public void MarkerCameraController()
    {
        SearchPlayer(); //プレイヤー検索 
        Move();            //移動
    }

    //初期化
    public void StartMarkerCamera()
    {
        tf = GetComponent<Transform>();
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
        //プレイヤーが取得できない時
        if (playerPos == null)  
        {
            //プレイヤーオブジェクト検索
            if (GameObject.FindWithTag("Player"))   
            {
                player = GameObject.FindWithTag("Player");           //プレイヤーオブジェクト取得
                playerPos = player.GetComponent<Transform>();   //コンポーネント取得

            }

        }
    }

    void Start()
    {
        StartMarkerCamera();
    }

    // Update is called once per frame
    void Update()
    {
        MarkerCameraController();
    }
}
