using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private TitleScript ts;
    Transform tf;

    private Vector2 moveBuff;
    private bool isShot;
    //���ł��~�T�C���Ǘ�
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
    //���ˊǗ�
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
    //���˃t���O�󂯓n��
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
