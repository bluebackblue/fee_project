

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ２Ｄ描画。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** Render2D
	*/
	public class Render2D : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Render2D s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Render2D();
			}
		}

		/** [シングルトン]インスタンス。チェック。
		*/
		public static bool IsCreateInstance()
		{
			if(s_instance != null){
				return true;
			}
			return false;
		}

		/** [シングルトン]インスタンス。取得。
		*/
		public static Render2D GetInstance()
		{
			#if(UNITY_EDITOR)
			if(s_instance == null){
				Tool.Assert(false);
			}
			#endif

			return s_instance;			
		}

		/** [シングルトン]インスタンス。削除。
		*/
		public static void DeleteInstance()
		{
			if(s_instance != null){
				s_instance.Delete();
				s_instance = null;
			}
		}

		/** ルート。
		*/
		private UnityEngine.GameObject root_gameobject;
		private UnityEngine.Transform root_transform;

		/** スクリーン。
		*/
		private Screen screen;

		/** マテリアルリスト。
		*/
		private MaterialList materiallist;

		/** スプライト。
		*/
		private System.Collections.Generic.List<Sprite2D> sprite_list;

		/** テキスト。
		*/
		private System.Collections.Generic.List<Text2D> text_list;

		/** 入力フィールド。
		*/
		private System.Collections.Generic.List<InputField2D> inputfield_list;

		/** 更新リクエスト。
		*/
		private bool update_request_sprite;
		private bool update_request_text;
		private bool update_request_inputfield;

		/** デフォルト。フォント。
		*/
		private UnityEngine.Font default_font;

		/** レイヤーリスト。
		*/
		private LayerList layerlist;

		/** [シングルトン]constructor。
		*/
		private Render2D()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "Render2D";
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();

			//スクリーン。
			this.screen = new Screen();

			//マテリアルリスト。
			this.materiallist = new MaterialList();

			//スプライト。
			this.sprite_list = new System.Collections.Generic.List<Sprite2D>();

			//テキスト。
			this.text_list = new System.Collections.Generic.List<Text2D>();

			//入力フィールド。
			this.inputfield_list = new System.Collections.Generic.List<InputField2D>();

			//更新リクエスト。
			this.update_request_sprite = true;
			this.update_request_text = true;
			this.update_request_inputfield = true;

			//デフォルト。フォント。
			this.default_font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>(Config.DEFAULT_FONT_NAME);

			//レイヤーリスト。
			this.layerlist = new LayerList(this.root_gameobject.GetComponent<UnityEngine.Transform>());
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** ＧＵＩスクリーン座標　＝＞　仮想スクリーン座標。
		*/
		public void GuiScreenToVirtualScreen(int a_gui_x,int a_gui_y,out int a_virtual_x,out int a_virtual_y)
		{
			this.screen.GuiScreenToVirtualScreen(a_gui_x,a_gui_y,out a_virtual_x,out a_virtual_y);
		}

		/** 仮想スクリーン座標　＝＞　ＧＵＩスクリーン座標。
		*/
		public void VirtualScreenToGuiScreen(int a_virtual_x,int a_virtual_y,out int a_gui_x,out int a_gui_y)
		{
			this.screen.VirtualScreenToGuiScreen(a_virtual_x,a_virtual_y,out a_gui_x,out a_gui_y);
		}

		/** ワールド座標 => 仮想スクリーン座標。
		*/
		public void WorldToVirtualScreen(UnityEngine.Camera a_camera,ref UnityEngine.Vector3 a_position,out int a_virtual_x,out int a_virtual_y)
		{
			UnityEngine.Vector2 t_gui_pos = UnityEngine.RectTransformUtility.WorldToScreenPoint(a_camera,a_position);
			Fee.Render2D.Render2D.GetInstance().GuiScreenToVirtualScreen((int)t_gui_pos.x,(int)(this.screen.GetGuiH() - t_gui_pos.y),out a_virtual_x,out a_virtual_y);
		}

		/** ＧＵＩスクリーン。取得。
		*/
		public int GetGuiW()
		{
			return this.screen.GetGuiW();
		}

		/** ＧＵＩスクリーン。取得。
		*/
		public int GetGuiH()
		{
			return this.screen.GetGuiH();
		}

		/** GetLastLayerIndex
		*/
		public int GetLastLayerIndex()
		{
			return (Config.MAX_LAYER - 1);
		}
		/** デフォルト。フォント設定。
		*/
		public void SetDefaultFont(UnityEngine.Font a_font)
		{
			this.default_font = a_font;
		}

		/** デフォルト。フォント取得。
		*/
		public UnityEngine.Font GetDefaultFont()
		{
			return this.default_font;
		}

		/** ＵＩテキストマテリアル。取得。
		*/
		public UnityEngine.Material GetUiTextMaterial()
		{
			return this.materiallist.GetUiTextMaterial();
		}

		/** ＵＩイメージマテリアル。取得。
		*/
		public UnityEngine.Material GetUiImageMaterial()
		{
			return this.materiallist.GetUiImageMaterial();
		}

		/** [RawText]作成。
		*/
		public UnityEngine.GameObject RawText_Create()
		{
			return Fee.Instantiate.Instantiate.CreateUiText("Text",this.root_transform);
		}

		/** [RawInputField]作成。
		*/
		public UnityEngine.GameObject RawInputField_Create()
		{
			return Fee.Instantiate.Instantiate.CreateUiInputField("InputField",this.root_transform);
		}

		/** [RawText]削除。
		*/
		public void RawText_Delete(UnityEngine.GameObject a_gameobject)
		{
			UnityEngine.GameObject.Destroy(a_gameobject);
		}

		/** [RawInputField]削除。
		*/
		public void RawInputField_Delete(UnityEngine.GameObject a_gameobject)
		{
			UnityEngine.GameObject.Destroy(a_gameobject);
		}

		/** スプライト作成。
		*/
		public void AddSprite2D(Sprite2D a_sprite)
		{
			this.sprite_list.Add(a_sprite);
			this.update_request_sprite = true;
		}

		/** テキスト作成。
		*/
		public void AddText2D(Text2D a_text)
		{
			this.text_list.Add(a_text);
			this.update_request_text = true;
		}

		/** 入力フィールド作成。
		*/
		public void AddInputField2D(InputField2D a_inputfield)
		{
			this.inputfield_list.Add(a_inputfield);
			this.update_request_inputfield = true;
		}

		/** 更新リクエスト。
		*/
		public void UpdateSpriteListRequest()
		{
			this.update_request_sprite = true;
		}

		/** 更新リクエスト。
		*/
		public void UpdateTextListRequest()
		{
			this.update_request_text = true;
		}

		/** 更新リクエスト。
		*/
		public void UpdateInputFieldListRequest()
		{
			this.update_request_inputfield = true;
		}

		/** デプスクリアーの設定。
		*/
		public void SetDepthClearGL(int a_layerindex,bool a_flag)
		{
			this.layerlist.SetDepthClearGL(a_layerindex,a_flag);
		}

		/** デプスクリアーの設定。
		*/
		public void SetDepthClearUI(int a_layerindex,bool a_flag)
		{
			this.layerlist.SetDepthClearUI(a_layerindex,a_flag);
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteX()
		{
			return this.screen.GetScreenCalcSpriteX();
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteY()
		{
			return this.screen.GetScreenCalcSpriteY();
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteW()
		{
			return this.screen.GetScreenCalcSpriteW();
		}

		/** 事前計算。取得。
		*/
		public float GetScreenCalcSpriteH()
		{
			return this.screen.GetScreenCalcSpriteH();
		}

		/** カメラデプス。取得。
		*/
		public float GetGLCameraDepth(int a_layerindex)
		{
			return this.layerlist.GetGLCameraDepth(a_layerindex);
		}

		/** カメラデプス。取得。
		*/
		public float GetUICameraDepth(int a_layerindex)
		{
			return this.layerlist.GetUICameraDepth(a_layerindex);
		}

		/** カメラデプス。取得。
		*/
		public float GetCameraBeforeDepth(int a_layerindex)
		{
			return Config.CAMERADEPTH_START + a_layerindex * Config.CAMERADEPTH_STEP + Config.CAMERADEPTH_OFFSET_BEFORE;
		}

		/** カメラデプス。取得。
		*/
		public float GetCameraAfterDepth(int a_layerindex)
		{
			return Config.CAMERADEPTH_START + a_layerindex * Config.CAMERADEPTH_STEP + Config.CAMERADEPTH_OFFSET_AFTER;
		}

		/** 描画前処理。
		*/
		public void PreDraw()
		{
			//事前計算。
			this.screen.CalcScreen(this.sprite_list);

			//スプライト。
			if(this.update_request_sprite == true){

				//削除。
				{
					int ii = 0;
					while(ii < this.sprite_list.Count){
						if(this.sprite_list[ii].IsDelete() == true){
							this.sprite_list.RemoveAt(ii);
						}else{
							ii++;
						}
					}
				}

				//ソート。
				this.sprite_list.Sort(Sprite2D.Sort_DrawPriority);
			}

			//テキスト。
			if(this.update_request_text == true){

				//削除。
				{
					int ii = 0;
					while(ii < this.text_list.Count){
						if(this.text_list[ii].IsDelete() == true){
							this.text_list.RemoveAt(ii);
						}else{
							ii++;
						}
					}
				}

				//ソート。
				this.text_list.Sort(Text2D.Sort_DrawPriority);
			}

			//入力フィールド。
			if(this.update_request_inputfield == true){

				//削除。
				{
					int ii = 0;
					while(ii < this.inputfield_list.Count){
						if(this.inputfield_list[ii].IsDelete() == true){
							this.inputfield_list.RemoveAt(ii);
						}else{
							ii++;
						}
					}
				}

				//ソート。
				this.inputfield_list.Sort(InputField2D.Sort_DrawPriority);
			}

			//インデックス計算。
			if((this.update_request_sprite == true)||(this.update_request_text == true)||(this.update_request_inputfield == true)){
				this.layerlist.CalcIndex(this.sprite_list,this.text_list,this.inputfield_list);
			}

			//テキスト。描画プライオリティ再設定。
			if(this.update_request_text == true){
				for(int ii=0;ii<this.text_list.Count;ii++){
					this.text_list[ii].Raw_SetLayer(this.layerlist.GetLayerTransformFromDrawPriority(this.text_list[ii].GetDrawPriority()));
				}
			}

			//入力フィールド。描画プライオリティ再設定。
			if(this.update_request_inputfield == true){
				for(int ii=0;ii<this.inputfield_list.Count;ii++){
					this.inputfield_list[ii].Raw_SetLayer(this.layerlist.GetLayerTransformFromDrawPriority(this.inputfield_list[ii].GetDrawPriority()));
				}
			}

			//ＵＩテキスト描画
			for(int ii=0;ii<this.layerlist.GetListMax();ii++){
				this.PreDraw_UI(ii);
			}

			//リセット。
			this.update_request_sprite = false;
			this.update_request_text = false;
			this.update_request_inputfield = false;

			//テキスト再計算フラグのリセット。
			this.screen.ResetUiReCalcFlag();
		}

		/** 描画前処理。ＵＩ。
		*/
		public void PreDraw_UI(int a_layerindex)
		{
			//テキスト。
			{
				int t_start_index = this.layerlist.GetStartIndex_Text(a_layerindex);
				int t_last_index = this.layerlist.GetLastIndex_Text(a_layerindex);

				if((t_start_index >= 0)&&(t_last_index >= 0)){
					for(int ii=t_start_index;ii<=t_last_index;ii++){
						Text2D t_text = this.text_list[ii];

						//フォントサイズの計算が必要。
						if((t_text.Raw_IsCalcFontSize() == true)||(this.screen.IsUiReCalcFlag() == true)){
							t_text.Raw_SetCalcFontSizeFlag(false);
							t_text.Raw_SetFontSize(this.screen.CalcFontSize(t_text));
						}

						//シェーダの変更が必要。
						if((t_text.Raw_IsChangeShader() == true)||(this.screen.IsUiReCalcFlag() == true)){
							t_text.Raw_SetChangeShaderFlag(false);

							if(t_text.IsClip() == false){
								//共通テキストマテリアル使用。
								t_text.Raw_SetTextMaterial(this.materiallist.GetUiTextMaterial());
							}else{
								//カスタムテキストマテリアル使用。
								{
									UnityEngine.Material t_material = t_text.GetCustomTextMaterial();
									int t_gui_x1;
									int t_gui_y1;
									int t_gui_x2;
									int t_gui_y2;
									this.VirtualScreenToGuiScreen(t_text.GetClipX(),t_text.GetClipY() + t_text.GetClipH(),out t_gui_x1,out t_gui_y1);
									this.VirtualScreenToGuiScreen(t_text.GetClipX() + t_text.GetClipW(),t_text.GetClipY(),out t_gui_x2,out t_gui_y2);
									t_material.SetInt("clip_flag",1);
									t_material.SetFloat("clip_x1",t_gui_x1);
									t_material.SetFloat("clip_y1",t_gui_y1);
									t_material.SetFloat("clip_x2",t_gui_x2);
									t_material.SetFloat("clip_y2",t_gui_y2);
									t_text.Raw_SetTextMaterial(t_material);
								}
							}
						}

						if((t_text.GetText().Length > 0)&&(t_text.IsVisible() == true)&&(t_text.GetDrawPriority() >= 0)){
							//サイズの計算が必要
							bool t_is_calcsize = false;
							if((t_text.Raw_IsCalcSize() == true)||(this.screen.IsUiReCalcFlag() == true)){
								t_text.Raw_SetCalcSizeFlag(false);
								t_is_calcsize = true;
							}

							//矩形計算。
							this.screen.CalcTextRect(t_text,t_is_calcsize);

							//表示。
							t_text.Raw_SetEnable(true);
						}else{
							//非表示。
							t_text.Raw_SetEnable(false);
						}
					}
				}
			}

			//入力フィールド。
			{
				int t_start_index = this.layerlist.GetStartIndex_InputField(a_layerindex);
				int t_last_index = this.layerlist.GetLastIndex_InputField(a_layerindex);

				if((t_start_index >= 0)&&(t_last_index >= 0)){
					for(int ii=t_start_index;ii<=t_last_index;ii++){
						InputField2D t_inputfield = this.inputfield_list[ii];

						//フォントサイズの計算が必要。
						if((t_inputfield.Raw_IsCalcFontSize() == true)||(this.screen.IsUiReCalcFlag() == true)){
							t_inputfield.Raw_SetCalcFontSizeFlag(false);
							t_inputfield.Raw_SetFontSize(this.screen.CalcFontSize(t_inputfield));
						}

						//シェーダの変更が必要。
						if((t_inputfield.Raw_IsChangeShader() == true)||(this.screen.IsUiReCalcFlag() == true)){
							t_inputfield.Raw_SetChangeShaderFlag(false);

							if(t_inputfield.IsClip() == false){
								//共通テキストマテリアル使用。
								t_inputfield.Raw_SetTextMaterial(this.materiallist.GetUiTextMaterial());
								t_inputfield.Raw_SetImageMaterial(this.materiallist.GetUiImageMaterial());
							}else{
								//カスタムテキストマテリアル使用。
								{
									UnityEngine.Material t_text_material = t_inputfield.GetCustomTextMaterial();
									int t_gui_x1;
									int t_gui_y1;
									int t_gui_x2;
									int t_gui_y2;
									this.VirtualScreenToGuiScreen(t_inputfield.GetClipX(),t_inputfield.GetClipY() + t_inputfield.GetClipH(),out t_gui_x1,out t_gui_y1);
									this.VirtualScreenToGuiScreen(t_inputfield.GetClipX() + t_inputfield.GetClipW(),t_inputfield.GetClipY(),out t_gui_x2,out t_gui_y2);
									t_text_material.SetInt("clip_flag",1);
									t_text_material.SetFloat("clip_x1",t_gui_x1);
									t_text_material.SetFloat("clip_y1",t_gui_y1);
									t_text_material.SetFloat("clip_x2",t_gui_x2);
									t_text_material.SetFloat("clip_y2",t_gui_y2);
									t_inputfield.Raw_SetTextMaterial(t_text_material);
								}

								//カスタムイメージマテリアル使用。
								{
									UnityEngine.Material t_image_material = t_inputfield.GetCustomImageMaterial();
									int t_gui_x1;
									int t_gui_y1;
									int t_gui_x2;
									int t_gui_y2;
									this.VirtualScreenToGuiScreen(t_inputfield.GetClipX(),t_inputfield.GetClipY() + t_inputfield.GetClipH(),out t_gui_x1,out t_gui_y1);
									this.VirtualScreenToGuiScreen(t_inputfield.GetClipX() + t_inputfield.GetClipW(),t_inputfield.GetClipY(),out t_gui_x2,out t_gui_y2);
									t_image_material.SetInt("clip_flag",1);
									t_image_material.SetFloat("clip_x1",t_gui_x1);
									t_image_material.SetFloat("clip_y1",t_gui_y1);
									t_image_material.SetFloat("clip_x2",t_gui_x2);
									t_image_material.SetFloat("clip_y2",t_gui_y2);
									t_inputfield.Raw_SetImageMaterial(t_image_material);
								}
							}
						}

						if((t_inputfield.IsVisible() == true)&&(t_inputfield.GetDrawPriority() >= 0)){
							//サイズの計算が必要
							bool t_is_calcsize = true;

							//矩形計算。
							this.screen.CalcInputFieldRect(t_inputfield,t_is_calcsize);

							//表示。
							t_inputfield.Raw_SetEnable(true);
						}else{
							//非表示。
							t_inputfield.Raw_SetEnable(false);
						}
					}
				}
			}
		}

		/** ＧＬ描画。MonoBehaviour_Camera_GLからの呼び出し。
		*/
		public void Draw_GL(int a_layerindex)
		{
			MaterialType t_current_material = MaterialType.None;

			int t_start_index = this.layerlist.GetStartIndex_Sprite(a_layerindex);
			int t_last_index = this.layerlist.GetLastIndex_Sprite(a_layerindex);

			//最初のカメラでレンダーテクスチャーをクリアする。
			if(Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE == true){
				if(a_layerindex == 0){
					UnityEngine.GL.Clear(true,true,new UnityEngine.Color(0.0f,0.0f,0.0f,1.0f));
				}
			}

			if((t_start_index >= 0)&&(t_last_index >= 0)){

				UnityEngine.GL.PushMatrix();

				float[] t_to_8 = new float[8];

				try
				{
					UnityEngine.GL.LoadOrtho();

					bool t_is_begin = false;

					{
						for(int ii=t_start_index;ii<=t_last_index;ii++){
							Sprite2D t_sprite = this.sprite_list[ii];

							if((t_sprite.IsVisible() == true)&&(t_sprite.GetDrawPriority() >= 0)){

								UnityEngine.Material t_material = this.materiallist.GetMaterial(t_sprite);

								//マテリアル変更。
								if(t_current_material != t_sprite.GetMaterialType()){
									if(t_is_begin == true){
										t_is_begin = false;
										UnityEngine.GL.End();
									}
									t_current_material = t_sprite.GetMaterialType();
								}

								//マテリアルの更新。
								bool t_change = t_sprite.UpdateMaterial(ref t_material);
								if(t_change == true){
									if(t_is_begin == true){
										t_is_begin = false;
										UnityEngine.GL.End();
									}
								}

								//パス設定。
								if(t_is_begin == false){
									t_is_begin = true;

									t_material.SetPass(0);

									UnityEngine.GL.Begin(UnityEngine.GL.TRIANGLES);
								}

								float t_from_x1 = t_sprite.GetTextureX() / Config.TEXTURE_W;
								float t_from_y1 = 1.0f - t_sprite.GetTextureY() / Config.TEXTURE_H;
								float t_from_x2 = (t_sprite.GetTextureX() + t_sprite.GetTextureW()) / Config.TEXTURE_W;
								float t_from_y2 = 1.0f - (t_sprite.GetTextureY() + t_sprite.GetTextureH()) / Config.TEXTURE_H;

								this.screen.CalcSpritePosition(t_sprite,t_to_8);

								UnityEngine.Color t_color = t_sprite.GetColor();

								UnityEngine.GL.Color(t_color);

								{
									//左上。
									UnityEngine.GL.TexCoord2(t_from_x1,t_from_y1);
									UnityEngine.GL.Vertex3(t_to_8[0],t_to_8[1],0.0f);

									//右上。
									UnityEngine.GL.TexCoord2(t_from_x2,t_from_y1);
									UnityEngine.GL.Vertex3(t_to_8[2],t_to_8[3],0.0f);

									//左下。
									UnityEngine.GL.TexCoord2(t_from_x1,t_from_y2);
									UnityEngine.GL.Vertex3(t_to_8[4],t_to_8[5],0.0f);
								}

								{
									//左下。
									UnityEngine.GL.TexCoord2(t_from_x1,t_from_y2);
									UnityEngine.GL.Vertex3(t_to_8[4],t_to_8[5],0.0f);

									//右上。
									UnityEngine.GL.TexCoord2(t_from_x2,t_from_y1);
									UnityEngine.GL.Vertex3(t_to_8[2],t_to_8[3],0.0f);

									//右下。
									UnityEngine.GL.TexCoord2(t_from_x2,t_from_y2);
									UnityEngine.GL.Vertex3(t_to_8[6],t_to_8[7],0.0f);	
								}
							}
						}
					}

					if(t_is_begin == true){
						UnityEngine.GL.End();
					}
				}catch(System.Exception t_exception){
					Tool.LogError(t_exception);
				}

				UnityEngine.GL.PopMatrix();
			}
		}
	}
}

