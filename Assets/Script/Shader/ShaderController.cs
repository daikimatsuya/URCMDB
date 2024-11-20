using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
    [SerializeField] private Shader radialBlur;

    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = new Material(radialBlur);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
