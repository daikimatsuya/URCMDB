using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

//�^�C�g������
public class TitleScript : MonoBehaviour
{
    private TitlegameScript ts;
    private StageSelectScript sss;
    private SceneChangeAnimationScript scas;
    private SceneChangeMissleActionScript scmas;
    private TitleCamera tc;

    private bool isSceneChangeMode;
    private string stage;
    private bool isShoot;

    [SerializeField] private float betTime;

    public Light Light { get; set; }


    //�t���O�֘A����
    private void Shoot()
    {
        if (!tc.GetCanShot())
        {
            return;
        }

        stage=sss.GetStage();

        if (stage == "")    //�I������Ă���X�e�[�W���^�C�g����������^�C�g����ʂɖ߂�////
        {
            if(Input.GetKeyDown(KeyCode.Space) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
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
        if (Input.GetKeyDown(KeyCode.Space) || Usefull.GetTriggerScript.GetAxisDown("RightTrigger"))
        {
            isShoot = true; //�X�e�[�W�`�F���W���o�J�n
        }
        if (!sss.GetFadeEnd())
        {
            return;
        }
        ChangeStage();
    }
    //�^�C�g����ʂɖ߂�
    private void ResetTitle()
    {
        //�X�e�[�W�Z���N�g���[�h�p�̃t���O��������
        isSceneChangeMode=false;
        ts.SetResetFlag(true);
        /////////////////////////////////////////////
    }
    //�V�C��l�דI�ɕς���
    private void SetWeather()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Usefull.WebAPIScript.SetRain();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Usefull.WebAPIScript.SetSun();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Usefull.WebAPIScript.SetRial();
        }
    }
    //�V�[���`�F���W
    public void ChangeStage()
    {
        Usefull.GetTriggerScript.SetValue();
        SceneManager.LoadScene(stage);
    }

    #region �l�󂯓n��

    public bool GetIsSceneChangeModeFlag()
    {
        return isSceneChangeMode;
    }
    #endregion

    //�^�C�g���Ǘ�
    private void TitleController()
    {
        Usefull.GetTriggerScript.AxisUpdate();              //�g���K�[�̓��͏����X�V
        Usefull.GetControllerScript.SearchController();  //�R���g���[���[���ڑ�����Ă��邩���m�F

        SetWeather();                                                              //�V��𑀂�
        Shoot();                                                                      //�X�e�[�W�ɔ���
        sss.SelectController(tc.GetCanShot());                                    //�X�e�[�W�Z���N�g  
        scas.UpDown(in isSceneChangeMode);                          //���ˑ�̏㉺�Ǘ�
        scmas.Shoot(in isShoot);                                              //�v���C���[���ˊǗ�
        tc.CameraController(in isShoot);
    }

    //�R���|�[�l���g�擾
    private void GetComponets()
    {
        ts = GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();
        Light = GameObject.FindWithTag("Light").GetComponent<Light>();
        Light.color = Color.white;
        sss = GetComponent<StageSelectScript>();
        scas=GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();
        scmas=GameObject.FindWithTag("titlePlayerModel").GetComponent<SceneChangeMissleActionScript>();
        tc = GameObject.FindWithTag("MainCamera").GetComponent<TitleCamera>();
    }
    private void StartTitle()
    {
        Application.targetFrameRate = 60;
        GetComponets();
        sss.StartStageSelect();
        scas.StartSceneChangeAnimation();
        scmas.StartSceneChandeMissleAnimation();
        tc.StartTitleCamera();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartTitle();
    }

    // Update is called once per frame
    void Update()
    {
        TitleController();
    }
}
