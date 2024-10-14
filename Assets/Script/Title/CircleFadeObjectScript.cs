using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFadeObjectScript : MonoBehaviour
{
    [SerializeField] private float increaseSpeed;
    [SerializeField] private float initialPosY;


    Transform tf;
    private SceneChangeAnimationScript scas;

    private void CircleFadeController()
    {
        if(scas.GetIsFadeStartFlag())
        {
            Fade();
        }
    }
    private void Fade()
    {
        tf.localScale = new Vector3(2, 2,tf.localScale.z + increaseSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
        scas = GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();

        tf =GetComponent<Transform>();
        tf.localScale = Vector3.zero;
        tf.localPosition=new Vector3(0,initialPosY,0);
    }

    // Update is called once per frame
    void Update()
    {
        CircleFadeController();
    }
}
