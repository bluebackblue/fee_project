
/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief プレハブリスト。
*/


/** TestScript.Common
*/
namespace TestScript.Common
{
	/** PrefabList
	*/
	public class PrefabList
	{
		/** prefab_list
		*/
		private Fee.Instantiate.PrefabList_MonoBehaviour prefab_list;

		/** texture_list
		*/
		private Fee.Instantiate.TextureList_MonoBehaviour texture_list;

		/** textasset_list
		*/
		private Fee.Instantiate.TextAssetList_MonoBehaviour textasset_list;

		/** font_list
		*/
		private Fee.Instantiate.FontList_MonoBehaviour font_list;

		/** constructor
		*/
		public PrefabList()
		{
			UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("create_from_excel_prefab");
			if(t_prefab != null){
				this.prefab_list = t_prefab.GetComponent<Fee.Instantiate.PrefabList_MonoBehaviour>();
				if(this.prefab_list != null){

					//TextureList
					UnityEngine.GameObject t_texture_prefab = this.prefab_list.prefab_list[(int)PrefabType.TextureList];
					if(t_texture_prefab != null){
						this.texture_list = t_texture_prefab.GetComponent<Fee.Instantiate.TextureList_MonoBehaviour>();
					}

					//TextAssetList
					UnityEngine.GameObject t_textasset_prefab = this.prefab_list.prefab_list[(int)PrefabType.TextAssetList];
					if(t_textasset_prefab != null){
						this.textasset_list = t_textasset_prefab.GetComponent<Fee.Instantiate.TextAssetList_MonoBehaviour>();
					}

					//FontList
					UnityEngine.GameObject t_font_prefab = this.prefab_list.prefab_list[(int)PrefabType.FontList];
					if(t_font_prefab != null){
						this.font_list = t_font_prefab.GetComponent<Fee.Instantiate.FontList_MonoBehaviour>();
					}
				}
			}
		}

		/** テクスチャー。取得。
		*/
		public UnityEngine.Texture2D GetTexture(TextureType a_type)
		{
			return this.texture_list.texture_list[(int)a_type];
		}

		/** プレハブ。取得。
		*/
		public UnityEngine.GameObject GetPrefab(PrefabType a_type)
		{
			return this.prefab_list.prefab_list[(int)a_type];
		}

		/** テキストアセット。取得。
		*/
		public UnityEngine.TextAsset GetTextAsset(TextAssetType a_type)
		{
			return this.textasset_list.textasset_list[(int)a_type];
		}

		/** フォント。取得。
		*/
		public UnityEngine.Font GetFont(FontType a_type)
		{
			return this.font_list.font_list[(int)a_type];
		}


		/** データ。
		*/
		public UnityEngine.TextAsset GetDataJson(bool a_is_release)
		{
			if(a_is_release == true){
				return UnityEngine.Resources.Load<UnityEngine.TextAsset>("create_from_excel_data_release");
			}else{
				return UnityEngine.Resources.Load<UnityEngine.TextAsset>("create_from_excel_data_debug");
			}
		}

		/** GetTest11SeBank
		*/
		public Fee.Audio.Bank GetSeBank()
		{
			UnityEngine.GameObject t_prefab = this.GetPrefab(PrefabType.Se);
			Fee.Instantiate.AudioClipList_MonoBehaviour t_audioclip_list = t_prefab.GetComponent<Fee.Instantiate.AudioClipList_MonoBehaviour>();
			Fee.Instantiate.AudioVolumeList_MonoBehaviour t_audiovolume_list = t_prefab.GetComponent<Fee.Instantiate.AudioVolumeList_MonoBehaviour>();

			if(t_audioclip_list != null){

				//ボリュームリスト。作成。
				System.Collections.Generic.Dictionary<string,float> t_volume_list = new System.Collections.Generic.Dictionary<string, float>();
				if(t_audiovolume_list != null){
					for(int ii=0;ii<t_audiovolume_list.tag_list.Length;ii++){
						t_volume_list.Add(t_audiovolume_list.tag_list[ii],t_audiovolume_list.audiovolume_list[ii]);
					}
				}

				//パック作成。
				Fee.Audio.Pack_AudioClip t_pack = new Fee.Audio.Pack_AudioClip();

				for(int ii=0;ii<t_audioclip_list.tag_list.Length;ii++){
					UnityEngine.AudioClip t_audioclip = t_audioclip_list.audioclip_list[ii];

					float t_volume;
					if(t_volume_list.TryGetValue(t_audioclip_list.tag_list[ii],out t_volume) == false){
						t_volume = 1.0f;
					}

					t_pack.audioclip_list.Add(t_audioclip);
					t_pack.volume_list.Add(t_volume);
				}

				return new Fee.Audio.Bank(t_pack);
			}

			return null;
		}

		/** GetTest11SeBank
		*/
		public Fee.Audio.Bank GetBgmBank()
		{
			UnityEngine.GameObject t_prefab = this.GetPrefab(PrefabType.Bgm);
			Fee.Instantiate.AudioClipList_MonoBehaviour t_audioclip_list = t_prefab.GetComponent<Fee.Instantiate.AudioClipList_MonoBehaviour>();
			Fee.Instantiate.AudioVolumeList_MonoBehaviour t_audiovolume_list = t_prefab.GetComponent<Fee.Instantiate.AudioVolumeList_MonoBehaviour>();

			if(t_audioclip_list != null){

				//ボリュームリスト。作成。
				System.Collections.Generic.Dictionary<string,float> t_volume_list = new System.Collections.Generic.Dictionary<string, float>();
				if(t_audiovolume_list != null){
					for(int ii=0;ii<t_audiovolume_list.tag_list.Length;ii++){
						t_volume_list.Add(t_audiovolume_list.tag_list[ii],t_audiovolume_list.audiovolume_list[ii]);
					}
				}

				//パック作成。
				Fee.Audio.Pack_AudioClip t_pack = new Fee.Audio.Pack_AudioClip();

				for(int ii=0;ii<t_audioclip_list.tag_list.Length;ii++){
					UnityEngine.AudioClip t_audioclip = t_audioclip_list.audioclip_list[ii];

					float t_volume;
					if(t_volume_list.TryGetValue(t_audioclip_list.tag_list[ii],out t_volume) == false){
						t_volume = 1.0f;
					}

					t_pack.audioclip_list.Add(t_audioclip);
					t_pack.volume_list.Add(t_volume);
				}

				return new Fee.Audio.Bank(t_pack);
			}

			return null;
		}

