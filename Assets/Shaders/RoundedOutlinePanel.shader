Shader "Custom/RoundedOutlinePanel"
{
    Properties
    {
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _Thickness    ("Outline Thickness (px)", Range(0.0, 64.0)) = 4.0
        _Radius       ("Corner Radius (px)", Range(0.0, 128.0)) = 16.0
        _Feather      ("Edge Feather (px)", Range(0.25, 3.0)) = 1.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "CanUseSpriteAtlas"="True" }
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t { float4 vertex:POSITION; float2 texcoord:TEXCOORD0; };
            struct v2f { float4 vertex:SV_POSITION; float2 uv:TEXCOORD0; };

            fixed4 _OutlineColor;
            float  _Thickness;   // px
            float  _Radius;      // px
            float  _Feather;     // px

            // Signed distance to rounded rectangle centered at 0 with half-extents b and radius r
            float sdRoundBox(float2 p, float2 b, float r)
            {
                float2 q = abs(p) - b + r;
                return length(max(q,0)) + min(max(q.x,q.y),0) - r;
            }

            v2f vert(appdata_t v)
            {
                v2f o; o.vertex = UnityObjectToClipPos(v.vertex); o.uv = v.texcoord; return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Center uv in [-0.5, +0.5]
                float2 p = i.uv - 0.5;

                // Convert px→uv using screen derivatives (consistent outline in screen pixels)
                float uvPerPx = max(length(ddx(i.uv)), length(ddy(i.uv)));
                uvPerPx = max(uvPerPx, 1e-5);

                float t  = _Thickness * 0.5 * uvPerPx;  // half thickness in uv
                float r  = _Radius * uvPerPx;
                float f  = _Feather * uvPerPx;

                // Panel quad half-extents in uv
                float2 he = float2(0.5, 0.5);
                float2 b = max(he - r, 0.0.xx);

                // d=0 on the rounded-rect edge; <0 inside, >0 outside
                float d = sdRoundBox(p, b, r);

                // Outer fade (edge at +t): 1 inside, 0 outside, feather width f
                float aOuter = 1.0 - smoothstep(t, t + f, d);
                // Inner fade (edge at -t): 0 deep inside, 1 at/after edge, feather width f
                float aInner = smoothstep(-t - f, -t, d);

                float alpha = saturate(aOuter * aInner);

                fixed4 col = _OutlineColor;
                col.a *= alpha;      // transparent center, only the ring
                return col;
            }
            ENDCG
        }
    }
}
