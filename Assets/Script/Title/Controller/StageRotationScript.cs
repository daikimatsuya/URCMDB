using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�X�e�[�W�Z���N�g��ʂŃX�e�[�W����]������
public class StageRotationScript : MonoBehaviour
{
    StageSelectScript sss;
    Transform tf;

    private float rotateBuff;
    private bool rotateEnd;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float baseRot;

    //�X�e�[�W�̉�]�Ǘ�
    private void StageSelectController()
    {
        Move(sss.GetStageChangeCount());    //�X�e�[�W����]������
    }
    //�X�e�[�W���ŉ�]
    private void Move(Vector2 stage)
    {
        float rot = (360 / (stage.y + 1)) * stage.x;    //360�x���X�e�[�W���ŕ���

         if (rot > rotateBuff)  //�ڕW��]�p�����p�x���������Ƃ�///////////////
         {
             rotateEnd = false;
             sss.SetRotateEnd(rotateEnd);   //��]�t���O����
             rotateBuff += rotateSpeed;      //��]������
             if (rot < rotateBuff)  //��]����������///////////////////
             {
                 rotateBuff = rot;
                 rotateEnd = true;
                 sss.SetRotateEnd(rotateEnd);   //��]�t���O����
            }//////////////////////////////////////////////////////////

         }/////////////////////////////////////////////////////////////////////////

         else if (rot < rotateBuff) //�ڕW��]�p�����p�x���傫���Ƃ�////
         {
             rotateEnd = false;
             sss.SetRotateEnd(rotateEnd);   //��]�t���O����
            rotateBuff -= rotateSpeed;        //��]������
            if (rot > rotateBuff) //��]����������///////////////////
            {
                 rotateBuff = rot;
                 rotateEnd = true;
                 sss.SetRotateEnd(rotateEnd);  //��]�t���O����
            }
         }//////////////////////////////////////////////////////////////////
         else
         {
             rotateEnd = true;
             sss.SetRotateEnd(rotateEnd);
         }
         tf.localEulerAngles = new Vector3(0, rotateBuff, 0);   //��]�p���

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
