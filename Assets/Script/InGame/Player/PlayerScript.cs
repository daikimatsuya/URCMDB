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
    private GameObject dust;
    private GameObject activateFadeObject;

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
    [SerializeField] private float firePosZ;
    [SerializeField] private Vector3 fireSize;
    [SerializeField] private float correctionRowring;
    private Vector2 rowling;
    [SerializeField] private float playerBoostTuner;
    [SerializeField] private float speedUpRingTuner;
    [SerializeField] private float boostBrakeTuner;
    [SerializeField] private float speedUpRingBrakeTuner;
    [SerializeField] private float redBoostTime;
    private float redBoostTimeBuff;
    [SerializeField] private float redBoostSpeed;

    private Vector3 playerMove;
    private Vector3 playerMoveBuff;
    private float accelelateSpeed;
    private float boostSpeed;

    private bool isFire;
    private bool isControl;
    private float ringSpeed;
    private bool PMS;
    [SerializeField] private bool redBustFlag;
    private float redBuff;
    private List<BoostEffectScript> boostEffectList = new List<BoostEffectScript>();

    //プレイヤー管理関数
    public void PlayerController(in bool isPose)
    {
        if(isPose)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        if (activateFadeObject==null)    //発射できるようになったら//////////////////
        {
            lp.SetStart(true);              //スタートフラグをオン
            SpeedControllDebager();   //デバッグ用
            Booooooomb();                //爆発処理
            ChangePMS();                  //PMS管理
            Operation();                     //プレイヤー操作
            Acceleration();                 //加速管理
            Move();                           //移動
            CountDown();                  //生存時間管理
            EffectController();            //演出管理

        }//////////////////////////////////////////////////////////////////////////
        else
        {
            SetPreShootAngle(); //発射前の角度調整
        }

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
        Vector2 rowlingBuff = Vector2.zero;

        if (isControl) //プレイヤーを操作できる//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            Vector2 speed;
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)||Input.GetAxis("LeftTrigger")==1)
            {
                speed = new Vector2(rowlingSpeedX, rowlingSpeedY);
            }
            else
            {
                speed = new Vector2(rowlingSpeedX/speedCut, rowlingSpeedY/speedCut);
            }

            //操作でプレイヤーの角度加算///////////////////////////////////////////////////

            //コントローラー操作/////////////////////
            float axisY = Input.GetAxis("LeftStickY");
            rowlingBuff.x = speed.y * axisY;

            float axisX = Input.GetAxis("LeftStickX");
            rowlingBuff.y = speed.x * axisX;
            //////////////////////////////////////////

            //キーボード操作//////////////////////////
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                rowlingBuff.x = -speed.y;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                rowlingBuff.x = speed.y;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                rowlingBuff.y = -speed.x;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                rowlingBuff.y = speed.x;
            }
            ///////////////////////////////////////////

            ////////////////////////////////////////////////////////////////////////////////

            rowling += rowlingBuff;

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

            if (Input.GetKeyDown(KeyCode.Space)||Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
            {
                isFire = true;  //発射フラグオン
                lp.Shoot();     //発射台のコントロールをオフ

            }
        }/////////////////////////////////////////////////////////////////////////////////////////////////////////////

        tf.eulerAngles = new Vector3(rowling.x, rowling.y, 0);  //角度をトランスフォームに代入
    }

    //PMSのオンオフ
    private void ChangePMS()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 3"))
        {
            if (PMS)    //PMS管理//////////////////
            {
                PMS = false;
            }
            else
            {
                PMS = true;
            }//////////////////////////////////////
            Usefull.PMSScript.SetPMS(PMS);
        }
    }

    //加減速処理
    private void Acceleration()
    {
        if (!isFire) //移動中////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {

            return;
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Space)||Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))    //一時的なブースト//////////////////////////////
        {           
            accelelateSpeed = (burst + playerSpeed / playerBoostTuner)+redBuff;   //加速分算出
            CreateBoostEffect();                                                                           //加速時演出生成

            if (redBustFlag)
            {
                TimeCountScript.SetTime(ref redBoostTimeBuff, redBoostTime);
                redBustFlag = false;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Input.GetKey(KeyCode.Space)||Input.GetAxis("RightTrigger")!=0)    //基本速度加速/////////////////////////
            {
                playerSpeed += accelerate;                         //基本速度に加算

            }////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            
        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if (Input.GetKey(KeyCode.Space)||Input.GetAxis("RightTrigger")!=0)    //基本速度加速/////////////////////////
        {
            playerSpeed += accelerate;  //基本速度に加算
        }////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


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
        
        if (!TimeCountScript.TimeCounter(ref redBoostTimeBuff))
        {
            accelelateSpeed +=playerSpeed * redBoostSpeed;
            redBuff +=playerSpeed * redBoostSpeed;
        }
        else
        {
            redBuff = 0;
        }

        boostSpeed = accelelateSpeed + ringSpeed;   //ブーストを合算
        
        if (isControl)
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
            lp.Bombed();                                        //発射台に操作を返す
            Destroy(this.gameObject);                     //プレイヤーオブジェクトを削除
        }
    }
    //ブースト時に火花が散るエフェクト生成
    private void CreateBoostEffect()
    {
        GameObject _ = Instantiate(boostEffect);                               //エフェクト生成
        _.transform.SetParent(tf.transform, true);                              //エフェクトをプレイヤーと親子付け
        _.transform.localPosition = new Vector3(0, 0, firePosZ);          //ポジション代入
        _.transform.localEulerAngles = new Vector3(0, 180, 0);          //角度代入
        _.transform.localScale = fireSize;                                           //サイズ代入
        BoostEffectScript bf=_.GetComponent<BoostEffectScript>();   //コンポーネント取得
        bf.SetTime();                                                                       //生存時間セット
        boostEffectList.Add(bf);                                                        //リストに入れる
    }
    //火花削除管理
    private void EffectController()
    {
        if (boostEffectList == null)    //リストにオブジェクトが入ってなかったらリターン//////
        {
            return;
        }////////////////////////////////////////////////////////////////////////////////////////

        for (int i = 0; i < boostEffectList.Count;)
        {
            if (boostEffectList[i].IsDelete())  //破壊フラグがオンになっていたら
            {
                boostEffectList[i].Break();                         //オブジェクトを削除
                boostEffectList.Remove(boostEffectList[i]); //リストから削除

            }/////////////////////////////////////////////////////////////////////
            else
            {
                boostEffectList[i].CountTime(); //生存時間を現象
                i++;
            }
        }
       
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

    //プレイヤーの角度を発射台に合わせる
    private void SetPreShootAngle()
    {
        tf.position = lp.GetPos();                                 //ポジション取得
        tf.localEulerAngles = new Vector3(0, 180, 0);   //角度を初期化
    }

    #region　値受け渡し
    public float GetMaxRingBoost()
    {
        return playerSpeed * speedUpRingTuner;
    }
    public float GetRingBoost()
    {
        return ringSpeed;
    }
    public float GetMaxBoost()
    {
        return (burst + playerSpeed / playerBoostTuner);
    }
    public float GetPlayerAcce()
    {
        return boostSpeed;
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
        if (tf == null)
        {
            tf = GetComponent<Transform>();
        }
        return tf.eulerAngles;
    }
    public bool GetControll()
    {
        return isControl;
    }

    public bool GetIsFire()
    {
        return isFire;
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public void SetTransform(in Transform tf)
    {
        this.tf = tf;
    }
    public Vector3 GetPlayerPos()
    {
        return tf.position;
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
            CreateBoostEffect();
        }
        if (other.CompareTag("SpeedUpRingRed"))
        {
            ringSpeed = playerSpeed * speedUpRingTuner ;
            playerSpeed += playerSpeed * ringBust;
            CreateBoostEffect();
            redBustFlag = true;
        }
        if (other.CompareTag("Bullet"))
        {
            playerHp = 0;
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

    //プレイヤー初期化
    public void StartPlayer(in LaunchPointScript lp,in GameObject afs)
    {
        this.lp = lp;
        this.activateFadeObject = afs;
        TimeCountScript.SetTime(ref time, time);
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();

        dust = GameObject.FindWithTag("PlayerDust");
        dust.SetActive(false);
        isFire = false;
        isControl = false;
        ringSpeed = 0;
        tf.position = lp.GetPos();
        redBustFlag = false;
        lp.SetStart(false);
        PMS = Usefull.PMSScript.GetPMS();
        redBoostTimeBuff = 0;
        redBustFlag = false;
    }

}
