using System;

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Usefull;

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

    //�v���C���[�Ǘ��֐�
    private void PlayerController()
    {
        if (gm.GetCanShotFlag())    //���˂ł���悤�ɂȂ�����//////////////////
        {
            lp.SetStart(true);//�X�^�[�g�t���O���I��
            SpeedControllDebager();//�f�o�b�O�p
            Booooooomb();   //��������
            Operation();    //�v���C���[����
            Acceleration(); //�����Ǘ�
            Move(); //�ړ�
            CountDown();    //�������ԊǗ�
            EffectController(); //���o�Ǘ�
            BlurIntnsityController();   //�����\���u���[�Ǘ�
           
        }//////////////////////////////////////////////////////////////////////////
        else
        {
            SetPreShootAngle();
        }

        gm.PlayerRotSet(rowling);   //�v���C���[�̊p�x����

    }
    //���x�𑫂��ăg�����X�t�H�[���̃o�b�t�@�ɓ����
    private void Move()
    {
        if (isFire)
        {
            Vector3 anglesBuff = tf.eulerAngles;

            //���ʕ����̑��x���Z�o//////////////////////////////////////////////////////////////////////////
            playerMove.x = speedBuff * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.y));
            playerMove.z = speedBuff * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.y));
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            //���������Ɛ��������̑��x���Z�o///////////////////////////////////////////////////////////////////////////
            playerMove.x = playerMove.x * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
            playerMove.z = playerMove.z * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));

            playerMove.y = speedBuff * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.x)) * -1;
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            rb.velocity = playerMove;   //���x�ɑ��
            playerMoveBuff = playerMove;
        }
        
    }
    //�v���C���[�̑���Ō����Ă������ς���
    private void Operation()
    {
        if (isControl) //�v���C���[�𑀍�ł���//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if(!Input.GetKey(KeyCode.LeftShift)&&!Input.GetKey(KeyCode.RightShift)) //��]���x����/////////////////////
            {
                //����Ńv���C���[�̊p�x���Z///////////////////////////////////////////////////
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
                //����Ńv���C���[�̊p�x���Z///////////////////////////////////////////////////
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

            //X���̉�]�����Ԃ�Ȃ��悤�ɒ�������
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
            
            //�p������V�X�e��/////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (PMS)
            {
                if (rowling.x < correctionRowring && rowling.x > 0) //X������ɂ���Ă���Ƃ��ɏC������//////
                {
                    rowling.x -= fixRowling;
                    if (rowling.x <= 0)
                    {
                        rowling.x = 0;
                    }
                }/////////////////////////////////////////////////////////////////////////////////////////////////////

                if (rowling.x > -correctionRowring && rowling.x < 0)//X�������ɂ���Ă���Ƃ��ɏC������/////////////
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

        else   //�v���C���[�����ˑ�̏�ɂ���///////////////////////////////////////////////////////////////////////
        {
            if(isFire)  //�v���C���[���ړ����Ă���///////
            {
                speedBuff += firstSpeed;
            }////////////////////////////////////////////
            else
            {
                tf.localPosition = Vector3.zero;
            }

            //�p�x��␳
            rowling.x = -lp.GetRowling().x;
            rowling.y = lp.GetRowling().y + 180;
            ////////////

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isFire = true;  //���˃t���O�I��
                lp.Shoot(); //���ˑ�̃R���g���[�����I�t

            }
        }/////////////////////////////////////////////////////////////////////////////////////////////////////////////

        PMS=gm.GetPMS();
        tf.eulerAngles = new Vector3(rowling.x, rowling.y, 0);  //�p�x���g�����X�t�H�[���ɑ��
    }
    //����������
    private void Acceleration()
    {
        if (isFire) //�ړ���///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            if (Input.GetKeyDown(KeyCode.Space))    //�ꎞ�I�ȃu�[�X�g//////////////////////////////
            {
                accelelateSpeed = burst + playerSpeed / playerBoostTuner;   //�������Z�o
                CreateBoostEffect();    //���������o����
                blurIntnsity = maxBlurIntensity;    //�������o�u���[�ɒl����
            }/////////////////////////////////////////////////////////////////////////////////////////////

            if (Input.GetKey(KeyCode.Space))    //��{���x����/////////////////////////
            {
                playerSpeed += accelerate;  //��{���x�ɉ��Z
                minBlurIntnsity = accelelatedBlurIntensity; //�������o�u���[�ɒl����
            }/////////////////////////////////////////////////////////////////////////////

            if(boostSpeed > 0)  //�ꎞ����������Ԃ̓u���[�ɒl������////////////////////////////
            {
                minBlurIntnsity = boostedBlurIntensity;
            }//////////////////////////////////////////////////////////////////////////////////////////

            else if (!Input.GetKey(KeyCode.Space))
            {
                minBlurIntnsity = 0;
            }

            //�ꎞ�������Y//////////////////////////////////////////////
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
            

            boostSpeed = accelelateSpeed + ringSpeed;   //�u�[�X�g�����Z

        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if(isControl)
        {
            speedBuff = playerSpeed + boostSpeed;   //���x�ƃu�[�X�g�����Z
        }
    }
    //�f�o�b�O�ő��x���߂���i��ŏ���
    private void SpeedControllDebager()
    {
        if(Input.GetKey(KeyCode.Alpha0))
        {
            //���x�O
            playerSpeed = 0;
            speedBuff = 0;
            ////////        
        }
        if ((Input.GetKey(KeyCode.Alpha1)))
        {
            playerSpeed++;  //���x���Z
        }
        if ((Input.GetKey(KeyCode.Alpha2)))
        {
            playerSpeed--;  //���x���Y
        }
    }

    //�̗͂������Ȃ����甚�j���ď���
    private void Booooooomb()
    {
        if (playerHp <= 0)
        {
            lp.Bombed();    //���ˑ�ɑ����Ԃ�
            GameObject _ = Instantiate(explode);    //�����G�t�F�N�g����
            _.transform.position = tf.position; //���W���
            Destroy(this.gameObject);   //�v���C���[�I�u�W�F�N�g���폜
        }
    }
    //�u�[�X�g���ɉΉԂ��U��G�t�F�N�g����
    private void CreateBoostEffect()
    {
        GameObject _ = Instantiate(boostEffect);    //�G�t�F�N�g����
        _.transform.SetParent(tf.transform, true);  //�G�t�F�N�g���v���C���[�Ɛe�q�t��
        _.transform.localPosition = new Vector3(0, 0, firePosZ);    //�|�W�V�������
        _.transform.localEulerAngles = new Vector3(0, 180, 0);  //�p�x���
        _.transform.localScale = fireSize;  //�T�C�Y���
        BoostEffectScript bf=_.GetComponent<BoostEffectScript>();   //�R���|�[�l���g�擾
        bf.SetTime();   //�������ԃZ�b�g
        boostEffectList.Add(bf);    //���X�g�ɓ����
    }
    //�Ήԍ폜�Ǘ�
    private void EffectController()
    {
        if (boostEffectList != null)    //���X�g�ɃI�u�W�F�N�g�������Ă�����///////////////////
        {
            for (int i = 0; i < boostEffectList.Count;)
            {
                if (boostEffectList[i].IsDelete())  //�j��t���O���I���ɂȂ��Ă�����
                {
                    boostEffectList[i].Break(); //�I�u�W�F�N�g���폜
                    boostEffectList.Remove(boostEffectList[i]); //���X�g����폜
                }/////////////////////////////////////////////////////////////////////
                else
                {
                    boostEffectList[i].CountTime(); //�������Ԃ�����
                    i++;
                }
            }
        }////////////////////////////////////////////////////////////////////////////////////////
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
        //���x���o�p�u���[�̋��x�������Ă���////
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
    //�v���C���[�̊p�x�𔭎ˑ�ɍ��킹��
    private void SetPreShootAngle()
    {
        tf.position = lp.GetPos();  //�|�W�V�����擾
        tf.localEulerAngles = new Vector3(0, 180, 0);   //�p�x��������
    }


    #region�@�l�󂯓n��
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
