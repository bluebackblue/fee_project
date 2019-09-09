

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。マテリアルリスト。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** MaterialList
	*/
	public class MaterialList
	{
		/** material_list
		*/
		private MaterialItem[] material_list;

		/** material_ui_text
		*/
		private MaterialItem material_ui_text;

		/** material_ui_image
		*/
		private MaterialItem material_ui_image;

		/** constructor
		*/
		public MaterialList()
		{
			//material_list
			this.material_list = new MaterialItem[Config.MATERIAL_NAME.Length];
			for(int ii=0;ii<this.material_list.Length;ii++){
				UnityEngine.Material t_material = UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME[ii]);
				if(t_material != null){
					//複製。
					this.material_list[ii] = new MaterialItem(t_material,true);
				}
			}

			//参照。
			this.material_ui_text = new MaterialItem(UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_UITEXT),false);

			//参照。
			this.material_ui_image = new MaterialItem(UnityEngine.Resources.Load<UnityEngine.Material>(Config.MATERIAL_NAME_UIIMAGE),false);
		}

		/** マテリアルアイテム。取得。
		*/
		public MaterialItem GetMaterialItem(Render2D.MaterialType a_material_type)
		{
			return this.material_list[(int)a_material_type];
		}

		/** ＵＩテキストマテリアルアイテム。取得。
		*/
		public MaterialItem GetUiTextMaterialItem()
		{
			return this.material_ui_text;
		}

		/** ＵＩイメージマテリアルアイテム。取得。
		*/
		public MaterialItem GetUiImageMaterialItem()
		{
			return this.material_ui_image;
		}

		/** 削除。
		*/
		public void Delete()
		{
			if(this.material_list != null){
				for(int ii=0;ii<this.material_list.Length;ii++){
					if(this.material_list[ii] != null){
						this.material_list[ii].DestroyImmediate();
						this.material_list[ii] = null;
					}
				}
				this.material_list = null;
			}
		}
	}
}

