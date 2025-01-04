using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�\�����Ǘ�
public class LineUIScript : MonoBehaviour
{
    Transform pos;
    LineRenderer line;

    private int timeBuff;
    private bool red;
    //private bool brock;
    private RaycastHit hit;
    private bool isShade;

    [SerializeField] private float randPow;
    [SerializeField] private float colorChangeTime;

    //�\�����ݒu
    public void SetLine(Vector3 Pos,Vector3 targetLength,float time)
    {
        //�Ə��������܂Ńu��������///////////////////////////////////////////////////////////////////////////////////////////
        float randX = (Random.Range(-time * randPow, time * randPow));
        float randY = (Random.Range(-time * randPow, time * randPow));
        float randZ = (Random.Range(-time * randPow, time * randPow));
        line.SetPosition(0, new Vector3( targetLength.x+randX , targetLength.y+randY,targetLength.z + randZ));
        pos.position = Pos;
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        if(Physics.Raycast(pos.position,Vector3.Normalize(targetLength),out hit, Vector3.Magnitude(targetLength)))  //���C�L���X�g�Ńv���C���[�ƖC�g�̊Ԃɏ�Q�������邩���m�F////////////////
        {
            if (!hit.collider.CompareTag("Player")) //�v���C���[�Ɉ�ԏ��߂ɂ���������//////
            {
                isShade = true;
            }/////////////////////////////////////////////////////////////////////////////////////
            else 
            {
                isShade = false;
            }
        }////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    //�\�����폜
    public void Death()
    {
        Destroy(this.gameObject);   //�I�u�W�F�N�g�폜
    }
    //�F��ԂɕύX
    public void SetRed()
    {
        //�Ԃɂ���
        line.startColor = Color.red;
        line.endColor = Color.red;
        ///////////
    }
    //�F��ԂƉ��œ_��
    public void SetWarning()
    {
        if (TimeCountScript.TimeCounter(ref timeBuff))  //���Ԃœ_�ł�����///////////
        {
            if (red)
            {
                line.startColor= Color.yellow;
                line.endColor = Color.yellow;
                SetTime();
                red = false;
            }
            else
            {
                line.startColor = Color.red;
                line.endColor = Color.red;
                SetTime();
                red = true;
            }
        }////////////////////////////////////////////////////////////////////////////////////
    }
    //�F�𓧖��ɂ���
    public void SetVoid()
    {
        //�����ɂ�����
        line.startColor = new Color(0, 0, 0, 0);
        line.endColor = new Color(0, 0, 0, 0);    
        ///////////////
    }
    //���Ԃ�������
    private void SetTime()
    {
        TimeCountScript.SetTime(ref timeBuff, colorChangeTime);
    }
    //�v���C���[�����Ă邩�ǂ������擾
    public bool GetShade()
    {
        return isShade;
    }
    // Start is called before the first frame update
    void Start()
    {
        pos=GetComponent<Transform>();
        line = GetComponent<LineRenderer>();

        SetTime();
        red = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
