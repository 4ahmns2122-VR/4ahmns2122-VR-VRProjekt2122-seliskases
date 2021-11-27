// Noise generation by: https://github.com/przemyslawzaworski/Unity3D-CG-programming

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

        [Header(Noise Generation)]
        _OffsetX ("OffsetX",Float) = 0.0
        _OffsetY ("OffsetY",Float) = 0.0      
        _Octaves ("Octaves",Int) = 7
        _Lacunarity("Lacunarity", Range( 1.0 , 5.0)) = 2
        _Gain("Gain", Range( 0.0 , 1.0)) = 0.5
        _Value("Value", Range( -2.0 , 2.0)) = 0.0
        _Amplitude("Amplitude", Range( 0.0 , 5.0)) = 1.5
        _Frequency("Frequency", Range( 0.0 , 6.0)) = 2.0
        _Power("Power", Range( 0.1 , 5.0)) = 1.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
         
        CGPROGRAM

        #pragma target 3.0
        #pragma surface surf Standard addshadow vertex:vert

        #pragma multi_compile FOOTSTEPS_ON FOOTSTEPS_OFF
        #pragma multi_compile NOISEOFFSET_ON NOISEOFFSET_OFF
 
        sampler2D _MainTex;
 
        struct Input {
            float2 uv_MainTex;
            float2 uv_MainNormal;
            float2 uv_SnowNormal;
            float2 uv_SnowTexture;
            float3 worldNormal;

            float3 test;        
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

        float _Octaves;
        float _Lacunarity;
        float _Gain;
        float _Value;
        float _Amplitude;
        float _Frequency;
        float _Power;
        float _OffsetX;
        float _OffsetY;
        float _Range;

        float SampleNoise (float2 position) {
            position = position * _Frequency + float2(_OffsetX, _OffsetY);

            for (int i = 0; i < _Octaves; i++) {
                float2 i = floor(position * _Frequency);
                float2 f = frac(position * _Frequency);      
                float2 t = f * f * f * ( f * ( f * 6.0 - 15.0 ) + 10.0 );
                float2 a = i + float2( 0.0, 0.0 );
                float2 b = i + float2( 1.0, 0.0 );
                float2 c = i + float2( 0.0, 1.0 );
                float2 d = i + float2( 1.0, 1.0 );
                a = -1.0 + 2.0 * frac( sin( float2( dot( a, float2( 127.1, 311.7 ) ),dot( a, float2( 269.5,183.3 ) ) ) ) * 43758.5453123 );
                b = -1.0 + 2.0 * frac( sin( float2( dot( b, float2( 127.1, 311.7 ) ),dot( b, float2( 269.5,183.3 ) ) ) ) * 43758.5453123 );
                c = -1.0 + 2.0 * frac( sin( float2( dot( c, float2( 127.1, 311.7 ) ),dot( c, float2( 269.5,183.3 ) ) ) ) * 43758.5453123 );
                d = -1.0 + 2.0 * frac( sin( float2( dot( d, float2( 127.1, 311.7 ) ),dot( d, float2( 269.5,183.3 ) ) ) ) * 43758.5453123 );
                float A = dot( a, f - float2( 0.0, 0.0 ) );
                float B = dot( b, f - float2( 1.0, 0.0 ) );
                float C = dot( c, f - float2( 0.0, 1.0 ) );
                float D = dot( d, f - float2( 1.0, 1.0 ) );
                float noise = ( lerp( lerp( A, B, t.x ), lerp( C, D, t.x ), t.y ) );              
                _Value += _Amplitude * noise;
                _Frequency *= _Lacunarity;
                _Amplitude *= _Gain;
            }

            _Value = clamp( _Value, -1.0, 1.0 );
            return pow(_Value * 0.5 + 0.5, _Power);
        }   

        void vert(inout appdata_full data, out Input IN) {
            UNITY_INITIALIZE_OUTPUT(Input, IN);

            float3 worldNormals = mul((float3x3)unity_ObjectToWorld, data.normal.xyz);
            half displacementMask = dot(worldNormals, normalize(_SnowDirection));
            half displacementMask01 = clamp(displacementMask, 0, 1);

            float3 displacementDir = normalize(data.normal.xyz + _SnowDirection.xyz * _SnowSharpness);
            data.vertex.xyz += displacementMask01 * _SnowDisplacementStrength * displacementDir;

            #if NOISEOFFSET_ON
            float noise = SampleNoise(displacementDir.xz);
            data.vertex.xyz += displacementMask01 * noise * displacementDir;
            #endif
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