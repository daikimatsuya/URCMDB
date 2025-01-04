using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    TitleScript ts;

    private float padRotBuff;
    private bool isShot;
    private bool isFadeStart;

    //���o�Ǘ�
    private void AnimationController()
    {
        UpDown();   //���ˑ䂪�㉺����
    }
    //���ˑ���㉺������
    private void UpDown()
    {
        if (ts.GetIsSceneChangeModeFlag())  //�㉺�ړ��t���O���I���̎�//////////////////////////////////////////////////////////////////////////////////////
        {
            if (TimeCountScript.TimeCounter(ref rotationTimeBuff))
            {
                padRotBuff += rotationSpeed;    //��]�p�ɑ��x�𑫂�
                pad.transform.localEulerAngles = new Vector3(padRotBuff, pad.transform.localEulerAngles.y, pad.transform.localEulerAngles.z);   //�g�����X�t�H�[���ɑ��
            }
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
        isShot=false;
        isFadeStart=false;
    }
    #region �l�󂯓n��
    public bool GetIsShotFlag()
    {
        return isShot;
    }
    public void SetStartFadeFlag(bool flag)
    {
        isFadeStart = flag;
    }
    public bool GetIsFadeStartFlag()
    {
        return isFadeStart;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        ts=GameObject.FindWithTag("TitleManager").GetComponent<TitleScript>();
        
        padRotBuff = pad.transform.localEulerAngles.x;
        rotationTimeBuff = (int)(rotationTime * 60);
        rotationSpeed = targetRot / rotationTimeBuff;


        ResetFlags();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationController();
    }
}
