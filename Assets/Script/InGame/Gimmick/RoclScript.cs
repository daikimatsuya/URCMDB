using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//���Ƃ�����Ǘ�
public class RoclScript : MonoBehaviour
{
    Rigidbody rb;
    Transform tf;

    float breakArea;
    float fallSpeed;

    //�Ǘ�
    private void RockController()
    {
        if (tf.position.y < breakArea)  //�ݒ�Y���W�����������j�󂷂�///
        {
            BreakRock();
        }//////////////////////////////////////////////////////////////////////

        Fall(); //����
    }
    //��������
    private void Fall()
    {
        rb.velocity = Vector3.zero;
        rb.velocity = new Vector3(0.0f, -fallSpeed, 0.0f);  //�������x���
    }
    //���j�󂷂�
    private void BreakRock()
    {
        Destroy(this.gameObject);
    }

    #region �l�󂯓n��
    public void SetBreakArea(float posY)
    {
        breakArea = posY;
    }
    public void SetFallSpeed(float speed)
    {
        fallSpeed= speed;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        RockController();
    }
}
