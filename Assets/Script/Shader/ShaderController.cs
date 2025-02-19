using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���f�B�A���u���[��������
public class ShaderController : MonoBehaviour
{
    [SerializeField] private Shader radialBlur;
    [SerializeField,Range(0.0f,1.0f)] private float intensity;
    
    private Material material;
    private PlayerControllerScript pcs;

    //�u���[�̋��x����
    public void BlurController()
    {
        if(pcs.GetPlayer() != null)  //�v���C���[��������u���[���x���グ��//
        {
            intensity = pcs.GetPlayer().GetBlurIntensity();
        }/////////////////////////////////////////////////////////////

        else
        {
            intensity = 0.0f;
        }
        material.SetFloat("_BlurIntensity", intensity); //�u���[���x�ݒ�
    }

    //�u���[��������
    private void OnRenderImage(RenderTexture source,  RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);   //�u���[��������
    }
    // Start is called before the first frame update
    public void StartShaderController(in PlayerControllerScript pcs)
    {
        this.pcs = pcs; 
        material = new Material(radialBlur);
        intensity = 0;
    }

}
