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
    private  Renderer[] renderers;
    private float dissolve;
    private float edge;

    //������
    public void StartExplodeEffect()
    {

        tf = GetComponent<Transform>();
        TimeCountScript.SetTime(ref expantionBuff, expantionTime);

        dissolve = 0;
        renderers =GetComponentsInChildren<Renderer>();

    }

    //�T�C�Y��傫������
    public void SizeUp()
    {
        expantionSpeed = 0;
        expantionSpeed = 1 - (expantionBuff / (expantionTime * 60));
        expantionSpeed = (1 - (float)Math.Pow(1 - expantionSpeed, 5) )* maxSize;
        tf.localScale = new Vector3(expantionSpeed, expantionSpeed, expantionSpeed);
    }

    //��]������
    public void Rotation()
    {
        tf.localEulerAngles += new Vector3(0, rotSpeed, 0);
    }

    //�f�B�]���u������
    public void Dissolve()
    {
        dissolve = 1-(expantionBuff / (expantionTime * 60));
        dissolve = dissolve * dissolve * dissolve;
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetFloat("_Dissolve", dissolve);
        }
    }
    //�[�����̐F�����Ԃŕς���
    public void Edge()
    {
        edge = 1 - (expantionBuff / (expantionTime * 60));
        edge=edge*edge*edge;
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetFloat("_Threshold", edge);
        }
    }

    //���Ԃ��Ǘ�����
    public bool CountDown()
    {
        return TimeCountScript.TimeCounter(ref expantionBuff);
    }

    //�폜����
    public void Break()
    {
        Destroy(this.gameObject);
    }

    #region �l�󂯓n��
    public void SetMaxSize(float maxSize)
    {
        this.maxSize=maxSize;
    }
    public void SetTime(float time)
    {
        expantionBuff = (int)time;
        expantionTime = time / 60;
    }
    public void SetDissolve(float dissolve)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetFloat("_Dissolve", dissolve);
        }
    }
    public void SetTillingOffset(Vector2 tilling,Vector2 offset)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetVector("_MainTex_ST", new Vector4(tilling.x, tilling.y, offset.x, offset.y));
        }
    }
    public void SetRotationSpeed(float speed)
    {
        this.rotSpeed = speed;
    }
    #endregion
}
