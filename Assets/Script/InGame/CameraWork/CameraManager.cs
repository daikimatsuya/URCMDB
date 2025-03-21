using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�C���Q�[�����̃J�����Ǘ�
public class CameraManager : MonoBehaviour
{
    [SerializeField] private float explodeEffectTime;
    private int explodeEffectTimeBuff;
    [SerializeField] private GameObject movieCanvas;
    [SerializeField] private GameObject watarEffect;
    [SerializeField] private float drownPos;

    private PlayerCameraScript pcs;
    private GameObject mainCanvas;
    private MovieFade mf;
    private PlayerScript ps;
    private TargetScript ts;
    private PlayerControllerScript playerController;
  
    private bool isPlayerDead;
    private bool isTargetBreak;

    //�J�����Ǘ�
    public  void CameraController(in bool isPose)
    {
        ps = playerController.GetPlayer();
        mf.MovieFadeController();   //�J�����̃t�F�[�h

        //�v���C���[������������
        if (isPlayerDead ) 
        {
            ExplodeCameraController(in ps);  //�������̃J�����̓���
        }

        //�v���C���[�I�u�W�F�N�g���Ȃ�
        if (ps == null) 
        {
            isPlayerDead = true;   
            watarEffect.SetActive(false);   //�����̃G�t�F�N�g��~
            return;
        }

        SetWaterEffect();   //�����̃J�������o�Ǘ�

        //�t�F�[�h���o���I�������
        if (mf.GetEffectEnd())  
        {
            isPlayerDead = false;

            //�v���C���[������ł���Ƃ�
            if (ps.GetControll())  
            {
                pcs.FollowPlayerInShoot(in isPose, in ps);  //�v���C���[��Ǐ]
            }

            //�v���C���[�����ˑ�ɌŒ肳��Ă鎞
            else
            {
                pcs.FollowPlayerInSet(in ps);   //�v���C���[��Ǐ]
            }

        }

        //�t�F�[�h���o
        else
        {
            pcs.MovieCut(); //���[�r�[���̉��o
        }

        CanvasActive(mf.GetEffectEnd());    //���o����Canvas�؂�ւ�

    }
    //�������̃J����
    private void ExplodeCameraController(in PlayerScript ps)
    {
        //�^�[�Q�b�g������Ƃ�
        if (ts)
        {
            isTargetBreak = ts.GetBreak();  //�t���O�擾
        }

        //�ڕW���j�󂳂ꂽ�Ƃ�
        if (isTargetBreak)
        {
            pcs.ClearCamera();      //�N���A���̔����G�t�F�N�g��������
            return;
        }

        //�ڕW�ɂ���������
        if (ts.GetHit())
        {

            pcs.HitExplodeCamera(); //�^�[�Q�b�g�ɂ������������G�t�F�N�g��������
            return;
        }

        pcs.MissExplodeCamera(playerController.GetPlayerdeadPos());    //���ʂ̔����G�t�F�N�g��������
    }

    //���ɓ��������̉��o�Ǘ�
    private void SetWaterEffect()
    {
        if (pcs.GetPos().y <= drownPos)
        {
            //�w�肵�����W�̉��ɓ���Ɖ�ʂ����Ȃ�
            watarEffect.SetActive(true);
        }
        else
        {
            //�オ��Ɩ߂�
            watarEffect.SetActive(false);
        }
    }
    //�J�n���o����Q�[����ʂւ̃L�����o�X�̃I���I�t�Ǘ�
    private void CanvasActive(bool flag)
    {
        if (flag)
        {
            //�Q�[����ʃI��
            movieCanvas.SetActive(false);
            mainCanvas.SetActive(true);
        }
        else
        {
            //���o��ʃI��
            mainCanvas.SetActive(false);
            movieCanvas.SetActive(true);
        }
    }

    #region �l�󂯓n��
    public PlayerCameraScript GetPlayerCamera()
    {
        if (pcs == null)
        {
            pcs = GameObject.FindWithTag("GameCamera").GetComponent<PlayerCameraScript>();
        }
        return pcs;
    }
    public void SetTarget(in TargetScript target)
    {
        ts = target;
    }

    #endregion

    //����������
    public void AwakeCameraManager(in PlayerControllerScript player)
    {
        GameObject _ = GameObject.FindWithTag("GameCamera");
        pcs = _.GetComponent<PlayerCameraScript>();
        pcs.AwakePlayerCamera();
        mainCanvas = GameObject.FindWithTag("UICanvas");
        mf = GetComponent<MovieFade>();
        playerController = player;
    }
    //������
    public void StartCameraManager()
    {
        mf.SetShadeLevel(1);
        pcs.SetMF(mf);
        ps = playerController.GetPlayer();
        pcs.SetPlayer(ps.transform,ps);
        TimeCountScript.SetTime(ref explodeEffectTimeBuff, explodeEffectTime);
        mainCanvas.SetActive(false);
        watarEffect.SetActive(false);
    }

}
