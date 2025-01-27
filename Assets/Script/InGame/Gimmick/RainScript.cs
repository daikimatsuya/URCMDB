using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�J�Ǘ�
public class RainScript : MonoBehaviour
{
    Transform cameraPos;
    Transform tf;

    [SerializeField] private float minDistance;

    //�ړ�������
    private void Move()
    {
        if(cameraPos == null)
        {
            return;
        }
        tf.position = new Vector3(cameraPos.position.x, cameraPos.position.y + minDistance, cameraPos.position.z);  //�ړ�
    }

    //�J�����̍��W�擾
    public void SetCameraTransform(in Transform cameraPos)
    {
         this.cameraPos = cameraPos;  //���W���
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
