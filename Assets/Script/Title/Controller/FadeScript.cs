using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//‰ñ“]‚³‚¹‚é
public class FadeScript : MonoBehaviour
{
    private RollingScript rs;
    private float rowSpeed = -1f;
    // Start is called before the first frame update
    void Start()
    {
        rs=new RollingScript();
    }

    // Update is called once per frame
    void Update()
    {
        rs.Rolling(this.gameObject.transform, rowSpeed, "z");
    }
}
