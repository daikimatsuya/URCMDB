using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//‰Q’ªŠÇ—
public class WhirlpoolsScript : MonoBehaviour
{
    [System.Serializable]
    public class WhirlpoolsObjects
    {
        [SerializeField] public Transform tf;
        [SerializeField] public float rotSpeed;
    }

    [SerializeField] private WhirlpoolsObjects[] children;

    Transform tf;

    [SerializeField] private float rotSpeed;

    //‰Q‚ğ‰ñ‚·
    private void RotChiildren()
    {
        for (int i = 0; i < children.Length; i++)
        {
            RollingScript.Rolling(children[i].tf, children[i].rotSpeed, "y");
        }
    }

    //‰Q‘S‘Ì‚ğ‰ñ‚·
    private void Rot()
    {
        RollingScript.Rolling(tf, rotSpeed, "y");
    }

    //XV
    public void UpdateWhirlpools()
    {
        Rot();
        RotChiildren();
    }

    //‰Šú‰»
    public void StartWhirlpools()
    {
        tf = GetComponent<Transform>();
    }

}
