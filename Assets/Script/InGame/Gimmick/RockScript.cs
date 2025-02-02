using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//���Ƃ�����Ǘ�
public class RockScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    //��������
    public void Fall(in float fallSpeed,in bool isPose)
    {
        rb.velocity = Vector3.zero;
        if (isPose)
        {
            return;
        }
        rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);  //�������x���
    }
    //���j�󂷂�
    public void BreakRock()
    {
        Destroy(this.gameObject);
    }

    #region �l�󂯓n��
    public float GetPos()
    {
        return tf.transform.position.y;
    }

    #endregion
    public void StartRock()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }
}
