using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�R���p�X��UI�Ǘ�
public class CompassScript : MonoBehaviour
{
    Transform tf;
    //�R���p�X���J�����̉�]�ɂ��Ă����Ȃ��悤�ɂ���֐�
    private void StopRot()
    {
        tf.eulerAngles = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        StopRot();
    }
}
