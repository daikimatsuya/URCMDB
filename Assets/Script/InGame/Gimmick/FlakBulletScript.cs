using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//砲台が飛ばした弾管理
public class FlakBulletScript : MonoBehaviour
{
    Rigidbody rb;
    Transform pos;

    private Vector3 speed;
    private MarkerScript ms;

    [SerializeField] private float deleteTime;
    [SerializeField] private GameObject marker;

    //移動
    public void Move(in bool isPose)
    {
        if(isPose)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = speed;       //速度加算
        ms.Move(pos.position);  //マーカーアイコンを弾丸に追従させる
        ms.AdjustmentSize();
    }
    //消去
    public void Delete()
    {
        ms.Delete();                        //マーカーアイコン削除
        Destroy(this.gameObject);    //オブジェクト削除
    }
    //マーカー生成
    private void CreateMarker(in PlayerControllerScript pcs)
    {
        GameObject _ = Instantiate(marker);         //マーカーアイコン生成
        ms = _.GetComponent<MarkerScript>();    //スクリプト取得
        ms.StartMarker(in pcs);
        ms.Move(pos.position);                              //追従させる

    }
    //初期化
    public void StartFlakBullet(Vector3 speed,in PlayerControllerScript pcs)
    {
        this.speed = speed;
        rb = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>();
        TimeCountScript.SetTime(ref deleteTime, deleteTime);
        CreateMarker(in pcs);
    }

    public bool GetDeleteFlag()
    {
        return TimeCountScript.TimeCounter(ref deleteTime);
    }
}
