
/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief メインベース。
*/


/** TestScript
*/
namespace TestScript
{
	/** MainBase
	*/
	public class MainBase : UnityEngine.MonoBehaviour , Fee.Ui.OnButtonClick_CallBackInterface<MainBase>
	{
		/** is_changescene
		*/
		public bool is_changescene = false;

		/** 戻るボタン。
		*/
		Fee.Ui.Button return_button = null;

		/** 戻るボタン作成。
		*/
		public void CreateReturnButton(Fee.Deleter.Deleter a_deleter,long a_drawpriority,string a_text)
		{
			this.return_button = new Fee.Ui.Button(a_deleter,a_drawpriority);
			this.return_button.SetOnButtonClick(this,this);
			this.return_button.SetText(a_text);
			this.return_button.SetRect(0,0,80,40);
			this.return_button.SetFontSize(10);
			this.return_button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.return_button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.return_button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.return_button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
			this.return_button.SetNormalTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LU);
			this.return_button.SetOnTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RU);
			this.return_button.SetDownTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_LD);
			this.return_button.SetLockTextureRect(ref Fee.Render2D.Config.TEXTURE_RECT_RD);
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(MainBase a_main_base)
		{
			a_main_base.is_changescene = true;
			a_main_base.StartCoroutine(a_main_base.ChangeScene());	
		}

		/** 削除前。
		*/
		public virtual bool PreDestroy(bool a_first)
		{
			return true;
		}

		/** シーン切り替え。チェック。
		*/
		public bool IsChangeScene()
		{
			return this.is_changescene;
		}

		/** シーン切り替え。
		*/
		public System.Collections.IEnumerator ChangeScene()
		{
			bool t_first = true;

			while(this.PreDestroy(t_first) == false){
				t_first = false;
				yield return null;
			}

			bool t_ok = false;
			while(t_ok == false){
				t_ok = true;

				if(Fee.File.File.IsCreateInstance() == true){
					if(Fee.File.File.GetInstance().IsBusy() == true){
						t_ok = false;
					}
				}

				if(Fee.Network.Network.IsCreateInstance() == true){
					if(Fee.Network.Network.GetInstance().IsBusy() == true){
						t_ok = false;
					}
				}

				if(t_ok == false){
					yield return null;
				}
			}

			UnityEngine.GameObject.Destroy(this.gameObject);

			UnityEngine.SceneManagement.SceneManager.LoadScene("main");

			yield break;
		}
	}
}

