using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            keybord.SetActive(false);
        }
        else
        {
            controller.SetActive(false);
        }
    }
    public void SetPoseActive(in bool flag)
    {
        keybord.SetActive(flag);
        controller.SetActive(flag);
        pose.SetActive(flag);
    }
}
