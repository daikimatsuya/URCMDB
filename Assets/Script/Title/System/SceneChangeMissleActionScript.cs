using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//ステージ選択からインゲームに遷移するときのミサイルの挙動管理
public class SceneChangeMissleActionScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxRotate;
    [SerializeField] private float targetPos;

    private SceneChangeAnimationScript scas;
    private StageSelectScript sss;

    Transform tf;
    private Vector2 moveBuff;

    //発射管理
    public void Shoot(in bool shootFlag,in bool upFlag)
    {
        if (!shootFlag||!upFlag)
        {
            return;
        }

        //目的地まで達したらフラグセット
        if (tf.localPosition.z < targetPos) 
        {
            scas.SetStartFadeFlag(true);
            sss.SetFadeFlag(true);
        }

        //スピードを計算して代入する
        Vector3 anglesBuff = tf.eulerAngles;
        moveBuff.x = moveSpeed * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
        moveBuff.y = moveSpeed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.x));

        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y-moveBuff.y, tf.localPosition.z-moveBuff.x);
        tf.localEulerAngles = new Vector3(tf.localEulerAngles.x+maxRotate, tf.localEulerAngles.y, tf.localEulerAngles.z);
        /////////////////////////////
    }

    //ミサイルアニメーション初期化
    public void StartSceneChandeMissleAnimation()
    {
        scas = GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();
        sss = GameObject.FindWithTag("TitleManager").GetComponent<StageSelectScript>();
        tf = GetComponent<Transform>();

    }

}