		/** ボタン。アクティブ設定。
		*/
		public void SetButtonActive(Fee.Ui.Button a_button,bool a_flag)
		{
			if(a_flag == true){
				a_button.SetNormalTexture(this.GetTexture(TextureType.Ui_Button_Active_Normal));
				a_button.SetOnTexture(this.GetTexture(TextureType.Ui_Button_Active_On));
				a_button.SetDownTexture(this.GetTexture(TextureType.Ui_Button_Active_Down));
				a_button.SetLockTexture(this.GetTexture(TextureType.Ui_Button_Active_Lock));
			}else{
				a_button.SetNormalTexture(this.GetTexture(TextureType.Ui_Button_Normal));
				a_button.SetOnTexture(this.GetTexture(TextureType.Ui_Button_On));
				a_button.SetDownTexture(this.GetTexture(TextureType.Ui_Button_Down));
				a_button.SetLockTexture(this.GetTexture(TextureType.Ui_Button_Lock));
			}
		}

		/** ボタン。作成。
		*/
		public Fee.Ui.Button CreateButton(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Fee.Ui.Button t_button = Fee.Ui.Button.Create(a_deleter,a_drawpriority);
			{
				t_button.SetTextureCornerSize(10);

				t_button.SetNormalTexture(this.GetTexture(TextureType.Ui_Button_Normal));
				t_button.SetOnTexture(this.GetTexture(TextureType.Ui_Button_On));
				t_button.SetDownTexture(this.GetTexture(TextureType.Ui_Button_Down));
				t_button.SetLockTexture(this.GetTexture(TextureType.Ui_Button_Normal));
				t_button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			}

			return t_button;
		}

		/** スライダー。作成。
		*/
		public Fee.Ui.Slider CreateSlider(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Fee.Ui.Slider t_slider = Fee.Ui.Slider.Create(a_deleter,a_drawpriority);
			{
				t_slider.SetButtonTextureCornerSize(2);
				t_slider.SetTextureCornerSize(10);

				t_slider.SetBgNormalTexture(this.GetTexture(TextureType.Ui_Slider_Bg_Normal));
				t_slider.SetBgLockTexture(this.GetTexture(TextureType.Ui_Slider_Bg_Lock));
				t_slider.SetValueNormalTexture(this.GetTexture(TextureType.Ui_Slider_Value_Normal));
				t_slider.SetValueLockTexture(this.GetTexture(TextureType.Ui_Slider_Value_Lock));
				t_slider.SetBgNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_slider.SetBgLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_slider.SetValueNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_slider.SetValueLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_slider.SetButtonNormalTexture(this.GetTexture(TextureType.Ui_Button_Normal));
				t_slider.SetButtonLockTexture(this.GetTexture(TextureType.Ui_Button_Lock));
				t_slider.SetButtonNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_slider.SetButtonLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			}

			return t_slider;
		}

		/** テキスト。作成。
		*/
		public Fee.Render2D.Text2D CreateText(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Fee.Render2D.Text2D t_text = Fee.Render2D.Text2D.Create(a_deleter,0);
			{
			}
			return t_text;
		}

		/** テキスト。作成。
		*/
		public Fee.Ui.Sprite_Clip CreateClipSprite(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Fee.Ui.Sprite_Clip t_sprite = Fee.Ui.Sprite_Clip.Create(a_deleter,a_drawpriority);
			{
				t_sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_sprite.SetRect(in Fee.Render2D.Config.VIRTUAL_RECT_MAX);
			}
			return t_sprite;
		}

		/** チェックボタン。作成。
		*/
		public Fee.Ui.CheckButton CreateCheckButton(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Fee.Ui.CheckButton t_checkbutton = Fee.Ui.CheckButton.Create(a_deleter,a_drawpriority);
			{
				t_checkbutton.SetBgNormalTexture(this.GetTexture(TextureType.Ui_CheckButton_Normal));
				t_checkbutton.SetBgOnTexture(this.GetTexture(TextureType.Ui_CheckButton_On));
				t_checkbutton.SetBgLockTexture(this.GetTexture(TextureType.Ui_CheckButton_Lock));
				t_checkbutton.SetBgNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_checkbutton.SetBgOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_checkbutton.SetBgLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_checkbutton.SetCheckNormalTexture(this.GetTexture(TextureType.Ui_CheckButton_Check_Normal));
				t_checkbutton.SetCheckLockTexture(this.GetTexture(TextureType.Ui_CheckButton_Check_Lock));
				t_checkbutton.SetCheckNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				t_checkbutton.SetCheckNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			}
			return t_checkbutton;
		}

		/** 入力フィールド。作成。
		*/
		public Fee.Render2D.InputField2D CreateInputField(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Fee.Render2D.InputField2D t_inputfield = Fee.Render2D.InputField2D.Create(a_deleter,a_drawpriority);
			{
			}
			return t_inputfield;
		}
	}
}

