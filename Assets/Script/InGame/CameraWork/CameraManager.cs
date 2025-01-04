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
    private GameManagerScript gm;
    private MovieFade mf;
    private PlayerScript player;
  
    private bool isExplodeEffectFade;
    private bool isPlayerDead;

    //�J�����Ǘ�
    public  void CameraController()
    {
        mf.MovieFadeController();   //�J�����̃t�F�[�h

        if (isPlayerDead )  //�v���C���[������������////////////////
        {
            ExplodeCameraController();  //�������̃J�����̓���
        }//////////////////////////////////////////////////////////////


        if (player == null) //�v���C���[�I�u�W�F�N�g���Ȃ���///////
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

                if (player.GetControll())   //�v���C���[������ł���Ƃ�////
                {
                    pcs.FollowPlayerInShoot();  //�v���C���[��Ǐ]
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
        if(gm.GetIsHitTarget()) //�ڕW�ɂ���������/////////////
        {
            if (gm.GetTargetDead()) //�ڕW���j�󂳂ꂽ�Ƃ�/
            {
                pcs.ClearCamera();
                return;
            }///////////////////////////////////////////////////
            pcs.HitExplodeCamera();
            return;
        }////////////////////////////////////////////////////////
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
        return pcs;
    }

    //�v���C���[�擾�p
    public void SetPlayer(PlayerScript player)
    {
        if (pcs == null)
        {
            pcs = GameObject.FindWithTag("GameCamera").GetComponent<PlayerCameraScript>();
        }

        this.player = player;
        pcs.SetPlayer(this.player.transform,this.player);
    }

    #endregion
    //������������ĂȂ��Ƃ��ɑ��̃X�N���v�g����Ăяo���ꂽ�Ƃ��ɏ���������
    private void InitialSet()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
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
    // Start is called before the first frame update
    void Start()
    {

        InitialSet();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
