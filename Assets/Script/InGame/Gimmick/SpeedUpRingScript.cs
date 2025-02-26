using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//スピードアップリング管理
public class SpeedUpRingScript : MonoBehaviour
{
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float ringSize;
    [SerializeField] private float offsetTime;
    [SerializeField] private GameObject marker;

    private bool isGet;

    private MarkerScript ms;
    CapsuleCollider  collider_;
    Transform tf;


    //消す
    public void Off()
    {
        if (!isGet)  //取得されたら機能を消す//////////
        {
            return;
        }//////////////////////////////////////////////

        tf.localScale = new Vector3(0, 0, 0);   //サイズを０にする
        if(ms!=null)
        {
            ms.Delete();    //マーカー削除
        }

    }
    //再表示
    public void ON()
    {
        isGet = false;                                                      //取得フラグを消す
        tf.localScale = new Vector3(1, ringSize, ringSize); //サイズ初期化
        collider_.enabled = true;                                     //コライダーオン
        if(ms==null)
        {
            CreateMarker(); //マーカー生成
        }        
    }
    //マーカー生成
    private void CreateMarker()
    {
        GameObject _ = Instantiate(marker);
        ms = _.GetComponent<MarkerScript>();
        ms.Move(tf.position);
        _.transform.SetParent(this.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isGet = true;
            collider_.enabled = false;
        }
    }
    //初期化
    public void StartSpeedUpRing()
    {
        tf = GetComponent<Transform>();
        collider_ = GetComponent<CapsuleCollider>();
        tf.localScale = new Vector3(1, ringSize, ringSize);

        isGet = false;
        CreateMarker();
    }

}
