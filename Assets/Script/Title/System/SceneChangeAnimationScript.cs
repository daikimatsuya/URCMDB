using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�X�e�[�W�Z���N�g����C���Q�[���ւ̉��o�Ǘ�
public class SceneChangeAnimationScript : MonoBehaviour
{
    [SerializeField] private GameObject missile;
    [SerializeField] private GameObject pad;
    [SerializeField] private float targetRot;
    private float rotationSpeed;
    [SerializeField] private float rotationTime;
    private float rotationTimeBuff;
    [SerializeField] private float missileMovepeed;

    private float padRotBuff;
    private bool isFadeStart;
    private bool isEnd;

    //���ˑ���㉺������
    public void UpDown(in bool isDown)
    {
        isEnd=false;
        if (isDown)  //�㉺�ړ��t���O���I���̎�//////////////////////////////////////////////////////////////////////////////////////
        {
            if (!TimeCountScript.TimeCounter(ref rotationTimeBuff))
            {
                padRotBuff += rotationSpeed;    //��]�p�ɑ��x�𑫂�
                pad.transform.localEulerAngles = new Vector3(padRotBuff, pad.transform.localEulerAngles.y, pad.transform.localEulerAngles.z);   //�g�����X�t�H�[���ɑ��
            }
            else
            {
                isEnd = true;
            }
     
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////Z
        else   //�㉺�ړ��t���O���I�t�̎�
        {
            if (rotationTimeBuff < (int)(rotationTime * 60))
            {
                padRotBuff -= rotationSpeed;    //��]�p�ɑ��x�����Z
                pad.transform.localEulerAngles = new Vector3(padRotBuff, pad.transform.localEulerAngles.y, pad.transform.localEulerAngles.z);  //�g�����X�t�H�[���ɑ��

                rotationTimeBuff++;
            }
        }
    }
    //�t���O���Z�b�g
    private void ResetFlags()
    {
        isFadeStart = false;
    }
    #region �l�󂯓n��
    public void SetStartFadeFlag(bool flag)
    {
        isFadeStart = flag;
    }
    public bool GetIsFadeStartFlag()
    {
        return isFadeStart;
    }
    public bool GetEndDown()
    {
        return isEnd;
    }
    #endregion
    public void StartSceneChangeAnimation()
    {
        padRotBuff = pad.transform.localEulerAngles.x;
        rotationTimeBuff = (int)(rotationTime * 60);
        rotationSpeed = targetRot / rotationTimeBuff;
        isEnd = false;
        ResetFlags();
    }

}
