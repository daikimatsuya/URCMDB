using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//óéÇ∆ÇµÇΩä‚ä«óù
public class RockScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;


    //ä«óù
    //public void RockController()
    //{
    //    if (tf.position.y < breakArea)  //ê›íËYç¿ïWÇâ∫âÒÇ¡ÇΩÇÁîjâÛÇ∑ÇÈ///
    //    {
    //        BreakRock();
    //    }//////////////////////////////////////////////////////////////////////

    //    Fall(); //óéâ∫
    //}
    //óéâ∫èàóù
    public void Fall(in float fallSpeed)
    {
        rb.velocity = Vector3.zero;
        rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);  //óéâ∫ë¨ìxë„ì¸
    }
    //ä‚ÇîjâÛÇ∑ÇÈ
    public void BreakRock()
    {
        Destroy(this.gameObject);
    }

    #region íléÛÇØìnÇµ
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
