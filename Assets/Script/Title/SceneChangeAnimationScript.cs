using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeAnimationScript : MonoBehaviour
{
    [SerializeField] private GameObject missile;
    [SerializeField] private GameObject pad;
    [SerializeField] private float targetRot;
    private float rotationSpeed;
    [SerializeField] private float rotationTime;
    private float rotationTimeBuff;
    [SerializeField] private float missileMovepeed;

    TitleScript ts;


    private float initialRot;
    private float padRotBuff;

    private void AnimationController()
    {
        UpDown();
    }
    private void UpDown()
    {
        if (ts.GetIsSceneChangeModeFlag())
        {
            if (rotationTimeBuff>0)
            {
                padRotBuff += rotationSpeed;
                pad.transform.localEulerAngles = new Vector3(padRotBuff, pad.transform.localEulerAngles.y, pad.transform.localEulerAngles.z);

                rotationTimeBuff--;
            }
        }
        else
        {
            if (rotationTimeBuff < (int)(rotationTime * 60))
            {
                padRotBuff -= rotationSpeed;
                pad.transform.localEulerAngles = new Vector3(padRotBuff, pad.transform.localEulerAngles.y, pad.transform.localEulerAngles.z);

                rotationTimeBuff++;
            }
        }
    }
    private void Shoot()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        ts=GameObject.FindWithTag("TitleManager").GetComponent<TitleScript>();
        
        initialRot = pad.transform.localEulerAngles.x;
        padRotBuff = pad.transform.localEulerAngles.x;

        rotationTimeBuff = (int)(rotationTime * 60);
        rotationSpeed = targetRot / rotationTimeBuff;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationController();
    }
}
