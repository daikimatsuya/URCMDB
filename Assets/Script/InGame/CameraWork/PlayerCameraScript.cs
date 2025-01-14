using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Usefull;

//インゲームでプレイヤーにくっついているカメラ管理
public class PlayerCameraScript : MonoBehaviour
{
    private Transform tf;
    private Transform playerPos;
    private Vector3 cameraRot;



    private MovieCamera mc;
    private ExplodeCamera ec;
    private MovieFade mf;
    private ShaderController sc;

    [SerializeField] private float cameraDeff;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float maxRot;
    private float rot;
    float rotBuff;


    //カメラが飛んでいるプレイヤーの後ろに追従する
    public void FollowPlayerInShoot()
    {
        if (playerPos != null)  //プレイヤーがゲーム内に存在している時/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            tf.rotation = playerPos.rotation;   //プレイヤーの回転角を取得
            Vector3 deff = Vector3.zero;
            rotBuff = rot;

            //入力によりずれを加算

            //キーボード操作//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (rot > -maxRot)
                {
                    rotBuff -= rotSpeed;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
            {
                if (rot < maxRot)
                {
                    rotBuff += rotSpeed;
                }
            }
            if ((Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))) 
            {
                if (rot > 0)
                {
                    rot -= rotSpeed;
                }
                if (rot < 0)
                {
                    rot += rotSpeed;
                }
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //コントローラー操作//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Input.GetAxis("LeftStickX") != 0)
            {
                rotBuff += rotSpeed * Input.GetAxis("LeftStickX");
                if (rotBuff > maxRot)
                {
                    rotBuff = maxRot;
                }
                if (rotBuff < -maxRot)
                {
                    rotBuff = -maxRot;
                }
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            

            if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)&&Input.GetAxis("LeftStickX")==0)
            {
                if(rot < 0)
                {
                    rotBuff += rotSpeed;
                    if(rotBuff >= 0)
                    {
                        rotBuff = 0;
                    }
                }
                if(rot > 0)
                {
                    rotBuff -= rotSpeed;
                    if(rotBuff <= 0)
                    {
                        rotBuff = 0;
                    }
                }
            }

            rot = rotBuff;
    

            //////////////////////////


            deff = FollowPlayer(playerPos.eulerAngles, rot);    //角度と設定した距離からずれを算出
            tf.position = new Vector3(playerPos.position.x - deff.x, playerPos.position.y - deff.y + 3, playerPos.position.z - deff.z); //プレイヤーの座標にずれを加算してトランスフォームに代入

        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    //カメラが発射台にあるプレイヤーの後ろに追従する
    public void FollowPlayerInSet()
    {
        tf.rotation = playerPos.rotation;   //プレイヤーの回転角を取得

        Vector3 deff = Vector3.zero;
        deff = FollowPlayer(playerPos.eulerAngles,0);    //角度と設定した距離からずれを算出

        tf.position = new Vector3(playerPos.position.x - deff.x, playerPos.position.y - deff.y + 3, playerPos.position.z - deff.z); //プレイヤーの座標にずれを加算してトランスフォームに代入
        rot = 0;
    }
    //追従中のカメラの位置出す
    private Vector3 FollowPlayer(Vector3 playerEulerAngles,float rot)
    {
        Vector3 deff = Vector3.zero;
        float buff = playerEulerAngles.y + rot;

        //平面の位置を算出
        deff.x = cameraDeff * (float)Math.Sin(ToRadianScript.ToRadian(ref buff));
        deff.z = cameraDeff * (float)Math.Cos(ToRadianScript.ToRadian(ref buff));
        ///////////////////

        //水平方向と垂直方向の位置を算出
        deff.x = deff.x * (float)Math.Cos(ToRadianScript.ToRadian(ref playerEulerAngles.x));
        deff.z = deff.z * (float)Math.Cos(ToRadianScript.ToRadian(ref playerEulerAngles.x));

        deff.y = (cameraDeff + 5) * (float)Math.Sin(ToRadianScript.ToRadian(ref playerEulerAngles.x)) * -1;
        //////////////////////////////////

        return deff;
    }

    //ステージ開始時のムービーのカメラワーク
    public void MovieCut()
    {
        mc.CameraController();  //ゲーム開始時の演出管理
        Fade(); //フェード管理
    }
    //プレイヤーがターゲット以外で爆発したときのカメラワーク
    public void MissExplodeCamera()
    {
        cameraRot = transform.localEulerAngles; //トランスフォームの値をvector3へ移動
        tf.position = ec.MissExplodeCamera(ref cameraRot);  //爆発エフェクト
        tf.localEulerAngles = cameraRot;    //算出した値をトランスフォームに代入
    }
    //プレイヤーがターゲットにぶつかったとのカメラワーク
    public void HitExplodeCamera()
    {
        cameraRot= transform.localEulerAngles; //トランスフォームの値をvector3へ移動
        tf.position=ec.HitTargetCamera(ref cameraRot);  //爆発エフェクト
        tf.localEulerAngles = cameraRot;   //算出した値をトランスフォームに代入
    }
    //クリア時のカメラ
    public void ClearCamera()
    {
        cameraRot = transform.localEulerAngles; //トランスフォームの値をvector3へ移動
        tf.position = ec.ClearCamera(ref cameraRot);    //クリアエフェクト
        tf.localEulerAngles = cameraRot;   //算出した値をトランスフォームに代入
    }
    //フェード管理
    private void Fade()
    {
        if (mc.GetEnd())    //ゲーム開始演出の終わり確認//////
        {
            mf.SetShadeLevel(3);    //フェードレベル設定

        }/////////////////////////////////////////////////////////

        else //ゲーム開始演出中////////////////////////////////////////////////////////////////////////////////////////
        {
            if (mc.GetSkip())   //演出のスキップ確認////////////////////////////////////////////////////
            {
                mf.SetShadeLevel(2);    //フェードレベル設定

                if (mf.GetIsShade())    //フェードオブジェクトの移動が終わったか/////////
                {
                    mf.SetShadeLevel(3);    //フェードレベル設定
                }////////////////////////////////////////////////////////////////////////////

            }///////////////////////////////////////////////////////////////////////////////////////////

            else if (mc.GetFadeoutTime() > mc.GetMoveTime())    //フェード開始フラグ確認////////
            {
                mf.SetShadeLevel(2);    //フェードレベル設定
            }/////////////////////////////////////////////////////////////////////////////////////////////
            else
            {
                mf.SetShadeLevel(1);    //フェードレベル設定
            }
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


    #region 値受け渡し
    //プレイヤーのトランスフォーム取得用プレイヤースクリプトも取得
    public void SetPlayer(in Transform tf,in PlayerScript ps)
    {
        playerPos = tf;
        if (sc == null)
        {
            sc = GetComponent<ShaderController>();
        }
        sc.SetPlayer(ps);
    }
    //MovieFade取得用
    public void SetMF(in MovieFade mf)
    {
        this.mf = mf;
    }
    //プレイヤーカメラのポジション取得用
    public Vector3 GetPos()
    {
        return tf.position;
    }
    public Transform GetTransform()
    {
        return tf;
    }
    #endregion

    private void Awake()
    {
        tf = GetComponent<Transform>();
        mc = GetComponent<MovieCamera>();
        ec = GetComponent<ExplodeCamera>();
        sc = GetComponent<ShaderController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
