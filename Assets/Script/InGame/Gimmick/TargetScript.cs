using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//破壊目標管理
public class TargetScript : MonoBehaviour
{
    [SerializeField] private float hp;
    private float maxHp;
    [SerializeField] GameObject explode;
    [SerializeField] private float  explodeTime;
    [SerializeField] private float brokePercent;
    private int explodeTimeBuff;

    private GameManagerScript gm;

    //ターゲット管理
    private void TargetController()
    {
        IsBreak();  //破壊管理
        if (hp <= 0)
        {
            Explode();  //爆発
        }
    }
    //爆発させる
    private void Explode()
    {
        if(TimeCountScript.TimeCounter(ref explodeTimeBuff))
        {
            GameObject _ = Instantiate(explode);    //爆発エフェクト生成
            _.transform.position=this.transform.position;   //座標代入
            _.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); //サイズ設定
            TimeCountScript.SetTime(ref explodeTimeBuff, explodeTime);  
        }
    }
    //ダメージ表現
    private void ModelBroken()
    {
        //途中////////////////////////////////////////////
        if (hp <= (maxHp / 100) * brokePercent)
        {

        }
        //////////////////////////////////////////////////
    }
    //消滅させる
    private void IsBreak()
    {
        if(gm.GetTargetBreakFlag())
        {
            GameObject _ = Instantiate(explode);    //爆発エフェクト生成
            _.transform.position = this.transform.position;  //座標代入
            _.transform.localScale = new Vector3(5, 5, 5); //サイズ設定
            Destroy(this.gameObject);   //オブジェクト削除
        }
    }

    #region 値受け渡し
    //HP渡し
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
