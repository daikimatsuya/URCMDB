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

    private bool isActive;
    private int number;



    Transform tf;
    private void CameraController()
    {
        if(isActive)
        {
            Move();
        }
    }
    private void Move()
    {

    }
    private void SetNext()
    {
        tf.position = startPosition[number];
        tf.eulerAngles = startRotation[number];

        targetPos = targetPosition[number];
        targetRot = targetRotation[number];

        TimeCountScript.SetTime(ref moveTimeBuff, moveTime[number]);
    }
    private void SetVector3(Vector3 target, float x, float y, float z)
    {
        target = new Vector3(x, y, z);
    }
    // Start is called before the first frame update
    void Start()
    {
        tf= GetComponent<Transform>();
        number = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
