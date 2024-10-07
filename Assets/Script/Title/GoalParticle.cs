using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalParticle : MonoBehaviour
{
    private TitlegameScript ts;
    ParticleSystem ps;

    
    private void PariticleController()
    {
        if (ts.GetGoalActionFlag())
        {
            ps.Play();
        }
        else
        {
            ps.Pause();
            ps.Clear();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ts=GameObject.FindWithTag("miniManager").GetComponent<TitlegameScript>();
        ps=GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        PariticleController();
    }
}
