using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoParentingScript : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Vector3 fixRotate;
    [SerializeField] Vector3 fixPos;
    Transform tf;

    private void Parent()
    {
        if (!parent)
        {
            return;
        }
            //Vector3 pos = parent.position + fixPos;
            //tf.position = pos;
            //Vector3 rotate = parent.eulerAngles + fixRotate;
            //tf.eulerAngles = rotate;
        tf.transform.SetParent(parent);
        tf.localPosition = fixPos;
        tf.localEulerAngles += fixRotate;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        tf = GetComponent<Transform>();
        
        Parent();
    }
    void Start()
    {
        //Parent();
    }

    // Update is called once per frame
    void Update()
    {
        //Parent();
    }
}
