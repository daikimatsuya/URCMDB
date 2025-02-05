using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//動く雲のスクリプト
public class MoveCloundScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxHight;
    [SerializeField] private int minHight;
    [SerializeField] private bool isUp;

    Transform tf;

    private float posBuff;
    //上下に動かす
    private void Move()
    {
        //上下移動を繰り返す///////////
        if (isUp)
        {
            posBuff += moveSpeed;
            if (posBuff > maxHight)
            {
                posBuff = maxHight;
                isUp = false;
            }
        }
        else
        {
            posBuff -= moveSpeed;
            if (posBuff < -maxHight)
            {
                posBuff = -maxHight;
                isUp = true;
            }
        }
        /////////////////////////////////
        tf.localPosition = new Vector3(tf.localPosition.x, posBuff, tf.localPosition.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        posBuff = tf.localPosition.y;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
