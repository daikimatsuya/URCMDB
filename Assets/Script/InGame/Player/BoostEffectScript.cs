using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//ブースト時のエフェクト管理
public class BoostEffectScript : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private int lifeTimeBuff;

    private bool deleteFlag=false;

    //フラグ管理
    public void CountTime()
    {
        if(TimeCountScript.TimeCounter(ref lifeTimeBuff))
        {
            deleteFlag = true;
        }
    }
    //生存時間セット
    public void SetTime()
    {
        TimeCountScript.SetTime(ref lifeTimeBuff, lifeTime);
    }
    //デリートフラグを返す
    public bool IsDelete()
    {
        return deleteFlag;
    }
    //デリート
    public void Break()
    {
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
