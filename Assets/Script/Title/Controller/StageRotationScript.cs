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
        float rot = (360 / (maxStage + 1)) * stageChangeCount;    //�I���X�e�[�W�̊p�x�Z�o

        //�ڕW��]�p����������
         if (rot > rotateBuff) 
         {
             rotateEnd = false;                    //��]�I���t���O�I�t
             rotateBuff += rotateSpeed;      //��]�p����

            //�ڕW��]�p���B
             if (rot <= rotateBuff)
             {
                 rotateBuff = rot;      //�l���
                 rotateEnd = true;    //��]�I���t���O�I��
            }

         }

         //�ڕW��]�p�����傫��
         else if (rot < rotateBuff)
         {
             rotateEnd = false;                    //��]�I���t���O�I�t
            rotateBuff -= rotateSpeed;        //��]�p����

            //�ڕW��]�p���B
            if (rot >= rotateBuff)
            {
                 rotateBuff = rot;
                 rotateEnd = true;
            }
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
