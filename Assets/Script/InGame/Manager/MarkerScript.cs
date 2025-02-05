using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�A�C�e���Ƃ��G�Ƃ��̃}�[�J�[�Ǘ�
public class MarkerScript : MonoBehaviour
{
    Transform tf;

    [SerializeField] private float markerPosY;

    //�ړ�������
    public void Move(Vector3 pos)
    {
        if(tf != null)  //�g�����X�t�H�[�����擾����Ă���Ƃ�///////////////////////////
        {
            tf.position = new Vector3(pos.x, markerPosY, pos.z);    //�ړ�������
        }/////////////////////////////////////////////////////////////////////////////////

        else   //�g�����X�t�H�[�����擾�ł��Ă��Ȃ���
        {
            tf=GetComponent<Transform>();                               //�R���|�[�l���g�擾
            tf.position = new Vector3(pos.x, markerPosY, pos.z);    //�ړ�
        }
    }
    //����
    public void Delete()
    {
        Destroy(this.gameObject);
    }

}
