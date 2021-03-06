

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief テスト。
*/


/** TestScript
*/
namespace TestScript
{
	/** test23

		フォーカス

	*/
	public class test23 : MainBase , Fee.Focus.Compare_Base<int>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test23",
				"test23",

				@"
				フォーカス
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** focusgroup
		*/
		private Fee.Focus.FocusGroup<int> focusgroup;

		/** Custom
		*/
		private class Custom : Fee.Focus.FocusItem_Base , Fee.EventPlate.OnEventPlateOver_CallBackInterface<int>
		{
			/** is_focus
			*/
			public bool is_focus;

			/** focus_id
			*/
			public int focus_id;

			/** focusgroup
			*/
			public Fee.Focus.FocusGroup<int> focusgroup;

			/** is_on
			*/
			public bool is_on;

			/** deleter
			*/
			public Fee.Deleter.Deleter deleter;

			/** eventplate
			*/
			public Fee.EventPlate.Item eventplate;

			/** text
			*/
			public Fee.Render2D.Text2D text;

			/** constructor
			*/
			public Custom(Fee.Deleter.Deleter a_deleter,Common.PrefabList a_prefablist)
			{
				this.deleter = a_deleter;
				this.text = a_prefablist.CreateText(this.deleter,0);
				this.is_focus = false;
				this.eventplate = new Fee.EventPlate.Item(this.deleter,Fee.EventPlate.EventType.Button,0);
				this.eventplate.SetOnEventPlateOver(this,0);
				this.is_on = false;
			}

			/** SetOnFocusCheck
			*/
			public void SetOnFocusCheck(Fee.Focus.FocusGroup<int> a_focusgroup,int a_focus_id)
			{
				this.focus_id = a_focus_id;
				this.focusgroup = a_focusgroup;
			}

			/** SetRect
			*/
			public void SetRect(int a_x,int a_y,int a_w,int a_h)
			{
				this.eventplate.SetRect(a_x,a_y,a_w,a_h);
				this.text.SetRect(a_x,a_y,0,0);
			}

			/** [Fee.Focus.FocusItem_Base]フォーカス。設定。

				OnFocusCheckを呼び出さない。

			*/
			public void SetFocus_NoCall(bool a_flag)
			{
				this.is_focus = a_flag;
			}

			/** [Fee.Focus.FocusItem_Base]フォーカス。設定。

				OnFocusCheckを呼び出す。

			*/
			public void SetFocus(bool a_flag)
			{
				this.SetFocus_NoCall(a_flag);
				this.focusgroup.OnFocusCheck(this.focus_id);
			}

			/** [Fee.Focus.FocusItem_Base]フォーカス。チェック。
			*/
			public bool IsFocus()
			{
				return this.is_focus;
			}

			/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートに入場。
			*/
			public void OnEventPlateEnter(int a_id)
			{
				this.is_on = true;
			}

			/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートから退場。
			*/
			public void OnEventPlateLeave(int a_id)
			{
				this.is_on = false;
			}
		}

		/** インスタンス。
		*/
		private Fee.Ui.Button button_1;
		private Fee.Ui.Button button_2;
		private Fee.Ui.Input input_1;
		private Fee.Ui.Input input_2;
		private Custom custom_1;
		private Custom custom_2;

		/** [Fee.Focus.Compare_Base]比較。
		*/
		public bool Compare(int a_id_a,int a_id_b)
		{
			return (a_id_a == a_id_b);
		}

