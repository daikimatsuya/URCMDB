Shader "Unlit/Texture"
{
Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex("Texture",2D)="white"{}
        _Dissolve("Dissolve",Range(0,1))=0.5
        _Threshold("DissolveThreshold",Range(0,1))=0.5
        _EdgeColor ("Edge Color", Color) = (0, 0, 0, 1)
        _EdgeWidth ("Edge Width", Range(0, 1)) = 0.01
        _Alfa("Alfa",Range(0,1))=1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 100

        CGINCLUDE
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
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
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _MaskTex;
            float4 _MaskTex_ST;
            float _Dissolve;
            fixed4 _Color;
            float _Threshold;
            fixed4 _EdgeColor;
            float _EdgeWidth;
            float _Alfa;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

        ENDCG

        Pass
        {
            Cull Off
            ZWrite Off
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
               
            fixed4 frag (v2f i) : SV_Target
            {
                float2 tiling = _MainTex_ST.xy;
                float2 offset = _MainTex_ST.zw;
                float2 tilingMask = _MaskTex_ST.xy;
                float offsetMask = _MaskTex_ST.zw;

                fixed4 col = tex2D(_MainTex, i.uv * tiling + offset);
                fixed4 colBuff=col;
                fixed4 mask = tex2D(_MaskTex, i.uv * tilingMask + offsetMask);
                
                // Dissolve effect
                if (_Dissolve >= 1)
                {
                clip(mask.r - (_Dissolve + 1));
                }
                else
                {
                clip(mask.r - (_Dissolve));
                }
                clip(colBuff.a - mask.r);

                // Edge effect
                float edgeMask = step(_Dissolve, mask.r) - step(_Dissolve + _Threshold, mask.r);
                colBuff.rgb = colBuff.rgb * (1 - edgeMask) + _EdgeColor.rgb * edgeMask;

                col=fixed4(colBuff.r,colBuff.g,colBuff.b,col.a*_Alfa);
                return col;
            }

            ENDCG
         }

        Pass
        {

            Cull Off
            ZWrite Off
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            fixed4 frag (v2f i) : SV_Target
            {
                float2 tiling = _MainTex_ST.xy;
                float2 offset = _MainTex_ST.zw;
                float2 tilingMask = _MaskTex_ST.xy;
                float offsetMask = _MaskTex_ST.zw;

                fixed4 col = tex2D(_MainTex, i.uv * tiling + offset);
                fixed4 colBuff=col;
                fixed4 mask = tex2D(_MaskTex, i.uv * tilingMask + offsetMask);
                
                // Dissolve effect
                if (_Dissolve >= 1)
                {
                    clip(mask.r - (_Dissolve + 1));
                }
                else
                {
                    clip(mask.r - (_Dissolve));
                }
                clip(colBuff.a - mask.r);



                // Edge effect
                float edgeMask = step(_Dissolve, mask.r) - step(_Dissolve + _Threshold, mask.r);
                colBuff.rgb = colBuff.rgb * (1 - edgeMask) + _EdgeColor.rgb * edgeMask;

                col=fixed4(colBuff.r,colBuff.g,colBuff.b,col.a*_Alfa);
                return col;
            }
            ENDCG
        }
    }
}
