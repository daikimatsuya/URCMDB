using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//アイテムとか敵とかのマーカー管理
public class MarkerScript : MonoBehaviour
{
    Transform tf;

    [SerializeField] private float markerPosY;
    [SerializeField] private float pretenseSize;
    [SerializeField] private float maxY;

    private PlayerControllerScript pcs;


    //移動させる
    public void Move(Vector3 pos)
    {
        tf.position = new Vector3(pos.x, markerPosY, pos.z);    //移動させる
    }

    //サイズ調整
    public void AdjustmentSize()
    {
        if (pcs.GetPlayer() != null)
        {
            Vector3 playerPos = pcs.GetPlayer().GetPlayerPos();
            Vector3 playerDis = playerPos - tf.transform.position;
            Vector2 playerDis2=new Vector2(playerDis.x, playerDis.z);
            float disFloat = playerDis2.magnitude;

            tf.transform.localScale = new Vector3( disFloat * pretenseSize, disFloat * pretenseSize,  disFloat * pretenseSize);
        }
    }
    public void StartMarker(in PlayerControllerScript pcs)
    {
        tf = GetComponent<Transform>();
        this.pcs = pcs;

    }
    public void SetActive(bool flag)
    {
        this.gameObject.SetActive(flag);
    }
    //消去
    public void Delete()
    {
        Destroy(this.gameObject);
    }

}
