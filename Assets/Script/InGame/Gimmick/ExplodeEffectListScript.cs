using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffectListScript : MonoBehaviour
{
    private List<ExplodeEffectScript> explodeEffectList;
    private bool playerExplodeFlag;
    private Vector3 playerPos;

    public void ExplodeEffectListController(in PlayerControllerScript pcs)
    {
        CreatePlayerExplode(in pcs);
        for(int i = 0; i < explodeEffectList.Count; i++)
        {
            explodeEffectList[i].SizeUp();
            explodeEffectList[i].Rotation();
            explodeEffectList[i].Dissolve();
        }
    }
    private void CreatePlayerExplode(in PlayerControllerScript pcs)
    {
        if (pcs.GetPlayer())
        {
            playerExplodeFlag = true;
            playerPos=pcs.GetPlayer().GetTransform().position;
        }
        else if(playerExplodeFlag) 
        {
            playerExplodeFlag=false;
            ExplodeEffectScript _ = pcs.CreateExplodeEffect(playerPos).GetComponent<ExplodeEffectScript>();
            _.StartExplodeEffect();
            explodeEffectList.Add(_);
        }

    }
    
    public void AwakeExplodeEffectList()
    {
        explodeEffectList = new List<ExplodeEffectScript>(FindObjectsOfType<ExplodeEffectScript>());
    }
}
