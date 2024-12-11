Shader "Unlit/RippleShader"
{
    Properties
    {
        _RippleCenter ("Ripple Center", Vector) = (0, 0, 0, 0)
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

            #include "UnityCG.cginc"

            // The position where the ripple will occur
            float4 _RippleCenter; // Send the hit position to the shader

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Calculate the distance between the current fragment position and the ripple center
                float dist = length(i.uv - _RippleCenter.xy);
                float ripple = exp(-dist * 10.0); // A simple ripple effect, modify as needed
                return fixed4(ripple, ripple, ripple, 1.0);
            }
            ENDCG
        }
    }
}
