using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCamera : MonoBehaviour
{
    [SerializeField] private Vector3 centerPos;
    [SerializeField] private float directionX;
    [SerializeField] private float distance;
    [SerializeField] private float rotateSpeed;

    private Vector3 pos;
    public Vector3 ExplodeCameraController(ref Vector3 rotation)
    {
        Move(rotation);
        rotation = new Vector3(directionX, rotation.y + rotateSpeed, 0);
        return pos;
    }
    private void Move(Vector3 rotation)
    {
        pos.x = -distance * (float)Math.Sin(ToRadian(rotation.y));
        pos.z = -distance * (float)Math.Cos(ToRadian(rotation.y));

        pos.x = pos.x * (float)Math.Cos(ToRadian(rotation.x));
        pos.z = pos.z * (float)Math.Cos(ToRadian(rotation.x));

        pos.y = -distance * (float)Math.Sin(ToRadian(rotation.x)) * -1;
    }
    public double ToRadian(double angle)
    {
        return angle * Math.PI / 180f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
