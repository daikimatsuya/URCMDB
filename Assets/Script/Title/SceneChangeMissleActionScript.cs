using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeMissleActionScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxRotate;
    [SerializeField] private float animationTime;
    private int animationTimeBuff;
    [SerializeField] private float targetPos;

    private SceneChangeAnimationScript scas;
    Transform tf;

    private Vector2 moveBuff;

    private void SCMAController()
    {
        if (scas.GetIsShotFlag())
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (tf.localPosition.z < targetPos)
        {
            scas.SetStartFadeFlag(true);
        }

        moveBuff.x = moveSpeed * (float)Math.Cos(ToRadian(tf.eulerAngles.x));
        moveBuff.y = moveSpeed * (float)Math.Sin(ToRadian(tf.eulerAngles.x));

        tf.localPosition = new Vector3(tf.localPosition.x, tf.localPosition.y-moveBuff.y, tf.localPosition.z-moveBuff.x);
        tf.localEulerAngles = new Vector3(tf.localEulerAngles.x+maxRotate, tf.localEulerAngles.y, tf.localEulerAngles.z);
    }
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }
    // Start is called before the first frame update
    void Start()
    {
        scas = GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();
        tf=GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SCMAController();
    }
}
