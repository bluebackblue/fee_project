

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＮＩＶＲＭ。
*/


/** Fee.UniVrm
*/
namespace Fee.UniVrm
{
	/** UniVrm
	*/
	public class UniVrm
	{
		/** [シングルトン]s_instance
		*/
		private static UniVrm s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new UniVrm();
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
		public static UniVrm GetInstance()
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

		/** load
		*/
		private UnityEngine.GameObject vrm_gameobject;
		private MonoBehaviour_Vrm vrm_script;

		/** work_list
		*/
		private System.Collections.Generic.List<Work> work_list;

		/** add_list
		*/
		private System.Collections.Generic.List<Work> add_list;

		/** [シングルトン]constructor
		*/
		private UniVrm()
		{
			//ルート。
			this.root_gameobject = new UnityEngine.GameObject();
			this.root_gameobject.name = "UniVrm";
			UnityEngine.GameObject.DontDestroyOnLoad(this.root_gameobject);
			this.root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();

			//vrm
			{
				this.vrm_gameobject = new UnityEngine.GameObject();
				this.vrm_gameobject.name = "UniVrm_Vrm";
				this.vrm_script = this.vrm_gameobject.AddComponent<MonoBehaviour_Vrm>();
				this.vrm_gameobject.GetComponent<UnityEngine.Transform>().SetParent(this.root_transform);
			}

			//work_list
			this.work_list = new System.Collections.Generic.List<Work>();

			//add_list
			this.add_list = new System.Collections.Generic.List<Work>();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
			//削除リクエスト。
			if(this.vrm_gameobject != null){
				this.vrm_gameobject.GetComponent<UnityEngine.Transform>().SetParent(null);
				UnityEngine.GameObject.DontDestroyOnLoad(this.vrm_gameobject);
			}
			this.vrm_script.DeleteRequest();

			//ルート削除。
			UnityEngine.GameObject.Destroy(this.root_gameobject);
		}

		/** MonoVrm。取得。
		*/
		public MonoBehaviour_Vrm GetMonoVrm()
		{
			return this.vrm_script;
		}

		/** リクエスト。ロード。
		*/
		public Item Request(byte[] a_binary)
		{
			Work t_work = new Work();
			t_work.Request(a_binary);

			this.add_list.Add(t_work);
			return t_work.GetItem();
		}

		/** 処理中。チェック。
		*/
		public bool IsBusy()
		{
			if((this.work_list.Count > 0)||(this.add_list.Count > 0)){
				return true;
			}
			return false;
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//追加。
				if(this.add_list.Count > 0){
					for(int ii=0;ii<this.add_list.Count;ii++){
						this.work_list.Add(this.add_list[ii]);
					}
					this.add_list.Clear();
				}

				int t_index = 0;
				while(t_index < this.work_list.Count){
					if(this.work_list[t_index].Main() == true){
						this.work_list.RemoveAt(t_index);
					}else{
						t_index++;
					}
				}
			}catch(System.Exception t_exception){
				Tool.LogError(t_exception);
			}
		}
	}
}

