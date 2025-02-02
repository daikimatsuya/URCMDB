using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//—‚Æ‚µ‚½ŠâŠÇ—
public class RockScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    //—‰ºˆ—
    public void Fall(in float fallSpeed,in bool isPose)
    {
        rb.velocity = Vector3.zero;
        if (isPose)
        {
            return;
        }
        rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);  //—‰º‘¬“x‘ã“ü
    }
    //Šâ‚ğ”j‰ó‚·‚é
    public void BreakRock()
    {
        Destroy(this.gameObject);
    }

    #region ’ló‚¯“n‚µ
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
