Shader "Custom/Defense"
{
    Properties
    {
        _FresnelColor ("Fresnel Color", color) = (1, 1, 1, 1)
        _NoiseColor ("Noise Color", color) = (1, 1, 1, 1)
        _FresnelExponent("Fresnel Exponent", Range(0.25, 4)) = 1
     
        _Octaves ("Octaves",Int) = 7
        _Lacunarity("Lacunarity", Range( 1.0 , 5.0)) = 2
        _Gain("Gain", Range( 0.0 , 1.0)) = 0.5
        _Value("Value", Range( -2.0 , 2.0)) = 0.0
        _Amplitude("Amplitude", Range( 0.0 , 5.0)) = 1.5
        _Frequency("Frequency", Range( 0.0 , 25.0)) = 2.0
        _Power("Power", Range( 0.1 , 5.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade
        #pragma target 3.0

        float3 _FresnelColor;
        float3 _NoiseColor;
        float _FresnelExponent;

        float _Octaves;
        float _Lacunarity;
        float _Gain;
        float _Value;
        float _Amplitude;
        float _Frequency;
        float _Power;
        float _OffsetX;
        float _OffsetY;

        struct Input
        {
            float3 worldNormal;
            float3 viewDir;
            INTERNAL_DATA
        };

        /* Noise generation by: https://github.com/przemyslawzaworski/Unity3D-CG-programming */
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

        void surf (Input i, inout SurfaceOutputStandard o)
        {
            float fresnel = dot(i.worldNormal, i.viewDir);
            fresnel = saturate(1 - fresnel);
            fresnel = pow(fresnel, _FresnelExponent);
            o.Alpha = 0 + fresnel;

            float3 noiseColor = (SampleNoise(i.worldNormal.xy) * -1 + 1) *_NoiseColor;
            float t = SampleNoise(i.worldNormal.xy) * -1 +1;
            float3 finalColor = lerp(_FresnelColor, _NoiseColor, t);

            o.Emission = finalColor;
        }
        ENDCG
    }
    FallBack "Standard"
}