using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�𐶐������Ƃ��̉��o
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

    //���o�̊Ǘ��֐�
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
    //��Ɉړ�����z���Ǘ�
    private void UpFade()
    {
        if(TimeCountScript.TimeCounter(ref delayBuff))
        {
            MoveFadeObject(moveSpeed);
        }
    }
    //���ɍs������Ǘ�
    private void DownFade()
    {
        if (TimeCountScript.TimeCounter(ref delayBuff))
        {
            MoveFadeObject(-moveSpeed);
        }
    }
    //�ς��Ə������Ǘ�
    private void DeleteFade()
    {
        if (TimeCountScript.TimeCounter(ref deleteTimeBuff))
        {
            Destroy(this.gameObject);
        }
    }
    //���x�𑫂��č��W���g�����X�t�H�[���ɂ����
    private void MoveFadeObject(float speed)
    {
        speedBuff += speed;
        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + speedBuff, tf.localPosition.z);
    }
    //�J�n�t���O���I���ɂ���
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
