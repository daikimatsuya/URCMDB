using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPScript : MonoBehaviour
{
    private ExplodeEffectScript ees;

    [SerializeField] private float chargeSize;
    [SerializeField] private float explodeSize;
    [SerializeField] private float explodeTime;
    private int explodeTimeBuff;
    [SerializeField] private float firstDissolveValue;
    [SerializeField] private Vector3 chargeOffsetSpeed;

    Rigidbody rb;

    public void Explode()
    {

    }
    public void Charge()
    {

    }
    public void Deploy()
    {

    }


    public void StartEMP(in bool isDeploy)
    {
        ees=GetComponent<ExplodeEffectScript>();
        ees.StartExplodeEffect();
        rb= GetComponent<Rigidbody>();
        if (isDeploy) 
        {
            rb.mass = 3.0f;
        }
        else
        {
            rb.mass = 0.0f;
        }

    }
}
