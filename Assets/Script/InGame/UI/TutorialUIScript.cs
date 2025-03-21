using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIScript : MonoBehaviour
{
    private enum Tutorial: int
    {
        shot,
        controll,
        boost,
        quick,
        acce,
        pms,
        finish
    }

    public class EmphasisTransformElement
    {
        [SerializeField] public Vector3 pos;
        [SerializeField] public Vector3 rot;
        [SerializeField] public Vector3 scale;
        [SerializeField] public float time;
    }

    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] controller;
    [SerializeField] private GameObject tutorialCompletion;
    private TextMeshProUGUI completion;
    [SerializeField] private EmphasisTransformElement emphasis;
    private EmphasisTransformElement emphasisBuff;

    private TutorialScript ts;

    private bool conectController;

    //�`���[�g���A��UI�Ǘ�
    public void TutorialUIController(in PlayerScript ps,in bool isConect)
    {
        CheckController(in isConect);              //�R���g���[���[�̐ڑ����m�F
        SelectTutorial(in ps);                          //UI�p���X�V
        ShowUI(conectController);                 //UI�\��
        ShowCompletion(ts.GetResetFlag());  //�`���[�g���A���i�s�x��\��

        if (ts.GetResetFlag())  
        {
            ResetTutorial();    //���Z�b�g�t���O���I���ɂȂ����烊�Z�b�g������
        }
    }

    //�N������`���[�g���A����I��
    private void SelectTutorial(in PlayerScript ps)
    {
        switch (ts.GetTutorialNum())
        {
            case (int)Tutorial.shot:
                ts.ShotTutorial(in ps);                                         //���˃`���[�g���A�����s
                break;

            case (int)Tutorial.controll:
                ts.ControlleTutorial(in ps,conectController);         //����`���[�g���A���i�s
                break;

            case (int)Tutorial.boost:
                ts.BoostTutorial(in ps);                                      //�u�[�X�g�`���[�g���A���i�s
                break;

            case (int)Tutorial.quick:
                ts.QuickMoveTutorial(in ps, conectController);    //��������`���[�g���A���i�s
                break;

            case (int)Tutorial.acce:
                ts.AcceTutorial(in ps, conectController);             //�����`���[�g���A���i�s
                break;

            case (int)Tutorial.pms:
                ts.PMSTutorial(in ps);                                      //PMS�`���[�g���A���i�s
                break;

            case (int)Tutorial.finish:
                ts.ResetAll(in ps);                                           //�`���[�g���A�����I�������烊�Z�b�g
                break;
        }
    }

    //�`���[�g���A���B���x�\��
    private void ShowCompletion(in bool showFlag)
    {
        tutorialCompletion.SetActive(!showFlag);        //�i�s�x�̕\��
        completion.text = ts.GetCompletion() + "%";  //�i�s�x�̒l�ύX
    }

    private void ShowUI(in bool isConectController)
    {
        //�R���g���[���[�p
        if (isConectController) 
        {
            if (controller.Length == 0) 
            {
                return;     //�R���g���[���[�p�t�h�摜����������Ă��Ȃ������烊�^�[��������
            }
            if(controller.Length <=ts.GetTutorialNum()) 
            {
                //�`���[�g���A���p�t�h�摜������Ȃ�������͈͓��̍Ō��\��
                controller[controller.Length-1].SetActive(true);                        //�Ō�̂��\��
                controller[controller.Length-2].SetActive(false);                       //�Ō�̈�O���\��
                return;
            }
            if(ts.GetTutorialNum() != 0) 
            {
                controller[ts.GetTutorialNum() - 1].SetActive(false); //���݂̈�O�̂t�h���\���ɂ���
            }
            controller[ts.GetTutorialNum()].SetActive(true);    //���݂̂t�h��\������
        }

        //�L�[�{�[�h�p
        else
        {
            if (keyboard.Length == 0)   
            {
                return;//�L�[�{�[�h�p�t�h�摜����������Ă��Ȃ������烊�^�[��������/////
            }
            if (keyboard.Length <= ts.GetTutorialNum()) 
            {
                //�`���[�g���A���p�t�h�摜������Ȃ�������͈͓��̍Ō��\��
                keyboard[keyboard.Length - 1].SetActive(true);                          //�Ō�̂��\��
                keyboard[keyboard.Length - 2].SetActive(false);                         //�Ō�̈�O���\��
                return;
            }
            if (ts.GetTutorialNum() != 0)   
            {
                keyboard[ts.GetTutorialNum() - 1].SetActive(false); //���݂̈�O�̂t�h���\���ɂ���
            }
            keyboard[ts.GetTutorialNum()].SetActive(true);    //���݂̂t�h��\������
        }
    }

    //�R���g���[���[�̐ڑ����`�F�b�N
    private void CheckController(in bool isConect)
    {
        bool buff = conectController;
        conectController = isConect; //�R���g���[���[�̐ڑ����擾

        if (buff != conectController) 
        {   
            ResetTutorial();     //�����ꂽ�u�ԂƐڑ������u�Ԃɕ\���t�h�����Z�b�g����
        }
    }
    //�`���[�g���A���\�������Z�b�g������
    private void ResetTutorial()
    {
        //��������Ă���t�h�̐���\������
        for (int i = 0; i < keyboard.Length; i++)
        {
            keyboard[i].SetActive(false);
        }
        for (int i = 0; i < controller.Length; i++)
        {
            controller[i].SetActive(false);
        }
    }

    //����������
    public void AwakeTutorialUI()
    {
        ResetTutorial();
        ts = GameObject.FindWithTag("GameController").GetComponent<TutorialScript>();
        completion = tutorialCompletion.GetComponent<TextMeshProUGUI>();
        conectController = Usefull.GetControllerScript.GetIsConectic();
    }

}
