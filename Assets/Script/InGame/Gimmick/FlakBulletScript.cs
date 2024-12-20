using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//砲台が飛ばした弾管理
public class FlakBulletScript : MonoBehaviour
{
    Rigidbody rb;
    Transform pos;

    private Vector3 speed;
    private MarkerScript ms;
    

    [SerializeField] private float deleteTime;
    [SerializeField] private GameObject marker;

    //砲弾管理
    private void BulletController()
    {
        Move();
        Delete();
    }
    //移動
    private void Move()
    {
        rb.velocity = speed;

        ms.Move(pos.position);
    }
    //消去
    public void Delete()
    {
        if (deleteTime <= 0)
        {
            ms.Delete();
            Destroy(this.gameObject);
        }
        deleteTime--;
    }
    //速度受け取り
    public void GetAcce(Vector3 acce)
    {
        speed = acce;
    }
    //マーカー生成
    private void CreateMarker()
    {
        GameObject _ = Instantiate(marker);
        ms = _.GetComponent<MarkerScript>();
        ms.Move(pos.position);

    }
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        pos= GetComponent<Transform>(); 
        deleteTime = deleteTime * 60;

        CreateMarker();
    }

    // Update is called once per frame
    void Update()
    {
        BulletController();
    }
}
