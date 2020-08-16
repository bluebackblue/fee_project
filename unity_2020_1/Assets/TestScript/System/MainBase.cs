
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
		public Fee.Ui.Button return_button = null;

		/** 終了処理。
		*/
		private void OnApplicationQuit()
		{
			//全部削除。
			DeleteLibInstance.DeleteAll();
			UnityEngine.Debug.Log("OnApplicationQuit");
		}

		/** constructor
		*/
		public MainBase()
		{
		}

		/** 戻るボタン作成。
		*/
		public void CreateReturnButton(Common.PrefabList a_prefablist,Fee.Deleter.Deleter a_deleter,long a_drawpriority,string a_text)
		{
			this.return_button = a_prefablist.CreateButton(a_deleter,a_drawpriority);
			{
				this.return_button.SetOnButtonClick(this,this);
				this.return_button.SetText(a_text);
				this.return_button.SetRect(0,0,80,40);
				this.return_button.SetFontSize(10);
			}
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

		/** 削除。
		*/
		public virtual void Destroy()
		{
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
			//テストクラスのPreDestroyを呼び出す。
			{
				bool t_first = true;
				while(this.PreDestroy(t_first) == false){
					t_first = false;
					yield return null;
				}
			}

			bool t_ok = false;
			while(t_ok == false){
				t_ok = true;

				//ファイル操作が終わるまで待つ。
				if(Fee.File.File.IsCreateInstance() == true){
					if(Fee.File.File.GetInstance().IsBusy() == true){
						t_ok = false;
					}
				}

				//通信が終わるまで待つ。
				if(Fee.Network.Network.IsCreateInstance() == true){
					if(Fee.Network.Network.GetInstance().IsBusy() == true){
						t_ok = false;
					}
				}

				yield return null;
			}

			//テストクラスのDestroyを呼び出す。
			this.Destroy();

			//テストクラスを削除。
			UnityEngine.GameObject.DestroyImmediate(this.gameObject);

			//シーンをロード。
			UnityEngine.SceneManagement.SceneManager.LoadScene("main");

			yield break;
		}
	}
}

