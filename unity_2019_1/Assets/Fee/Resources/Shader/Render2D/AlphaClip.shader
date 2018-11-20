﻿/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダ。半透明クリップ。
*/


Shader "Render2D/AlphaClip"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		[MaterialToggle] clip_flag ("Clip Flag", Int) = 0
		clip_x1 ("Clip X1", Float) = 0
		clip_y1 ("Clip Y1", Float) = 0
		clip_x2 ("Clip X2", Float) = 0
		clip_y2 ("Clip Y2", Float) = 0
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
		Cull Off
		ZWrite Off
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			/** appdata
			*/
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			/** v2f
			*/
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			float4 _MainTex_ST;

			/** clip_flag
			*/
			int clip_flag;

			/** clip
			*/
			float clip_x1;
			float clip_y1;
			float clip_x2;
			float clip_y2;
			
			/** vert
			*/
			v2f vert(appdata v)
			{
				v2f t_ret;
				{
					t_ret.vertex = UnityObjectToClipPos(v.vertex);
					t_ret.uv = TRANSFORM_TEX(v.uv,_MainTex);
					t_ret.color = v.color;
				}
				return t_ret;
			}
			
			/** frag
			*/
			fixed4 frag(v2f i) : SV_Target
			{
				//クリップ。
				if(clip_flag > 0){
					if(clip_x1>i.vertex.x){
						discard;
					}

					if(i.vertex.x>clip_x2){
						discard;
					}

					#if(UNITY_UV_STARTS_AT_TOP)
					if(clip_y2>i.vertex.y){
						discard;
					}

					if(i.vertex.y>clip_y1){
						discard;
					}
					#else
					if((_ScreenParams.y - clip_y1)>i.vertex.y){
						discard;
					}

					if(i.vertex.y>(_ScreenParams.y - clip_y2)){
						discard;
					}
					#endif
				}

				fixed4 t_color = tex2D(_MainTex,i.uv) * i.color;

				return t_color;
			}
			ENDCG
		}
	}
}
