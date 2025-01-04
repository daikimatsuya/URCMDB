using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    public class ToRadianScript : MonoBehaviour
    {
        //デグをラッドに変換
        static public double ToRadian(ref float angle)
        {
            return angle * Math.PI / 180f;  //degreeをradianに変換
        }
    }
}
