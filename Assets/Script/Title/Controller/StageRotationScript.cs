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
        Move(sss.GetStageCount());
    }
    //�X�e�[�W���ŉ�]
    private void Move(Vector2 stage)
    {
        float rot = (360 / (stage.y + 1)) * stage.x;

         if (rot > rotateBuff)
         {
             rotateEnd = false;
             sss.SetRotateEnd(rotateEnd);
             rotateBuff += rotateSpeed;
             if (rot < rotateBuff)
             {
                 rotateBuff = rot;
                 rotateEnd = true;
                 sss.SetRotateEnd(rotateEnd);
             }
         }
         else if (rot < rotateBuff)
         {
             rotateEnd = false;
             sss.SetRotateEnd(rotateEnd);
             rotateBuff -= rotateSpeed;
             if (rot > rotateBuff)
             {
                 rotateBuff = rot;
                 rotateEnd = true;
                 sss.SetRotateEnd(rotateEnd);
             }
         }
         else
         {
             rotateEnd = true;
             sss.SetRotateEnd(rotateEnd);
         }
         tf.localEulerAngles = new Vector3(0, rotateBuff, 0);

    }
    // Start is called before the first frame update
    void Start()
    {
        sss = GameObject.FindWithTag("TitleManager").GetComponent<StageSelectScript>();
        tf = GetComponent<Transform>();
        rotateBuff = tf.localEulerAngles.y;

    }

    // Update is called once per frame
    void Update()
    {
        StageSelectController();
    }
}
