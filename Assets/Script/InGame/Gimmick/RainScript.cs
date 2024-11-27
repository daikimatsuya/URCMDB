using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//âJä«óù
public class RainScript : MonoBehaviour
{
    Transform cameraPos;
    Transform tf;

    [SerializeField] private float minDistance;

    private void Move()
    {
        tf.position = new Vector3(cameraPos.position.x, cameraPos.position.y + minDistance, cameraPos.position.z);
    }
    public void SetCameraTransform(Transform cameraPos)
    {
         this.cameraPos = cameraPos;
    }
    // Start is called before the first frame update
    void Start()
    {
        tf=GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
