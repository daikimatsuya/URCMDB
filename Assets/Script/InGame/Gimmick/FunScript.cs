using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���C��̉�]�Ǘ�
public class FunScript : MonoBehaviour
{
    private Transform tf;

    [SerializeField] float rotateSpeed;

    private Vector3 rotation;
    //�t�@������]������
    private void RotateFun()
    {
        rotation = new Vector3(rotation.x+rotateSpeed,rotation.y,rotation.z);   //��]
        tf.localEulerAngles = rotation; //�g�����X�t�H�[���ɑ��
    }

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rotation = tf.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        RotateFun();
    }
}
