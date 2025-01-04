using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

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
        Move();    //移動演出
        SetNext();  //次の座標を設定
    }
    //移動させる関数
    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //演出スキップ
            isSkip = true;
        }
        if (isMove) //演出させる/////////////////////////////////////////////////////
        {
            if (TimeCountScript.TimeCounter(ref moveTimeBuff))
            {
                //座標移動
                posBuff += moveSpeed;
                rotBuff += RotSpeed;

                SetTransform();//ポジションとローテーションをトランスフォームに代入
            }
            else
            {
                //終了フラグセット
                ready = false;
                isMove = false;
            }
        }////////////////////////////////////////////////////////////////////////////

    }
    //次に移動するために必要なものを準備する
    private void SetNext()
    {
        if (!ready) //演出準備が出来ていない時////////////////////////////////////////////////////////////////////////////////
        {
            if (knotNumber > number)    //設定した演出個数以内なら///////////////////////////////////////
            {
                //初期値と目標値を設定
                posBuff = elements[number].startPosition;
                rotBuff = elements[number].startRotation;

                targetPos = elements[number].targetPosition;
                targetRot = elements[number].targetRotation;
                ///////////////////////

                //演出時間セット
                TimeCountScript.SetTime(ref moveTimeBuff, elements[number].moveTime);

                //移動速度算出
                posRange = targetPos - posBuff;
                rotRange = targetRot - rotBuff;

                moveSpeed = posRange / moveTimeBuff;
                RotSpeed = rotRange / moveTimeBuff;
                /////////////

                //フラグ類セット
                number++;
                ready = true;
                isMove = true;
                ////////////////

            }///////////////////////////////////////////////////////////////////////////////////////////////

            else   //設定した回数演出が終わった///
            {
                isEnd = true;   //演出終了フラグオン               
            }//////////////////////////////////////

        }///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    private void SetTransform()
    {
        tf.position = posBuff;
        tf.eulerAngles=rotBuff;
    }
    #region 値受け渡し
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
    #endregion
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
