using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationFadeScript : MonoBehaviour
{
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
        if (life < 0)
        {
           Destroy(this.gameObject);
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
        life--;
    }
    private void UpFade()
    {
        if(delayBuff<0)
        {
            speedBuff += moveSpeed;
            tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y+speedBuff, tf.localPosition.z);
        }
        delayBuff--;
    }
    private void DownFade()
    {
        if (delayBuff < 0)
        {
            speedBuff += moveSpeed;
            tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y - speedBuff, tf.localPosition.z);
        }
        delayBuff--;
    }
    private void DeleteFade()
    {
        if (deleteTimeBuff < 0)
        {
            this.gameObject.SetActive(false);
        }
        deleteTimeBuff--;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();

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
