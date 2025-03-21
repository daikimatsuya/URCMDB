using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using Usefull;

//  砲台管理
public class FlakScript : MonoBehaviour
{
    Transform tf;

    private LineUIScript lineUI;
    private CreateMarkerScript cms;

    [SerializeField] private bool autShotSwitch;
    [SerializeField] private Transform body;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject line;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shotInterval;
    [SerializeField] private float range;
    [SerializeField] private float setWarning;
    [SerializeField] private float setVoid;
    private float voidColorTime;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;
    private int intervalBuff;
    List<FlakBulletScript> flakBulletList = new List<FlakBulletScript>();
    private bool isAffective;

    //砲台の向きと射撃
    public void Aim(in PlayerScript ps)
    {
        if (ps == null)
        {
            isAffective = false;
            return;
        }
        //偏差予測
        Vector3 playerPos = ps.GetPlayerPos();                        //プレイヤーの位置取得
        playerDis =playerPos-barrel.position;                           //距離算出
        lineUI.SetLine(barrel.position,playerDis,intervalBuff);    //予測線設定
        Vector3 playerSpeed= ps.GetPlayerSpeed();                 //プレイヤーの速度取得


        //解の公式を使用して値を算出///////////////////////////////////////////////////////
        float a = Vector3.Dot(playerSpeed, playerSpeed) - (bulletSpeed*bulletSpeed);   
        float b = Vector3.Dot(playerDis, playerSpeed) * 2; 
        float c = Vector3.Dot(playerDis, playerDis);

        float discriminant = (b * b) - (4 * a * c);
        if(discriminant < 0)
        {
            return; //ありえない値の時returnを返す
        }
        float t1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        float t2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

        float t = new float[] { t1, t2 }.Where(t => t > 0).DefaultIfEmpty().Max();

        //方向算出
        playerDis = new Vector3((playerPos.x + (playerSpeed.x * t) - barrel.position.x), (playerPos.y + (playerSpeed.y * t) - barrel.position.y), (playerPos.z + (playerSpeed.z * t) - barrel.position.z));
        playerDisNormal = playerDis.normalized;
        
        //方向から角度を算出
        float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;                                                                                                          //水平方向角度算出
        float vertical = Mathf.Atan2(Mathf.Sqrt( playerDisNormal.x*playerDisNormal.x + playerDisNormal.z*playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg;    //垂直方向角度算出
        body.localEulerAngles=new Vector3(body.localEulerAngles.x,body.localEulerAngles.y,horizontal+180);                                                                                      //砲台のボディーを回転
        barrel.localEulerAngles = new Vector3((vertical*-1.0f)+90, barrel.localEulerAngles.y, barrel.localEulerAngles.z);                                                                        //砲身を回転

        cms.Adjustment();      //マーカー補正
   
    }


    //弾をリストで管理する
    public void BulletController(in bool isPose)
    {
        if (flakBulletList == null)
        {
            return;
        }
        for (int i = 0; i < flakBulletList.Count;)
        {
            if (flakBulletList[i].GetDeleteFlag())
            {
                flakBulletList[i].Delete();
                flakBulletList.Remove(flakBulletList[i]);
            }
            else
            {
                flakBulletList[i].Move(in isPose);
                i++;
            }
        }
    }
    //クールタイムをセット
    public void SetTime()
    {
        TimeCountScript.SetTime(ref intervalBuff, shotInterval);
    }
    //クールタイムが終わったかを返す
    public bool GetTime()
    {
        if (lineUI.GetShade())
        {
            if (intervalBuff < shotInterval * 60)
            {
                intervalBuff++;
            }
            lineUI.SetVoid();
        }
        else
        {
            SetLineColor();
            intervalBuff--;
        }
        if (intervalBuff <= 0)
        {
            return true;
        }
        return false;
    }
    //弾丸発生処理
    public void Shot(in PlayerControllerScript pcs)
    {
        TimeCountScript.SetTime(ref intervalBuff, shotInterval);                                                                                                                 //クールタイムリセット
        Vector3 speed = new Vector3(playerDisNormal.x * bulletSpeed, playerDisNormal.y * bulletSpeed, playerDisNormal.z * bulletSpeed);   //弾丸の速度算出
        GameObject _=Instantiate(bullet);                                                                                                                                                //弾丸生成
        FlakBulletScript fb = _.GetComponent<FlakBulletScript>();                                                                                                             //コンポーネント取得
        fb.StartFlakBullet(speed,pcs);                                                                                                                                                             //弾丸初期化
        _.transform.localPosition = new Vector3(bulletPoint.position.x, bulletPoint.position.y, bulletPoint.position.z);                                       //ポジションを代入
        _.transform.localEulerAngles = new Vector3(-barrel.localEulerAngles.x, body.localEulerAngles.z + 180, 0);                                        //角度を代入
        flakBulletList.Add(fb);                                                                                                                                                                  //弾丸をリストに追加
    }
    //予測線の色管理
    public void SetLineColor()
    {
        //予測線の色を発射までの時間で変化させる
        if (intervalBuff > voidColorTime) 
        {
            lineUI.SetVoid();   //透明にする
        }
        else if(intervalBuff > setWarning) 
        {
            lineUI.SetRed();    //赤くする
        }
        else
        {
            lineUI.SetWarning();    //黄色の点滅にする
        }
        //////////////////////////////////////////
    }


    //予測線生成
    private void CreateLine()
    {
        GameObject _ = Instantiate(line);                              //予測線生成
        lineUI = _.GetComponent<LineUIScript>();                //コンポーネント取得
        lineUI.StartLine();                                                     //予測線初期化
        _.transform.position = barrel.position;                        //位置を砲身に移動
        _.transform.SetParent(this.transform);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            isAffective = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isAffective = false;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isAffective=true;
        }
    }

    #region 値受け渡し

    public bool GetIsAffective()
    {
        return isAffective;
    }
    #endregion

    //高角砲初期化
    public void StartFlak(in PlayerControllerScript pcs) 
    {
        //コンポーネント取得
        tf = GetComponent<Transform>();
        cms=GetComponent<CreateMarkerScript>();
        //////////////////////

        //時間設定
        TimeCountScript.SetTime(ref intervalBuff, shotInterval);
        voidColorTime = shotInterval - setWarning - setVoid;
        TimeCountScript.SetTime(ref voidColorTime, voidColorTime);
        TimeCountScript.SetTime(ref setWarning, setWarning);
        //////////

        cms.CreateMarker(in tf,in pcs);
        CreateLine();
    }


}
