Shader "Custom/DualLightShader"
{
    Properties
    {
        _CirclePos("Circle Light Position", Vector) = (0,0,0,0)
        _ConePos("Cone Light Position", Vector) = (0,0,0,0)
        _ConeRotation("Cone Rotation (Radians)", Float) = 0
        _CircleRadius("Circle Light Radius", Float) = 2.0
        _ConeLength("Cone Length", Float) = 4.0
        _ConeStartWidth("Cone Start Width", Float) = 0.5
        _ConeEndWidth("Cone End Width", Float) = 2.0
        _Softness("Edge Softness", Range(0,1)) = 0.1
    }

    SubShader
    {
        Tags 
        { 
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD0;
            };

            float4 _CirclePos;
            float4 _ConePos;
            float _ConeRotation;
            float _CircleRadius;
            float _ConeLength;
            float _ConeStartWidth;
            float _ConeEndWidth;
            float _Softness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float calculateConeLight(float2 worldPos, float2 conePos, float rotation)
            {
                float2 toPoint = worldPos - conePos;
                float distToPoint = length(toPoint);
                
                // Early out if beyond cone length
                if (distToPoint > _ConeLength)
                    return 0;

                // Calculate the current width at this distance
                float t = distToPoint / _ConeLength;
                float currentWidth = lerp(_ConeStartWidth, _ConeEndWidth, t);
                
                // Calculate perpendicular distance from cone centerline
                float2 coneDir = float2(cos(rotation), sin(rotation));
                float2 perp = float2(-coneDir.y, coneDir.x);
                float perpDist = abs(dot(toPoint, perp));
                
                // Check both forward and backward directions
                float forwardProj = dot(toPoint, coneDir);
                float backwardProj = dot(toPoint, -coneDir);
                
                // Use the smaller perpendicular distance
                float proj = max(forwardProj, backwardProj);
                
                // Calculate falloff based on perpendicular distance
                float edgeSoftness = currentWidth * _Softness;
                float lightIntensity = 1 - smoothstep(currentWidth - edgeSoftness, 
                                                    currentWidth, 
                                                    perpDist);
                
                // Distance attenuation
                float distanceFalloff = 1 - t;
                
                return lightIntensity * distanceFalloff;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate circular light
                float circDist = distance(i.worldPos.xy, _CirclePos.xy);
                float circleLight = 1 - smoothstep(_CircleRadius * (1 - _Softness), 
                                                 _CircleRadius, 
                                                 circDist);

                // Calculate cone light
                float coneLight = calculateConeLight(i.worldPos.xy, _ConePos.xy, _ConeRotation);

                // Combine lights
                float combinedLight = saturate(circleLight + coneLight);

                // Return black color with alpha based on light
                return fixed4(0, 0, 0, 1 - combinedLight);
            }
            ENDCG
        }
    }
}