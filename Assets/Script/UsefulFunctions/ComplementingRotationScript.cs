using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{
    //‰ñ“]‚·‚éƒIƒuƒWƒFƒNƒg‚Ì‰ñ“]‚ð•âŠ®‚·‚é
    public class ComplementingRotationScript : MonoBehaviour
    {
        //‰ñ“]•âŠÔ
        public float Rotate(float rotateSpeed, float targetRot, float objectRot)
        {
            //Œ»Ý‚ÌŠp“x‚Æ–Ú•WŠp“x‚Ì·‚ðŒvŽZ
            int rot;
            rot = (int)(targetRot - objectRot);
            ///////////////////////////////////

            //”½‘Î•ûŒü‚É‰ñ‚Á‚½‚Ù‚¤‚ª‹ß‚¢ê‡‚Ì·‚ðC³
            if (rot > 180)
            {
                rot = -360 + rot;
            }
            if (rot < -180)
            {
                rot = 360 + rot;
            }
            /////////////////////////////////////////////

            //Šp“x‰ÁŽZ////////////////////
            if (rot == 0)
            {
                return 0;
            }

            if (rot < 0)
            {
                if (rot > rotateSpeed)
                {
                    return rot;
                }
                return -rotateSpeed;
            }

            if (rot > rotateSpeed)
            {
                return rotateSpeed;
            }
            return rot;
            /////////////////////////////
        }

    }
}