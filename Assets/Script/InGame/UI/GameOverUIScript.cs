using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//�Q�[���I�[�o�[����UI�Ǘ�
public class GameOverUIScript : MonoBehaviour
{
    [SerializeField] private float retryPos;
    [SerializeField] private float backTitlePos;
    [SerializeField] private Transform cursorPos;
    [SerializeField] private TextMeshProUGUI targetHpTex;

    private GameObject target;
    private TargetScript targetScript;
    private int targetHp;

    //���g���C��I�������Ƃ��ɃJ�[�\�����ړ�
    public void MoveRetry()
    {
        cursorPos.localPosition = new Vector3(retryPos, cursorPos.localPosition.y, cursorPos.localPosition.z);  
    }
    //�^�C�g���ɖ߂����󂵂��Ƃ��ɃJ�[�\�����ړ�
    public void MoveBackTitle()
    {
        cursorPos.localPosition = new Vector3(backTitlePos, cursorPos.localPosition.y, cursorPos.localPosition.z);  
    }
    //�J�[�\���ʒu�����Z�b�g
    public void ResetPos()
    {
        cursorPos.localPosition = new Vector3(0, cursorPos.localPosition.y, cursorPos.localPosition.z);
    }
    #region �l�󂯓n��
    public float GetPos()
    {
        return cursorPos.localPosition.x;
    }
    public void SetTargetHp()
    {
        if (targetScript != null)
        {
            targetHp = (int)targetScript.GetHp();
        }
    }

    #endregion
    //�^�[�Q�b�g��HP��\��
    public void TargetHpUI()
    {
        if (target != null)
        {
            SetTargetHp();  //�^�[�Q�b�g�̗͎̑擾

            //�^�[�Q�b�g�̗͕̑\��///////////////////////
            if (targetHp > 0)  
            {
                targetHpTex.text = "Hp" + targetHp;
            }
            else
            {
                targetHpTex.text = "Clear";
            }
            //////////////////////////////////////////////
        }
        else   //�N���A�\��//////////////
        {
            targetHpTex.text = "Clear";
        }/////////////////////////////////

    }
    
    //������
    public void StartGameOverUI()
    {
        target = GameObject.FindWithTag("Target");
        targetScript = target.GetComponent<TargetScript>();
    }

}
