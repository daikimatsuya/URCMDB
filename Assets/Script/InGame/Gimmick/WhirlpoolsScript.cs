using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Usefull;

//渦潮管理
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

    //渦を回す
    private void RotChiildren()
    {
        for (int i = 0; i < children.Length; i++)
        {
            RollingScript.Rolling(children[i].tf, children[i].rotSpeed, "y");
        }
    }

    //渦全体を回す
    private void Rot()
    {
        RollingScript.Rolling(tf, rotSpeed, "y");
    }

    //更新
    public void UpdateWhirlpools()
    {
        Rot();
        RotChiildren();
    }

    //初期化
    public void StartWhirlpools()
    {
        tf = GetComponent<Transform>();
    }

}
