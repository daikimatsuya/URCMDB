using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������I�u�W�F�N�g����]������
public class RollingScript : MonoBehaviour
{
    [SerializeField] private float rowSpeed;
    private float rowSpeedBuff;
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;

    Transform tf;
    //��]�������W���g�����X�t�H�[���ɓ����
    private void Rolling()
    {
        
        if(x)//x������]������
        {
            float buff = tf.localEulerAngles.x + rowSpeedBuff;
            Vector3 testBuff = new Vector3(buff, 0, 0);
            tf.localEulerAngles= testBuff;//�����̑�����Ȃ����o�O���Ă�
        }
        if(y)//y������]������
        {
            tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y + rowSpeedBuff, tf.localEulerAngles.z);
        }
        if (z)//z������]������
        {
            tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y, tf.localEulerAngles.z + rowSpeedBuff);
        }
      
    }
    //��]���x���O������Z�b�g
    public void SetRowSpeed(float rowSpeed)
    {
        this.rowSpeedBuff = rowSpeed;
    }
    //��]���x��Ԃ�
    public float GetRowSpeed()
    {
        return rowSpeed;
    }
    //��]���x�������l�Ƀ��Z�b�g
    public void ResetRowSpeed()
    {
        rowSpeedBuff = rowSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rowSpeedBuff = rowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Rolling();
    }
}
