using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�C�䂪��΂����e�Ǘ�
public class FlakBulletScript : MonoBehaviour
{
    Rigidbody rb;
    Transform pos;

    private Vector3 speed;
    private CreateMarkerScript cms;

    [SerializeField] private float deleteTime;

    //�ړ�
    public void Move(in bool isPause)
    {
        if(isPause)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = speed;       //���x���Z
        cms.Move(in pos);         //�}�[�J�[�ړ�
        cms.Adjustment();        //�}�[�J�[�T�C�Y�␳
    }
    //����
    public void Delete()
    {
        cms.Delete();                       //�}�[�J�[�A�C�R���폜
        Destroy(this.gameObject);    //�I�u�W�F�N�g�폜
    }

    //������
    public void StartFlakBullet(Vector3 speed,in PlayerControllerScript pcs)
    {
        this.speed = speed;
        rb = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>();
        cms=GetComponent<CreateMarkerScript>();

        TimeCountScript.SetTime(ref deleteTime, deleteTime);
        cms.CreateMarker(in pos, in pcs);
        cms.SetMarkerSize();
    }

    public bool GetDeleteFlag()
    {
        return TimeCountScript.TimeCounter(ref deleteTime);
    }
}
