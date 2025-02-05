using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�|�[�Y���j���[UI�Ǘ�
public class PoseMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject keybord;
    [SerializeField] private GameObject controller;
    [SerializeField] private GameObject pose;

    public void StartPoseMenu(in bool isConect)
    {
        ViewPoseMenu(isConect);
    }
    public void ViewPoseMenu(in bool isConect)
    {
        if (isConect)
        {
            keybord.SetActive(false);   //�R���g���[���[���ڑ�����Ă�����L�[�{�[�h�pUI��false
        }
        else
        {
            controller.SetActive(false);    //�R���g���[���[���ڑ�����Ă��Ȃ�������R���g���[���[�pUI��false
        }
    }
    public void SetPoseActive(in bool flag)
    {
        keybord.SetActive(flag);
        controller.SetActive(flag);
        pose.SetActive(flag);
    }
}
