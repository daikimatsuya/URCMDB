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
    private bool isShoot;


    [SerializeField] private bool isCameraMove;
    [SerializeField] private float betTime;



    //タイトル管理
    private void TitleController()
    {
        Shoot();
    }
    //フラグ関連処理
    private void Shoot()
    {

        if (!isCameraMove)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isCameraMove = true;
            }
            return;
        }

        if (!isStageSelect)
        {
            return;
        }

        if (stage == "")
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ResetTitle();
                return;
            }
            isSceneChangeMode = false;
            return;
        }
        else
        {
            isSceneChangeMode = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShoot = true;
        }
    }
    //タイトル画面に戻す
    private void ResetTitle()
    {
        isCameraMove = false;
        isStageSelect = false;
        isSceneChangeMode=false;

        ts.SetResetFlag(true);
    }
    //シーンチェンジ
    public void SceneChange()
    {
        SceneManager.LoadScene(stage);
    }

    #region 値受け渡し用関数群
    public bool GetShootFlag()
    {
        return isShoot;
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
