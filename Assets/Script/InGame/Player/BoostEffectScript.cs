using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostEffectScript : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private int lifeTimeBuff;

    private bool deleteFlag=false;
    public void CountTime()
    {
        if(lifeTimeBuff<=0)
        {
            deleteFlag = true;
        }
        TimeCountScript.TimeCounter(ref lifeTimeBuff);
    }
    public void SetTime()
    {
        TimeCountScript.SetTime(ref lifeTimeBuff, lifeTime);
    }
    public bool IsDelete()
    {
        return deleteFlag;
    }
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
