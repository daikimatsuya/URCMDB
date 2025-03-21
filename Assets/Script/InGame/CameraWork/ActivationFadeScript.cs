using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

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
    [SerializeField] private float playerMoveTime;

    Transform tf;

    private float speedBuff;

    //���o�̊Ǘ��֐�
    public void ActivationFadeController()
    {
        if (start)
        {
            if (TimeCountScript.TimeCounter(ref life))
            {
                //���o���Ԃ��I�������Q�[�����J�n���ăI�u�W�F�N�g���폜
                Destroy(GameObject.FindWithTag("FadeObject"));
            }

            //�t�F�[�h������
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
    //��Ɉړ�����z���Ǘ�
    private void UpFade()
    {
        if(TimeCountScript.TimeCounter(ref delayBuff))
        {
            //��ɓ�����
            MoveFadeObject(moveSpeed);
        }

    }
    //���ɍs������Ǘ�
    private void DownFade()
    {
        if (TimeCountScript.TimeCounter(ref delayBuff))
        {
            //���ɓ�����
            MoveFadeObject(-moveSpeed);
        }
    }
    //�ς��Ə������Ǘ�
    private void DeleteFade()
    {
        if (TimeCountScript.TimeCounter(ref deleteTimeBuff))
        {
            //���Ԃŏ����Ă���
            Destroy(this.gameObject);
        }
    }
    //���x�𑫂��č��W���g�����X�t�H�[���ɂ����
    private void MoveFadeObject(float speed)
    {
        //�X�s�[�h�����Z���ăI�u�W�F�N�g���ړ�������
        speedBuff += speed;
        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y + speedBuff, tf.localPosition.z);
    }
    //�J�n�t���O���I���ɂ���
    public void SetStart()
    {
        start = true;
    }

    //������
    public void StartActivationFade()
    {
        //�R���|�[�l���g�擾
        tf = GetComponent<Transform>();

        start = false;

        //�������Ԃ��t���[���Őݒ�
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
