using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//スピードアップリング管理
public class SpeedUpRingScript : MonoBehaviour
{
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float ringSize;
    [SerializeField] private float offsetTime;
    [SerializeField] private GameObject particle;

    private bool isGet;

    private CreateMarkerScript cms;

    CapsuleCollider  collider_;
    Transform tf;
    private PlayerControllerScript pcs;


    //消す
    public void Off()
    {
        if (!isGet) //取得されていない時
        {
            cms.Adjustment();
            return;
        }

        tf.localScale = new Vector3(0, 0, 0);   //サイズを０にする
        cms.SetActive(false);
        particle.SetActive(false);
    }

    //再表示
    public void ON()
    {
        isGet = false;                                                      //取得フラグを消す
        tf.localScale = new Vector3(1, ringSize, ringSize); //サイズ初期化
        collider_.enabled = true;                                     //コライダーオン
        cms.SetActive(true);
        particle.SetActive(true);
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
        cms=GetComponent<CreateMarkerScript>();
        tf.localScale = new Vector3(1, ringSize, ringSize);

        isGet = false;
        cms.CreateMarker(in tf,in pcs);
    }

}
