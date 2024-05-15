using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPointScript : MonoBehaviour
{
    private Transform tf;

    [SerializeField] private float rowlingSpeedX;
    [SerializeField] private float rowlingSpeedY;

    private bool isControll;
    private Vector2 rowling;

    private void LaunchPointController()
    {
        if(isControll)
        {
            Move();
        }        
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rowling.x += rowlingSpeedY;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rowling.x -= rowlingSpeedY;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rowling.y -= rowlingSpeedX;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rowling.y += rowlingSpeedX;
        }

        if(rowling.x <= 0)
        {
            rowling.x = 0;
        }
        if(rowling.x >= 89)
        {
            rowling.x = 89;
        }
        tf.localEulerAngles = new Vector3(rowling.x, rowling.y,0);
    }

    public void Shoot()
    {
        isControll= false;
    }
    public void Bombed()
    {
        isControll = true;
    }

    public Vector3 GetPos()
    {
        return tf.position;
    }
    public Vector2 GetRowling()
    {
        return tf.localEulerAngles;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();   

        isControll = true;
       
    }

    // Update is called once per frame
    void Update()
    {
        LaunchPointController();  
    }
}
