﻿Shader "Unlit/Glass Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
		SubShader{
			// Queue is important! this object must be rendered after
			// Opaque objects.
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}

			LOD 100

			GrabPass
			{
				// "_BGTex"
				// if the name is assigned to the grab pass
				// all objects that use this shader also use a shared
				// texture grabbed before rendering them.
				// otherwise a new _GrabTexture will be created for each object.
			}

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "UnityCG.cginc"

				struct appdata {
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f {
					float2 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
					// this is a slot to put our screen coordinates into
					// it is a float4 instead of float2
					// because we need to use tex2Dproj() instead of tex2D()
					float4 screenUV : TEXCOORD1;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;

				// builtin variable to get Grabbed Texture if GrabPass has no name
				sampler2D _GrabTexture;

				v2f vert(appdata v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					// builtin function to get screen coordinates for tex2Dproj()
					o.screenUV = ComputeGrabScreenPos(o.vertex);
					return o;
				}

				fixed4 frag(v2f i) : SV_TARGET{
					fixed grab = tex2Dproj(_GrabTexture, i.screenUV);
					return grab;
				}
				ENDCG
			}
    }
}
