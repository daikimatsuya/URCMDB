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

    private bool isGet;

    private MarkerScript ms;
    CapsuleCollider  collider_;
    Transform tf;

    private GameManagerScript gm;
    //�Ǘ�
    private void SpeedUpRingController()
    {   
        ON();   //�N���Ǘ�
        Off();  //�I�t�Ǘ�
    }
    //����
    private void Off()
    {
        if (isGet)  //�擾���ꂽ��@�\������//////////
        {
            tf.localScale = new Vector3(0, 0, 0);   //�T�C�Y���O�ɂ���
            if(ms!=null)
            {
                ms.Delete();    //�}�[�J�[�폜
            }
        }//////////////////////////////////////////////
    }
    //�ĕ\��
    private void ON()
    {
        if (gm.IsPlayerDead()==true)    //�v���C���[������������@�\���N��
        {
            isGet = false;  //�擾�t���O������
            tf.localScale = new Vector3(1, ringSize, ringSize); //�T�C�Y������
            collider_.enabled = true;   //�R���C�_�[�I��
            if(ms==null)
            {
                CreateMarker(); //�}�[�J�[����
            }        
        }//////////////////////////////////////////////////////////////////////
    }
    //�}�[�J�[����
    private void CreateMarker()
    {
        GameObject _ = Instantiate(marker);
        ms = _.GetComponent<MarkerScript>();
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
    // Start is called before the first frame update
    void Start()
    {
        gm=GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        tf = GetComponent<Transform>();
        collider_=GetComponent<CapsuleCollider>();
        tf.localScale = new Vector3(1, ringSize, ringSize);

        isGet = false;
        CreateMarker();
    }

    // Update is called once per frame
    void Update()
    {
        SpeedUpRingController();  
    }
}
