using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//‰JŠÇ—
public class RainScript : MonoBehaviour
{
    Transform cameraPos;
    Transform tf;

    [SerializeField] private float minDistance;

    //ˆÚ“®‚³‚¹‚é
    private void Move()
    {
        if(cameraPos == null)
        {
            return;
        }
        tf.position = new Vector3(cameraPos.position.x, cameraPos.position.y + minDistance, cameraPos.position.z);  //ˆÚ“®
    }

    //ƒJƒƒ‰‚ÌÀ•Wæ“¾
    public void SetCameraTransform(in Transform cameraPos)
    {
         this.cameraPos = cameraPos;  //À•W‘ã“ü
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
