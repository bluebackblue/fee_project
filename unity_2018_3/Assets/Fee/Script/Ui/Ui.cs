

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ＵＩ。
*/


/** Fee.Ui
*/
namespace Fee.Ui
{
	/** Ui
	*/
	public class Ui : Config
	{
		/** [シングルトン]s_instance
		*/
		private static Ui s_instance = null;

		/** [シングルトン]インスタンス。作成。
		*/
		public static void CreateInstance()
		{
			if(s_instance == null){
				s_instance = new Ui();
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
		public static Ui GetInstance()
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

		//ウィンドウリスト。
		private Ui_WindowList windowlist;

		//ターゲットリスト。
		private System.Collections.Generic.List<OnTargetCallBack_Base> target_list;

		//ターゲット追加リスト。
		private System.Collections.Generic.List<OnTargetCallBack_Base> target_add_list;

		//ターゲット削除リスト。
		private System.Collections.Generic.List<OnTargetCallBack_Base> target_remove_list;

		//ウィンドウレジュームリスト。
		private Ui_WindowResumeList windowresumelist;

		/** [シングルトン]constructor
		*/
		private Ui()
		{
			//ウィンドウリスト。
			this.windowlist = new Ui_WindowList();

			//target_list
			this.target_list = new System.Collections.Generic.List<OnTargetCallBack_Base>();

			//target_add_list
			this.target_add_list = new System.Collections.Generic.List<OnTargetCallBack_Base>();

			//target_remove_list
			this.target_remove_list = new System.Collections.Generic.List<OnTargetCallBack_Base>();

			//ウィンドウレジュームリスト。
			this.windowresumelist = new Ui_WindowResumeList();
		}

		/** [シングルトン]削除。
		*/
		private void Delete()
		{
		}

		/** 更新。
		*/
		public void Main()
		{
			try{
				//ウィンドウリスト。
				this.windowlist.Main();

				//追加。
				for(int ii=0;ii<this.target_add_list.Count;ii++){
					this.target_list.Add(this.target_add_list[ii]);
				}
				this.target_add_list.Clear();

				//削除。
				for(int ii=0;ii<this.target_remove_list.Count;ii++){
					this.target_list.Remove(this.target_remove_list[ii]);
				}
				this.target_remove_list.Clear();

				//呼び出し。
				for(int ii=0;ii<this.target_list.Count;ii++){
					this.target_list[ii].OnTarget();
				}
			}catch(System.Exception t_exception){
				Tool.DebugReThrow(t_exception);
			}
		}

		/** 追加リクエスト。設定。
		*/
		public void SetTargetRequest(OnTargetCallBack_Base a_item)
		{
			this.target_add_list.Add(a_item);
			this.target_remove_list.Remove(a_item);
		}

		/** 削除リクエスト。解除。
		*/
		public void UnSetTargetRequest(OnTargetCallBack_Base a_item)
		{
			this.target_remove_list.Add(a_item);
			this.target_add_list.Remove(a_item);
		}

		/** ウィンドウ登録。
		*/
		public void RegisterWindow(Window_Base a_window)
		{
			this.windowlist.Register(a_window);
		}

		/** ウィンドウ解除。
		*/
		public void UnRegisterWindow(Window_Base a_window)
		{
			this.windowlist.UnRegister(a_window);
		}

		/** ウィンドウを最前面にする。
		*/
		public void SetWindowPriorityTopMost(Window_Base a_window)
		{
			this.windowlist.SetWindowPriorityTopMost(a_window);
		}

		/** ウィンドウスタートレイヤーインデックス。
		*/
		public void SetWindowStartLayerIndex(int a_layerindex)
		{
			this.windowlist.SetStartLayerIndex(a_layerindex);
		}

		/** 最前面ウィンドウ矩形。取得。
		*/
		public bool GetTopWindowXY(out int a_x,out int a_y)
		{
			return this.windowlist.GetTopWindowXY(out a_x,out a_y);
		}

		/** ウィンドウ数。取得。
		*/
		public int GetWindowCount()
		{
			return this.windowlist.GetWindowCount();
		}

		/** ウィンドウレジューム。登録。

			a_new_rect : 新規作成の場合に設定する矩形。

		*/
		public WindowResumeItem RegisterWindowResume(string a_label,ref Render2D.Rect2D_R<int> a_new_rect)
		{
			//登録。
			bool t_is_new = false;
			if(this.windowresumelist.Register(a_label) == true){
				t_is_new = true;
			}

			//取得。
			WindowResumeItem t_item = this.windowresumelist.GetItem(a_label);
			if(t_item != null){
				if(t_is_new == true){
					t_item.rect = a_new_rect;
				}
			}

			return t_item;
		}

		/** ウィンドウレジューム。解除。
		*/
		public void UnRegisterWindowResume(string a_label)
		{
			this.windowresumelist.UnRegist(a_label);
		}

		/** ウィンドウレジューム。取得。
		*/
		public WindowResumeItem GetWindowResumeItem(string a_label)
		{
			return this.windowresumelist.GetItem(a_label);
		}
	}
}

