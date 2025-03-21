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
        fades.Add(up.GetComponent<ActivationFadeScript>());
        fades.Add (down.GetComponent<ActivationFadeScript>());

        foreach(Transform children in delete.transform)
        {
            fades.Add(children.GetComponent<ActivationFadeScript>());
        }

        for (int i = 0; i < fades.Count; i++)
        {
            fades[i].StartActivationFade();
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

    public void SetStart()
    {
        for (int i = 0; i < fades.Count;i++)
        {
            fades[i].SetStart();
        }
    }
}
