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
    Transform pos;
    Transform linePos;
    LineUIScript lineScript;

    private Transform playerPos;
    private PlayerScript playerScript;
    private LineUIScript lineUI;
    private MarkerScript ms; 

    [SerializeField] private bool autShotSwitch;

    [SerializeField] private Transform body;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject marker;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shotInterval;
    [SerializeField] private float range;
    [SerializeField] private float setWarning;
    [SerializeField] private float setVoid;
    private float voidColorTime;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;
    private int intervalBuff;

    //砲台の動きコントローラー
    private void FlakController()
    {
        Aim();  //射撃管理
    }
    //砲台の向きと射撃
    private void Aim()
    {
        if(playerPos != null)   //プレイヤーが射程内に入っているか確認////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            //偏差予測///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            playerDis=playerPos.position-barrel.position;   //距離算出
            lineUI.SetLine(barrel.position,playerDis,intervalBuff); //予測線設定
            Vector3 playerSpeed= playerScript.GetPlayerSpeed(); //プレイヤーの速度取得

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

            float t = new float[] { t1, t2 }.Where(t => t > 0).Max();
            ///////////////////////////////////////////////////////////////////////////////////

            //速度計算///////////////////////////////////////////////////////////////////////////////////////////////////
            playerDis = new Vector3((playerPos.position.x + (playerSpeed.x * t) - barrel.position.x), (playerPos.position.y + (playerSpeed.y * t) - barrel.position.y), (playerPos.position.z + (playerSpeed.z * t) - barrel.position.z));
            playerDisNormal = playerDis.normalized;
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            //速度から角度を算出//////////////////////////////////////////////////////////////////////
            float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;
            float vertical = Mathf.Atan2(Mathf.Sqrt( playerDisNormal.x*playerDisNormal.x + playerDisNormal.z*playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg;
            body.localEulerAngles=new Vector3(body.localEulerAngles.x,body.localEulerAngles.y,horizontal+180);  //砲台のボディーを回転
            barrel.localEulerAngles = new Vector3((vertical*-1.0f)+90, barrel.localEulerAngles.y, barrel.localEulerAngles.z);   //砲身を回転
            ///////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //発射タイマー管理////
            if (intervalBuff <= 0)
            {
                if(autShotSwitch)
                {
                    Shot();
                    TimeCountScript.SetTime(ref intervalBuff, shotInterval);
                }         
            }
            ///////////////////////
           
            //プレイヤーが遮蔽に隠れているか
            if (lineUI.GetShade())
            {
                intervalBuff++;
                lineUI.SetVoid();
            }
            else
            {
                SetLineColor();
                intervalBuff--;
            }
            ////////////////////////////////

        }/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        else
        {
            TimeCountScript.SetTime(ref intervalBuff, shotInterval);

            if(lineUI != null)
            {
                lineUI.Death(); //lineUIを削除
            }
            
        }
    }
    //弾丸発生処理
    private void Shot()
    {
        GameObject _=Instantiate(bullet);   //弾丸生成
        _.transform.localPosition = new Vector3(bulletPoint.position.x, bulletPoint.position.y, bulletPoint.position.z);    //ポジションを代入
        _.transform.localEulerAngles = new Vector3(-barrel.localEulerAngles.x, body.localEulerAngles.z + 180, 0);   //角度を代入
        FlakBulletScript fb=_.GetComponent<FlakBulletScript>(); //コンポーネント取得
        fb.GetAcce(new Vector3(playerDisNormal.x*bulletSpeed,playerDisNormal.y*bulletSpeed,playerDisNormal.z*bulletSpeed)); //加速度代入
    }
    //予測線の色管理
    private void SetLineColor()
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

    //マーカー生成
    private void CreateMarker()
    {
        GameObject _ = Instantiate(marker); //生成
        ms = _.GetComponent<MarkerScript>();    //コンポーネント取得
        ms.Move(pos.position);  //位置代入
     
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerPos = other.transform;    //プレイヤーの位置取得
            playerScript = other.GetComponent<PlayerScript>();  //コンポーネント取得
            GameObject _ = Instantiate(line);   //予測線生成
            lineUI = _.GetComponent<LineUIScript>();    //コンポーネント取得
            _.transform.position = barrel.position; //位置を砲身に移動
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            TimeCountScript.SetTime(ref intervalBuff, shotInterval);
            playerPos = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        pos=GetComponent<Transform>();
        linePos = line.GetComponent<Transform>();
        lineScript=line.GetComponent<LineUIScript>();
        //////////////////////

        //時間設定
        TimeCountScript.SetTime(ref intervalBuff, shotInterval);
        voidColorTime = shotInterval - setWarning - setVoid;
        TimeCountScript.SetTime(ref voidColorTime, voidColorTime);
        TimeCountScript.SetTime(ref setWarning, setWarning);
        //////////

        CreateMarker();
    }

    // Update is called once per frame
    void Update()
    {
        FlakController();
    }
}
