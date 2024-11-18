using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//破壊目標管理
public class TargetScript : MonoBehaviour
{
    [SerializeField] private int hp;

    private GameManagerScript gm;

    //ターゲット管理
    private void TargetController()
    {
        IsBreak();
    }
    //HPが無くなったら消滅
    private void IsBreak()
    {
        if(gm.GetTargetBreakFlag())
        {  
            Destroy(this.gameObject);
        }
    }
    //HP渡し
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
