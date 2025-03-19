using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���f�B�A���u���[��������
public class RadialBlurController : MonoBehaviour
{
    [SerializeField] private Shader radialBlur;
    [SerializeField,Range(0.0f,1.0f)] private float intensity;
    
    private Material material;

    public void StartShaderController()
    {
        material = new Material(radialBlur);
        intensity = 0;
    }

    //�u���[�̋��x����
    public void BlurController()
    {
        material.SetFloat("_BlurIntensity", intensity); //�u���[���x�ݒ�
    }


    #region �l�󂯓n��
    public void SetBlurIntensity(in float blurIntensity)
    {
        intensity = blurIntensity;
    }
    #endregion
    //�u���[��������
    private void OnRenderImage(RenderTexture source,  RenderTexture destination)
    {
        if (material == null)
        {
            return;
        }
        Graphics.Blit(source, destination, material);   //�u���[��������
    }


}
