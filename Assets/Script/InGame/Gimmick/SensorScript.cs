using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorScript : MonoBehaviour
{
    [SerializeField] bool up;
    [SerializeField] bool down;
    [SerializeField] bool left;
    [SerializeField] bool right;
    [SerializeField] bool children;
    [SerializeField] bool master;
    [SerializeField] string[] ignoreTags;
    

    [System.Serializable] enum Script
    {
        up,
        down,
        left,
        right,
    }
    [System.Serializable] public class HIT
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;
    }

    private HIT hit= new HIT{};
    private HIT hitChild= new HIT{};

    private SensorScript[] oneBlelowScript=new SensorScript[4];

    //外周のセンサーチェック
    private void SetSensorChildren(bool flag)
    {
        //あたってたら対応したフラグをオンにする
        if(up)
        {
            hit.up = flag;
        }
        if(down)
        {
            hit.down = flag;
        }
        if(left)
        {
            hit.left = flag;
        }
        if (right)
        {
            hit.right = flag;
        }

    }

    //内周のセンサーチェック
    private void SetSensor(bool flag)
    {
        //あたってたら対応したフラグをオンにする
        if (up)
        {
            hit.up = flag;
        }
        if (down)
        {
            hit.down = flag;
        }
        if (left)
        {
            hit.left = flag;
        }
        if (right)
        {
            hit.right = flag;
        }
    }

    public HIT GetHit()
    {
        //マスターならフラグを管理
        if (master) 
        {
            hit.up = oneBlelowScript[(int)Script.up].GetHit().up;
            hit.down = oneBlelowScript[(int)Script.down].GetHit().down;
            hit.right = oneBlelowScript[(int)Script.right].GetHit().right;
            hit.left = oneBlelowScript[(int)Script.left].GetHit().left;
        }
        return hit; //マスター以外ならフラグを返す
    }
    //あたってもいいもののタグ取得用
    public string[] GetIgnoreTag()
    {
        return ignoreTags;
    }
    public HIT GetHitChild()
    {
        //子オブジェクトセンサーならフラグを返す
        if (children) 
        {
            return hit; 
        }

        //マスターならフラグを管理
        if (master) 
        {
            hitChild.up = oneBlelowScript[(int)Script.up].GetHitChild().up;
            hitChild.down= oneBlelowScript[(int)Script.down].GetHitChild().down;
            hitChild.right= oneBlelowScript[(int)Script.right].GetHitChild().right;
            hitChild.left= oneBlelowScript[(int)Script.left].GetHitChild().left;

            return hitChild;
        }

        //フラグと子オブジェクトのフラグを返す
        hitChild = oneBlelowScript[0].GetHit();
        return hitChild;
    }

    private void OnTriggerStay(Collider other)
    {
        //あたってもいいものは無視する
        for(int i=0;i< ignoreTags.Length; i++)
        {
            if (other.CompareTag(ignoreTags[i]))
            {
                return;
            }
        }
        
         if (master)
        {
            return;
        }
        if (children)
        {
            SetSensorChildren(true);
            return;
        }
        SetSensor(true);
    }

    //初期化
    public void StartSensor()
    {
        if (!master)
        {
            SensorScript ss = GameObject.FindWithTag("Sensor").GetComponent<SensorScript>();
            ignoreTags = ss.GetIgnoreTag(); ;
        }

        if (children)
        {
            return;
        }
        if (!master)
        {
            oneBlelowScript[0] = this.gameObject.transform.GetChild(0).gameObject.GetComponent<SensorScript>(); //子オブジェクトのコンポーネント取得
            return;
        }

        //四方のセンサーコンポーネント取得
        oneBlelowScript[(int)Script.up] = this.gameObject.transform.GetChild(0).gameObject.GetComponent<SensorScript>();
        oneBlelowScript[(int)Script.down] = this.gameObject.transform.GetChild(1).gameObject.GetComponent<SensorScript>();
        oneBlelowScript[(int)Script.left] = this.gameObject.transform.GetChild(2).gameObject.GetComponent<SensorScript>();
        oneBlelowScript[(int)Script.right] = this.gameObject.transform.GetChild(3).gameObject.GetComponent<SensorScript>();

    }
    void Start()
    {
        StartSensor();
    }

    private void FixedUpdate()
    {
        SetSensorChildren(false);
        SetSensor(false);
    }
  
}
