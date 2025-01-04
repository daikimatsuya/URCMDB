using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usefull
{

    //くっつけたオブジェクトを回転させる
    public class RollingScript : MonoBehaviour
    {
        [SerializeField] private float rowSpeed;
        private float rowSpeedBuff;
        [SerializeField] private bool x;
        [SerializeField] private bool y;
        [SerializeField] private bool z;

        Transform tf;
        private void Yakekuso(Vector3 vec)
        {
            if (vec == tf.localEulerAngles)
            {
                return;
            }
            tf.localEulerAngles = vec;
            Yakekuso(vec);
        }
        //回転した座標をトランスフォームに入れる
        private void Rolling()
        {

            if (x)//x軸を回転させる
            {
                float buff = tf.localEulerAngles.x + rowSpeedBuff;
                Vector3 testBuff = new Vector3(buff, 0, 0);

                tf.localEulerAngles = Vector3.zero;
                tf.localEulerAngles = testBuff;//ここの代入がなぜかバグってる
                                               //Yakekuso(testBuff);

                //tf.localRotation = Quaternion.Euler(testBuff);
            }
            if (y)//y軸を回転させる
            {
                tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y + rowSpeedBuff, tf.localEulerAngles.z);
            }
            if (z)//z軸を回転させる
            {
                tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y, tf.localEulerAngles.z + rowSpeedBuff);
            }

        }
        //回転速度を外部からセット
        public void SetRowSpeed(float rowSpeed)
        {
            this.rowSpeedBuff = rowSpeed;
        }
        //回転速度を返す
        public float GetRowSpeed()
        {
            return rowSpeed;
        }
        //回転速度を初期値にリセット
        public void ResetRowSpeed()
        {
            rowSpeedBuff = rowSpeed;
        }
        // Start is called before the first frame update
        void Start()
        {
            tf = GetComponent<Transform>();
            rowSpeedBuff = rowSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            Rolling();
        }
    }
}
