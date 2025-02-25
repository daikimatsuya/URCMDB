using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoll : MonoBehaviour
{
    Transform tf;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y+speed, tf.localEulerAngles.z);
    }
}
