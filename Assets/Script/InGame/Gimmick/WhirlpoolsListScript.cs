using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//‰Q’ªƒŠƒXƒgŠÇ—
public class WhirlpoolsListScript : MonoBehaviour
{
    private List<WhirlpoolsScript> whirlpoolsList;

    //ŠÇ—
    public void WhirlpoolsListController(in bool isPause)
    {
        if (isPause)
        {
            return;
        }
        for (int i = 0; i < whirlpoolsList.Count; i++)
        {
            whirlpoolsList[i].UpdateWhirlpools();
        }
    }

    //‰Šú‰»
    public void StartWhirlpoolsList()
    {
        whirlpoolsList = new List<WhirlpoolsScript>(FindObjectsOfType<WhirlpoolsScript>());
        for (int i = 0; i < whirlpoolsList.Count; i++)
        {
            whirlpoolsList[i].StartWhirlpools();
        }
    }
}
