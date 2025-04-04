using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ステージの説明を表示する
public class StageVersionScript : MonoBehaviour
{
    [SerializeField] private GameObject[] stage;

    private StageSelectScript sss;

    //ステージ概要表示
    private void ViewVersion()
    {
       
        if(sss.GetStageCount() == 0)    //ステージ１の時/////////////////////////////////////////////////
        {
            stage[(int)sss.GetStageChangeCount().y].SetActive(false);   //最終ステージの説明を非表示
            stage[(int)sss.GetStageCount()+1].SetActive(false);            //ステージ２の説明を非表示
        }/////////////////////////////////////////////////////////////////////////////////////////////////////

        else if (sss.GetStageCount() == sss.GetStageChangeCount().y)    //最終ステージの時///////////////
        {
            stage[0].SetActive(false);                                          //ステージ１の説明を非表示
            stage[(int)sss.GetStageCount() -1].SetActive(false);    //一つ前のステージの説明を非表示
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////

        else   //それ以外のステージの時///////////////////////////////////////////////////////////////
        {
            stage[(int)sss.GetStageCount() + 1].SetActive(false);   //一つ後ろのステージの説明を非表示
            stage[(int)sss.GetStageCount() - 1].SetActive(false);    //一つ前のステージの説明を非表示
        }/////////////////////////////////////////////////////////////////////////////////////////////

        stage[(int)sss.GetStageCount()].SetActive(true);    //現在のステージの説明を表示
    }

    //すべてのステージ説明を消す
    private void SetFalfeMonitor()
    {
        for(int i=0;i<stage.Length;i++)
        {
            stage[i].SetActive(false);
        }
    }
    
    public void StartStageVersion()
    {
        sss = GameObject.FindWithTag("TitleManager").GetComponent<StageSelectScript>();
        SetFalfeMonitor();
        ViewVersion();
    }
    void Start()
    {
        StartStageVersion();
    }

    // Update is called once per frame
    void Update()
    {
        ViewVersion();
    }
}
