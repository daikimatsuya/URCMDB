using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//î≠éÀë‰ä«óù
public class LaunchPointScript : MonoBehaviour
{
    private Transform tf;

    [SerializeField] private float rowlingSpeedX;
    [SerializeField] private float rowlingSpeedY;
    [SerializeField] private Vector2 maxRow; 

    private bool isControll;
    private Vector2 rowling;
    private bool isStart;


    //î≠éÀë‰ä«óùä÷êî
    private void LaunchPointController()
    {
        if (isStart)
        {
            if (isControll)
            {
                Move();
            }
        }
    }
    //å¸Ç´ÇïœçX
    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rowling.x += rowlingSpeedY;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rowling.x -= rowlingSpeedY;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rowling.y -= rowlingSpeedX;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rowling.y += rowlingSpeedX;
        }
        if (rowling.y <= -maxRow.y)
        {
            rowling.y = -maxRow.y;
        }
        if (rowling.y >= maxRow.y)
        {
            rowling.y = maxRow.y;
        }
        if(rowling.x <= 0)
        {
            rowling.x = 0;
        }
        if(rowling.x >= maxRow.x)
        {
            rowling.x = maxRow.x;
        }
        tf.localEulerAngles = new Vector3(rowling.x, rowling.y,0);
    }
    #region íléÛÇØìnÇµ
    public void SetStart(bool flag)
    {
        isStart = flag;
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
        return tf.eulerAngles;
    }
    #endregion
    private void Awake()
    {
        tf = GetComponent<Transform>();
        rowling = new Vector2(tf.localEulerAngles.x, tf.localEulerAngles.y);

        isControll = true;
        isStart = false;
    }
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        LaunchPointController();  
    }
}
