using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���n�u�Ƃ��̉e���ŃQ�[���O�Őe�q�t��������Ƃ��ɃX�N���v�g����e�q�t������
public class PseudoParentingScript : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Vector3 fixRotate;
    [SerializeField] Vector3 fixPos;
    Transform tf;

    //�e�q�t������
    private void Parent()
    {
        if (!parent)    //�e�q�t���悪�ݒ肳��Ă��Ȃ�������return��Ԃ�////
        {
            return;
        }//////////////////////////////////////////////////////////////////////

        tf.transform.SetParent(parent); //�e�q�t��
        tf.localPosition = fixPos;  //�|�W�V�������C��
        tf.localEulerAngles += fixRotate;   //�p�x���C��
    }

    // Start is called before the first frame update
    private void Awake()
    {
        tf = GetComponent<Transform>();   
        Parent();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
