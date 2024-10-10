using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeMissleActionScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float animationTime;
    private int animationTimeBuff;

    private SceneChangeAnimationScript scas;
    Transform tf;

    private void SCMAController()
    {
        if (scas.GetIsShotFlag())
        {
            Shoot();
        }
    }
    private void Shoot()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        scas = GameObject.FindWithTag("LaunchBase").GetComponent<SceneChangeAnimationScript>();
        tf=GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SCMAController();
    }
}
