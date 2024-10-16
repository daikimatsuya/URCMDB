using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void ActivationFadeController()
    {
        if (start)
        {
            if (TimeCounter(ref life))
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
    private void UpFade()
    {
        if(TimeCounter(ref delayBuff))
        {
            MoveFadeObject(moveSpeed);
        }
    }
    private void DownFade()
    {
        if (TimeCounter(ref delayBuff))
        {
            MoveFadeObject(-moveSpeed);
        }
    }
    private void DeleteFade()
    {
        if (TimeCounter(ref deleteTimeBuff))
        {
            Destroy(this.gameObject);
        }
    }

    private void MoveFadeObject(float speed)
    {
        speedBuff += speed;
        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + speedBuff, tf.localPosition.z);
    }
    private bool TimeCounter(ref int timeBuff)
    {
        if (timeBuff <= 0)
        {
            return true;
        }
        else
        {
            timeBuff--;
            return false;
        }
    }
    private bool TimeCounter(ref float timeBuff)
    {
        if (timeBuff <= 0)
        {
            return true;
        }
        else
        {
            timeBuff--;
            return false;
        }
    }
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
