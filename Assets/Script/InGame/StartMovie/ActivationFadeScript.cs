using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Transform tf;


    private float speedBuff;

    //演出の管理関数
    private void ActivationFadeController()
    {
        if (start)
        {
            if (TimeCountScript.TimeCounter(ref life))
            {
                Destroy(GameObject.FindWithTag("FadeObject"));
            }
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

        if(Input.GetKeyUp(KeyCode.U))
        {
            SetStart();
        }
    }
    //上に移動する奴を管理
    private void UpFade()
    {
        if(TimeCountScript.TimeCounter(ref delayBuff))
        {
            MoveFadeObject(moveSpeed);
        }
    }
    //下に行くやつを管理
    private void DownFade()
    {
        if (TimeCountScript.TimeCounter(ref delayBuff))
        {
            MoveFadeObject(-moveSpeed);
        }
    }
    //ぱっと消えるやつ管理
    private void DeleteFade()
    {
        if (TimeCountScript.TimeCounter(ref deleteTimeBuff))
        {
            Destroy(this.gameObject);
        }
    }
    //速度を足して座標をトランスフォームにいれる
    private void MoveFadeObject(float speed)
    {
        speedBuff += speed;
        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + speedBuff, tf.localPosition.z);
    }
    //開始フラグをオンにする
    private void SetStart()
    {
        start = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();

        start = false;

        if (upFade)
        {
            delayBuff = (int)(moveDelay * 60);
        }
        if (downFade)
        {
            delayBuff = (int)(moveDelay * 60);
        }
        if (deleteFade)
        {
            deleteTimeBuff = (int)(deleteTime * 60);
        }
        life = life * 60;
        
    }

    // Update is called once per frame
    void Update()
    {
        ActivationFadeController();
    }
}
