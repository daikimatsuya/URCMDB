using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテムとか敵とかのマーカー管理
public class MarkerScript : MonoBehaviour
{
    Transform tf;

    [SerializeField] private float markerPosY;

    //移動させる
    public void Move(Vector3 pos)
    {
        if(tf != null)  //トランスフォームが取得されているとき///////////////////////////
        {
            tf.position = new Vector3(pos.x, markerPosY, pos.z);    //移動させる
        }/////////////////////////////////////////////////////////////////////////////////

        else   //トランスフォームが取得できていない時
        {
            tf=GetComponent<Transform>();                               //コンポーネント取得
            tf.position = new Vector3(pos.x, markerPosY, pos.z);    //移動
        }
    }
    //消去
    public void Delete()
    {
        Destroy(this.gameObject);
    }

}
