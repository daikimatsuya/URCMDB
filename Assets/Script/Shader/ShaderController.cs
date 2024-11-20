using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���f�B�A���u���[��������
public class ShaderController : MonoBehaviour
{
    [SerializeField] private Shader radialBlur;
    [SerializeField,Range(0.0f,1.0f)] private float intensity;
    

    private Material material;
    private PlayerScript player;

    //�u���[�̋��x����
    private void BlurController()
    {
        if(player != null)
        {
            intensity = player.GetBlurIntensity();
        }
        else
        {
            intensity = 0.0f;
        }
        material.SetFloat("_BlurIntensity", intensity);
    }
    //�u���[�̋��x�𑼃X�N���v�g����ݒ肷��
    public void SetPlayer(PlayerScript ps)
    {
        player = ps;
    }
    //�u���[��������
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);
    }
    // Start is called before the first frame update
    void Start()
    {
        material = new Material(radialBlur);
        intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        BlurController();
    }
}
