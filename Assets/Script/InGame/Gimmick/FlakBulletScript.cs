using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//砲台が飛ばした弾管理
public class FlakBulletScript : MonoBehaviour
{
    Rigidbody rb;
    Transform pos;

    private Vector3 speed;
    private MarkerScript ms;
    
    [SerializeField] private float deleteTime;
    [SerializeField] private GameObject marker;

    //砲弾管理
    private void BulletController()
    {
        Move(); //移動管理
        Delete();   //削除管理
    }
    //移動
    private void Move()
    {
        rb.velocity = speed;    //速度加算

        ms.Move(pos.position);  //マーカーアイコンを弾丸に追従させる
    }
    //消去
    public void Delete()
    {
        if (TimeCountScript.TimeCounter(ref deleteTime))    //生存時間確認//////
        {
            ms.Delete();    //マーカーアイコン削除
            Destroy(this.gameObject);   //オブジェクト削除

        }///////////////////////////////////////////
    }
    //速度受け取り
    public void GetAcce(Vector3 acce)
    {
        speed = acce;   //値代入
    }
    //マーカー生成
    private void CreateMarker()
    {
        GameObject _ = Instantiate(marker); //マーカーアイコン生成
        ms = _.GetComponent<MarkerScript>();    //スクリプト取得
        ms.Move(pos.position);  //追従させる

    }
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        pos= GetComponent<Transform>(); 
        deleteTime = deleteTime * 60;

        CreateMarker();
    }

    // Update is called once per frame
    void Update()
    {
        BulletController();
    }
}
