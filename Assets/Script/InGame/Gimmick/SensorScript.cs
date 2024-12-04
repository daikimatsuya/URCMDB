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
        if (master)
        {
            hit.up = oneBlelowScript[(int)Script.up].GetHit().up;
            hit.down = oneBlelowScript[(int)Script.down].GetHit().down;
            hit.right = oneBlelowScript[(int)Script.right].GetHit().right;
            hit.left = oneBlelowScript[(int)Script.left].GetHit().left;

        }
        return hit;
    }
    public HIT GetHitChild()
    {
        if (children)
        {
            return hit;
        }
        if (master)
        {
            hitChild.up = oneBlelowScript[(int)Script.up].GetHitChild().up;
            hitChild.down= oneBlelowScript[(int)Script.down].GetHitChild().down;
            hitChild.right= oneBlelowScript[(int)Script.right].GetHitChild().right;
            hitChild.left= oneBlelowScript[(int)Script.left].GetHitChild().left;

            return hitChild;
        }
        hitChild = oneBlelowScript[0].GetHit();
        return hitChild;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SensorChildren"))
        {
            return;
        }
        if (other.CompareTag("stage"))
        {
            return;
        }
        if (other.CompareTag("SpeedUpRing"))
        {
            return;
        }
        if (other.CompareTag("Monitor"))
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            return;
        }
        if (other.CompareTag("Boat"))
        {
            return;
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

    // Start is called before the first frame update
    void Start()
    {
        if (children)
        {     
            return;
        }
        if (!master)
        {
            oneBlelowScript[0] = this.gameObject.transform.GetChild(0).gameObject.GetComponent<SensorScript>();
            return;
        }

        oneBlelowScript[(int)Script.up] = this.gameObject.transform.GetChild(0).gameObject.GetComponent<SensorScript>();
        oneBlelowScript[(int)Script.down] = this.gameObject.transform.GetChild(1).gameObject.GetComponent<SensorScript>();
        oneBlelowScript[(int)Script.left] = this.gameObject.transform.GetChild(2).gameObject.GetComponent<SensorScript>();
        oneBlelowScript[(int)Script.right] = this.gameObject.transform.GetChild(3).gameObject.GetComponent<SensorScript>();

        //int test2 = oneBlelowScript[(int)Script.up].GetTest();
    }

    private void FixedUpdate()
    {
        SetSensorChildren(false);
        SetSensor(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
