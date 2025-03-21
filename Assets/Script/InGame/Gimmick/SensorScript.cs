using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorScript : MonoBehaviour
{
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] bool left;
    [SerializeField] bool right;
    [SerializeField] bool children;
    [SerializeField] bool master;
    [SerializeField] string[] ignoreTags;
    

    [System.Serializable] enum Script
    {
        up,
        down,
        left,
        right,
    }
    [System.Serializable] public class HIT
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;
    }

    private HIT hit= new HIT{};
    private HIT hitChild= new HIT{};

    private SensorScript[] oneBlelowScript=new SensorScript[4];

    //�O���̃Z���T�[�`�F�b�N
    private void SetSensorChildren(bool flag)
    {
        //�������Ă���Ή������t���O���I���ɂ���
        if(up)
        {
            hit.up = flag;
        }
        if(down)
        {
            hit.down = flag;
        }
        if(left)
        {
            hit.left = flag;
        }
        if (right)
        {
            hit.right = flag;
        }

    }

    //�����̃Z���T�[�`�F�b�N
    private void SetSensor(bool flag)
    {
        //�������Ă���Ή������t���O���I���ɂ���
        if (up)
        {
            hit.up = flag;
        }
        if (down)
        {
            hit.down = flag;
        }
        if (left)
        {
            hit.left = flag;
        }
        if (right)
        {
            hit.right = flag;
        }
    }

    public HIT GetHit()
    {
        //�}�X�^�[�Ȃ�t���O���Ǘ�
        if (master) 
        {
            hit.up = oneBlelowScript[(int)Script.up].GetHit().up;
            hit.down = oneBlelowScript[(int)Script.down].GetHit().down;
            hit.right = oneBlelowScript[(int)Script.right].GetHit().right;
            hit.left = oneBlelowScript[(int)Script.left].GetHit().left;
        }
        return hit; //�}�X�^�[�ȊO�Ȃ�t���O��Ԃ�
    }
    //�������Ă��������̂̃^�O�擾�p
    public string[] GetIgnoreTag()
    {
        return ignoreTags;
    }
    public HIT GetHitChild()
    {
        //�q�I�u�W�F�N�g�Z���T�[�Ȃ�t���O��Ԃ�
        if (children) 
        {
            return hit; 
        }

        //�}�X�^�[�Ȃ�t���O���Ǘ�
        if (master) 
        {
            hitChild.up = oneBlelowScript[(int)Script.up].GetHitChild().up;
            hitChild.down= oneBlelowScript[(int)Script.down].GetHitChild().down;
            hitChild.right= oneBlelowScript[(int)Script.right].GetHitChild().right;
            hitChild.left= oneBlelowScript[(int)Script.left].GetHitChild().left;

            return hitChild;
        }

        //�t���O�Ǝq�I�u�W�F�N�g�̃t���O��Ԃ�
        hitChild = oneBlelowScript[0].GetHit();
        return hitChild;
    }

    private void OnTriggerStay(Collider other)
    {
        //�������Ă��������͖̂�������
        for(int i=0;i< ignoreTags.Length; i++)
        {
            if (other.CompareTag(ignoreTags[i]))
            {
                return;
            }
        }
        
         if (master)
        {
            return;
        }
        if (children)
        {
            SetSensorChildren(true);
            return;
        }
        SetSensor(true);
    }

    //������
    public void StartSensor()
    {
        if (!master)
        {
            SensorScript ss = GameObject.FindWithTag("Sensor").GetComponent<SensorScript>();
            ignoreTags = ss.GetIgnoreTag(); ;
        }

        if (children)
        {
            return;
        }
        if (!master)
        {
            oneBlelowScript[0] = this.gameObject.transform.GetChild(0).gameObject.GetComponent<SensorScript>(); //�q�I�u�W�F�N�g�̃R���|�[�l���g�擾
            return;
        }

        //�l���̃Z���T�[�R���|�[�l���g�擾
        oneBlelowScript[(int)Script.up] = this.gameObject.transform.GetChild(0).gameObject.GetComponent<SensorScript>();
        oneBlelowScript[(int)Script.down] = this.gameObject.transform.GetChild(1).gameObject.GetComponent<SensorScript>();
        oneBlelowScript[(int)Script.left] = this.gameObject.transform.GetChild(2).gameObject.GetComponent<SensorScript>();
        oneBlelowScript[(int)Script.right] = this.gameObject.transform.GetChild(3).gameObject.GetComponent<SensorScript>();

    }
    void Start()
    {
        StartSensor();
    }

    private void FixedUpdate()
    {
        SetSensorChildren(false);
        SetSensor(false);
    }
  
}
