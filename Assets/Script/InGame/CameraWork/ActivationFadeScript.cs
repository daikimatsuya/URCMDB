using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//プレイヤーを生成したときの演出
public class ActivationFadeScript : MonoBehaviour
{
    [SerializeField] private bool start;
    [SerializeField] private bool upFade;
    [SerializeField] private bool downFade;
    [SerializeField] private bool deleteFade;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveDelay;
    private int delayBuff=0;
    [SerializeField] private float deleteTime;
    private int deleteTimeBuff=0;
    [SerializeField]private float life;
    [SerializeField] private float playerMoveTime;

    Transform tf;

    private float speedBuff;

    //演出の管理関数
    public void ActivationFadeController()
    {
        if (start)
        {
            if (TimeCountScript.TimeCounter(ref life))
            {
                //演出時間が終わったらゲームを開始してオブジェクトを削除
                Destroy(GameObject.FindWithTag("FadeObject"));
            }

            //フェードさせる
            if (upFade)
            {
                UpFade();
            }
            if (downFade)
            {
                DownFade();
            }
            if (deleteFade)
            {
                DeleteFade();
            }
        }
    }
    //上に移動する奴を管理
    private void UpFade()
    {
        if(TimeCountScript.TimeCounter(ref delayBuff))
        {
            //上に動かす
            MoveFadeObject(moveSpeed);
        }

    }
    //下に行くやつを管理
    private void DownFade()
    {
        if (TimeCountScript.TimeCounter(ref delayBuff))
        {
            //下に動かす
            MoveFadeObject(-moveSpeed);
        }
    }
    //ぱっと消えるやつ管理
    private void DeleteFade()
    {
        if (TimeCountScript.TimeCounter(ref deleteTimeBuff))
        {
            //時間で消していく
            Destroy(this.gameObject);
        }
    }
    //速度を足して座標をトランスフォームにいれる
    private void MoveFadeObject(float speed)
    {
        //スピードを加算してオブジェクトを移動させる
        speedBuff += speed;
        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + speedBuff, tf.localPosition.z);
    }
    //開始フラグをオンにする
    public void SetStart()
    {
        start = true;
    }

    //初期化
    public void StartActivationFade()
    {
        //コンポーネント取得
        tf = GetComponent<Transform>();

        start = false;

        //制限時間をフレームで設定
        if (deleteFade)
        {
            TimeCountScript.SetTime(ref deleteTimeBuff, deleteTime);
        }
        else
        {
            TimeCountScript.SetTime(ref delayBuff, moveDelay);
        }

        TimeCountScript.SetTime(ref life, life);
    }

}
