using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour
{
    [SerializeField] private bool setInitialPos;
    [SerializeField] private bool zoomIn;
    [SerializeField] private bool zoomOut;

    [SerializeField] private float zoomLength;

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

        }
        else
        {

        }
    }
    private void ZoomOUT()
    {
        if (setInitialPos)
        {

        }
        else
        {

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
