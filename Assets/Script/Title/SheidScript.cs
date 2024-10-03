using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheidScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float limitPos;
    [SerializeField] private float resetPos;

    Transform tf;
    TitlegameScript ts;

    private Vector3 initialPos;


    private void SheidController()
    {
        if(ts.GetResetActionFlag())
        {
            Move();
        }
    }
    private void Move()
    {
        tf.position = new Vector3(tf.position.x - moveSpeed, tf.position.y , tf.position.z);

        if(tf.position.x < resetPos && tf.position.x > resetPos - moveSpeed)
        {
            ts.SetResetFlag(true);
        }

        if(tf.position.x < limitPos)
        {
            ResetAction();
        }
    }
    private void ResetAction()
    {
        ts.SetResetActionFlag(false);
        tf.position = initialPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        ts=GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();
        initialPos = tf.position;
    }

    // Update is called once per frame
    void Update()
    {
        SheidController();
    }
}
