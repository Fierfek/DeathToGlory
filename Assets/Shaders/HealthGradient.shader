﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/HealthGradient"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TopColor("Top Color", Color) = (1,1,1,1)
		_BottomColor("Bottom Color", Color) = (1,1,1,1)
		_GradientLevel("Gradient Blend Amount", Range (0.0, 1)) = 0.5
	}
	SubShader
	{
		//Tags { "Queue" = "Transparent" "RenderType"="Transparent" }


		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
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
				float4 position : SV_POSITION;
				float3 worldPosition : TEXCOORD0;
				//float2 uv : TEXCOORD0;
				//float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _TopColor, _BottomColor;
			float _GradientLevel;

			
			v2f vert (appdata v)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.worldPosition = mul(unity_ObjectToWorld, v.vertex).xyz;
				//o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv) + _TopColor;
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				//return col;
				

				return lerp(_BottomColor, _TopColor, i.worldPosition.y * _GradientLevel);
				
			}
			ENDCG
		}
	}
}
