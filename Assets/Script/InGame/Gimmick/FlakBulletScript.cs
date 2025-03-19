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
    private CreateMarkerScript cms;

    [SerializeField] private float deleteTime;

    //移動
    public void Move(in bool isPause)
    {
        if(isPause)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = speed;       //速度加算
        cms.Move(in pos);         //マーカー移動
        cms.Adjustment();        //マーカーサイズ補正
    }
    //消去
    public void Delete()
    {
        cms.Delete();                       //マーカーアイコン削除
        Destroy(this.gameObject);    //オブジェクト削除
    }

    //初期化
    public void StartFlakBullet(Vector3 speed,in PlayerControllerScript pcs)
    {
        this.speed = speed;
        rb = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>();
        cms=GetComponent<CreateMarkerScript>();

        TimeCountScript.SetTime(ref deleteTime, deleteTime);
        cms.CreateMarker(in pos, in pcs);
        cms.SetMarkerSize();
    }

    public bool GetDeleteFlag()
    {
        return TimeCountScript.TimeCounter(ref deleteTime);
    }
}
