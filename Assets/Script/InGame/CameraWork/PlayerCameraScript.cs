using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Usefull;

//�C���Q�[���Ńv���C���[�ɂ������Ă���J�����Ǘ�
public class PlayerCameraScript : MonoBehaviour
{
    private Transform tf;
    private Vector3 cameraRot;



    private MovieCamera mc;
    private ExplodeCamera ec;
    private MovieFade mf;
    private RadialBlurController rbc;

    [SerializeField] private float cameraDeff;
    [SerializeField] private float rotSpeed;
    [SerializeField] private float maxRot;
    private float rot;
    float rotBuff;


    //�J���������ł���v���C���[�̌��ɒǏ]����
    public void FollowPlayerInShoot(in bool isPose,in PlayerScript ps)
    {
        if (ps != null)  //�v���C���[���Q�[�����ɑ��݂��Ă��鎞/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            tf.rotation = ps.GetTransform().rotation;   //�v���C���[�̉�]�p���擾
            Vector3 deff = Vector3.zero;        //������[��
            rotBuff = rot;                              //Buff��rot����

            //���͂ɂ�肸������Z
            if (!isPose)
            {
                //�L�[�{�[�h����//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    if (rot > -maxRot)
                    {
                        rotBuff -= rotSpeed;
                    }
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    if (rot < maxRot)
                    {
                        rotBuff += rotSpeed;
                    }
                }
                if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
                {
                    if (rot > 0)
                    {
                        rot -= rotSpeed;
                    }
                    if (rot < 0)
                    {
                        rot += rotSpeed;
                    }
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //�R���g���[���[����//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (Input.GetAxis("LeftStickX") != 0)
                {
                    rotBuff += rotSpeed * Input.GetAxis("LeftStickX");
                    if (rotBuff > maxRot)
                    {
                        rotBuff = maxRot;
                    }
                    if (rotBuff < -maxRot)
                    {
                        rotBuff = -maxRot;
                    }
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                if (!Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && Input.GetAxis("LeftStickX") == 0)
                {
                    if (rot < 0)
                    {
                        rotBuff += rotSpeed;
                        if (rotBuff >= 0)
                        {
                            rotBuff = 0;
                        }
                    }
                    if (rot > 0)
                    {
                        rotBuff -= rotSpeed;
                        if (rotBuff <= 0)
                        {
                            rotBuff = 0;
                        }
                    }
                }
            }
            rot = rotBuff;
    

            //////////////////////////


            deff = FollowPlayer(ps.GetTransform().eulerAngles, rot);                                                                                                       //�p�x�Ɛݒ肵���������炸����Z�o
            tf.position = new Vector3(ps.GetTransform().position.x - deff.x, ps.GetTransform().position.y - deff.y + 3, ps.GetTransform().position.z - deff.z); //�v���C���[�̍��W�ɂ�������Z���ăg�����X�t�H�[���ɑ��

        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    //�J���������ˑ�ɂ���v���C���[�̌��ɒǏ]����
    public void FollowPlayerInSet(in PlayerScript ps)
    {
        tf.rotation = ps.GetTransform().rotation;                                                                                                                               //�v���C���[�̉�]�p���擾
        Vector3 deff = Vector3.zero;
        deff = FollowPlayer(ps.GetTransform().eulerAngles,0);                                                                                                           //�p�x�Ɛݒ肵���������炸����Z�o
        tf.position = new Vector3(ps.GetTransform() .position.x - deff.x, ps.GetTransform().position.y - deff.y + 3, ps.GetTransform().position.z - deff.z);  //�v���C���[�̍��W�ɂ�������Z���ăg�����X�t�H�[���ɑ��
        rot = 0;
    }
    //�Ǐ]���̃J�����̈ʒu�o��
    private Vector3 FollowPlayer(Vector3 playerEulerAngles,float rot)
    {
        Vector3 deff = Vector3.zero;
        float buff = playerEulerAngles.y + rot;

        //���ʂ̈ʒu���Z�o
        deff.x = cameraDeff * (float)Math.Sin(ToRadianScript.ToRadian(ref buff));
        deff.z = cameraDeff * (float)Math.Cos(ToRadianScript.ToRadian(ref buff));
        ///////////////////

        //���������Ɛ��������̈ʒu���Z�o
        deff.x = deff.x * (float)Math.Cos(ToRadianScript.ToRadian(ref playerEulerAngles.x));
        deff.z = deff.z * (float)Math.Cos(ToRadianScript.ToRadian(ref playerEulerAngles.x));

        deff.y = (cameraDeff + 5) * (float)Math.Sin(ToRadianScript.ToRadian(ref playerEulerAngles.x)) * -1;
        //////////////////////////////////

        return deff;
    }

    //�X�e�[�W�J�n���̃��[�r�[�̃J�������[�N
    public void MovieCut()
    {
        mc.CameraController();  //�Q�[���J�n���̉��o�Ǘ�
        Fade();                          //�t�F�[�h�Ǘ�
    }
    //�v���C���[���^�[�Q�b�g�ȊO�Ŕ��������Ƃ��̃J�������[�N
    public void MissExplodeCamera(in Vector3 playerPos)
    {
        cameraRot = transform.localEulerAngles;                  //�g�����X�t�H�[���̒l��vector3�ֈړ�
        tf.position = ec.MissExplodeCamera(ref cameraRot,in playerPos);  //�����G�t�F�N�g
        tf.localEulerAngles = cameraRot;                              //�Z�o�����l���g�����X�t�H�[���ɑ��
    }
    //�v���C���[���^�[�Q�b�g�ɂԂ������Ƃ̃J�������[�N
    public void HitExplodeCamera()
    {
        cameraRot= transform.localEulerAngles;             //�g�����X�t�H�[���̒l��vector3�ֈړ�
        tf.position=ec.HitTargetCamera(ref cameraRot);  //�����G�t�F�N�g
        tf.localEulerAngles = cameraRot;                        //�Z�o�����l���g�����X�t�H�[���ɑ��
    }
    //�N���A���̃J����
    public void ClearCamera()
    {
        cameraRot = transform.localEulerAngles;          //�g�����X�t�H�[���̒l��vector3�ֈړ�
        tf.position = ec.ClearCamera(ref cameraRot);    //�N���A�G�t�F�N�g
        tf.localEulerAngles = cameraRot;                      //�Z�o�����l���g�����X�t�H�[���ɑ��
    }
    //�t�F�[�h�Ǘ�
    private void Fade()
    {
        if (mc.GetEnd())    //�Q�[���J�n���o�̏I���m�F//////
        {
            mf.SetShadeLevel(3);    //�t�F�[�h���x���ݒ�

        }/////////////////////////////////////////////////////////

        else //�Q�[���J�n���o��////////////////////////////////////////////////////////////////////////////////////////
        {
            if (mc.GetSkip())   //���o�̃X�L�b�v�m�F////////////////////////////////////////////////////
            {
                mf.SetShadeLevel(2);    //�t�F�[�h���x���ݒ�

                if (mf.GetIsShade())    //�t�F�[�h�I�u�W�F�N�g�̈ړ����I�������/////////
                {
                    mf.SetShadeLevel(3);    //�t�F�[�h���x���ݒ�
                }////////////////////////////////////////////////////////////////////////////

            }///////////////////////////////////////////////////////////////////////////////////////////

            else if (mc.GetFadeoutTime() > mc.GetMoveTime())    //�t�F�[�h�J�n�t���O�m�F////////
            {
                mf.SetShadeLevel(2);    //�t�F�[�h���x���ݒ�
            }/////////////////////////////////////////////////////////////////////////////////////////////
            else
            {
                mf.SetShadeLevel(1);    //�t�F�[�h���x���ݒ�
            }
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


    #region �l�󂯓n��
    //�v���C���[�̃g�����X�t�H�[���擾�p�v���C���[�X�N���v�g���擾
    public void SetPlayer(in Transform tf,in PlayerScript ps)
    {
        ps.SetTransform(tf);
    }
    //MovieFade�擾�p
    public void SetMF(in MovieFade mf)
    {
        this.mf = mf;
    }
    //�v���C���[�J�����̃|�W�V�����擾�p
    public Vector3 GetPos()
    {
        return tf.position;
    }
    public Transform GetTransform()
    {
        return tf;
    }
    #endregion

    public void AwakePlayerCamera()
    {
        tf = GetComponent<Transform>();
        mc = GetComponent<MovieCamera>();
        ec = GetComponent<ExplodeCamera>();
        rbc = GetComponent<RadialBlurController>();
    }

}
