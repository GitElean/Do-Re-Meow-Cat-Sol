Shader "Custom/BorderShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BorderColor ("Border Color", Color) = (1,1,1,1)
        _Thickness ("Thickness", Range(0, 1)) = 0.05
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float4 _BorderColor;
        float _Thickness;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float dist = distance(IN.worldPos.xy, float2(0.5, 0.5));
            if (dist > 1 - _Thickness)
                o.Albedo = _BorderColor.rgb;
            else
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
    }
}
