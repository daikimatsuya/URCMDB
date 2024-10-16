using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.Editor;
using UnityEngine;

//インゲーム開始時のカメラ演出時の演出
public class MovieFade : MonoBehaviour
{
    [SerializeField] private bool upside;
    [SerializeField] private float parfectShadePos;
    [SerializeField] private float movieShadePos;
    [SerializeField] private float openlyPos;
    [SerializeField] private float moveSpeed;

    private int shadeLevel;

    private void MovieFadeController()
    {
        if(shadeLevel == 0)
        {
            Openly();
        }
        else if(shadeLevel == 1)
        {
            MovieShade();
        }
        else
        {
            ParfectSgade();
        }
    }
    private void ParfectSgade()
    {

    }
    private void MovieShade()
    {

    }
    private void Openly()
    {

    }
    public void SetShadeLevel(int level)
    {
        shadeLevel = level;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(upside)
        {
            moveSpeed *= 1;
        }
        else
        {
            moveSpeed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovieFadeController();
    }
}
