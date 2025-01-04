using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//換気扇の回転管理
public class FunScript : MonoBehaviour
{
    private Transform tf;

    [SerializeField] float rotateSpeed;

    private Vector3 rotation;
    //ファンを回転させる
    private void RotateFun()
    {
        rotation = new Vector3(rotation.x+rotateSpeed,rotation.y,rotation.z);   //回転
        tf.localEulerAngles = rotation; //トランスフォームに代入
    }

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rotation = tf.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        RotateFun();
    }
}
