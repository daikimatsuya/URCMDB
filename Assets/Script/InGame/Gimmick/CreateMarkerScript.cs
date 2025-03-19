using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CreateMarkerScript : MonoBehaviour
{
    [SerializeField] private GameObject marker;
    [SerializeField] private float markerSize;

    private MarkerScript ms;

    //�}�[�J�[����
    public void CreateMarker(in Transform tf,in PlayerControllerScript pcs)
    {
        GameObject _ = Instantiate(marker);                     //����
        ms = _.GetComponent<MarkerScript>();                //�R���|�[�l���g�擾
        ms.StartMarker(in pcs, this.gameObject.transform);//�}�[�J�[������
        ms.Move(tf.position);                                            //�ʒu���
        _.transform.SetParent(this.transform);                   //�e�q�t��
    }

    //�}�[�J�[�̃T�C�Y�Ƃ���␳
    public void Adjustment()
    {
        ms.AdjustmentSize();
        ms.AdjustmentPos();
    }

    //�}�[�J�[���ړ�(y���W�͕ω������Ȃ�)
    public void Move(in Transform tf)
    {
        ms.Move(tf.position);
    }

    //�}�[�J�[������
    public void Delete()
    {
        ms.Delete();
    }

    //�}�[�J�[�̑傫�����Z�b�g
    public void SetMarkerSize()
    {
        ms.SetSize(markerSize);
    }

    //�I���I�t�؂�ւ�
    public void SetActive(in bool flag)
    {
        ms.SetActive(flag);
    }
}
