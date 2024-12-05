using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//インゲームが始まった時のカメラワークの処理
public class MovieCamera : MonoBehaviour
{
    [System.Serializable]
    public class MovieCameraElement
    {
        [SerializeField] public Vector3 startPosition;
        [SerializeField] public Vector3 startRotation;
        [SerializeField] public Vector3 targetPosition;
        [SerializeField] public Vector3 targetRotation;

        [SerializeField] public float moveTime;
    }

    [SerializeField] private MovieCameraElement[] elements;
    [SerializeField] private int knotNumber;
    private Vector3 targetPos;
    private Vector3 targetRot;
    private int moveTimeBuff;
    [SerializeField] private float fadeoutTime;



    private bool ready;
    private bool isMove;
    private int number;
    private Vector3 posBuff;
    private Vector3 rotBuff;
    private Vector3 posRange;
    private Vector3 rotRange;
    private Vector3 moveSpeed;
    private Vector3 RotSpeed;
    private int shadelevel;
    private bool isSkip;
    private bool isEnd;

    Transform tf;
    //カメラを動かす関数
    public void CameraController()
    {
        Move();    
        SetNext();
    }
    //移動させる関数
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSkip = true;
        }
        if (isMove)
        {
            if (moveTimeBuff > 0)
            {


                posBuff += moveSpeed;
                rotBuff += RotSpeed;

                SetTransform();

            }
            else
            {

                ready = false;
                isMove = false;
            }
            moveTimeBuff--;
        }

    }
    //次に移動するために必要なものを準備する
    private void SetNext()
    {
        if (!ready)
        {
            if (knotNumber > number)
            {
                posBuff = elements[number].startPosition;
                rotBuff = elements[number].startRotation;

                targetPos = elements[number].targetPosition;
                targetRot = elements[number].targetRotation;

                TimeCountScript.SetTime(ref moveTimeBuff, elements[number].moveTime);

                posRange = targetPos - posBuff;
                rotRange = targetRot - rotBuff;

                moveSpeed = posRange / moveTimeBuff;
                RotSpeed = rotRange / moveTimeBuff;
    

                number++;
                ready = true;
                isMove = true;
            }
            else
            {
                isEnd = true;
                
            }
        }
    }
    private void SetTransform()
    {
        tf.position = posBuff;
        tf.eulerAngles=rotBuff;
    }
    public bool GetEnd()
    {
        return  isEnd;
    }
    public bool GetSkip()
    {
        return isSkip;
    }
    public float GetMoveTime()
    {
        return moveTimeBuff;
    }
    public float GetFadeoutTime()
    {
        return fadeoutTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        isSkip = false;
        ready = false;
       
        tf= GetComponent<Transform>();
        number = 0;
        SetNext();
        isMove = true;
        isEnd = false;
        fadeoutTime *= 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
