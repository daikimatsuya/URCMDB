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
    [SerializeField] private GameObject particle;

    private bool isGet;

    private MarkerScript ms;
    CapsuleCollider  collider_;
    Transform tf;
    private PlayerControllerScript pcs;


    //消す
    public void Off()
    {
        if (!isGet)  //取得されたら機能を消す//////////
        {
            ms.AdjustmentSize();
            ms.AdjustmentPos();
            return;
        }//////////////////////////////////////////////

        tf.localScale = new Vector3(0, 0, 0);   //サイズを０にする
        ms.SetActive(false);
        particle.SetActive(false);
    }
    //再表示
    public void ON()
    {
        isGet = false;                                                      //取得フラグを消す
        tf.localScale = new Vector3(1, ringSize, ringSize); //サイズ初期化
        collider_.enabled = true;                                     //コライダーオン
        ms.SetActive(true);
        particle.SetActive(true);
    }
    //マーカー生成
    private void CreateMarker(in PlayerControllerScript pcs)
    {
        GameObject _ = Instantiate(marker);
        ms = _.GetComponent<MarkerScript>();
        _.transform.SetParent(this.transform);
        ms.StartMarker(in pcs, this.gameObject.transform);
        ms.transform.rotation=this.transform.rotation;
        ms.Move(tf.position);

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
    public void StartSpeedUpRing(in PlayerControllerScript pcs)
    {
        tf = GetComponent<Transform>();
        collider_ = GetComponent<CapsuleCollider>();
        tf.localScale = new Vector3(1, ringSize, ringSize);

        isGet = false;
        CreateMarker(in pcs);
    }

}
