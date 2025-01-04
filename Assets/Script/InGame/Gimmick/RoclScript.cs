using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//óéÇ∆ÇµÇΩä‚ä«óù
public class RoclScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    float breakArea;
    float fallSpeed;

    //ä«óù
    private void RockController()
    {
        if (tf.position.y < breakArea)  //ê›íËYç¿ïWÇâ∫âÒÇ¡ÇΩÇÁîjâÛÇ∑ÇÈ///
        {
            BreakRock();
        }//////////////////////////////////////////////////////////////////////

        Fall(); //óéâ∫
    }
    //óéâ∫èàóù
    private void Fall()
    {
        rb.velocity = Vector3.zero;
        rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);  //óéâ∫ë¨ìxë„ì¸
    }
    //ä‚ÇîjâÛÇ∑ÇÈ
    private void BreakRock()
    {
        Destroy(this.gameObject);
    }

    #region íléÛÇØìnÇµ
    public void SetBreakArea(float posY)
    {
        breakArea = posY;
    }
    public void SetFallSpeed(float speed)
    {
        fallSpeed= speed;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RockController();
    }
}
