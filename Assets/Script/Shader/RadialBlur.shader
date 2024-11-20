Shader "Unlit/RadialBlurWithDithering"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {} // ���̓e�N�X�`��
        _BlurCenter("Blur Center", Vector) = (0.5, 0.5, 0, 0) // �u���[�̒��S
        _BlurIntensity("Blur Intensity", Range(0, 1)) = 0.5 // �u���[�̋��x
        _SampleCount("Sample Count", Range(1, 64)) = 16 // �T���v�����O��
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

                // �f�B�U�����O�p�m�C�Y�֐�
                float2 GenerateDitherNoise(float2 uv)
                {
                    // �[�������_���m�C�Y����
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

                    float2 noiseOffset = GenerateDitherNoise(uv); // �m�C�Y����

                    // ���ˏ�T���v�����O + �f�B�U�����O
                    for (int j = 0; j < sampleCount; j++)
                    {
                        float t = (float)j / (sampleCount - 1);

                        // �m�C�Y�����������T���v�����O���W
                        float2 sampleUV = lerp(uv, center, t * intensity) + noiseOffset * intensity*0.01; // intensity�̓m�C�Y�̃X�P�[��
                        fixed4 sampleColor = tex2D(_MainTex, sampleUV);
                        color += sampleColor;
                    }

                    color /= sampleCount;
                    color.a = 1.0; // �A���t�@���Œ�l��

                    UNITY_APPLY_FOG(i.fogCoord, color);

                    return color;
                }
                ENDCG
            }
        }
}
