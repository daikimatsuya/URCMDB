using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�j��ڕW�Ǘ�
public class TargetScript : MonoBehaviour
{
    [SerializeField] private float hp;
    private float maxHp;
    [SerializeField] GameObject explode;
    [SerializeField] private float  explodeTime;
    [SerializeField] private float brokePercent;
    [SerializeField] private float brokeTime;
    private int brokeTimeBuff;
    private int explodeTimeBuff;

    private bool isHit;
    private bool isBreak;

    //�^�[�Q�b�g�Ǘ�
    public void TargetController(in bool isPose)
    {
        if(isPose)
        {
            return;
        }
        IsBreak();  //�j��Ǘ�
        if (hp <= 0)
        {
            Explode();  //����
        }
    }
    //����������
    private void Explode()
    {
        if(TimeCountScript.TimeCounter(ref explodeTimeBuff))
        {
            GameObject _ = Instantiate(explode);                                    //�����G�t�F�N�g����
            _.transform.position=this.transform.position;                          //���W���
            _.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);            //�T�C�Y�ݒ�
            TimeCountScript.SetTime(ref explodeTimeBuff, explodeTime);  //���ԃZ�b�g
        }
    }
    //�_���[�W�\��
    private void ModelBroken()
    {
        //�r��////////////////////////////////////////////
        if (hp <= (maxHp / 100) * brokePercent)
        {

        }
        //////////////////////////////////////////////////
    }
    //���ł�����
    private void IsBreak()
    {
        if (!isBreak)
        {
            return;
        }
        if(Usefull.TimeCountScript.TimeCounter(ref brokeTimeBuff))
        {
            GameObject _ = Instantiate(explode);              //�����G�t�F�N�g����
            _.transform.position = this.transform.position;  //���W���
            _.transform.localScale = new Vector3(5, 5, 5);  //�T�C�Y�ݒ�
            Destroy(this.gameObject);                               //�I�u�W�F�N�g�폜
        }
    }

    #region �l�󂯓n��
    //HP�n��
    public float GetHp()
    {
        return hp;
    }
    public Vector3 GetPos()
    {
        return this.transform.position;
    }
    public bool GetHit()
    {
        return isHit;
    }
    public bool GetBreak()
    {
        return isBreak;
    }
    #endregion
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript ps=collision.gameObject.GetComponent<PlayerScript>();
            hp-=(int)(ps.GetPlayerSpeedBuffFloat()/10);
            ModelBroken();
            if (hp <= 0)
            {
                isBreak = true;
            }
            isHit = true;
        }
    }

    public void StartTarget()
    {
        Usefull.TimeCountScript.SetTime(ref brokeTimeBuff, brokeTime);
        maxHp = hp;
    }

}
