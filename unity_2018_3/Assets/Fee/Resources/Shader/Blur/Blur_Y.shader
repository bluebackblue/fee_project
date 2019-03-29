/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief シェーダ。ブラー。
*/


Shader "Blur/BlurY"
{
    Properties
	{
        _MainTex ("Texture", 2D) = "white" {}
		texture_original ("Texture Original", 2D) = "white" {}

		rate_y ("rate_y", Float) = 1
		rate_blend ("rate_blend", Float) = 1
    }
    SubShader
    {
		Tags { "RenderType" = "Opaque" }
        Cull Off
        ZTest Always
        ZWrite Off

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
			};

			/** v2f
			*/
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			/** _MainTex
			*/
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;

			/** texture_original
			*/
			sampler2D texture_original;

			/** rate_y
			*/
			float rate_y;

			/** rate_blend
			*/
			float rate_blend;

			/** vert
			*/
			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			/** frag
			*/
			fixed4 frag(v2f i) : SV_Target
			{
				half3 t_color = half3(0.0f,0.0f,0.0f);

				/*
				float[] t_table = new float[8];
				float t_total = 0.0f;
				float t_dispersion = 4.0f;
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] = Mathf.Exp(-0.5f * ((float)(ii*ii)) / t_dispersion);
					t_total += t_table[ii] * 2;
				}
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] /= t_total;
				}
				//total = 0.5f
				*/

				float t_weight_a_1 = 0.16637f;
				float t_weight_a_2 = 0.14677f;
				float t_weight_a_3 = 0.10087f;
				float t_weight_a_4 = 0.05399f;
				float t_weight_a_5 = 0.02250f;
				float t_weight_a_6 = 0.00730f;
				float t_weight_a_7 = 0.00184f;
				float t_weight_a_8 = 0.00036f;

				float t_weight_b_1 = 0.28520f;
				float t_weight_b_2 = 0.17296f;
				float t_weight_b_3 = 0.03859f;
				float t_weight_b_4 = 0.00316f;
				float t_weight_b_5 = 0.00009f;
				float t_weight_b_6 = 0.00000f;
				float t_weight_b_7 = 0.00000f;
				float t_weight_b_8 = 0.00000f;

				float t_weight_1 = (t_weight_a_1 * rate_y) + (t_weight_b_1 * (1.0f - rate_y));
				float t_weight_2 = (t_weight_a_2 * rate_y) + (t_weight_b_2 * (1.0f - rate_y));
				float t_weight_3 = (t_weight_a_3 * rate_y) + (t_weight_b_3 * (1.0f - rate_y));
				float t_weight_4 = (t_weight_a_4 * rate_y) + (t_weight_b_4 * (1.0f - rate_y));
				float t_weight_5 = (t_weight_a_5 * rate_y) + (t_weight_b_5 * (1.0f - rate_y));
				float t_weight_6 = (t_weight_a_6 * rate_y) + (t_weight_b_6 * (1.0f - rate_y));
				float t_weight_7 = (t_weight_a_7 * rate_y) + (t_weight_b_7 * (1.0f - rate_y));
				float t_weight_8 = (t_weight_a_8 * rate_y) + (t_weight_b_8 * (1.0f - rate_y));

				t_color += tex2D(_MainTex,i.uv + float2(0, _MainTex_TexelSize.y *  1)).rgb * t_weight_1;
				t_color += tex2D(_MainTex,i.uv + float2(0,-_MainTex_TexelSize.y *  1)).rgb * t_weight_1;
				t_color += tex2D(_MainTex,i.uv + float2(0, _MainTex_TexelSize.y *  3)).rgb * t_weight_2;
				t_color += tex2D(_MainTex,i.uv + float2(0,-_MainTex_TexelSize.y *  3)).rgb * t_weight_2;
				t_color += tex2D(_MainTex,i.uv + float2(0, _MainTex_TexelSize.y *  5)).rgb * t_weight_3;
				t_color += tex2D(_MainTex,i.uv + float2(0,-_MainTex_TexelSize.y *  5)).rgb * t_weight_3;
				t_color += tex2D(_MainTex,i.uv + float2(0, _MainTex_TexelSize.y *  7)).rgb * t_weight_4;
				t_color += tex2D(_MainTex,i.uv + float2(0,-_MainTex_TexelSize.y *  7)).rgb * t_weight_4;
				t_color += tex2D(_MainTex,i.uv + float2(0, _MainTex_TexelSize.y *  9)).rgb * t_weight_5;
				t_color += tex2D(_MainTex,i.uv + float2(0,-_MainTex_TexelSize.y *  9)).rgb * t_weight_5;

				if(t_weight_6 > 0.0f){
					t_color += tex2D(_MainTex,i.uv + float2(0, _MainTex_TexelSize.y * 11)).rgb * t_weight_6;
					t_color += tex2D(_MainTex,i.uv + float2(0,-_MainTex_TexelSize.y * 11)).rgb * t_weight_6;
					t_color += tex2D(_MainTex,i.uv + float2(0, _MainTex_TexelSize.y * 13)).rgb * t_weight_7;
					t_color += tex2D(_MainTex,i.uv + float2(0,-_MainTex_TexelSize.y * 13)).rgb * t_weight_7;
					t_color += tex2D(_MainTex,i.uv + float2(0, _MainTex_TexelSize.y * 15)).rgb * t_weight_8;
					t_color += tex2D(_MainTex,i.uv + float2(0,-_MainTex_TexelSize.y * 15)).rgb * t_weight_8;
				}

				if(rate_blend < 1.0f){
					t_color = t_color * rate_blend + tex2D(texture_original,i.uv) * (1.0f - rate_blend);
				}

				return fixed4(t_color,1.0f);
			}
			ENDCG
		}
    }
}

