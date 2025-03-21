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
    Transform tf;

    private LineUIScript lineUI;
    private CreateMarkerScript cms;

    [SerializeField] private bool autShotSwitch;
    [SerializeField] private Transform body;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject line;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shotInterval;
    [SerializeField] private float range;
    [SerializeField] private float setWarning;
    [SerializeField] private float setVoid;
    private float voidColorTime;

    private Vector3 playerDis;
    private Vector3 playerDisNormal;
    private int intervalBuff;
    List<FlakBulletScript> flakBulletList = new List<FlakBulletScript>();
    private bool isAffective;

    //�C��̌����Ǝˌ�
    public void Aim(in PlayerScript ps)
    {
        if (ps == null)
        {
            isAffective = false;
            return;
        }
        //�΍��\��
        Vector3 playerPos = ps.GetPlayerPos();                        //�v���C���[�̈ʒu�擾
        playerDis =playerPos-barrel.position;                           //�����Z�o
        lineUI.SetLine(barrel.position,playerDis,intervalBuff);    //�\�����ݒ�
        Vector3 playerSpeed= ps.GetPlayerSpeed();                 //�v���C���[�̑��x�擾


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

        float t = new float[] { t1, t2 }.Where(t => t > 0).DefaultIfEmpty().Max();

        //�����Z�o
        playerDis = new Vector3((playerPos.x + (playerSpeed.x * t) - barrel.position.x), (playerPos.y + (playerSpeed.y * t) - barrel.position.y), (playerPos.z + (playerSpeed.z * t) - barrel.position.z));
        playerDisNormal = playerDis.normalized;
        
        //��������p�x���Z�o
        float horizontal = Mathf.Atan2(playerDisNormal.x, playerDisNormal.z) * Mathf.Rad2Deg;                                                                                                          //���������p�x�Z�o
        float vertical = Mathf.Atan2(Mathf.Sqrt( playerDisNormal.x*playerDisNormal.x + playerDisNormal.z*playerDisNormal.z), playerDisNormal.y) * Mathf.Rad2Deg;    //���������p�x�Z�o
        body.localEulerAngles=new Vector3(body.localEulerAngles.x,body.localEulerAngles.y,horizontal+180);                                                                                      //�C��̃{�f�B�[����]
        barrel.localEulerAngles = new Vector3((vertical*-1.0f)+90, barrel.localEulerAngles.y, barrel.localEulerAngles.z);                                                                        //�C�g����]

        cms.Adjustment();      //�}�[�J�[�␳
   
    }


    //�e�����X�g�ŊǗ�����
    public void BulletController(in bool isPose)
    {
        if (flakBulletList == null)
        {
            return;
        }
        for (int i = 0; i < flakBulletList.Count;)
        {
            if (flakBulletList[i].GetDeleteFlag())
            {
                flakBulletList[i].Delete();
                flakBulletList.Remove(flakBulletList[i]);
            }
            else
            {
                flakBulletList[i].Move(in isPose);
                i++;
            }
        }
    }
    //�N�[���^�C�����Z�b�g
    public void SetTime()
    {
        TimeCountScript.SetTime(ref intervalBuff, shotInterval);
    }
    //�N�[���^�C�����I���������Ԃ�
    public bool GetTime()
    {
        if (lineUI.GetShade())
        {
            if (intervalBuff < shotInterval * 60)
            {
                intervalBuff++;
            }
            lineUI.SetVoid();
        }
        else
        {
            SetLineColor();
            intervalBuff--;
        }
        if (intervalBuff <= 0)
        {
            return true;
        }
        return false;
    }
    //�e�۔�������
    public void Shot(in PlayerControllerScript pcs)
    {
        TimeCountScript.SetTime(ref intervalBuff, shotInterval);                                                                                                                 //�N�[���^�C�����Z�b�g
        Vector3 speed = new Vector3(playerDisNormal.x * bulletSpeed, playerDisNormal.y * bulletSpeed, playerDisNormal.z * bulletSpeed);   //�e�ۂ̑��x�Z�o
        GameObject _=Instantiate(bullet);                                                                                                                                                //�e�ې���
        FlakBulletScript fb = _.GetComponent<FlakBulletScript>();                                                                                                             //�R���|�[�l���g�擾
        fb.StartFlakBullet(speed,pcs);                                                                                                                                                             //�e�ۏ�����
        _.transform.localPosition = new Vector3(bulletPoint.position.x, bulletPoint.position.y, bulletPoint.position.z);                                       //�|�W�V��������
        _.transform.localEulerAngles = new Vector3(-barrel.localEulerAngles.x, body.localEulerAngles.z + 180, 0);                                        //�p�x����
        flakBulletList.Add(fb);                                                                                                                                                                  //�e�ۂ����X�g�ɒǉ�
    }
    //�\�����̐F�Ǘ�
    public void SetLineColor()
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


    //�\��������
    private void CreateLine()
    {
        GameObject _ = Instantiate(line);                              //�\��������
        lineUI = _.GetComponent<LineUIScript>();                //�R���|�[�l���g�擾
        lineUI.StartLine();                                                     //�\����������
        _.transform.position = barrel.position;                        //�ʒu��C�g�Ɉړ�
        _.transform.SetParent(this.transform);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            isAffective = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isAffective = false;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            isAffective=true;
        }
    }

    #region �l�󂯓n��

    public bool GetIsAffective()
    {
        return isAffective;
    }
    #endregion

    //���p�C������
    public void StartFlak(in PlayerControllerScript pcs) 
    {
        //�R���|�[�l���g�擾
        tf = GetComponent<Transform>();
        cms=GetComponent<CreateMarkerScript>();
        //////////////////////

        //���Ԑݒ�
        TimeCountScript.SetTime(ref intervalBuff, shotInterval);
        voidColorTime = shotInterval - setWarning - setVoid;
        TimeCountScript.SetTime(ref voidColorTime, voidColorTime);
        TimeCountScript.SetTime(ref setWarning, setWarning);
        //////////

        cms.CreateMarker(in tf,in pcs);
        CreateLine();
    }


}
