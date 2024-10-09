using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    private Transform cameraPos;
    private TitlegameScript ts;

    private bool cameraMoveEnd;
    private bool isStageSelect;
    private bool isSceneChangeMode;
    private bool sceneChangeFlag;
    private bool isPush;

    [SerializeField] private bool isCameraMove;
    [SerializeField] private float betTime;
    private int betBuff;


    private void TitleController()
    {
        isPush = false;
        SceneChange();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraStartMove();
        }
        //SceneCountDow();
    }
    private void CameraStartMove()
    {
        if (!isPush)
        {
            isCameraMove = true;
            isPush = true;
        }
    }
    private void SceneCountDow()
    {
        if(sceneChangeFlag)
        {
            if (betBuff <= 0)
            {
                SceneManager.LoadScene(stage[stageCount]);
            }
            betBuff--;
        }
    }//Žg‚í‚È‚³‚»‚¤
    private void SceneChange()
    {
        if (!isPush)
        {
            if (cameraMoveEnd && isCameraMove && rotateEnd)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (stageCount == 0)
                    {
                        isCameraMove = false;
                        stageCount = 1;
                        stageChangeCount = 1;
                        ts.SetResetFlag(true);
                    }
                    else
                    {
                        isSceneChangeMode = true;
                        betBuff = (int)(betTime * 60);
                    }
                    isPush = true;
                }
            }
        }
    }
    public bool GetStageSelectFlag()
    {
        if (cameraMoveEnd && isCameraMove)
        {
            isStageSelect = true;
        }
        else
        {
            isStageSelect = false;
        }
        return isStageSelect;
    }
    public bool GetIsStageSelect()
    {
        return isCameraMove;
    }
    public bool GetIsSceneChangeModeFlag()
    {
        return isSceneChangeMode;
    }
    public void SendMoveEnd(bool end)
    {
        cameraMoveEnd = end;
    }
    public void SetSceneChangeFlag(bool flag)
    {
        sceneChangeFlag = flag;
    }


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        ts=GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();

        TitleController();

    }

    // Update is called once per frame
    void Update()
    {
        TitleController();
    }
}
