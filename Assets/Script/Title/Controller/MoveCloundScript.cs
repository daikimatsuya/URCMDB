using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����_�̃X�N���v�g
public class MoveCloundScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxHight;
    [SerializeField] private int minHight;
    [SerializeField] private bool isUp;

    Transform tf;

    private float posBuff;
    //�㉺�ɓ�����
    private void Move()
    {
        //�㉺�ړ����J��Ԃ�///////////
        if (isUp)
        {
            posBuff += moveSpeed;
            if (posBuff > maxHight)
            {
                posBuff = maxHight;
                isUp = false;
            }
        }
        else
        {
            posBuff -= moveSpeed;
            if (posBuff < -maxHight)
            {
                posBuff = -maxHight;
                isUp = true;
            }
        }
        /////////////////////////////////
        tf.localPosition = new Vector3(tf.localPosition.x, posBuff, tf.localPosition.z);
    }
    // Start is called before the first frame update
    public void StartMoveCloud()
    {
        tf = GetComponent<Transform>();
        posBuff = tf.localPosition.y;
    }
    void Start()
    {
        StartMoveCloud();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
