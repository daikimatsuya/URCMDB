using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトルシーンからインゲームへ演出の円が収縮していく演出管理
public class CircleFadeObjectScript : MonoBehaviour
{
    [SerializeField] private float increaseSpeed;
    [SerializeField] private float initialPosY;


    Transform tf;
    private SceneChangeAnimationScript scas;

    //演出フラグ
    private void CircleFadeController()
    {
        if(scas.GetIsFadeStartFlag())   //フェードフラグがオンになっているとフェード開始////
        {
            Fade();
        }/////////////////////////////////////////////////////////////////////////////////////////
    }
    private void Fade()
    {
        tf.localScale = new Vector3(2, 2,tf.localScale.z + increaseSpeed);  //サイズを縮小
    }
    // Start is called before the first frame update
    public void StartCircleFadeObject()
    {
        scas = GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();

        tf = GetComponent<Transform>();
        tf.localScale = Vector3.zero;
        tf.localPosition = new Vector3(0, initialPosY, 0);
    }
    void Start()
    {
        StartCircleFadeObject();
    }

    // Update is called once per frame
    void Update()
    {
        CircleFadeController();
    }
}
