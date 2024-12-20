using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageVersionScript : MonoBehaviour
{
    [SerializeField] private GameObject[] stage;

    private StageSelectScript sss;


    private void ViewVersion()
    {
       
        if(sss.GetStageCount() == 0)
        {
            stage[(int)sss.GetStageChangeCount().y].SetActive(false);
            stage[(int)sss.GetStageCount()+1].SetActive(false);
        }
        else if (sss.GetStageCount() == sss.GetStageChangeCount().y)
        {
            stage[0].SetActive(false);
            stage[(int)sss.GetStageCount() -1].SetActive(false);
        }
        else
        {
            stage[(int)sss.GetStageCount() + 1].SetActive(false);
            stage[(int)sss.GetStageCount() - 1].SetActive(false);
        }
        stage[(int)sss.GetStageCount()].SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        sss=GameObject.FindWithTag("TitleManager").GetComponent<StageSelectScript>();
        ViewVersion();
    }

    // Update is called once per frame
    void Update()
    {
        ViewVersion();
    }
}
