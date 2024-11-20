using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ラディアンブラーをかける
public class ShaderController : MonoBehaviour
{
    [SerializeField] private Shader radialBlur;
    [SerializeField,Range(0.0f,1.0f)] private float intensity;
    

    private Material material;
    private PlayerScript player;

    //ブラーの強度を代入
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
    //ブラーの強度を他スクリプトから設定する
    public void SetPlayer(PlayerScript ps)
    {
        player = ps;
    }
    //ブラーをかける
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
