using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ステージ選択からインゲームに遷移するときのミサイルの挙動管理
public class SceneChangeMissleActionScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxRotate;
    [SerializeField] private float animationTime;
    private int animationTimeBuff;
    [SerializeField] private float targetPos;

    private SceneChangeAnimationScript scas;
    private StageSelectScript sss;
    private TitleScript ts;
    Transform tf;

    private Vector2 moveBuff;
    private bool isShot;
    //飛んでくミサイル管理
    private void SCMAController()
    {
        if(ts == null)
        {
            ts=sss.GetTitleScript();
        }
        if (ts.GetShootFlag())
        {
            Shoot();
        }
    }
    //発射管理
    public void Shoot()
    {
        if (tf.localPosition.z < targetPos)
        {
            scas.SetStartFadeFlag(true);
            sss.SetFadeFlag(true);
        }

        Vector3 anglesBuff = tf.eulerAngles;
        moveBuff.x = moveSpeed * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
        moveBuff.y = moveSpeed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.x));

        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y-moveBuff.y, tf.localPosition.z-moveBuff.x);
        tf.localEulerAngles = new Vector3(tf.localEulerAngles.x+maxRotate, tf.localEulerAngles.y, tf.localEulerAngles.z);
    }
    //発射フラグ受け渡し
    public void SetShotFlag(bool flag)
    {
        isShot= flag;
    }

    // Start is called before the first frame update
    void Start()
    {
        scas = GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();
        sss=GameObject.FindWithTag("TitleManager").GetComponent<StageSelectScript>();
        tf=GetComponent<Transform>();
        ts=sss.GetTitleScript();
    }

    // Update is called once per frame
    void Update()
    {
        SCMAController();
    }
}
