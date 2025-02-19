using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ラディアンブラーをかける
public class ShaderController : MonoBehaviour
{
    [SerializeField] private Shader radialBlur;
    [SerializeField,Range(0.0f,1.0f)] private float intensity;
    
    private Material material;
    private PlayerControllerScript pcs;

    //ブラーの強度を代入
    public void BlurController()
    {
        if(pcs.GetPlayer() != null)  //プレイヤーがいたらブラー強度を上げる//
        {
            intensity = pcs.GetPlayer().GetBlurIntensity();
        }/////////////////////////////////////////////////////////////

        else
        {
            intensity = 0.0f;
        }
        material.SetFloat("_BlurIntensity", intensity); //ブラー強度設定
    }

    //ブラーをかける
    private void OnRenderImage(RenderTexture source,  RenderTexture destination)
    {
        Graphics.Blit(source, destination, material);   //ブラーをかける
    }
    // Start is called before the first frame update
    public void StartShaderController(in PlayerControllerScript pcs)
    {
        this.pcs = pcs; 
        material = new Material(radialBlur);
        intensity = 0;
    }

}
