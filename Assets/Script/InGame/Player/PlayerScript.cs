using System;

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Usefull;

//インゲームのプレイヤーの処理
public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    private LaunchPointScript lp;
    private GameManagerScript gm;
    private GameObject dust;

    [SerializeField] private float time;
    [SerializeField] private float playerHp;
    [SerializeField] private GameObject boostEffect;
    [SerializeField] private GameObject explode;
    [SerializeField] private float speedBuff;
    [SerializeField] private float firstSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float ringBust;
    [SerializeField] private float accelerate;
    [SerializeField] private float burst;
    [SerializeField] private float rowlingSpeedX;
    [SerializeField] private float rowlingSpeedY;
    [SerializeField] private float fixRowling;
    [SerializeField] private float speedCut;
    [SerializeField, Range(0f, 0.3f)] private float maxBlurIntensity;
    private float blurIntnsity;
    [SerializeField, Range(0f, 0.2f)] private float boostedBlurIntensity;
    [SerializeField, Range(0f, 0.1f)] private float accelelatedBlurIntensity;
    private float minBlurIntnsity;
    [SerializeField, Range(0f, 0.1f)] private float blurIntensityBrake;
    [SerializeField] private float firePosZ;
    [SerializeField] private Vector3 fireSize;
    [SerializeField] private float correctionRowring;
    private Vector2 rowling;
    [SerializeField] private float playerBoostTuner;
    [SerializeField] private float speedUpRingTuner;
    [SerializeField] private float boostBrakeTuner;
    [SerializeField] private float speedUpRingBrakeTuner;

    private Vector3 playerMove;
    private Vector3 playerMoveBuff;
    private float accelelateSpeed;
    private float boostSpeed;

    private bool isFire;
    private bool isControl;
    private float ringSpeed;
    private bool PMS;
    private List<BoostEffectScript> boostEffectList = new List<BoostEffectScript>();

    //プレイヤー管理関数
    private void PlayerController()
    {
        if (gm.GetCanShotFlag())    //発射できるようになったら//////////////////
        {
            lp.SetStart(true);//スタートフラグをオン
            SpeedControllDebager();//デバッグ用
            Booooooomb();   //爆発処理
            Operation();    //プレイヤー操作
            Acceleration(); //加速管理
            Move(); //移動
            CountDown();    //生存時間管理
            EffectController(); //演出管理
            BlurIntnsityController();   //加速表現ブラー管理
           
        }//////////////////////////////////////////////////////////////////////////
        else
        {
            SetPreShootAngle();
        }

        gm.PlayerRotSet(rowling);   //プレイヤーの角度を代入

    }
    //速度を足してトランスフォームのバッファに入れる
    private void Move()
    {
        if (isFire)
        {
            Vector3 anglesBuff = tf.eulerAngles;

            //平面方向の速度を算出//////////////////////////////////////////////////////////////////////////
            playerMove.x = speedBuff * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.y));
            playerMove.z = speedBuff * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.y));
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            //水平方向と垂直方向の速度を算出///////////////////////////////////////////////////////////////////////////
            playerMove.x = playerMove.x * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
            playerMove.z = playerMove.z * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));

            playerMove.y = speedBuff * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.x)) * -1;
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            rb.velocity = playerMove;   //速度に代入
            playerMoveBuff = playerMove;
        }
        
    }
    //プレイヤーの操作で向いてる方向を変える
    private void Operation()
    {
        if (isControl) //プレイヤーを操作できる//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if(!Input.GetKey(KeyCode.LeftShift)&&!Input.GetKey(KeyCode.RightShift)) //回転速度半減/////////////////////
            {
                //操作でプレイヤーの角度加算///////////////////////////////////////////////////
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    rowling.x -= rowlingSpeedY/speedCut;
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    rowling.x += rowlingSpeedY/speedCut;
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    rowling.y -= rowlingSpeedX/speedCut;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    rowling.y += rowlingSpeedX/speedCut;
                }
                /////////////////////////////////////////////////////////////////////////////////
                ///
            }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            else
            {
                //操作でプレイヤーの角度加算///////////////////////////////////////////////////
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    rowling.x -= rowlingSpeedY;
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    rowling.x += rowlingSpeedY;
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    rowling.y -= rowlingSpeedX;
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    rowling.y += rowlingSpeedX;
                }
                /////////////////////////////////////////////////////////////////////////////////
            }

            //X軸の回転が裏返らないように調整する
            if (rowling.x > 270)
            {
                rowling.x -= 360;
            }
            if (rowling.x <= -270)
            {
                rowling.x += 360;
            }
            if (rowling.x <= -90) 
            {
                rowling.x = -89;
            }
            if (rowling.x >= 90 ) 
            {
                rowling.x = 89;
            }
            ///////////////////////////////////////
            
            //姿勢制御システム/////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (PMS)
            {
                if (rowling.x < correctionRowring && rowling.x > 0) //X軸が上にずれているときに修正する//////
                {
                    rowling.x -= fixRowling;
                    if (rowling.x <= 0)
                    {
                        rowling.x = 0;
                    }
                }/////////////////////////////////////////////////////////////////////////////////////////////////////

                if (rowling.x > -correctionRowring && rowling.x < 0)//X軸が下にずれているときに修正する/////////////
                {
                    rowling.x += fixRowling;
                    if (rowling.x >= 0)
                    {
                        rowling.x = 0;
                    }
                }////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        else   //プレイヤーが発射台の上にいる///////////////////////////////////////////////////////////////////////
        {
            if(isFire)  //プレイヤーが移動している///////
            {
                speedBuff += firstSpeed;
            }////////////////////////////////////////////
            else
            {
                tf.localPosition = Vector3.zero;
            }

            //角度を補正
            rowling.x = -lp.GetRowling().x;
            rowling.y = lp.GetRowling().y + 180;
            ////////////

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFire = true;  //発射フラグオン
                lp.Shoot(); //発射台のコントロールをオフ

            }
        }/////////////////////////////////////////////////////////////////////////////////////////////////////////////

        PMS=gm.GetPMS();
        tf.eulerAngles = new Vector3(rowling.x, rowling.y, 0);  //角度をトランスフォームに代入
    }
    //加減速処理
    private void Acceleration()
    {
        if (isFire) //移動中///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (Input.GetKeyDown(KeyCode.Space))    //一時的なブースト//////////////////////////////
            {
                accelelateSpeed = burst + playerSpeed / playerBoostTuner;   //加速分算出
                CreateBoostEffect();    //加速時演出生成
                blurIntnsity = maxBlurIntensity;    //加速演出ブラーに値を代入
            }/////////////////////////////////////////////////////////////////////////////////////////////

            if (Input.GetKey(KeyCode.Space))    //基本速度加速/////////////////////////
            {
                playerSpeed += accelerate;  //基本速度に加算
                minBlurIntnsity = accelelatedBlurIntensity; //加速演出ブラーに値を代入
            }/////////////////////////////////////////////////////////////////////////////

            if(boostSpeed > 0)  //一時加速がある間はブラーに値を入れる////////////////////////////
            {
                minBlurIntnsity = boostedBlurIntensity;
            }//////////////////////////////////////////////////////////////////////////////////////////

            else if (!Input.GetKey(KeyCode.Space))
            {
                minBlurIntnsity = 0;
            }

            //一時加速減産//////////////////////////////////////////////
            accelelateSpeed -= playerSpeed / boostBrakeTuner;
            ringSpeed -= playerSpeed * speedUpRingBrakeTuner;
            if (accelelateSpeed <= 0)
            {
                accelelateSpeed = 0;
            }
            if(ringSpeed <= 0)
            {
                ringSpeed = 0;
            }
            /////////////////////////////////////////////////////////////
            

            boostSpeed = accelelateSpeed + ringSpeed;   //ブーストを合算

        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if(isControl)
        {
            speedBuff = playerSpeed + boostSpeed;   //速度とブーストを加算
        }
    }
    //デバッグで速度調節する（後で消す
    private void SpeedControllDebager()
    {
        if(Input.GetKey(KeyCode.Alpha0))
        {
            //速度０
            playerSpeed = 0;
            speedBuff = 0;
            ////////        
        }
        if ((Input.GetKey(KeyCode.Alpha1)))
        {
            playerSpeed++;  //速度加算
        }
        if ((Input.GetKey(KeyCode.Alpha2)))
        {
            playerSpeed--;  //速度減産
        }
    }

    //体力が無くなったら爆破して消す
    private void Booooooomb()
    {
        if (playerHp <= 0)
        {
            lp.Bombed();    //発射台に操作を返す
            GameObject _ = Instantiate(explode);    //爆発エフェクト生成
            _.transform.position = tf.position; //座標代入
            Destroy(this.gameObject);   //プレイヤーオブジェクトを削除
        }
    }
    //ブースト時に火花が散るエフェクト生成
    private void CreateBoostEffect()
    {
        GameObject _ = Instantiate(boostEffect);    //エフェクト生成
        _.transform.SetParent(tf.transform, true);  //エフェクトをプレイヤーと親子付け
        _.transform.localPosition = new Vector3(0, 0, firePosZ);    //ポジション代入
        _.transform.localEulerAngles = new Vector3(0, 180, 0);  //角度代入
        _.transform.localScale = fireSize;  //サイズ代入
        BoostEffectScript bf=_.GetComponent<BoostEffectScript>();   //コンポーネント取得
        bf.SetTime();   //生存時間セット
        boostEffectList.Add(bf);    //リストに入れる
    }
    //火花削除管理
    private void EffectController()
    {
        if (boostEffectList != null)    //リストにオブジェクトが入っていたら///////////////////
        {
            for (int i = 0; i < boostEffectList.Count;)
            {
                if (boostEffectList[i].IsDelete())  //破壊フラグがオンになっていたら
                {
                    boostEffectList[i].Break(); //オブジェクトを削除
                    boostEffectList.Remove(boostEffectList[i]); //リストから削除
                }/////////////////////////////////////////////////////////////////////
                else
                {
                    boostEffectList[i].CountTime(); //生存時間を現象
                    i++;
                }
            }
        }////////////////////////////////////////////////////////////////////////////////////////
    }
    //プレイヤーの爆発までのカウントダウン
    private void CountDown()
    {
        if (isControl)
        {
            if (TimeCountScript.TimeCounter(ref time))
            {
                playerHp = 0;
            }

        }
    }
    //ブラー強度管理
    private void BlurIntnsityController()
    {
        //速度演出用ブラーの強度を下げていく////
        if (blurIntnsity > minBlurIntnsity)
        {
            blurIntnsity -= blurIntensityBrake;
            if(blurIntnsity<minBlurIntnsity)
            {
                blurIntnsity = minBlurIntnsity;
            }
        }
        ///////////////////////////////////////////
    }
    //プレイヤーの角度を発射台に合わせる
    private void SetPreShootAngle()
    {
        tf.position = lp.GetPos();  //ポジション取得
        tf.localEulerAngles = new Vector3(0, 180, 0);   //角度を初期化
    }


    #region　値受け渡し
    public void SetLaunchpad(in LaunchPointScript lp)
    {
        this.lp = lp;
    }
    public void SetGameManager(in GameManagerScript gm)
    {
        this.gm = gm;
    }
    public Vector3 GetPlayerSpeed()
    {
        return playerMoveBuff;
    }
    public float GetPlayerSpeedBuffFloat()
    {
        return speedBuff;
    }
    public float GetPlayerSpeedFloat()
    {
        return playerSpeed;
    }
    public Vector3 GetPlayerRot()
    {
        return tf.eulerAngles;
    }
    public bool GetControll()
    {
        return isControl;
    }
    public float GetBlurIntensity()
    {
        return blurIntnsity;
    }
    #endregion

    public void OnCollisionEnter(Collision collision)
    {
        playerHp = 0;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LaunchPad"))
        {     
            isControl =true;
            dust.SetActive(true);
            tf.parent = null;
            
        }
        if(other.CompareTag("SpeedUpRing"))
        {
            ringSpeed = playerSpeed * speedUpRingTuner;
            playerSpeed += playerSpeed * ringBust;
            blurIntnsity = maxBlurIntensity;
            CreateBoostEffect();
        }
        if (other.CompareTag("Bullet"))
        {
            playerHp = 0;
            other.GetComponent<FlakBulletScript>().Delete();            
        }
        if( other.CompareTag("Rock"))
        {
            playerHp = 0;
        }
        if( other.CompareTag("stage"))
        {

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("stage"))
        {

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        PMS = false;
        TimeCountScript.SetTime(ref time, time);
        blurIntnsity = 0.0f;

        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();

        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        dust = GameObject.FindWithTag("PlayerDust");
        dust.SetActive(false);
        isFire = false;
        isControl = false;
        ringSpeed = 0;
        tf.position=lp.GetPos();


        lp.SetStart(false);
        gm.SetIsHitTarget(false);

    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
    }
}
