using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//スピードアップリング管理
public class SpeedUpRingScript : MonoBehaviour
{

    [SerializeField] private float shrinkSpeed;
    [SerializeField] private float ringSize;
    [SerializeField] private float offsetTime;
    [SerializeField] private GameObject marker;


    private int offsetBuff;
    private bool isGet;

    private MarkerScript ms;
    CapsuleCollider  collider_;
    Transform tf;

    private GameManagerScript gm;

    private void SpeedUpRingController()
    {
        
        ON();
        Off();
    }
    private void Off()
    {
        if (isGet)
        {
            tf.localScale = new Vector3(0, 0, 0);
            if(ms!=null)
            {
                ms.Delete();
            }
        }
    }
    private void ON()
    {
        if (gm.IsPlayerDead()==true)
        {
            isGet = false;
            tf.localScale = new Vector3(1, ringSize, ringSize);
            collider_.enabled = true;
            if(ms==null)
            {
                CreateMarker();
            }
            
        }
    }
    private void CreateMarker()
    {
        GameObject _ = Instantiate(marker);
        ms = _.GetComponent<MarkerScript>();
        ms.Move(tf.position);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            offsetBuff = (int)(offsetTime * 60);
            isGet = true;
            collider_.enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gm=GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        tf = GetComponent<Transform>();
        collider_=GetComponent<CapsuleCollider>();
        tf.localScale = new Vector3(1, ringSize, ringSize);
        offsetBuff = (int)(offsetTime * 60);
        isGet = false;
        CreateMarker();
    }

    // Update is called once per frame
    void Update()
    {
        SpeedUpRingController();  
    }
}
