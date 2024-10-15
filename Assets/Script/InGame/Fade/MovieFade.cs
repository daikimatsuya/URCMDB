using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieFade : MonoBehaviour
{
    [SerializeField] private bool upside;
    [SerializeField] private bool downside;
    [SerializeField] private float parfectShadePos;
    [SerializeField] private float movieShadePos;
    [SerializeField] private float openlyPos;

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
        if(upside)
        {

        }
        else
        {

        }
    }
    private void MovieShade()
    {
        if (upside)
        {

        }
        else
        {

        }
    }
    private void Openly()
    {
        if (upside)
        {

        }
        else
        {

        }
    }
    public void SetShadeLevel(int level)
    {
        shadeLevel = level;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
