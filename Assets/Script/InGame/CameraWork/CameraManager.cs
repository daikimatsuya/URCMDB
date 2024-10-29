using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private PlayerCameraScript pcs;
    public  void CameraController()
    {
        pcs.PlayerCameraController();
    }
    // Start is called before the first frame update
    void Start()
    {
        pcs = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCameraScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
