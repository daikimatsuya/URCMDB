using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�^�C�g���̃~�j�Q�[���̃X�e�[�W���X�N���[��������
public class miniGameScrollScript : MonoBehaviour
{
    [SerializeField] float scrollSpeed;

    Transform tf;
    TitlegameScript ts;

    private Vector3 initialPos;

    //�~�j�Q�[���̃X�N���[���Ǘ�
    private void ScrollController()
    {
        if (ts.GetMoveFlag())
        {
            Scroll();
        }

        if(ts.GetResetFlag())
        {
            ScrollReset();
        }
    }
    //�X�N���[��������
    private void Scroll()
    {
        tf.position = new Vector3(tf.position.x-scrollSpeed, tf.position.y, tf.position.z);
    }
    //�X�N���[���l���Z�b�g
    private void ScrollReset()
    {
        tf.position=initialPos;
    }
    //�����l�o�^
    public void SetInitialPos(Vector3 pos)
    {
        initialPos = pos;
    }

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        ts = GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();

        initialPos = ts.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollController();
    }
}
