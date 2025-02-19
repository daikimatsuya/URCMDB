using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

public class ExplodeEffectScript : MonoBehaviour
{
    [SerializeField] float maxSize;
    [SerializeField] float expantionTime;
    private int expantionBuff;
    [SerializeField] float rotSpeed;

    Transform tf;

    private float expantionSpeed;
    private new Renderer[] renderer;
    private float dissolve;
    public void StartExplodeEffect()
    {

        tf = GetComponent<Transform>();
        TimeCountScript.SetTime(ref expantionBuff, expantionTime);

        dissolve = 0;
        renderer =GetComponentsInChildren<Renderer>();

    }
    public void SizeUp()
    {
        expantionSpeed = 1 - (expantionBuff / (expantionTime * 60));
        expantionSpeed = (1 - (float)Math.Pow(1 - expantionSpeed, 3) )* maxSize;
        tf.localScale = new Vector3(expantionSpeed, expantionSpeed, expantionSpeed);
    }
    public void Rotation()
    {
        tf.localEulerAngles += new Vector3(0, rotSpeed, 0);
    }
    public void Transparency()
    {

    }
    public void Dissolve()
    {
        dissolve = 1-(expantionBuff / (expantionTime * 60));
        dissolve = dissolve * dissolve * dissolve;
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.SetFloat("_Dissolve", dissolve);
        }
    }
    public bool CountDown()
    {
        return TimeCountScript.TimeCounter(ref expantionBuff);
    }
    public void Break()
    {
        Destroy(this.gameObject);
    }
}
