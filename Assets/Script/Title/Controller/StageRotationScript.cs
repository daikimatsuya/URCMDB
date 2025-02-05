using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ステージセレクト画面でステージを回転させる
public class StageRotationScript : MonoBehaviour
{
    StageSelectScript sss;
    Transform tf;

    private float rotateBuff;
    private bool rotateEnd;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float baseRot;

    //ステージの回転管理
    private void StageSelectController()
    {
        Move(sss.GetStageChangeCount());    //ステージを回転させる
    }
    //ステージ数で回転
    private void Move(Vector2 stage)
    {
        float rot = (360 / (stage.y + 1)) * stage.x;    //360度をステージ数で分割

         if (rot > rotateBuff)  //目標回転角よりも角度が小さいとき///////////////
         {
             rotateEnd = false;
             sss.SetRotateEnd(rotateEnd);   //回転フラグを代入
             rotateBuff += rotateSpeed;      //回転させる
             if (rot < rotateBuff)  //回転しすぎた時///////////////////
             {
                 rotateBuff = rot;
                 rotateEnd = true;
                 sss.SetRotateEnd(rotateEnd);   //回転フラグを代入
            }//////////////////////////////////////////////////////////

         }/////////////////////////////////////////////////////////////////////////

         else if (rot < rotateBuff) //目標回転角よりも角度が大きいとき////
         {
             rotateEnd = false;
             sss.SetRotateEnd(rotateEnd);   //回転フラグを代入
            rotateBuff -= rotateSpeed;        //回転させる
            if (rot > rotateBuff) //回転しすぎた時///////////////////
            {
                 rotateBuff = rot;
                 rotateEnd = true;
                 sss.SetRotateEnd(rotateEnd);  //回転フラグを代入
            }
         }//////////////////////////////////////////////////////////////////
         else
         {
             rotateEnd = true;
             sss.SetRotateEnd(rotateEnd);
         }
         tf.localEulerAngles = new Vector3(0, rotateBuff, 0);   //回転角代入

    }
    public void StartStageRotation()
    {
        sss = GameObject.FindWithTag("TitleManager").GetComponent<StageSelectScript>();
        tf = GetComponent<Transform>();
        rotateBuff = tf.localEulerAngles.y;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartStageRotation();
    }

    // Update is called once per frame
    void Update()
    {
        StageSelectController();
    }
}
