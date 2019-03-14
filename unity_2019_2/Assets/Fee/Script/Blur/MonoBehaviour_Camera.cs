﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ブラー。カメラ。
*/


/** NBlur
*/
namespace NBlur
{
	/** MonoBehaviour_Camera
	*/
	public class MonoBehaviour_Camera : MonoBehaviour
	{
		/** mycamera
		*/
		public Camera mycamera;

		/** material_blur_x
		*/
		private Material material_blur_x;

		/** material_blur_y
		*/
		private Material material_blur_y;

		/** work_rendertexture
		*/
		private RenderTexture work_rendertexture;

		/** 初期化。
		*/
		public void Initialize()
		{
			//カメラ取得。
			this.mycamera = this.GetComponent<Camera>();

			//マテリアル読み込み。
			this.material_blur_x = Resources.Load<Material>(Config.MATERIAL_NAME_BLURX);
			this.material_blur_y = Resources.Load<Material>(Config.MATERIAL_NAME_BLURY);

			//レンダーテクスチャー。
			this.work_rendertexture = null;

			#if(UNITY_EDITOR)
			{
				float[] t_table = new float[8];
				float t_total = 0.0f;
				float t_dispersion = 4.0f;
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] = Mathf.Exp(-0.5f * ((float)(ii*ii)) / t_dispersion);
					t_total += t_table[ii] * 2;
				}
				for(int ii=0;ii<t_table.Length;ii++){
					t_table[ii] /= t_total;
					Tool.Log("MonoBehaviour_Camera","param = " + t_table[ii].ToString());
				}
			}
			#endif
		}

		/** 削除。
		*/
		public void Delete()
		{
		}

		/** カメラデプス。設定。
		*/
		public void SetCameraDepth(float a_depth)
		{
			this.mycamera.depth = a_depth;
		}

		/** OnRenderImage
		*/
		private void OnRenderImage(RenderTexture a_source,RenderTexture a_dest)
		{
			//レンダリングテクスチャー作成。
			this.work_rendertexture = RenderTexture.GetTemporary(a_source.width/2,a_source.height/2,0,a_source.format,RenderTextureReadWrite.Default);

			try{
				UnityEngine.Graphics.Blit(a_source,this.work_rendertexture,this.material_blur_x);
				UnityEngine.Graphics.Blit(this.work_rendertexture,a_dest,this.material_blur_y);
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}

			//レンダーテクスチャー解放。
			if(this.work_rendertexture != null){
				RenderTexture.ReleaseTemporary(this.work_rendertexture);
				this.work_rendertexture = null;
			}
		}
	}
}

