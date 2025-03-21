using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//r���[���̃V���[�g�J�b�g�J�ʂ�����Ƃ��̂��
public class LidStoneScript : MonoBehaviour
{
    [SerializeField] GameObject shortcut;
    [SerializeField] GameObject detour;

    //�v���C���[���Ԃ����Ă�����V���[�g�J�b�g�J�ʂ����ď�����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shortcut.SetActive(true);       //�V���[�g�J�b�g��\��
            detour.SetActive(false);         //��蓹��\��
            Destroy(this.gameObject);    //�폜
        }
    }

    //������
    public void StartLibStone()
    {
        shortcut.SetActive(false);
    }
}
