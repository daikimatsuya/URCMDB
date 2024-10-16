using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGameScrollScript : MonoBehaviour
{
    [SerializeField] float scrollSpeed;

    Transform tf;
    TitlegameScript ts;

    private Vector3 initialPos;

    private void ScrollController()
    {
        if (ts.GetMoveFlag())
        {
            Scroll();
        }

        if(ts.GetResetFlag())
        {
            ScrollReset();
        }
    }
    private void Scroll()
    {
        tf.position = new Vector3(tf.position.x-scrollSpeed, tf.position.y, tf.position.z);
    }
    private void ScrollReset()
    {
        tf.position=initialPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        ts = GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();

        initialPos = ts.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollController();
    }
}