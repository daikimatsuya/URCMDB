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
        SetHit();
        SensorChecker(up,hit.up,hitChildren.up);
        SensorChecker(down,hit.down,hitChildren.down);
        SensorChecker(left,hit.left, hitChildren.left);
        SensorChecker(right,hit.right, hitChildren.right);
    }
    //�Z���T�[UI�̃t���O�`�F�b�N
    private void SensorChecker(Image image,bool flag,bool flagChild)
    {
        if (flag)
        {
            SetCloseToObject(image);
            return;
        }
        if (flagChild)
        {
            SetGreen(image);
            return;
        }

        SetSafe(image);
    }
    //�q�b�g�t���O�擾
    private void SetHit()
    {
        if (ss == null)
        {
            ss = GameObject.FindWithTag("Sensor").GetComponent<SensorScript>();
        }
        else
        {
            hit = ss.GetHit();
            hitChildren = ss.GetHitChild();
        }
    }
    //�Z���T�[UI�𓧖��ɂ���
    private void SetSafe(Image image)
    {
        image.color = Color.clear;
    }
    //�Z���T�[UI��΂ɂ���
    private void SetGreen(Image image)
    {
        image.color = Color.green;
    }
    //�Z���T�[UI��Ԃɂ���
    private void SetCloseToObject(Image image)
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
        ss=GameObject.FindWithTag("Sensor").GetComponent<SensorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
