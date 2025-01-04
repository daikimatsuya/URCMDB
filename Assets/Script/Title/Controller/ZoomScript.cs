using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ズームするような動きを管理
public class ZoomScript : MonoBehaviour
{
    [SerializeField] private bool setInitialPos;//初期値に値を足して位置をずらしてから初期値まで動かす
    [SerializeField] private bool zoomIn;
    [SerializeField] private bool zoomOut;

    [SerializeField] private float zoomLength;
    private float lengthBuff;
    [SerializeField] private float zoomTime;
    private int timeBuff;

    Transform tf;

    private float PosBuffZ;
    private Vector3 initialPos;
    //ズームインアウト管理
    private void ZoomController()
    {
        if(zoomIn)
        {
            ZoomIn();   //ズームインっぽい動きをさせる
        }
        if(zoomOut)
        {
            ZoomOUT();  //ズームアウトっぽい動きをさせる
        }
    }
    //ズームインのような動きをさせる
    private void ZoomIn()
    {
        if (setInitialPos)  //初期値まで動かす場合///////////////////////////////////////////////////
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ -= lengthBuff; //座標加算
            }
            else
            {
                zoomIn = false; //移動終了
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ + zoomLength);   //座標代入
        }////////////////////////////////////////////////////////////////////////////////////////////

        else   //初期値から動かす場合//////////////////////////////////////////////////////////////
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ -= lengthBuff; //座標加算
            }
            else
            {
                zoomIn = false; //移動終了
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ);   //座標代入
        }///////////////////////////////////////////////////////////////////////////////////////////
    }
    //ズームアウトのような動きをさせる
    private void ZoomOUT()
    {
        if (setInitialPos)  //初期値まで動かす場合///////////////////////////////////////////////////
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ += lengthBuff; //座標加算
            }
            else
            {
                zoomIn = false; //移動終了
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ - zoomLength);   //座標代入
        }////////////////////////////////////////////////////////////////////////////////////////////

        else   //初期値から動かす場合//////////////////////////////////////////////////////////////
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ += lengthBuff; //座標加算
            }
            else
            {
                zoomIn = false; //移動終了
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ);   //座標代入
        }///////////////////////////////////////////////////////////////////////////////////////////
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
        initialPos = tf.position;
        PosBuffZ = initialPos.z;

        timeBuff = (int)(zoomTime * 60);
        lengthBuff= zoomLength /timeBuff;
    }

    // Update is called once per frame
    void Update()
    {
        ZoomController();
    }
}
