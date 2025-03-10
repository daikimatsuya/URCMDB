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

    public void Explode()
    {

    }
    public void Charge()
    {

    }
    public void StartEMP()
    {
        ees=GetComponent<ExplodeEffectScript>();
        ees.StartExplodeEffect();
    }
}
