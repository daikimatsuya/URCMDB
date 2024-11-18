using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�j��ڕW�Ǘ�
public class TargetScript : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] GameObject explode;
    [SerializeField] private float  explodeTime;
    private int explodeTimeBuff;

    private GameManagerScript gm;

    

    //�^�[�Q�b�g�Ǘ�
    private void TargetController()
    {
        IsBreak();
        if (hp <= 0)
        {
            Explode();
        }
    }
    //����������
    private void Explode()
    {
        if(explodeTimeBuff<=0)
        {
            GameObject _ = Instantiate(explode);
            _.transform.position=this.transform.position;
            _.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            TimeCountScript.SetTime(ref explodeTimeBuff, explodeTime);
        }
        explodeTimeBuff--;
    }
    //���ł�����
    private void IsBreak()
    {
        if(gm.GetTargetBreakFlag())
        {
            GameObject _ = Instantiate(explode);
            _.transform.position = this.transform.position;
            _.transform.localScale = new Vector3(5, 5, 5);
            Destroy(this.gameObject);
        }
    }
    //HP�n��
    public int GetHp()
    {
        return hp;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript ps=collision.gameObject.GetComponent<PlayerScript>();
            hp-=(int)(ps.GetPlayerSpeedBuffFloat()/10);
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
    }

    // Update is called once per frame
    void Update()
    {
        TargetController();
    }
}
