using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ラディアンブラーをかける
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

    //ブラーの強度を代入
    public void BlurController()
    {
        material.SetFloat("_BlurIntensity", intensity); //ブラー強度設定
    }


    #region 値受け渡し
    public void SetBlurIntensity(in float blurIntensity)
    {
        intensity = blurIntensity;
    }
    #endregion
    //ブラーをかける
    private void OnRenderImage(RenderTexture source,  RenderTexture destination)
    {
        if (material == null)
        {
            return;
        }
        Graphics.Blit(source, destination, material);   //ブラーをかける
    }


}
