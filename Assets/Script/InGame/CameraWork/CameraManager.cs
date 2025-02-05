using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�C���Q�[�����̃J�����Ǘ�
public class CameraManager : MonoBehaviour
{
    [SerializeField] private float explodeEffectTime;
    private int explodeEffectTimeBuff;
    [SerializeField] private float explodeFadeTime;
    private int explodeFadeTimeBuff;
    [SerializeField] private GameObject movieCanvas;
    [SerializeField] private GameObject watarEffect;
    [SerializeField] private float drownPos;

    private PlayerCameraScript pcs;
    private GameObject mainCanvas;
    private MovieFade mf;
    private PlayerScript ps;
    private TargetScript ts;
  
    private bool isExplodeEffectFade;
    private bool isPlayerDead;
    private bool isTargetBreak;

    //�J�����Ǘ�
    public  void CameraController(in bool isPose)
    {

        mf.MovieFadeController();   //�J�����̃t�F�[�h

        if (isPlayerDead )  //�v���C���[������������////////////////
        {
            ExplodeCameraController();  //�������̃J�����̓���
        }//////////////////////////////////////////////////////////////


        if (ps == null) //�v���C���[�I�u�W�F�N�g���Ȃ���///////
        {
            isPlayerDead = true;   
            watarEffect.SetActive(false);   //�����̃G�t�F�N�g��~
        }/////////////////////////////////////////////////////////////

        else   //�v���C���[�I�u�W�F�N�g������Ƃ�//////////////////////////////////////////////////////
        {
            SetWaterEffect();   //�����̃J�������o�Ǘ�
            if (mf.GetEffectEnd())  //�t�F�[�h���o���I�������////////////////
            {
                isPlayerDead= false;

                if (ps.GetControll())   //�v���C���[������ł���Ƃ�////
                {
                    
                    pcs.FollowPlayerInShoot(in isPose);  //�v���C���[��Ǐ]
                }/////////////////////////////////////////////////////////////

                else   //�v���C���[�����ˑ�ɌŒ肳��Ă鎞///
                {
                    pcs.FollowPlayerInSet();
                }////////////////////////////////////////////////

            }///////////////////////////////////////////////////////////////////////

            else   //�t�F�[�h���o/////////////
            {
                pcs.MovieCut(); //���[�r�[���̉��o
            }/////////////////////////////////

            CanvasActive(mf.GetEffectEnd());    //���o����Canvas�؂�ւ�
        }
    }
    //�������̃J����
    private void ExplodeCameraController()
    {
        if (ts)
        {
            isTargetBreak = ts.GetBreak();
        }
        if (isTargetBreak) //�ڕW���j�󂳂ꂽ�Ƃ�////////////////////
        {
            pcs.ClearCamera();
            return;
        }///////////////////////////////////////////////////

        if (ts.GetHit()) //�ڕW�ɂ���������/////////////
        {

            pcs.HitExplodeCamera();
            return;
        }///////////////////////////////////////////////
        pcs.MissExplodeCamera();    
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

    //�v���C���[�擾�p
    public void SetPlayer(in PlayerScript player)
    {
        if (pcs == null)
        {
            pcs = GameObject.FindWithTag("GameCamera").GetComponent<PlayerCameraScript>();
        }

        this.ps = player;
        pcs.SetPlayer(this.ps.transform,this.ps);
    }

    #endregion
    //������������ĂȂ��Ƃ��ɑ��̃X�N���v�g����Ăяo���ꂽ�Ƃ��ɏ���������
    public void StartCameraManager()
    {
        GameObject _ = GameObject.FindWithTag("GameCamera");
        pcs = _.GetComponent<PlayerCameraScript>();
        mf = GetComponent<MovieFade>();
        mf.SetShadeLevel(1);
        pcs.SetMF(mf);
        //pcs.SetMoviewFade(mf);
        TimeCountScript.SetTime(ref explodeEffectTimeBuff, explodeEffectTime);
        mainCanvas = GameObject.FindWithTag("UICanvas");
        mainCanvas.SetActive(false);
        watarEffect.SetActive(false);
    }

}
