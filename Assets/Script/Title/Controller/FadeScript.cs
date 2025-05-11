using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//‰ñ“]‚³‚¹‚é
public class FadeScript : MonoBehaviour
{
    private float rowSpeed = -1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RollingScript.Rolling(this.gameObject.transform, rowSpeed, "z");
    }
}
