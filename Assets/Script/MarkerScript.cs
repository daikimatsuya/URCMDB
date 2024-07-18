using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerScript : MonoBehaviour
{
    Transform tf;

    [SerializeField] private float markerPosY;
    private void MarkerController()
    {

    }
    public void Move(Vector3 pos)
    {
        if(tf != null)
        {
            tf.position = new Vector3(pos.x, pos.y, pos.z);
        }
        else
        {
            tf=GetComponent<Transform>();
            tf.position = new Vector3(pos.x, pos.y, pos.z);
        }
    }
    public void Delete()
    {
        Destroy(this.gameObject);
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
