
/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief メイン。
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
	}
}

