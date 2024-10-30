using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//�C���Q�[���Ńv���C���[�ɂ������Ă���J�����Ǘ�
public class PlayerCameraScript : MonoBehaviour
{
    private Transform tf;
    private Transform playerPos;
    private Vector3 cameraRot;
    private float rot;


    private MovieCamera mc;
    private ExplodeCamera ec;
    private PlayerScript ps;
    private GameObject player;
    private MovieFade mf;

    [SerializeField] private float cameraDeff;
    [SerializeField] private float rotSpeed;


    //�J�������v���C���[�̌��ɒǏ]����
    public void FollowPlayer()
    {
        if (playerPos != null)
        {
            tf.localRotation = playerPos.localRotation;

            Vector3 deff = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (rot < 10)
            {
                rot += rotSpeed;
            }

        }
        if (Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
            if (rot > -10)
            {
                rot -= rotSpeed;
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
        if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)&& !Input.GetKey(KeyCode.A)&& !Input.GetKey(KeyCode.D)) 
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

            
            deff.x = cameraDeff * (float)Math.Sin(ToRadian(playerPos.eulerAngles.y + rot));
            deff.z = cameraDeff * (float)Math.Cos(ToRadian(playerPos.eulerAngles.y + rot));
            

            deff.x = deff.x * (float)Math.Cos(ToRadian(playerPos.eulerAngles.x));
            deff.z = deff.z * (float)Math.Cos(ToRadian(playerPos.eulerAngles.x));

            deff.y = (cameraDeff + 5) * (float)Math.Sin(ToRadian(playerPos.eulerAngles.x)) * -1;


            tf.position = new Vector3(playerPos.position.x - deff.x, playerPos.position.y - deff.y + 3, playerPos.position.z - deff.z);
        }
    }
    public void MovieCut()
    {
        mc.CameraController();
        Fade();
    }
    //�v���C���[�����Ȃ�������Ď擾����
    private void SearchPlayer()
    {

        if (playerPos == null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cameraRot = transform.localEulerAngles;
                tf.position = ec.ExplodeCameraController(ref cameraRot);
                tf.localEulerAngles = cameraRot;
            }
            if (GameObject.FindWithTag("Player"))
            {
                rot = 0;
                player = GameObject.FindWithTag("Player");
                playerPos = player.GetComponent<Transform>();
                ps = player.GetComponent<PlayerScript>();
            }
        }
    }
    //�t�F�[�h�Ǘ�
    private void Fade()
    {
        if (mc.GetEnd())
        {
            mf.SetShadeLevel(3);
        }
        else
        {
            if (mc.GetSkip())
            {

                mf.SetShadeLevel(2);
            }
            else if (mc.GetFadeoutTime() > mc.GetMoveTime())
            {
                mf.SetShadeLevel(2);
            }
            else
            {
                mf.SetShadeLevel(1);
            }
        }
    }

    //�f�O���h�ϊ�
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }
    //�v���C���[�̃g�����X�t�H�[���擾�p
    public void SetPlayer(Transform tf)
    {
        playerPos = tf;
    }
    //MovieFade�擾�p
    public void SetMF(MovieFade mf)
    {
        this.mf = mf;
    }



    //MoviewFade�擾�p
    //public void SetMoviewFade(MovieFade mf)
    //{
    //    this.mf = mf;
    //}

    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        mc=GetComponent<MovieCamera>();
        ec=GetComponent<ExplodeCamera>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
