using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

//ステージセレクト画面でステージを回転させる
public class StageRotationScript : MonoBehaviour
{
    Transform tf;

    private float rotateBuff;
    private bool rotateEnd;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float baseRot;

    //ステージ数で回転
    public void Move(int stageChangeCount,int maxStage)
    {
        float rot = (360 / (maxStage + 1)) * stageChangeCount;    //360度をステージ数で分割

         if (rot > rotateBuff)  //目標回転角よりも角度が小さいとき///////////////
         {
             rotateEnd = false;
             rotateBuff += rotateSpeed;      //回転させる
             if (rot < rotateBuff)  //回転しすぎた時///////////////////
             {
                 rotateBuff = rot;
                 rotateEnd = true;
            }//////////////////////////////////////////////////////////

         }/////////////////////////////////////////////////////////////////////////

         else if (rot < rotateBuff) //目標回転角よりも角度が大きいとき////
         {
             rotateEnd = false;
            rotateBuff -= rotateSpeed;        //回転させる
            if (rot > rotateBuff) //回転しすぎた時///////////////////
            {
                 rotateBuff = rot;
                 rotateEnd = true;
            }
         }//////////////////////////////////////////////////////////////////
         else
         {
             rotateEnd = true;
         }
         tf.localEulerAngles = new Vector3(0, rotateBuff, 0);   //回転角代入

    }
    #region 値受け渡し
    public bool GetRotateEnd()
    {
        return rotateEnd;
    }
    public void ResetRotate(int stageChangeCount, int maxStage)
    {
        rotateBuff =  (360 / (maxStage + 1)) * stageChangeCount; ;
    }
    #endregion
    public void StartStageRotation()
    {
        tf = GetComponent<Transform>();
        rotateBuff = tf.localEulerAngles.y;
    }

}
