using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ポーズメニューUI管理
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
            keybord.SetActive(false);   //コントローラーが接続されていたらキーボード用UIをfalse
        }
        else
        {
            controller.SetActive(false);    //コントローラーが接続されていなかったらコントローラー用UIをfalse
        }
    }
    public void SetPoseActive(in bool flag)
    {
        keybord.SetActive(flag);
        controller.SetActive(flag);
        pose.SetActive(flag);
    }
}
