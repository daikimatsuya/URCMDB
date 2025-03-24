using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Usefull;

//�`���[�g���A���pUI�\���Ǘ�
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

    [System.Serializable]
    public class EmphasisTransformElement
    {
        [SerializeField] public Vector3 pos;
        [SerializeField] public Vector3 rot;
        [SerializeField] public Vector3 scale;
        [SerializeField] public float time;
    }

    public class DoubleVector3
    {
        public double x;
        public double y;
        public double z;
    }

    [SerializeField] private GameObject[] keyboard;
    [SerializeField] private GameObject[] controller;
    [SerializeField] private GameObject tutorialCompletion;
    private TextMeshProUGUI completion;
    [SerializeField] private EmphasisTransformElement emphasis;
    private EmphasisTransformElement emphasisBuff;
    [SerializeField] private RectTransform canvasTransform;
    private DoubleVector3 initialPos;
    private DoubleVector3 initialRot;
    private DoubleVector3 initialScale;

    private TutorialScript ts;

    private bool conectController;

    //�`���[�g���A��UI�Ǘ�
    public void TutorialUIController(in PlayerScript ps,in bool isConect)
    {
        CheckController(in isConect);              //�R���g���[���[�̐ڑ����m�F
        SelectTutorial(in ps);                          //UI�p���X�V
        SetEmphasis();
        EmphasisTransition();
        ShowUI(conectController);                 //UI�\��
        ShowCompletion(ts.GetResetFlag());  //�`���[�g���A���i�s�x��\��


        if (ts.GetResetFlag())  
        {
            ResetTutorial();    //���Z�b�g�t���O���I���ɂȂ����烊�Z�b�g������
        }
    }

    //�����\������ʏ�\���ւ̑J��
    private void EmphasisTransition()
    {
        if(TimeCountScript.TimeCounter(ref emphasisBuff.time))
        {
            return;
        }
        float t = emphasisBuff.time / emphasis.time;
        t = 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));


        canvasTransform.position = initialPos + new Vector3(emphasisBuff.pos.x * t, emphasisBuff.pos.y * t, emphasisBuff.pos.z * t);
        canvasTransform.localEulerAngles = initialRot + new Vector3(emphasisBuff.rot.x * t, emphasisBuff.rot.y * t, emphasisBuff.rot.z * t);
        canvasTransform.localScale = initialScale + new Vector3(emphasisBuff.scale.x * t, emphasisBuff.scale.y * t, emphasisBuff.scale.z * t);
    }

    //�����\���̒l������
    private void SetEmphasis()
    {
        if (!ts.GetNextSwitch())
        {
            return;
        }
        SetEmphasisTransformElement(emphasisBuff, emphasis);
        ts.SetNextSwitch(false);     
    }

    private void SetEmphasisTransformElement(EmphasisTransformElement buff,EmphasisTransformElement emphasis)
    {
        buff.pos=emphasis.pos;
        buff.rot=emphasis.rot;
        buff.scale=emphasis.scale;
        buff.time=emphasis.time;
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

    //UI��\��
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


        initialPos = new Vector3 (canvasTransform.position.x, canvasTransform.position.y, canvasTransform.position.z);
        initialRot = new Vector3(canvasTransform.localEulerAngles.x, canvasTransform.localEulerAngles.y, canvasTransform.localEulerAngles.z);
        initialScale = new Vector3(canvasTransform.localScale.x, canvasTransform.localScale.y, canvasTransform.localScale.z);

        emphasisBuff = new EmphasisTransformElement();


        emphasis.pos = emphasis.pos - initialPos;
        emphasis.rot = emphasis.rot - initialRot;
        emphasis.scale = emphasis.scale - initialScale;

        emphasis.pos = new Vector3(emphasis.pos.x / emphasis.time, emphasis.pos.y / emphasis.time, emphasis.pos.z / emphasis.time);
        emphasis.rot = new Vector3(emphasis.rot.x / emphasis.time, emphasis.rot.y / emphasis.time, emphasis.rot.z / emphasis.time);
        emphasis.scale = new Vector3(emphasis.scale.x / emphasis.time, emphasis.scale.y / emphasis.time, emphasis.scale.z / emphasis.time);

        SetEmphasis();
    }

}
