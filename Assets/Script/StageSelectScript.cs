using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScript : MonoBehaviour
{

    TitleScript ts;
    Transform tf;

    private void StageSelectController()
    {
        Move(ts.GetStageCount());
    }
    private void Move(Vector2 stage)
    {
        float rot = (360 / (stage.y+1)) * stage.x;

        tf.eulerAngles=new Vector3 (0,rot,0);
    }
    // Start is called before the first frame update
    void Start()
    {
        ts = GameObject.FindWithTag("TitleManager").GetComponent<TitleScript>();
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        StageSelectController();
    }
}
