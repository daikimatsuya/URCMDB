using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplementingRotationScript : MonoBehaviour
{
    public float Rotate(float rotateSpeed,float targetRot,float objectRot)
    {

        int rot;

        rot = (int)(targetRot-objectRot);

        if (rot > 180)
        {
            rot = -360 + rot;
        }
        if (rot < -180)
        {
            rot = 360 + rot;
        }


        if (rot == 0)
        {
            return 0;
        }

        if(rot < 0)
        {

            if (rot > rotateSpeed)
            {
                return rot;
            }      
            return -rotateSpeed;
        }

        if (rot > rotateSpeed)
        {
            return rotateSpeed;
        }
        return rot;

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
