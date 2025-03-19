using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CreateMarkerScript : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    [SerializeField] private float markerSize;

    private MarkerScript ms;

    //マーカー生成
    public void CreateMarker(in Transform tf,in PlayerControllerScript pcs)
    {
        GameObject _ = Instantiate(marker);                     //生成
        ms = _.GetComponent<MarkerScript>();                //コンポーネント取得
        ms.StartMarker(in pcs, this.gameObject.transform);//マーカー初期化
        ms.Move(tf.position);                                            //位置代入
        _.transform.SetParent(this.transform);                   //親子付け
    }

    //マーカーのサイズとかを補正
    public void Adjustment()
    {
        ms.AdjustmentSize();
        ms.AdjustmentPos();
    }

    //マーカーを移動(y座標は変化させない)
    public void Move(in Transform tf)
    {
        ms.Move(tf.position);
    }

    //マーカーを消す
    public void Delete()
    {
        ms.Delete();
    }

    //マーカーの大きさをセット
    public void SetMarkerSize()
    {
        ms.SetSize(markerSize);
    }

    //オンオフ切り替え
    public void SetActive(in bool flag)
    {
        ms.SetActive(flag);
    }
}
