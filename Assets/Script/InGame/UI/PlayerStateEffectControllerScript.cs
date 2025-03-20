using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateEffectControllerScript : MonoBehaviour
{
    private PlayerControllerScript pcs;

    [SerializeField] private EMPEffectScript ees;
    [SerializeField] private RedBoostEffectScript rbes;

    public void PlayerStateEffectController()
    {
        if (pcs.GetPlayer() == null)
        {
            AllOff();
            return;
        }
        ees.EMPEffectController(pcs.GetPlayer().GetIsEMP());
        rbes.RedBoostEffectController(pcs.GetPlayer().GetIsRedBoost());
    }
    private void AllOff()
    {
        ees.Off();
        rbes.Off();
    }
    public void StartPlayerStateEffectController(in PlayerControllerScript pcs)
    {
        this.pcs = pcs;
        AllOff();
    }
}
