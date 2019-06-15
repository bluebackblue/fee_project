
/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief メインベース。
*/


/** TestScript
*/
namespace TestScript
{
	/** MainBase
	*/
	public class MainBase : UnityEngine.MonoBehaviour
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
			this.return_button = new Fee.Ui.Button(a_deleter,a_drawpriority,Click,0);
			this.return_button.SetTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.UI_TEXTURE_BUTTON));
			this.return_button.SetText(a_text);
			this.return_button.SetRect(0,0,80,40);
			this.return_button.SetFontSize(10);
		}

		/** クリック。
		*/
		private void Click(int a_id)
		{
			this.is_changescene = true;
			this.StartCoroutine(ChangeScene());	
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

