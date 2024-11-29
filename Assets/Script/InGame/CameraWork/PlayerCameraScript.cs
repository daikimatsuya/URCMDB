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
    private MovieFade mf;
    private ShaderController sc;

    [SerializeField] private float cameraDeff;
    [SerializeField] private float rotSpeed;


    //�J���������ł���v���C���[�̌��ɒǏ]����
    public void FollowPlayerInShoot()
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



            deff = FollowPlayer(playerPos.eulerAngles, rot);

            tf.position = new Vector3(playerPos.position.x - deff.x, playerPos.position.y - deff.y + 3, playerPos.position.z - deff.z);
        }
    }
    //�J���������ˑ�ɂ���v���C���[�̌��ɒǏ]����
    public void FollowPlayerInSet()
    {
        tf.localRotation = playerPos.localRotation;

        Vector3 deff = Vector3.zero;
        deff = FollowPlayer(playerPos.eulerAngles,0);

        tf.position = new Vector3(playerPos.position.x - deff.x, playerPos.position.y - deff.y + 3, playerPos.position.z - deff.z);
        rot = 0;
    }
    //�Ǐ]���̃J�����̈ʒu�o��
    private Vector3 FollowPlayer(Vector3 playerEulerAngles,float rot)
    {
        Vector3 deff = Vector3.zero;

        deff.x = cameraDeff * (float)Math.Sin(ToRadian(playerEulerAngles.y + rot));
        deff.z = cameraDeff * (float)Math.Cos(ToRadian(playerEulerAngles.y + rot));

        deff.x = deff.x * (float)Math.Cos(ToRadian(playerEulerAngles.x));
        deff.z = deff.z * (float)Math.Cos(ToRadian(playerEulerAngles.x));

        deff.y = (cameraDeff + 5) * (float)Math.Sin(ToRadian(playerEulerAngles.x)) * -1;

        return deff;
    }

    //�X�e�[�W�J�n���̃��[�r�[�̃J�������[�N
    public void MovieCut()
    {
        mc.CameraController();
        Fade();
    }
    //�v���C���[���^�[�Q�b�g�ȊO�Ŕ��������Ƃ��̃J�������[�N
    public void MissExplodeCamera()
    {
        cameraRot = transform.localEulerAngles;
        tf.position = ec.MissExplodeCamera(ref cameraRot);
        tf.localEulerAngles = cameraRot;
    }
    //�v���C���[���^�[�Q�b�g�ɂԂ������Ƃ̃J�������[�N
    public void HitExplodeCamera()
    {
        cameraRot= transform.localEulerAngles;
        tf.position=ec.HitTargetCamera(ref cameraRot);
        tf.localEulerAngles = cameraRot;
    }
    //�N���A���̃J����
    public void ClearCamera()
    {
        cameraRot = transform.localEulerAngles;
        tf.position = ec.ClearCamera(ref cameraRot);
        tf.localEulerAngles = cameraRot;
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
                if (mf.GetIsShade())
                {
                    mf.SetShadeLevel(3);
                }
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
    //�v���C���[�̃g�����X�t�H�[���擾�p�v���C���[�X�N���v�g���擾
    public void SetPlayer(Transform tf,PlayerScript ps)
    {
        playerPos = tf;
        sc.SetPlayer(ps);
    }
    //MovieFade�擾�p
    public void SetMF(MovieFade mf)
    {
        this.mf = mf;
    }
    //�v���C���[�J�����̃|�W�V�����擾�p
    public Vector3 GetPos()
    {
        return tf.position;
    }
    public Transform GetTransform()
    {
        return tf;
    }

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
