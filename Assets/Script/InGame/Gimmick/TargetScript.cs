using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//�j��ڕW�Ǘ�
public class TargetScript : MonoBehaviour
{
    [SerializeField] private float hp;
    private float maxHp;
    [SerializeField] private float  explodeTime;
    [SerializeField] private float brokePercent;
    [SerializeField] private float brokeTime;
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject brokenModel;
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

            Destroy(this.gameObject);                               //�I�u�W�F�N�g�폜
        }
    }

    private void Breakage()
    {
        model.SetActive(false);
        brokenModel.SetActive(true);
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

            if (hp <= 0)
            {
                isBreak = true;
                return;
            }

            if (hp / maxHp * 100 < brokePercent)
            {
                Breakage();
            }

            isHit = true;
        }
    }

    //������
    public void StartTarget()
    {
        Usefull.TimeCountScript.SetTime(ref brokeTimeBuff, brokeTime);
        maxHp = hp;
    }

}
