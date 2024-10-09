using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void ZoomController()
    {
        if(zoomIn)
        {
            ZoomIn();
        }
        if(zoomOut)
        {
            ZoomOUT();
        }
    }
    private void ZoomIn()
    {
        if (setInitialPos)
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ -= lengthBuff;
            }
            else
            {
                zoomIn = false;
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ + zoomLength);
        }
        else
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ -= lengthBuff;
            }
            else
            {
                zoomIn = false;
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ);
        }
    }
    private void ZoomOUT()
    {
        if (setInitialPos)
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ += lengthBuff;
            }
            else
            {
                zoomIn = false;
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ - zoomLength);
        }
        else
        {
            timeBuff--;
            if (timeBuff >= 0)
            {
                PosBuffZ += lengthBuff;
            }
            else
            {
                zoomIn = false;
            }
            tf.position = new Vector3(initialPos.x, initialPos.y, PosBuffZ);
        }
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
