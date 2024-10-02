using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheidScript : MonoBehaviour
{
    Transform tf;
    TitlegameScript ts;

    private Vector3 initialPos;
    private float moveSpeed;
    private bool isAction;

    private void SheidController()
    {
        if(isAction)
        {
            Move();
        }
    }
    private void Move()
    {

    }
    private void ResetPos()
    {

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
