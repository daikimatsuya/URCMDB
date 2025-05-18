using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFishListScript : MonoBehaviour
{
    private List<AnglerfishScript> anglerfishList;
    private PlayerControllerScript pcs;

    public void AnglerFishListController(in bool isPause)
    {
        if (isPause)
        {
            return;
        }
        for (int i = 0; i < anglerfishList.Count; i++)
        {
            anglerfishList[i].AnglerfishController();
        }
    }
    public void StartAnglerFishList(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;
        anglerfishList = new List<AnglerfishScript>(FindObjectsOfType<AnglerfishScript>());
        for (int i = 0; i < anglerfishList.Count; i++)
        {
            anglerfishList[i].StartAnglerfish(pcs);
        }
    }
}
