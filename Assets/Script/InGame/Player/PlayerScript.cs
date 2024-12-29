using System;

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//�C���Q�[���̃v���C���[�̏���
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
    //private bool isInStage;
    //[SerializeField] private bool RockOned;

    //�v���C���[�Ǘ��֐�
    private void PlayerController()
    {
        if (gm.GetCanShotFlag())
        {
            lp.SetStart(true);
            //RockOned = false;

            SpeedControllDebager();//�f�o�b�O�p

            Booooooomb();
            Operation();
            Acceleration();
            Move();
            CountDown();
            EffectController();
            BlurIntnsityController();
           
        }
        gm.PlayerRotSet(rowling);
        if (!isFire)
        {
            tf.position = lp.GetPos();
        }
    }
    //���x�𑫂��ăg�����X�t�H�[���̃o�b�t�@�ɓ����
    private void Move()
    {
        if (isFire)
        {
            Vector3 anglesBuff = tf.eulerAngles;
            playerMove.x = speedBuff * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.y));
            playerMove.z = speedBuff * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.y));

            playerMove.x = playerMove.x * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
            playerMove.z = playerMove.z * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));

            playerMove.y = speedBuff * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.x)) * -1;


            rb.velocity = playerMove;
            playerMoveBuff = playerMove;
        }

    }
    //�v���C���[�̑���Ō����Ă������ς���
    private void Operation()
    {
        if (isControl)
        {
            if(!Input.GetKey(KeyCode.LeftShift)&&!Input.GetKey(KeyCode.RightShift))
            {
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
            }
            else
            {
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
            }
            
            if (rowling.x <= -90)
            {
                rowling.x = -89;
            }
            if (rowling.x >= 90)
            {
                rowling.x = 89;
            }

            if (PMS)
            {
                if (rowling.x < correctionRowring && rowling.x > 0)
                {
                    rowling.x -= fixRowling;
                    if (rowling.x <= 0)
                    {
                        rowling.x = 0;
                    }
                }
                if (rowling.x > -correctionRowring && rowling.x < 0)
                {
                    rowling.x += fixRowling;
                    if (rowling.x >= 0)
                    {
                        rowling.x = 0;
                    }
                }
            }
            
        }
        else
        {
            if(isFire)
            {
                speedBuff += firstSpeed;
            }
            rowling.x = -lp.GetRowling().x;
            rowling.y = lp.GetRowling().y + 180;


            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFire = true;
                lp.Shoot();

            }
        }
        PMS=gm.GetPMS();

        tf.localEulerAngles = new Vector3(rowling.x, rowling.y, tf.localEulerAngles.z);
    }
    //����������
    private void Acceleration()
    {
        if (isFire)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                accelelateSpeed = burst + playerSpeed / playerBoostTuner;
                CreateBoostEffect();
                blurIntnsity = maxBlurIntensity;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                playerSpeed += accelerate;
                minBlurIntnsity = accelelatedBlurIntensity;
            }
            if(boostSpeed > 0)
            {
                minBlurIntnsity = boostedBlurIntensity;
            }
            else if (!Input.GetKey(KeyCode.Space))
            {
                minBlurIntnsity = 0;
            }

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
            boostSpeed = accelelateSpeed + ringSpeed;
        }
        if(isControl)
        {
            speedBuff = playerSpeed + boostSpeed;
        }
    }
    //�f�o�b�O�ő��x���߂���i��ŏ���
    private void SpeedControllDebager()
    {
        if(Input.GetKey(KeyCode.Alpha0))
        {
            playerSpeed = 0;
            speedBuff = 0;
        }
        if ((Input.GetKey(KeyCode.Alpha1)))
        {
            playerSpeed++;
        }
        if ((Input.GetKey(KeyCode.Alpha2)))
        {
            playerSpeed--;
        }
    }

    //�̗͂������Ȃ����甚�j���ď���
    private void Booooooomb()
    {
        if (playerHp <= 0)
        {
            lp.Bombed();
            GameObject _ = Instantiate(explode);
            _.transform.position = tf.position;
            Destroy(this.gameObject);
        }
    }
    //�u�[�X�g���ɉΉԂ��U��G�t�F�N�g����
    private void CreateBoostEffect()
    {
        GameObject _ = Instantiate(boostEffect);
        _.transform.SetParent(tf.transform, true);
        _.transform.localPosition = new Vector3(0, 0, firePosZ);
        _.transform.localEulerAngles = new Vector3(0, 180, 0);
        _.transform.localScale = fireSize;
        BoostEffectScript bf=_.GetComponent<BoostEffectScript>();
        bf.SetTime();
        boostEffectList.Add(bf);
    }
    //�Ήԍ폜�Ǘ�
    private void EffectController()
    {
        if (boostEffectList != null)
        {
            for (int i = 0; i < boostEffectList.Count;)
            {
                if (boostEffectList[i].IsDelete())
                {
                    boostEffectList[i].Break();
                    boostEffectList.Remove(boostEffectList[i]);
                }
                else
                {
                    boostEffectList[i].CountTime();
                    i++;
                }
            }
        }
    }
    //�v���C���[�̔����܂ł̃J�E���g�_�E��
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
    //�u���[���x�Ǘ�
    private void BlurIntnsityController()
    {
        if (blurIntnsity > minBlurIntnsity)
        {
            blurIntnsity -= blurIntensityBrake;
            if(blurIntnsity<minBlurIntnsity)
            {
                blurIntnsity = minBlurIntnsity;
            }
        }

    }
    #region�@�l�󂯓n��
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
    //�_���Ă��邩�̃`�F�b�N
    public void IsLock()
    {
        //RockOned = true;
    }


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
            //isInStage = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("stage"))
        {
            //isInStage = false;
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


        lp=GameObject.FindWithTag("LaunchPoint").GetComponent<LaunchPointScript>();
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        dust = GameObject.FindWithTag("PlayerDust");
        dust.SetActive(false);
        //effectTimer = 0;
        isFire = false;
        isControl = false;
        ringSpeed = 0;
        tf.position=lp.GetPos();

        rowling.x = -lp.GetRowling().x;
        rowling.y = lp.GetRowling().y + 180;
        tf.localEulerAngles = new Vector3(rowling.x, rowling.y, tf.localEulerAngles.z);
        lp.SetStart(false);
        gm.SetIsHitTarget(false);
        //isInStage = true;
        //RockOned = false;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerController();
    }
}
