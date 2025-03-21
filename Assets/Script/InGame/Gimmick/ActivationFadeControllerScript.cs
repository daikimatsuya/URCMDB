using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのフェード管理
public class ActivationFadeControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject down;
    [SerializeField] private GameObject delete;

    private List<ActivationFadeScript> fades=new List<ActivationFadeScript>();

    //初期化
    public void StartActivationFadeController()
    {
        fades.Add(up.GetComponent<ActivationFadeScript>());               //upFade取得
        fades.Add (down.GetComponent<ActivationFadeScript>());          //downFade取得

        foreach(Transform children in delete.transform)                           //deleteFade群取得
        {
            fades.Add(children.GetComponent<ActivationFadeScript>());
        }

        for (int i = 0; i < fades.Count; i++)
        {
            fades[i].StartActivationFade();                              //Fade群初期化
        }
    }

    //フェード管理
    public void ActivationFadeController()
    {
        for (int i = 0; i < fades.Count;)
        {
            if (fades[i] != null)
            {
                fades[i].ActivationFadeController();
                i++;
            }
            else
            {
                fades.RemoveAt(i);
            }
        }
    }

    //演出開始フラグセット
    public void SetStart()
    {
        for (int i = 0; i < fades.Count;i++)
        {
            fades[i].SetStart();
        }
    }
}
