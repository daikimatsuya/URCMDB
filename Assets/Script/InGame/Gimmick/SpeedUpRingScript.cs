using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�X�s�[�h�A�b�v�����O�Ǘ�
public class SpeedUpRingScript : MonoBehaviour
{
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float ringSize;
    [SerializeField] private float offsetTime;
    [SerializeField] private GameObject particle;

    private bool isGet;

    private CreateMarkerScript cms;

    CapsuleCollider  collider_;
    Transform tf;
    private PlayerControllerScript pcs;


    //����
    public void Off()
    {
        if (!isGet) //�擾����Ă��Ȃ���
        {
            cms.Adjustment();
            return;
        }

        tf.localScale = new Vector3(0, 0, 0);   //�T�C�Y���O�ɂ���
        cms.SetActive(false);
        particle.SetActive(false);
    }

    //�ĕ\��
    public void ON()
    {
        isGet = false;                                                      //�擾�t���O������
        tf.localScale = new Vector3(1, ringSize, ringSize); //�T�C�Y������
        collider_.enabled = true;                                     //�R���C�_�[�I��
        cms.SetActive(true);
        particle.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isGet = true;
            collider_.enabled = false;
        }
    }
    //������
    public void StartSpeedUpRing(in PlayerControllerScript pcs)
    {
        tf = GetComponent<Transform>();
        collider_ = GetComponent<CapsuleCollider>();
        cms=GetComponent<CreateMarkerScript>();
        tf.localScale = new Vector3(1, ringSize, ringSize);

        isGet = false;
        cms.CreateMarker(in tf,in pcs);
    }

}
