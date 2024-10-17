using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�C�䂪��΂����e�Ǘ�
public class FlakBulletScript : MonoBehaviour
{
    Rigidbody rb;
    Transform pos;

    private Vector3 speed;
    private MarkerScript ms;
    

    [SerializeField] private float deleteTime;
    [SerializeField] private GameObject marker;

    //�C�e�Ǘ�
    private void BulletController()
    {
        Move();
        Delete();
    }
    //�ړ�
    private void Move()
    {
        rb.velocity = speed;

        ms.Move(pos.position);
    }
    //����
    public void Delete()
    {
        if (deleteTime <= 0)
        {
            ms.Delete();
            Destroy(this.gameObject);
        }
        deleteTime--;
    }
    //���x�󂯎��
    public void GetAcce(Vector3 acce)
    {
        speed = acce;
    }
    //�}�[�J�[����
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
