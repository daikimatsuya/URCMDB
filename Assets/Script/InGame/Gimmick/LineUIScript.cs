using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ó\ë™ê¸ä«óù
public class LineUIScript : MonoBehaviour
{
    Transform pos;
    LineRenderer line;

    private int timeBuff;
    private bool red;
    //private bool brock;

    [SerializeField] private float rand;
    [SerializeField] private float colorChangeTime;

    public void SetLine(Vector3 Pos,Vector3 targetLength)
    {
        line.SetPosition(0, targetLength);
        pos.position = Pos;
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
    public void SetRed()
    {
        line.startColor = Color.red;
        line.endColor = Color.red;
    }
    public void SetWarning()
    {
        if (timeBuff <= 0)
        {
            if (red)
            {
                line.startColor= Color.yellow;
                line.endColor = Color.yellow;
                SetTime();
                red = false;
            }
            else
            {
                line.startColor = Color.red;
                line.endColor = Color.red;
                SetTime();
                red = true;
            }
        }
        timeBuff--;
    }
    public void SetVoid()
    {
        line.startColor = new Color(0, 0, 0, 0);
        line.endColor = new Color(0, 0, 0, 0);    
    }
    private void SetTime()
    {
        timeBuff = (int)(colorChangeTime * 60);
    }

    public void OnTriggerStay(Collider other)
    {
        //if (other.tag != "Player")
        //{
        //      brock=true;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerPos=GameObject.FindWithTag("Player").GetComponent<Transform>();
        pos=GetComponent<Transform>();
        line = GetComponent<LineRenderer>();

        SetTime();
        red = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
