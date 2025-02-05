using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�R���p�X�Ɏʂ��}�[�J�[�Ǘ�
public class MarkerCamera : MonoBehaviour
{
    Transform tf;

    private GameObject player;
    private Transform playerPos;

    [SerializeField] private float pos;

    //MakerCamera�Ǘ�
    private void MarkerCameraController()
    {
        SearchPlayer(); //�v���C���[���� 
        Move();            //�ړ�
    }
    //�J�����ړ�
    private void Move()
    {
        if(playerPos!=null)
        {
            tf.position = new Vector3(playerPos.position.x, pos, playerPos.position.z);                                    //�|�W�V�������
            tf.eulerAngles = new Vector3(tf.eulerAngles.x, player.transform.eulerAngles.y, tf.eulerAngles.z);   //�p�x���
        }
    }
    //�v���C���[�����m�F�Ǝ擾
    private void SearchPlayer()
    {
        if (playerPos == null)  //�v���C���[���擾�ł��Ȃ���////////////////////////////////////////////////
        {
            if (GameObject.FindWithTag("Player"))   //�v���C���[�I�u�W�F�N�g����/////
            {
                player = GameObject.FindWithTag("Player");           //�v���C���[�I�u�W�F�N�g�擾
                playerPos = player.GetComponent<Transform>();   //�R���|�[�l���g�擾

            }///////////////////////////////////////////////////////////////////////////////

        }////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MarkerCameraController();
    }
}
