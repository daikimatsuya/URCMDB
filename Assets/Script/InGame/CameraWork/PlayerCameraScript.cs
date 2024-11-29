using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//インゲームでプレイヤーにくっついているカメラ管理
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


    //カメラが飛んでいるプレイヤーの後ろに追従する
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
    //カメラが発射台にあるプレイヤーの後ろに追従する
    public void FollowPlayerInSet()
    {
        tf.localRotation = playerPos.localRotation;

        Vector3 deff = Vector3.zero;
        deff = FollowPlayer(playerPos.eulerAngles,0);

        tf.position = new Vector3(playerPos.position.x - deff.x, playerPos.position.y - deff.y + 3, playerPos.position.z - deff.z);
        rot = 0;
    }
    //追従中のカメラの位置出す
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

    //ステージ開始時のムービーのカメラワーク
    public void MovieCut()
    {
        mc.CameraController();
        Fade();
    }
    //プレイヤーがターゲット以外で爆発したときのカメラワーク
    public void MissExplodeCamera()
    {
        cameraRot = transform.localEulerAngles;
        tf.position = ec.MissExplodeCamera(ref cameraRot);
        tf.localEulerAngles = cameraRot;
    }
    //プレイヤーがターゲットにぶつかったとのカメラワーク
    public void HitExplodeCamera()
    {
        cameraRot= transform.localEulerAngles;
        tf.position=ec.HitTargetCamera(ref cameraRot);
        tf.localEulerAngles = cameraRot;
    }
    //クリア時のカメラ
    public void ClearCamera()
    {
        cameraRot = transform.localEulerAngles;
        tf.position = ec.ClearCamera(ref cameraRot);
        tf.localEulerAngles = cameraRot;
    }
    //フェード管理
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

    //デグラド変換
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }
    //プレイヤーのトランスフォーム取得用プレイヤースクリプトも取得
    public void SetPlayer(Transform tf,PlayerScript ps)
    {
        playerPos = tf;
        sc.SetPlayer(ps);
    }
    //MovieFade取得用
    public void SetMF(MovieFade mf)
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
