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
    private float maxDisY;
    private float maxPosY;
    private float divide;

    private PlayerControllerScript pcs;
    private Transform objectTransform;


    //移動させる
    public void Move(Vector3 pos)
    {
        tf.position = new Vector3(pos.x, markerPosY, pos.z);    //移動させる
    }

    //サイズ調整
    public void AdjustmentSize()
    {
        if (pcs.GetPlayer() == null)
        {
            return;
        }

        tf.transform.localScale = new Vector3(Distance() * pretenseSize, Distance() * pretenseSize, Distance() * pretenseSize);
        
    }
    public void AdjustmentPos()
    {
        if (pcs.GetPlayer() == null)
        {
            return;
        }

        float playerDis = pcs.GetPlayer().GetPlayerPos().y - objectTransform.transform.position.y;
        float t = (playerDis / maxDisY);
        if (t >= 1)
        {
            t = 1f;
        }
        if (t < -1)
        {
            t = -1;
        }
        float dis = Distance()/divide;
        float adjustmentPos = (dis *-t * maxPosY);

        tf.transform.position = new Vector3(tf.position.x, markerPosY + adjustmentPos, tf.position.z);
    }
    
    public void StartMarker(in PlayerControllerScript pcs,in Transform objectTransform)
    {
        tf = GetComponent<Transform>();
        this.pcs = pcs;
        this.objectTransform = objectTransform;
        divide = 25;
        maxDisY = 688;
        maxPosY = 1.25f;
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
    private float Distance()
    {
        Vector3 playerPos = pcs.GetPlayer().GetPlayerPos();
        Vector3 playerDis = playerPos - tf.transform.position;
        Vector2 playerDis2 = new Vector2(playerDis.x, playerDis.z);
        float disFloat = playerDis2.magnitude;
        return disFloat; ;
    }
}
