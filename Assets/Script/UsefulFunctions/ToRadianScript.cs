using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    public class ToRadianScript : MonoBehaviour
    {
        //�f�O�����b�h�ɕϊ�
        static public double ToRadian(ref float angle)
        {
            return angle * Math.PI / 180f;  //degree��radian�ɕϊ�
        }
    }
}