		/** Start
		*/
		private void Start()
		{
			//プレイヤーループシステム。インスタンス作成。
			Fee.PlayerLoopSystem.PlayerLoopSystem.CreateInstance(null);
			Fee.PlayerLoopSystem.PlayerLoopSystem.GetInstance().RemoveFromType(typeof(UnityEngine.PlayerLoop.PreUpdate.SendMouseEvents));

			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.CreateInstance();
			Fee.Function.Function.GetInstance().SetMonoBehaviour(this);
			Fee.Function.Function.GetInstance().SetRowUpdate(this.RowUpdate);

			//インスタンス作成。
			Fee.Instantiate.Instantiate.CreateInstance();

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//入力。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Input.CreateInstance(true,false,true,false);
			Fee.Input.Input.GetInstance().key.Regist(Fee.Input.Status_Key_Type.Space);

			//イベントプレート。
			//Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			//Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//フォーカス。
			Fee.Focus.Focus.CreateInstance();

			//プレハブリスト。
			this.prefablist = new Common.PrefabList();

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont(Common.FontType.Font));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Config.MAX_LAYER - 1) * Fee.Render2D.Config.DRAWPRIORITY_STEP,this.name + ":Return");

			{
				this.button_1 = this.prefablist.CreateButton(this.deleter,0);
				this.button_1.SetRect(100 + 100 * 0,100,100,60);
				this.button_1.SetText("button_1");

				this.button_2 = this.prefablist.CreateButton(this.deleter,0);
				this.button_2.SetRect(100 + 100 * 1,100,100,60);
				this.button_2.SetText("button_2");

				this.input_1 = this.prefablist.CreateInputField(this.deleter,0);
				this.input_1.SetRect(100 + 100 * 2,100,100,60);

				this.input_2 = this.prefablist.CreateInputField(this.deleter,0);
				this.input_2.SetRect(100 + 100 * 3,100,100,60);

				this.custom_1 = new Custom(this.deleter,this.prefablist);
				this.custom_1.SetRect(100 + 100 * 4,100,100,60);
				this.custom_1.text.SetText("custom_1");

				this.custom_2 = new Custom(this.deleter,this.prefablist);
				this.custom_2.SetRect(100 + 100 * 5,100,100,60);
				this.custom_2.text.SetText("custom_2");

				this.focusgroup = new Fee.Focus.FocusGroup<int>(-1,this);
				{
					System.Action<Fee.Ui.Button,bool> t_button_on = (Fee.Ui.Button a_item,bool a_change) => {
						if(a_change == true){
							a_item.SetOnTextColor(1.0f,0.0f,0.0f,1.0f);
							a_item.SetNormalTextColor(1.0f,0.0f,0.0f,1.0f);
						}
					};
					System.Action<Fee.Ui.Button,bool> t_button_off = (Fee.Ui.Button a_item,bool a_change) => {
						if(a_change == true){
							a_item.SetOnTextColor(1.0f,1.0f,1.0f,1.0f);
							a_item.SetNormalTextColor(1.0f,1.0f,1.0f,1.0f);
						}
					};

					System.Action<Fee.Ui.Input,bool> t_input_on = (Fee.Ui.Input a_item,bool a_change) => {
						if(a_change == true){
							a_item.SetTextColor(1.0f,0.0f,0.0f,1.0f);
						}
					};
					System.Action<Fee.Ui.Input,bool> t_input_off = (Fee.Ui.Input a_item,bool a_change) => {
						if(a_change == true){
							a_item.SetTextColor(0.0f,0.0f,0.0f,1.0f);
						}
					};

					System.Action<Custom,bool> t_custom_on = (Custom a_item,bool a_change) => {
						if(a_change == true){
							a_item.text.SetColor(1.0f,0.0f,0.0f,1.0f);
						}
					};
					System.Action<Custom,bool> t_custom_off = (Custom a_item,bool a_change) => {
						if(a_change == true){
							a_item.text.SetColor(1.0f,1.0f,1.0f,1.0f);
						}
					};

					//ＩＤ、コールバックの関連付け。
					this.focusgroup.AddID<Fee.Ui.Button>(0,this.button_1,t_button_on,t_button_off);
					this.focusgroup.AddID<Fee.Ui.Button>(1,this.button_2,t_button_on,t_button_off);
					this.focusgroup.AddID<Fee.Ui.Input>(2,this.input_1,t_input_on,t_input_off);
					this.focusgroup.AddID<Fee.Ui.Input>(3,this.input_2,t_input_on,t_input_off);
					this.focusgroup.AddID<Custom>(4,this.custom_1,t_custom_on,t_custom_off);
					this.focusgroup.AddID<Custom>(5,this.custom_2,t_custom_on,t_custom_off);
				}

				//インスタンス、ＩＤの関連付け。
				this.button_1.SetOnFocusCheck(this.focusgroup,0);
				this.button_2.SetOnFocusCheck(this.focusgroup,1);
				this.input_1.SetOnFocusCheck(this.focusgroup,2);
				this.input_2.SetOnFocusCheck(this.focusgroup,3);
				this.custom_1.SetOnFocusCheck(this.focusgroup,4);
				this.custom_2.SetOnFocusCheck(this.focusgroup,5);
			}
		}

		/** RowUpdate
		*/
		private void RowUpdate()
		{
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
		}

		/** Update
		*/
		private void Update()
		{
			if(this.custom_1.is_on == true){
				if(Fee.Input.Input.GetInstance().mouse.left.down == true){
					this.custom_1.SetFocus(true);
				}
			}else if(this.custom_2.is_on == true){
				if(Fee.Input.Input.GetInstance().mouse.left.down == true){
					this.custom_2.SetFocus(true);
				}
			}

			if(Fee.Input.Input.GetInstance().key.GetKey(Fee.Input.Status_Key_Type.Space).digital.down == true){
				this.focusgroup.SetFocusAllOff();
			}
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
		}

		/** 強制終了。
		*/
		public override void Shutdown()
		{
			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.RowUpdate);
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			Fee.Function.Function.GetInstance().UnSetRowUpdate(this.RowUpdate);
			return true;
		}

		/** 削除。
		*/
		public override void Destroy()
		{
			//削除。
			this.deleter.DeleteAll();

			//ライブラリ停止。
			DeleteLibInstance.DeleteAll();
		}
	}
}

