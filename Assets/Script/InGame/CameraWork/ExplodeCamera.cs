using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

public class ExplodeCamera : MonoBehaviour
{
    [SerializeField] private float directionX;
    [SerializeField] private float distance;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 hitCameraPos;
    [SerializeField] private Vector3 hitCameraRot;
    [SerializeField] private Vector3 clearCameraPos;
    [SerializeField] private Vector3 clearCameraRot;

    private Vector3 pos;
    //�v���C���[���ǂƂ��Ŕ��������Ƃ��̃J�����̓���
    public Vector3 MissExplodeCamera(ref Vector3 rotation,in Vector3 playerPos)
    {
        Rotation(rotation,in playerPos);                                                      //��]������
        rotation = new Vector3(directionX, rotation.y + rotateSpeed, 0);      //�K�v�Ȓl�������
        return pos;
    }
    //�^�[�Q�b�g�ɂԂ����Ĕ����������̃J��������
    public Vector3 HitTargetCamera(ref Vector3 rotation)
    {
        rotation = hitCameraRot;    //�p�x�Ɍ��߂��l���
        return hitCameraPos;
    }
    //�N���A���̃J����
    public Vector3 ClearCamera(ref Vector3 rotation)
    {
        rotation = clearCameraRot;    //�p�x�Ɍ��߂��l���
        return clearCameraPos;
    }

    //�������̃J��������
    private void Rotation(Vector3 rotation,in Vector3 playerPos)
    {
        //���ʂŒl���o��
        pos.x = -distance * (float)Math.Sin(ToRadianScript.ToRadian(ref rotation.y));
        pos.z = -distance * (float)Math.Cos(ToRadianScript.ToRadian(ref rotation.y));

        //���������Ɛ��������Œl���o��
        pos.x = pos.x * (float)Math.Cos(ToRadianScript.ToRadian(ref rotation.x));
        pos.z = pos.z * (float)Math.Cos(ToRadianScript.ToRadian(ref rotation.x));

        pos.y = -distance * (float)Math.Sin(ToRadianScript.ToRadian(ref rotation.x)) * -1;

        pos += playerPos;
    }

}
