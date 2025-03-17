using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SensorScript;

//ƒvƒŒƒCƒ„[‚ªEMP‚É‚ ‚½‚Á‚½Žž‚Ìˆ—
public class PlayerHitEMPScript : MonoBehaviour
{
    [SerializeField] private float empLevel;
    [SerializeField] private float dicreaseEMP;
    [SerializeField] private float shockPower;
    [SerializeField] private float zonePower;
    [SerializeField] private float maxEMP;

    //‰e‹¿‚ðŽó‚¯‚Ä‚¢‚é‚©
    public int EMPAfect()
    {

        if(empLevel > 0)
        {
            return -1;
        }
        return 1;
    }

    //‰e‹¿Œ¸­
    public void Dicrease(in bool isHit)
    {
        if (!isHit)
        {
            if (empLevel > maxEMP)
            {
                empLevel = maxEMP;
            }
            if (empLevel > 0)
            {
                empLevel -= dicreaseEMP;
                if (empLevel < 0)
                {
                    empLevel = 0;
                }
            }
        }

    }

    //”g‚É‚ ‚½‚é
    public void HitEMPShock()
    {
        empLevel += shockPower;
    }
    //ƒGƒŠƒA‚É“ü‚é
    public void HitEMPZone(in bool isHit)
    {
        if (isHit)
        {
            empLevel += zonePower;
        }
    }
    //‰Šú‰»
    public void StartPlayerHitEMP()
    {
        empLevel = 0;
    }
}
