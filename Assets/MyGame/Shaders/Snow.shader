// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Snow" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _MainNormal ("MainNormal", 2D) = "bump" {}
         
        [Header(Snow info)]
        _SnowTexture("Snow Texture", 2D) = "white" {}
        _SnowNormal("Snow Normal", 2D) = "bump" {}
        _SnowColor("Snow Color", color) = (1,1,1,1)
        _SnowDirection ("Snow Direction", Vector) = (0, 1, 0)
        _SnowFalloff ("Snow Falloff", Range(0, 1)) = 0.5
        _SnowGlossiness("Snow Glossiness", Range(0, 1)) = 0.5
        _SnowDisplacementStrength ("Snow Dislpacement Strength", Range(0, 1)) = 0
        _SnowSharpness ("Snow Sharpness", Range(0, 4)) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
         
        CGPROGRAM

        #pragma target 3.0
        #pragma surface surf Standard addshadow vertex:vert

        #pragma multi_compile FOOTSTEPS_ON FOOTSTEPS_OFF
 
        sampler2D _MainTex;
 
        struct Input {
            float2 uv_MainTex;
            float2 uv_MainNormal;
            float2 uv_SnowNormal;
            float2 uv_SnowTexture;
            float3 worldNormal;         
            INTERNAL_DATA
        };
 
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        sampler2D _MainNormal;
 
        sampler2D _SnowTexture;
        sampler2D _SnowNormal;
        fixed4 _SnowColor;
        float4 _SnowDirection;
        float _SnowFalloff;
        float _SnowGlossiness;
        float _SnowDisplacementStrength;
        float _SnowSharpness;

        void vert(inout appdata_full data, out Input IN) {
            UNITY_INITIALIZE_OUTPUT(Input, IN);

            float3 normals = mul((float3x3)unity_ObjectToWorld, data.normal.xyz);

            half displacementMask = dot(normals, normalize(_SnowDirection));
            half displacementMask01 = clamp(displacementMask, 0, 1);

            float3 displacementDir = normalize(data.normal.xyz + _SnowDirection.xyz * _SnowSharpness);
            data.vertex.xyz += displacementMask01 * _SnowDisplacementStrength * displacementDir;
        }   
 
        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            float3 normals = UnpackNormal (tex2D(_MainNormal, IN.uv_MainNormal));

            fixed4 snowColor = tex2D(_SnowTexture, IN.uv_SnowTexture) * _SnowColor;
            float3 snowNormals = UnpackNormal(tex2D(_SnowNormal, IN.uv_SnowNormal));
            half snowDot = dot(WorldNormalVector(IN, normals), normalize(_SnowDirection));
            half snowDot01 = clamp(snowDot, 0, 1);
            float t = -exp(snowDot01 * (100 * _SnowFalloff - 100)) * (1 - snowDot01) + 1;
 
            o.Normal = lerp(normals, snowNormals, t);
            o.Albedo = lerp(c.rgb, snowColor.rgb, t);
            o.Metallic = lerp(_Metallic, 0, t);
            o.Smoothness = lerp(_Glossiness, _SnowGlossiness, t);
            o.Alpha = c.a;
        }

        ENDCG
    }

    CustomEditor "SnowShaderInspector"
    FallBack "Diffuse"
}