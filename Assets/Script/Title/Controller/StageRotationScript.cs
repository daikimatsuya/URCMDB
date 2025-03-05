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
        float rot = (360 / (maxStage + 1)) * stageChangeCount;    //選択ステージの角度算出

        //目標回転角よりも小さい
         if (rot > rotateBuff) 
         {
             rotateEnd = false;                    //回転終了フラグオフ
             rotateBuff += rotateSpeed;      //回転角増加

            //目標回転角到達
             if (rot <= rotateBuff)
             {
                 rotateBuff = rot;      //値代入
                 rotateEnd = true;    //回転終了フラグオン
            }

         }

         //目標回転角よりも大きい
         else if (rot < rotateBuff)
         {
             rotateEnd = false;                    //回転終了フラグオフ
            rotateBuff -= rotateSpeed;        //回転角減少

            //目標回転角到達
            if (rot >= rotateBuff)
            {
                 rotateBuff = rot;
                 rotateEnd = true;
            }
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
