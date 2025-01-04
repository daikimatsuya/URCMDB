using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

//���̂Ƃ���^�C�g���̃f�[�^�[�󂯓n���◠���̏����̂��̂����ɂ���
public class TitleScript : MonoBehaviour
{
    private TitlegameScript ts;

    private bool cameraMoveEnd;
    private bool isStageSelect;
    private bool isSceneChangeMode;
    private string stage;
    private bool isShoot;

    [SerializeField] private bool isCameraMove;
    [SerializeField] private float betTime;

    public Light Light { get; set; }

    //�^�C�g���Ǘ�
    private void TitleController()
    {
        Shoot();
    }
    //�t���O�֘A����
    private void Shoot()
    {

        if (!isCameraMove)  //�X�e�[�W�Z���N�g���[�h�ɂȂ��ĂȂ��Ƃ��Ƀ{�^���������ƃJ�������ړ�///
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isCameraMove = true;
            }
            return;
        }////////////////////////////////////////////////////////////////////////////////////////////////////

        if (!isStageSelect) //�J�����̈ړ����I���܂Ń��^�[��������///////
        {
            return;
        }//////////////////////////////////////////////////////////////////////

        if (stage == "")    //�I������Ă���X�e�[�W���^�C�g����������^�C�g����ʂɖ߂�////
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                ResetTitle();   //�^�C�g���ɖ߂�
                return;
            }
            isSceneChangeMode = false;
            return;
        }////////////////////////////////////////////////////////////////////////////////////////
        else
        {
            isSceneChangeMode = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShoot = true; //�X�e�[�W�`�F���W���o�J�n
        }
    }
    //�^�C�g����ʂɖ߂�
    private void ResetTitle()
    {
        //�X�e�[�W�Z���N�g���[�h�p�̃t���O��������
        isCameraMove = false;
        isStageSelect = false;
        isSceneChangeMode=false;
        ts.SetResetFlag(true);
        /////////////////////////////////////////////
    }
    //�V�[���`�F���W
    public void SceneChange()
    {
        SceneManager.LoadScene(stage);
    }

    #region �l�󂯓n���p�֐��Q
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

        Light = GameObject.FindWithTag("Light").GetComponent<Light>();
        Light.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        TitleController();
    }
}
