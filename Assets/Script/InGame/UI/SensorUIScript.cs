using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�Z���T�[UI�̐F��ς���
public class SensorUIScript : MonoBehaviour
{
    [SerializeField] private Image up;
    [SerializeField] private Image down;
    [SerializeField] private Image left;
    [SerializeField] private Image right;

    SensorScript ss;

    private SensorScript.HIT hit;
    private SensorScript.HIT hitChildren;

    //�Z���T�[UI�Ǘ�
    public void SensorUIController()
    {
        SetHit();   //�Z���T�[���擾
        if (hit == null || hitChildren == null)
        {
            return;
        }
        SensorChecker(up,hit.up,hitChildren.up);    //��Z���T�[�\��
        SensorChecker(down,hit.down,hitChildren.down);  //���Z���T�[�\��
        SensorChecker(left,hit.left, hitChildren.left); //���Z���T�[�\��
        SensorChecker(right,hit.right, hitChildren.right);  //�E�Z���T�[�\��
    }
    //�Z���T�[UI�̃t���O�`�F�b�N
    private void SensorChecker(in Image image,bool flag,bool flagChild)
    {
        if (flag)
        {
            SetCloseToObject(image);   //�ԕ\��
            return;
        }
        if (flagChild)
        {
            SetGreen(image);    //�Ε\��
            return;
        }

        SetSafe(image); //�\���Ȃ�
    }
    //�q�b�g�t���O�擾
    private void SetHit()
    {
        if (ss == null)
        {
            if (GameObject.FindWithTag("Sensor") != null)
            {
                ss = GameObject.FindWithTag("Sensor").GetComponent<SensorScript>();
            }
        }
        else
        {
            hit = ss.GetHit();  //���Z���T�[���擾
            hitChildren = ss.GetHitChild(); //�O�Z���T�[��\�擾
        }
    }
    //�Z���T�[UI�𓧖��ɂ���
    private void SetSafe(in Image image)
    {
        image.color = Color.clear;
    }
    //�Z���T�[UI��΂ɂ���
    private void SetGreen(in Image image)
    {
        image.color = Color.green;
    }
    //�Z���T�[UI��Ԃɂ���
    private void SetCloseToObject(in Image image)
    {
        image.color = Color.red;
    }
    //�Z���T�[�I���I�t
    public void SetSensorActive(bool flag)
    {
        up.enabled = flag;
        down.enabled = flag;
        left.enabled = flag;
        right.enabled = flag;

    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
