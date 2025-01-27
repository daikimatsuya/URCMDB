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
    private int explodeTimeBuff;

    private GameManagerScript gm;

    //�^�[�Q�b�g�Ǘ�
    private void TargetController()
    {
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
            GameObject _ = Instantiate(explode);    //�����G�t�F�N�g����
            _.transform.position=this.transform.position;   //���W���
            _.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); //�T�C�Y�ݒ�
            TimeCountScript.SetTime(ref explodeTimeBuff, explodeTime);  
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
        if(gm.GetTargetBreakFlag())
        {
            GameObject _ = Instantiate(explode);    //�����G�t�F�N�g����
            _.transform.position = this.transform.position;  //���W���
            _.transform.localScale = new Vector3(5, 5, 5); //�T�C�Y�ݒ�
            Destroy(this.gameObject);   //�I�u�W�F�N�g�폜
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
                gm.SetClearFlag();
            }
            gm.SetIsHitTarget(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gm=GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        maxHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        TargetController();
    }
}
