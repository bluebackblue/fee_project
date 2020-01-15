
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
		/** prefablist
		*/
		public Fee.Instantiate.PrefabList prefablist;

		/** texturelist
		*/
		public Fee.Instantiate.TextureList texturelist;

		/** textassetlist
		*/
		public Fee.Instantiate.TextAssetList textassetlist;

		/** fontlist
		*/
		public Fee.Instantiate.FontList fontlist;

		/** constructor
		*/
		public PrefabList()
		{
			UnityEngine.GameObject t_prefab = UnityEngine.Resources.Load<UnityEngine.GameObject>("PrefabList");
			if(t_prefab != null){
				this.prefablist = new Fee.Instantiate.PrefabList(t_prefab.GetComponent<Fee.Instantiate.PrefabList_MonoBehaviour>());
			}
		}

		/** テクスチャーリスト。ロード。
		*/
		public void LoadTextureList()
		{
			UnityEngine.GameObject t_prefab = this.prefablist.GetPrefab("TEXTURELIST");
			if(t_prefab != null){
				this.texturelist = new Fee.Instantiate.TextureList(t_prefab.GetComponent<Fee.Instantiate.TextureList_MonoBehaviour>());
			}
		}

		/** テキストアセットリスト。ロード。
		*/
		public void LoadTextAssetList()
		{
			UnityEngine.GameObject t_prefab = this.prefablist.GetPrefab("TEXTASSETLIST");
			if(t_prefab != null){
				this.textassetlist = new Fee.Instantiate.TextAssetList(t_prefab.GetComponent<Fee.Instantiate.TextAssetList_MonoBehaviour>());
			}
		}

		/** フォントリスト。ロード。
		*/
		public void LoadFontList()
		{
			UnityEngine.GameObject t_prefab = this.prefablist.GetPrefab("FONTLIST");
			if(t_prefab != null){
				this.fontlist = new Fee.Instantiate.FontList(t_prefab.GetComponent<Fee.Instantiate.FontList_MonoBehaviour>());
			}
		}

		/** テクスチャー。取得。
		*/
		public UnityEngine.Texture2D GetTexture(string a_tag)
		{
			return this.texturelist.GetTexture(a_tag);
		}

		/** プレハブ。取得。
		*/
		public UnityEngine.GameObject GetPrefab(string a_tag)
		{
			return this.prefablist.GetPrefab(a_tag);
		}

		/** テキストアセット。取得。
		*/
		public UnityEngine.TextAsset GetTextAsset(string a_tag)
		{
			return this.textassetlist.GetTextAsset(a_tag);
		}

		/** フォント。取得。
		*/
		public UnityEngine.Font GetFont(string a_tag)
		{
			return this.fontlist.GetFont(a_tag);
		}

		/** ボタン。アクティブ設定。
		*/
		public void SetButtonActive(Fee.Ui.Button a_button,bool a_flag)
		{
			if(a_flag == true){
				a_button.SetNormalTexture(this.GetTexture("UI_BUTTON_ACTIVE"));
				a_button.SetOnTexture(this.GetTexture("UI_BUTTON_ACTIVE"));
				a_button.SetDownTexture(this.GetTexture("UI_BUTTON_ACTIVE"));
				a_button.SetLockTexture(this.GetTexture("UI_BUTTON_ACTIVE"));
			}else{
				a_button.SetNormalTexture(this.GetTexture("UI_BUTTON"));
				a_button.SetOnTexture(this.GetTexture("UI_BUTTON"));
				a_button.SetDownTexture(this.GetTexture("UI_BUTTON"));
				a_button.SetLockTexture(this.GetTexture("UI_BUTTON"));
			}
		}

		/** ボタン。作成。
		*/
		public Fee.Ui.Button CreateButton(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Fee.Ui.Button t_button = Fee.Ui.Button.Create(a_deleter,a_drawpriority);
			{
				t_button.SetTextureCornerSize(10);

				t_button.SetNormalTexture(this.GetTexture("UI_BUTTON"));
				t_button.SetOnTexture(this.GetTexture("UI_BUTTON"));
				t_button.SetDownTexture(this.GetTexture("UI_BUTTON"));
				t_button.SetLockTexture(this.GetTexture("UI_BUTTON"));
				t_button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				t_button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				t_button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				t_button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
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

				t_slider.SetBgNormalTexture(this.GetTexture("UI_SLIDER"));
				t_slider.SetBgLockTexture(this.GetTexture("UI_SLIDER"));
				t_slider.SetValueNormalTexture(this.GetTexture("UI_SLIDER"));
				t_slider.SetValueLockTexture(this.GetTexture("UI_SLIDER"));
				t_slider.SetBgNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				t_slider.SetBgLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				t_slider.SetValueNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				t_slider.SetValueLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
				t_slider.SetButtonNormalTexture(this.GetTexture("UI_BUTTON"));
				t_slider.SetButtonLockTexture(this.GetTexture("UI_BUTTON"));
				t_slider.SetButtonNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				t_slider.SetButtonLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
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
		public Fee.Ui.Sprite2D_Clip CreateClipSprite(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Fee.Ui.Sprite2D_Clip t_sprite = Fee.Ui.Sprite2D_Clip.Create(a_deleter,a_drawpriority);
			{
				t_sprite.SetTextureRect(in Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
				t_sprite.SetRect(in Fee.Render2D.Render2D.VIRTUAL_RECT_MAX);
			}
			return t_sprite;
		}

		/** チェックボタン。作成。
		*/
		public Fee.Ui.CheckButton CreateCheckButton(Fee.Deleter.Deleter a_deleter,long a_drawpriority)
		{
			Fee.Ui.CheckButton t_checkbutton = Fee.Ui.CheckButton.Create(a_deleter,a_drawpriority);
			{
				t_checkbutton.SetBgNormalTexture(this.GetTexture("UI_CHECKBUTTON"));
				t_checkbutton.SetBgOnTexture(this.GetTexture("UI_CHECKBUTTON"));
				t_checkbutton.SetBgLockTexture(this.GetTexture("UI_CHECKBUTTON"));
				t_checkbutton.SetBgNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				t_checkbutton.SetBgOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				t_checkbutton.SetBgLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);
				t_checkbutton.SetCheckNormalTexture(this.GetTexture("UI_CHECKBUTTON"));
				t_checkbutton.SetCheckLockTexture(this.GetTexture("UI_CHECKBUTTON"));
				t_checkbutton.SetCheckNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				t_checkbutton.SetCheckNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
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

