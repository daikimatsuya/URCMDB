using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������I�u�W�F�N�g����]������
public class RollingScript : MonoBehaviour
{
    [SerializeField] private float rowSpeed;

    Transform tf;
    //��]�������W���g�����X�t�H�[���ɓ����
    private void Rolling()
    {
        tf.localEulerAngles=new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y,tf.localEulerAngles.z+rowSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
      tf=GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        Rolling();
    }
}
