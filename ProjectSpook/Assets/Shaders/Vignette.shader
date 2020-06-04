Shader "Unlit/Vignette"
{
    Properties
    {
		_MainTex("Color (HSV) Alpha(A)", 2D) = "none" {}
		_Colour("Color (HSV)", Color) = (1, 1, 1, 1)
		_VRadius("Vignette Radius", Range(0.0, 1.0)) = 0.8
		_VSoft("Vignette Softness", Range(0.0, 1.0)) = 0.5
    }

    SubShader {
		Tags{"Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100
		Blend SrcAlpha SrcAlpha
		ZWrite Off

		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _Color;
			float _VRadius;
			float _VSoft;
			float _Transparency;

			float4 frag(v2f_img input) : COLOR {
				float4 base = tex2D(_MainTex, input.uv);
				float distFromCenter = distance(input.uv.xy, float2(0.5, 0.5));
				float vignette = smoothstep(_VRadius, _VRadius - _VSoft, distFromCenter);
				base = base * -vignette;
				base.a = vignette;

				return base;
			}
			ENDCG
		}
    }
	Fallback "Transparent"
}
