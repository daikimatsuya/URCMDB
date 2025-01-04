using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレハブとかの影響でゲーム外で親子付けが難しいときにスクリプトから親子付けする
public class PseudoParentingScript : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Vector3 fixRotate;
    [SerializeField] Vector3 fixPos;
    Transform tf;

    //親子付けする
    private void Parent()
    {
        if (!parent)    //親子付け先が設定されていなかったらreturnを返す////
        {
            return;
        }//////////////////////////////////////////////////////////////////////

        tf.transform.SetParent(parent); //親子付け
        tf.localPosition = fixPos;  //ポジションを修正
        tf.localEulerAngles += fixRotate;   //角度を修正
    }

    // Start is called before the first frame update
    private void Awake()
    {
        tf = GetComponent<Transform>();   
        Parent();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
