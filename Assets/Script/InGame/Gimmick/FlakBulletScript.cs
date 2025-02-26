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
    private MarkerScript ms;

    [SerializeField] private float deleteTime;
    [SerializeField] private GameObject marker;

    //�ړ�
    public void Move(in bool isPose)
    {
        if(isPose)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        rb.velocity = speed;       //���x���Z
        ms.Move(pos.position);  //�}�[�J�[�A�C�R����e�ۂɒǏ]������
        ms.AdjustmentSize();
    }
    //����
    public void Delete()
    {
        ms.Delete();                        //�}�[�J�[�A�C�R���폜
        Destroy(this.gameObject);    //�I�u�W�F�N�g�폜
    }
    //�}�[�J�[����
    private void CreateMarker(in PlayerControllerScript pcs)
    {
        GameObject _ = Instantiate(marker);         //�}�[�J�[�A�C�R������
        ms = _.GetComponent<MarkerScript>();    //�X�N���v�g�擾
        ms.StartMarker(in pcs);
        ms.Move(pos.position);                              //�Ǐ]������

    }
    //������
    public void StartFlakBullet(Vector3 speed,in PlayerControllerScript pcs)
    {
        this.speed = speed;
        rb = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>();
        TimeCountScript.SetTime(ref deleteTime, deleteTime);
        CreateMarker(in pcs);
    }

    public bool GetDeleteFlag()
    {
        return TimeCountScript.TimeCounter(ref deleteTime);
    }
}
