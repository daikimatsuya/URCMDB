using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//くっつけたオブジェクトを上下に動かす
public class ButtobeScript : MonoBehaviour
{
    [SerializeField] private float maxHight;
    [SerializeField] private float moveSpeed;

    Transform tf;

    private float initialPosY;
    private float posBuff;
    private bool isUp;
    //上下に移動
    private void Move()
    {
        //上下移動を繰り返す////////////////
        if(isUp)
        {
            posBuff += moveSpeed;

            if(posBuff>maxHight)    //最大値まで上がると移動方向切り替え//
            {
                posBuff = maxHight;
                isUp = false;
            }//////////////////////////////////////////////////////////////////
        }
        else
        {
            posBuff -= moveSpeed;

            if(posBuff<-maxHight)   //最低値まで下がると移動方向切り替え//
            {
                posBuff=-maxHight;
                isUp = true;
            }//////////////////////////////////////////////////////////////////
        }
        //////////////////////////////////
        tf.localPosition = new Vector3(tf.localPosition.x, initialPosY + posBuff, tf.localPosition.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        initialPosY=tf.localPosition.y;
        isUp=true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();  
    }
}
