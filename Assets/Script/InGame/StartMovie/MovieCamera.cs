using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//インゲームが始まった時のカメラワークの処理
public class MovieCamera : MonoBehaviour
{
    [SerializeField] private int knotNumber;
    [SerializeField] private Vector3[] targetPosition;
    private Vector3 targetPos;
    [SerializeField] private Vector3[] startPosition;
    [SerializeField] private Vector3[] targetRotation;
    private Vector3 targetRot;
    [SerializeField] private Vector3[] startRotation;
    [SerializeField] private float[] moveTime;
    private int moveTimeBuff;

    private bool ready;
    [SerializeField] private bool isMove;
    private int number;
    private Vector3 posBuff;
    private Vector3 rotBuff;
    private Vector3 posRange;
    private Vector3 rotRange;
    private Vector3 moveSpeed;
    private Vector3 RotSpeed;

    Transform tf;
    public void CameraController()
    {
  
        Move();
        
        SetNext();
    }
    private void Move()
    {
        if (isMove)
        {
            if (moveTimeBuff > 0)
            {

            }
            else
            {
                ready = false;
                isMove = false;
            }
            moveTimeBuff--;
        }
    }
    private void SetNext()
    {
        if (!ready)
        {
            if (knotNumber > number)
            {
                posBuff = startPosition[number];
                rotBuff = startRotation[number];

                targetPos = targetPosition[number];
                targetRot = targetRotation[number];

                TimeCountScript.SetTime(ref moveTimeBuff, moveTime[number]);

                posRange = targetPos - posBuff;
                rotRange = targetRot - rotBuff;

                moveSpeed = posRange / moveTimeBuff;
                RotSpeed = rotRange / moveTimeBuff;

                number++;
                ready = true;
                //isMove = true;
            }
            else
            {

            }
        }
    }
    private void SetVector3(Vector3 target, float x, float y, float z)
    {
        target = new Vector3(x, y, z);
    }

    // Start is called before the first frame update
    void Start()
    {
        ready = false;
        tf= GetComponent<Transform>();
        number = 0;
        SetNext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
