Shader "Unlit/RadialBlurWithDithering"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {} // 入力テクスチャ
        _BlurCenter("Blur Center", Vector) = (0.5, 0.5, 0, 0) // ブラーの中心
        _BlurIntensity("Blur Intensity", Range(0, 1)) = 0.5 // ブラーの強度
        _SampleCount("Sample Count", Range(1, 64)) = 16 // サンプリング回数
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    UNITY_FOG_COORDS(1)
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float2 _BlurCenter;
                float _BlurIntensity;
                int _SampleCount;

                // ディザリング用ノイズ関数
                float2 GenerateDitherNoise(float2 uv)
                {
                    // 擬似ランダムノイズ生成
                    return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
                }

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o, o.vertex);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float2 uv = i.uv;
                    float2 center = _BlurCenter;
                    float intensity = _BlurIntensity;
                    int sampleCount = _SampleCount;

                    fixed4 color = fixed4(0, 0, 0, 0);
                    float totalWeight = 0.0;

                    float2 noiseOffset = GenerateDitherNoise(uv); // ノイズ生成

                    // 放射状サンプリング + ディザリング
                    for (int j = 0; j < sampleCount; j++)
                    {
                        float t = (float)j / (sampleCount - 1);

                        // ノイズを加味したサンプリング座標
                        float2 sampleUV = lerp(uv, center, t * intensity) + noiseOffset * intensity*0.01; // intensityはノイズのスケール
                        fixed4 sampleColor = tex2D(_MainTex, sampleUV);
                        color += sampleColor;
                    }

                    color /= sampleCount;
                    color.a = 1.0; // アルファを固定値に

                    UNITY_APPLY_FOG(i.fogCoord, color);

                    return color;
                }
                ENDCG
            }
        }
}
