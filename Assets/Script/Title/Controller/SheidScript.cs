using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�^�C�g���Q�[��������������Ƃ��̃~�T�C���������炭�铮���Ǘ�
public class SheidScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float limitPos;
    [SerializeField] private float resetPos;

    Transform tf;
    TitlegameScript ts;

    private Vector3 initialPos;

    private void SheidController()
    {
        if(ts.GetResetActionFlag()) //���Z�b�g�t���O���I���ɂȂ����瓮����///
        {
            Move();
        }/////////////////////////////////////////////////////////////////////////
    }
    private void Move()
    {
        tf.position = new Vector3(tf.position.x - moveSpeed, tf.position.y , tf.position.z);

        if(tf.position.x < resetPos && tf.position.x > resetPos - moveSpeed)    //�����W�܂Ői�񂾂�~�j�Q�[�������Z�b�g����////
        {
            ts.SetResetFlag(true);
        }//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if(tf.position.x < limitPos)    //�w�肵�����W�܂Ői�񂾂���W�ƃt���O�����Z�b�g����////
        {
            ResetAction();
        }//////////////////////////////////////////////////////////////////////////////////////////
    }
    private void ResetAction()
    {
        ts.SetResetActionFlag(false);   //���Z�b�g�p�t���O���I�t�ɂ���
        tf.position = initialPos;   //���W�������l�ɕύX
    }

    public void StartSheid()
    {
        tf = GetComponent<Transform>();
        ts = GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();
        initialPos = tf.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartSheid();
    }

    // Update is called once per frame
    void Update()
    {
        SheidController();
    }
}
