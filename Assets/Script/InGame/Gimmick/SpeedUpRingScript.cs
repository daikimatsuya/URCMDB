using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�X�s�[�h�A�b�v�����O�Ǘ�
public class SpeedUpRingScript : MonoBehaviour
{
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float ringSize;
    [SerializeField] private float offsetTime;
    [SerializeField] private GameObject marker;
    [SerializeField] private GameObject particle;

    private bool isGet;

    private MarkerScript ms;
    CapsuleCollider  collider_;
    Transform tf;
    private PlayerControllerScript pcs;


    //����
    public void Off()
    {
        if (!isGet)  //�擾���ꂽ��@�\������//////////
        {
            ms.AdjustmentSize();
            ms.AdjustmentPos();
            return;
        }//////////////////////////////////////////////

        tf.localScale = new Vector3(0, 0, 0);   //�T�C�Y���O�ɂ���
        ms.SetActive(false);
        particle.SetActive(false);
    }
    //�ĕ\��
    public void ON()
    {
        isGet = false;                                                      //�擾�t���O������
        tf.localScale = new Vector3(1, ringSize, ringSize); //�T�C�Y������
        collider_.enabled = true;                                     //�R���C�_�[�I��
        ms.SetActive(true);
        particle.SetActive(true);
    }
    //�}�[�J�[����
    private void CreateMarker(in PlayerControllerScript pcs)
    {
        GameObject _ = Instantiate(marker);
        ms = _.GetComponent<MarkerScript>();
        _.transform.SetParent(this.transform);
        ms.StartMarker(in pcs, this.gameObject.transform);
        ms.transform.rotation=this.transform.rotation;
        ms.Move(tf.position);

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
        tf.localScale = new Vector3(1, ringSize, ringSize);

        isGet = false;
        CreateMarker(in pcs);
    }

}
