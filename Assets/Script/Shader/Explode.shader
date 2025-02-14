Shader "Simple"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex("Texture",2D)="white"{}
        _Dissolve("Dissolve",Range(0,1.1))=0.5
        _Color("DissolveColor",Color)=(1,0,0,1)
        _Threshold("DissolveThreshold",Range(0,1))=0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
            Cull front
            CGPROGRAM
            fixed4 frag (v2f i) : SV_Target
            {
                float2 tiling=_MainTex_ST.xy;
                float2 offset=_MainTex_ST.zw;
                float2 tilingMask=_MaskTex_ST.xy;
                float offsetMask=_MaskTex_ST.zw;

                fixed4 col =tex2D(_MainTex,i.uv*tiling+offset);
                fixed4 mask=tex2D(_MaskTex,i.uv*tilingMask+offsetMask);
                col.rgb = lerp(col.rgb ,1 ,step(col.a ,0));
                col=lerp(col,_Color,step(mask,_Dissolve+_Threshold));             
                col.a=lerp(1,0,step(mask,_Dissolve));
                clip(mask.r-_Dissolve);

                return col;
            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            fixed4 frag (v2f i) : SV_Target
            {
                float2 tiling=_MainTex_ST.xy;
                float2 offset=_MainTex_ST.zw;
                float2 tilingMask=_MaskTex_ST.xy;
                float offsetMask=_MaskTex_ST.zw;

                fixed4 col =tex2D(_MainTex,i.uv*tiling+offset);
                fixed4 mask=tex2D(_MaskTex,i.uv*tilingMask+offsetMask);
                col.rgb = lerp(col.rgb ,1 ,step(col.a ,0));
                col=lerp(col,_Color,step(mask,_Dissolve+_Threshold));             
                col.a=lerp(1,0,step(mask,_Dissolve));
                clip(mask.r-_Dissolve);

                return col;
            }
            ENDCG
        }
    }
}
