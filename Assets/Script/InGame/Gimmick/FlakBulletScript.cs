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



    //�C�e�Ǘ�
    private void BulletController()
    {
        Move(); //�ړ��Ǘ�
        Delete();   //�폜�Ǘ�
    }
    //�ړ�
    public void Move()
    {
        rb.velocity = speed;    //���x���Z

        ms.Move(pos.position);  //�}�[�J�[�A�C�R����e�ۂɒǏ]������
    }
    //����
    public void Delete()
    {
        if (TimeCountScript.TimeCounter(ref deleteTime))    //�������Ԋm�F//////
        {
            ms.Delete();    //�}�[�J�[�A�C�R���폜
            Destroy(this.gameObject);   //�I�u�W�F�N�g�폜

        }///////////////////////////////////////////
    }
    //���x�󂯎��
    public void GetAcce(Vector3 acce)
    {
        speed = acce;   //�l���
    }
    //�}�[�J�[����
    private void CreateMarker()
    {
        GameObject _ = Instantiate(marker); //�}�[�J�[�A�C�R������
        ms = _.GetComponent<MarkerScript>();    //�X�N���v�g�擾
        ms.Move(pos.position);  //�Ǐ]������

    }
    public void StartFlakBullet()
    {
        rb = GetComponent<Rigidbody>();
        pos = GetComponent<Transform>();
        deleteTime = deleteTime * 60;

        CreateMarker();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartFlakBullet();
    }

    // Update is called once per frame
    void Update()
    {
        BulletController();
    }
}
