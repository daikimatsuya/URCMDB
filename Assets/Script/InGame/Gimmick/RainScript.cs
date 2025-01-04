using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//雨管理
public class RainScript : MonoBehaviour
{
    Transform cameraPos;
    Transform tf;

    [SerializeField] private float minDistance;

    //移動させる
    private void Move()
    {
        tf.position = new Vector3(cameraPos.position.x, cameraPos.position.y + minDistance, cameraPos.position.z);  //移動
    }

    //カメラの座標取得
    public void SetCameraTransform(in Transform cameraPos)
    {
         this.cameraPos = cameraPos;  //座標代入
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
