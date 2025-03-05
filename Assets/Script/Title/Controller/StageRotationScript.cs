using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

//�X�e�[�W�Z���N�g��ʂŃX�e�[�W����]������
public class StageRotationScript : MonoBehaviour
{
    Transform tf;

    private float rotateBuff;
    private bool rotateEnd;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float baseRot;

    //�X�e�[�W���ŉ�]
    public void Move(int stageChangeCount,int maxStage)
    {
        float rot = (360 / (maxStage + 1)) * stageChangeCount;    //360�x���X�e�[�W���ŕ���

         if (rot > rotateBuff)  //�ڕW��]�p�����p�x���������Ƃ�///////////////
         {
             rotateEnd = false;
             rotateBuff += rotateSpeed;      //��]������
             if (rot < rotateBuff)  //��]����������///////////////////
             {
                 rotateBuff = rot;
                 rotateEnd = true;
            }//////////////////////////////////////////////////////////

         }/////////////////////////////////////////////////////////////////////////

         else if (rot < rotateBuff) //�ڕW��]�p�����p�x���傫���Ƃ�////
         {
             rotateEnd = false;
            rotateBuff -= rotateSpeed;        //��]������
            if (rot > rotateBuff) //��]����������///////////////////
            {
                 rotateBuff = rot;
                 rotateEnd = true;
            }
         }//////////////////////////////////////////////////////////////////
         else
         {
             rotateEnd = true;
         }
         tf.localEulerAngles = new Vector3(0, rotateBuff, 0);   //��]�p���

    }
    #region �l�󂯓n��
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
