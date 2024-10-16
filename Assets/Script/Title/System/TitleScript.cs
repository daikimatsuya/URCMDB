using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

//今のところタイトルのデーター受け渡しや裏方の処理のちのち奇麗にする
public class TitleScript : MonoBehaviour
{
    private TitlegameScript ts;

    private bool cameraMoveEnd;
    private bool isStageSelect;
    private bool isSceneChangeMode;
    private bool isPush;
    private string stage;



    [SerializeField] private bool isCameraMove;
    [SerializeField] private float betTime;




    private void TitleController()
    {
        isPush = false;
        SceneChange();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraStartMove();
        }

    }
    private void CameraStartMove()
    {
        if (!isPush)
        {
            isCameraMove = true;
            isPush = true;
        }
    }
    private void SceneChange()
    {
        if (!isPush)
        {
            if (isStageSelect)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isSceneChangeMode = true;
                    isPush = true;
                }
            }
        }
    }
    //public void ResetTitle()
    //{
    //    ts.SetResetFlag(true);
    //    isStageSelect = false;
    //    isSceneChangeMode=false;
    //    isCameraMove=false;
    //    isPush = true;
    //}
    #region 値受け渡し用関数群
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
    public void SetCameraMove(bool flag)
    {
        isCameraMove = flag;
    }
    public bool GetIsSceneChangeModeFlag()
    {
        return isSceneChangeMode;
    }
    public void SendMoveEnd(bool end)
    {
        cameraMoveEnd = end;
    }

    public void SetStage(string stage)
    {
        this.stage = stage;
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        ts=GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();

        //TitleController();

    }

    // Update is called once per frame
    void Update()
    {
        TitleController();
    }
}
