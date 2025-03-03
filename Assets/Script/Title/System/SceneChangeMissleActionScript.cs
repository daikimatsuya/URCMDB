using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�X�e�[�W�I������C���Q�[���ɑJ�ڂ���Ƃ��̃~�T�C���̋����Ǘ�
public class SceneChangeMissleActionScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxRotate;
    [SerializeField] private float animationTime;
    private int animationTimeBuff;
    [SerializeField] private float targetPos;

    private SceneChangeAnimationScript scas;
    private StageSelectScript sss;


    Transform tf;
    private Vector2 moveBuff;

    //���ˊǗ�
    public void Shoot(in bool shootFlag)
    {
        if (!shootFlag)
        {
            return;
        }
        if (tf.localPosition.z < targetPos) //�ݒ���W�ɓ��B������////////
        {
            //�t���O���I���ɂ���
            scas.SetStartFadeFlag(true);
            sss.SetFadeFlag(true);
            /////////////////////
            
        }//////////////////////////////////////////////////////////////////

        //�X�s�[�h���v�Z���đ������
        Vector3 anglesBuff = tf.eulerAngles;
        moveBuff.x = moveSpeed * (float)Math.Cos(ToRadianScript.ToRadian(ref anglesBuff.x));
        moveBuff.y = moveSpeed * (float)Math.Sin(ToRadianScript.ToRadian(ref anglesBuff.x));

        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y-moveBuff.y, tf.localPosition.z-moveBuff.x);
        tf.localEulerAngles = new Vector3(tf.localEulerAngles.x+maxRotate, tf.localEulerAngles.y, tf.localEulerAngles.z);
        /////////////////////////////
    }


    // Start is called before the first frame update
    public void StartSceneChandeMissleAnimation()
    {
        scas = GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();
        sss = GameObject.FindWithTag("TitleManager").GetComponent<StageSelectScript>();
        tf = GetComponent<Transform>();

    }

}
