using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassScript : MonoBehaviour
{
    Transform tf;
    private void StopRot()
    {
        tf.eulerAngles = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();  
    }

    // Update is called once per frame
    void Update()
    {
        StopRot();
    }
}
