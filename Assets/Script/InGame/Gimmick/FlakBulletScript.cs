using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ñCë‰Ç™îÚÇŒÇµÇΩíeä«óù
public class FlakBulletScript : MonoBehaviour
{
    Rigidbody rb;
    Transform pos;

    private Vector3 speed;
    private MarkerScript ms;
    

    [SerializeField] private float deleteTime;
    [SerializeField] private GameObject marker;

    //ñCíeä«óù
    private void BulletController()
    {
        Move();
        Delete();
    }
    //à⁄ìÆ
    private void Move()
    {
        rb.velocity = speed;

        ms.Move(pos.position);
    }
    //è¡ãé
    public void Delete()
    {
        if (deleteTime <= 0)
        {
            ms.Delete();
            Destroy(this.gameObject);
        }
        deleteTime--;
    }
    //ë¨ìxéÛÇØéÊÇË
    public void GetAcce(Vector3 acce)
    {
        speed = acce;
    }
    //É}Å[ÉJÅ[ê∂ê¨
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
