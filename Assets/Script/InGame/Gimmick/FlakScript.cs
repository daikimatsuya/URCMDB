using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using Usefull;

//  �C��Ǘ�
public class FlakScript : MonoBehaviour
{
    Transform pos;
    Transform linePos;
    LineUIScript lineScript;

    private Transform playerPos;
    private PlayerScript playerScript;
    private LineUIScript lineUI;
    private MarkerScript ms; 

    [SerializeField] private bool autShotSwitch;

    [SerializeField] private Transform body;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject marker;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shotInterval;
    [SerializeField] private float range;
    [SerializeField] private float setWarning;
    [SerializeField] private float setVoid;
    private float voidColorTime;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;
    private int intervalBuff;

    //�C��̓����R���g���[���[
    private void FlakController()
    {
        Aim();  //�ˌ��Ǘ�
    }
    //�C��̌����Ǝˌ�
    private void Aim()
    {
        if(playerPos != null)   //�v���C���[���˒����ɓ����Ă��邩�m�F////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        {
            //�΍��\��///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            playerDis=playerPos.position-barrel.position;   //�����Z�o
            lineUI.SetLine(barrel.position,playerDis,intervalBuff); //�\�����ݒ�
            Vector3 playerSpeed= playerScript.GetPlayerSpeed(); //�v���C���[�̑��x�擾

            //���̌������g�p���Ēl���Z�o///////////////////////////////////////////////////////
            float a = Vector3.Dot(playerSpeed, playerSpeed) - (bulletSpeed*bulletSpeed);   
            float b = Vector3.Dot(playerDis, playerSpeed) * 2; 
            float c = Vector3.Dot(playerDis, playerDis);

            float discriminant = (b * b) - (4 * a * c);
            if(discriminant < 0)
            {
                return; //���肦�Ȃ��l�̎�return��Ԃ�
            }
            float t1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            float t2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

            float t = new float[] { t1, t2 }.Where(t => t > 0).Max();
            ///////////////////////////////////////////////////////////////////////////////////

            //���x�v�Z///////////////////////////////////////////////////////////////////////////////////////////////////
            playerDis = new Vector3((playerPos.position.x + (playerSpeed.x * t) - barrel.position.x), (playerPos.position.y + (playerSpeed.y * t) - barrel.position.y), (playerPos.position.z + (playerSpeed.z * t) - barrel.position.z));
            playerDisNormal = playerDis.normalized;
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
            
            //���x����p�x���Z�o//////////////////////////////////////////////////////////////////////
            float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;
            float vertical = Mathf.Atan2(Mathf.Sqrt( playerDisNormal.x*playerDisNormal.x + playerDisNormal.z*playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg;
            body.localEulerAngles=new Vector3(body.localEulerAngles.x,body.localEulerAngles.y,horizontal+180);  //�C��̃{�f�B�[����]
            barrel.localEulerAngles = new Vector3((vertical*-1.0f)+90, barrel.localEulerAngles.y, barrel.localEulerAngles.z);   //�C�g����]
            ///////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //���˃^�C�}�[�Ǘ�////
            if (intervalBuff <= 0)
            {
                if(autShotSwitch)
                {
                    Shot();
                    TimeCountScript.SetTime(ref intervalBuff, shotInterval);
                }         
            }
            ///////////////////////
           
            //�v���C���[���Օ��ɉB��Ă��邩
            if (lineUI.GetShade())
            {
                intervalBuff++;
                lineUI.SetVoid();
            }
            else
            {
                SetLineColor();
                intervalBuff--;
            }
            ////////////////////////////////

        }/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        else
        {
            TimeCountScript.SetTime(ref intervalBuff, shotInterval);

            if(lineUI != null)
            {
                lineUI.Death(); //lineUI���폜
            }
            
        }
    }
    //�e�۔�������
    private void Shot()
    {
        GameObject _=Instantiate(bullet);   //�e�ې���
        _.transform.localPosition = new Vector3(bulletPoint.position.x, bulletPoint.position.y, bulletPoint.position.z);    //�|�W�V��������
        _.transform.localEulerAngles = new Vector3(-barrel.localEulerAngles.x, body.localEulerAngles.z + 180, 0);   //�p�x����
        FlakBulletScript fb=_.GetComponent<FlakBulletScript>(); //�R���|�[�l���g�擾
        fb.GetAcce(new Vector3(playerDisNormal.x*bulletSpeed,playerDisNormal.y*bulletSpeed,playerDisNormal.z*bulletSpeed)); //�����x���
    }
    //�\�����̐F�Ǘ�
    private void SetLineColor()
    {
        //�\�����̐F�𔭎˂܂ł̎��Ԃŕω�������
        if (intervalBuff > voidColorTime) 
        {
            lineUI.SetVoid();   //�����ɂ���
        }
        else if(intervalBuff > setWarning) 
        {
            lineUI.SetRed();    //�Ԃ�����
        }
        else
        {
            lineUI.SetWarning();    //���F�̓_�łɂ���
        }
        //////////////////////////////////////////
    }

    //�}�[�J�[����
    private void CreateMarker()
    {
        GameObject _ = Instantiate(marker); //����
        ms = _.GetComponent<MarkerScript>();    //�R���|�[�l���g�擾
        ms.Move(pos.position);  //�ʒu���
     
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerPos = other.transform;    //�v���C���[�̈ʒu�擾
            playerScript = other.GetComponent<PlayerScript>();  //�R���|�[�l���g�擾
            GameObject _ = Instantiate(line);   //�\��������
            lineUI = _.GetComponent<LineUIScript>();    //�R���|�[�l���g�擾
            _.transform.position = barrel.position; //�ʒu��C�g�Ɉړ�
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            TimeCountScript.SetTime(ref intervalBuff, shotInterval);
            playerPos = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //�R���|�[�l���g�擾
        pos=GetComponent<Transform>();
        linePos = line.GetComponent<Transform>();
        lineScript=line.GetComponent<LineUIScript>();
        //////////////////////

        //���Ԑݒ�
        TimeCountScript.SetTime(ref intervalBuff, shotInterval);
        voidColorTime = shotInterval - setWarning - setVoid;
        TimeCountScript.SetTime(ref voidColorTime, voidColorTime);
        TimeCountScript.SetTime(ref setWarning, setWarning);
        //////////

        CreateMarker();
    }

    // Update is called once per frame
    void Update()
    {
        FlakController();
    }
}
