using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//爆発演出をリストで管理
public class ExplodeEffectListScript : MonoBehaviour
{
    private List<ExplodeEffectScript> explodeEffectList;
    private bool playerExplodeFlag;
    private Vector3 playerPos;

    //演出管理
    public void ExplodeEffectListController(in PlayerControllerScript pcs)
    {
        CreatePlayerExplode(in pcs);    //プレイヤーの爆風生成

        //プレイヤーが存在してたら演出を消す
        if (pcs.GetPlayer() != null)
        {
            for (int i = 0; i < explodeEffectList.Count; i++)
            {
                explodeEffectList[i].Break();       //削除
                explodeEffectList.RemoveAt(i);   //リストから削除
            }
            return;
        }

        //演出を動かす
        for (int i = 0; i < explodeEffectList.Count;)
        {
            explodeEffectList[i].SizeUp();      //拡大
            explodeEffectList[i].Rotation();    //回転
            explodeEffectList[i].Dissolve();    //ディゾルブさせる
            explodeEffectList[i].Edge();        //端もディゾルブさせる

            //時間で破壊する
            if (explodeEffectList[i].CountDown())
            {
                explodeEffectList[i].Break();       //削除
                explodeEffectList.RemoveAt(i);   //リストから削除
            }
            else
            {
                i++;
            }
        }
    }

    //プレイヤーの爆発エフェクト生成
    private void CreatePlayerExplode(in PlayerControllerScript pcs)
    {
        //プレイヤーがあったら生成フラグをオンにして位置を保存
        if (pcs.GetPlayer())
        {
            playerExplodeFlag = true;
            playerPos=pcs.GetPlayer().GetTransform().position;
        }

        //生成フラグをオフにしてプレイヤーの位置に爆発を生成
        else if(playerExplodeFlag) 
        {
            playerExplodeFlag=false;
            ExplodeEffectScript _ = pcs.CreateExplodeEffect(playerPos).GetComponent<ExplodeEffectScript>();
            _.StartExplodeEffect();
            explodeEffectList.Add(_);
        }

    }
    
    //早期初期化
    public void StartExplodeEffectList()
    {
        explodeEffectList = new List<ExplodeEffectScript>(FindObjectsOfType<ExplodeEffectScript>());
    }
}
