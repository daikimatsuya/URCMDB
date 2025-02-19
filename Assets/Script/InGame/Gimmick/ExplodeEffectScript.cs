using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        expantionSpeed = 100;
        dissolve = 0;
        renderer =GetComponentsInChildren<Renderer>();

    }
    public void SizeUp()
    {
        tf.localScale += new Vector3(expantionSpeed, expantionSpeed, expantionSpeed);
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
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.SetFloat("_Dissolve", dissolve);
        }
    }
}
