using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
	/** test19

		パーセプトロン

	*/
	public class test19 : MainBase , Fee.Ui.OnButtonClick_CallBackInterface<test19.ButtonID>
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test19",
				"test19",

				@"
				パーセプトロン
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** パーセプトロン。
		*/
		private Fee.Perceptron.Perceptron perceptron;

		/** backpropagation_button
		*/
		private Fee.Ui.Button backpropagation_button;
		private bool backpropagation_flag;

		/** ButtonID
		*/
		public enum ButtonID
		{
			BackPropagation
		}

		/** Item
		*/
		private class Item : Fee.EventPlate.OnEventPlateOver_CallBackInterface<Fee.Perceptron.Node>
		{
			/** sprite
			*/
			Fee.Render2D.Sprite2D sprite_bg;
			Fee.Render2D.Sprite2D sprite;

			/** eventplate
			*/
			Fee.EventPlate.Item eventplate;

			/** node
			*/
			Fee.Perceptron.Node node;

			/** text
			*/
			Fee.Render2D.Text2D text;

			/** constructor
			*/
			public Item(Fee.Deleter.Deleter a_deleter,int a_x,int a_y,Fee.Perceptron.Node a_node)
			{
				{
					int t_x = 100 + 50 * a_x;
					int t_y = 100 + 50 * a_y;
					int t_size = 30;

					//sprite_bg
					this.sprite_bg = new Fee.Render2D.Sprite2D(a_deleter,1);
					this.sprite_bg.SetTexture(UnityEngine.Texture2D.whiteTexture);
					this.sprite_bg.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
					this.sprite_bg.SetRect(t_x,t_y,t_size,t_size);
					this.sprite_bg.SetColor(1.0f,1.0f,1.0f,1.0f);

					//sprite
					this.sprite = new Fee.Render2D.Sprite2D(a_deleter,2);
					this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
					this.sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
					this.sprite.SetRect(t_x + 2,t_y + 2,t_size - 4,t_size - 4);
					this.sprite.SetColor(1.0f,1.0f,1.0f,1.0f);

					//eventplate
					this.eventplate = new Fee.EventPlate.Item(a_deleter,Fee.EventPlate.EventType.Button,1);
					this.eventplate.SetRect(t_x,t_y,t_size,t_size);
					this.eventplate.SetOnEventPlateOver<Fee.Perceptron.Node>(this,a_node);

					//text
					this.text = new Fee.Render2D.Text2D(a_deleter,1);
					this.text.SetText("");
					this.text.SetVisible(false);
					this.text.SetRect(t_x + 20,t_y - 20,0,0);
					this.text.SetColor(1.0f,0.2f,0.2f,1.0f);
				}

				//node
				this.node = a_node;
			}

			/** Update
			*/
			public void Update()
			{
				float t_color = this.node.value;
				this.sprite.SetColor(t_color,t_color,t_color,1.0f);

				if(this.node.is_bias == true){
					this.sprite_bg.SetColor(0.0f,1.0f,0.0f,1.0f);
				}else{
					this.sprite_bg.SetColor(1.0f,1.0f,1.0f,1.0f);
				}

				if(this.eventplate.IsOnOver() == true){

					if(Fee.Input.Mouse.GetInstance().left.down == true){
						if(this.node.value == 0.0f){
							this.node.value = 1.0f;
						}else{
							this.node.value = 0.0f;
						}
					}

					this.text.SetVisible(true);
					this.text.SetText(this.node.value.ToString());

				}else{

					this.text.SetVisible(false);

				}
			}

			/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートに入場。
			*/
			public void OnEventPlateEnter(Fee.Perceptron.Node a_id)
			{
			}

			/** [Fee.Ui.OnEventPlateOver_CallBackInterface]イベントプレートから退場。
			*/
			public void OnEventPlateLeave(Fee.Perceptron.Node a_id)
			{
			}
		}

		/** list
		*/
		private System.Collections.Generic.List<Item> list;

		/** Start
		*/
		private void Start()
		{
			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Mouse.CreateInstance();

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//パーセプトロン。
			this.perceptron = new Fee.Perceptron.Perceptron(5,4,5);

			//backpropagation
			{
				int t_x = 100;
				int t_y = 50;
				int t_w = 150;
				int t_h = 30;

				//backpropagation_button
				this.backpropagation_button = new Fee.Ui.Button(this.deleter,1);
				this.backpropagation_button = new Fee.Ui.Button(this.deleter,0);
				this.backpropagation_button.SetOnButtonClick(this,ButtonID.BackPropagation);
				this.backpropagation_button.SetRect(t_x,t_y,t_w,t_h);
				this.backpropagation_button.SetText("BackPropagation");
				this.backpropagation_button.SetTextureCornerSize(10);
				this.backpropagation_button.SetFontSize(13);
				this.backpropagation_button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.backpropagation_button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.backpropagation_button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.backpropagation_button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
				this.backpropagation_button.SetNormalTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LU);
				this.backpropagation_button.SetOnTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RU);
				this.backpropagation_button.SetDownTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_LD);
				this.backpropagation_button.SetLockTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_RD);

				//backpropagation_flag
				this.backpropagation_flag = false;
			}

			//表示。
			this.list = new List<Item>();
			
			//計算層。
			for(int xx=0;xx<this.perceptron.layer_list.Count;xx++){
				for(int yy=0;yy<this.perceptron.layer_list[xx].node_list.Count;yy++){
					this.list.Add(new Item(this.deleter,xx,yy,this.perceptron.layer_list[xx].node_list[yy]));
				}
			}

			//教師層。
			{
				int xx = this.perceptron.layer_list.Count + 1;
				for(int yy=0;yy<this.perceptron.layer_teacher.node_list.Count;yy++){
					this.list.Add(new Item(this.deleter,xx,yy,this.perceptron.layer_teacher.node_list[yy]));
				}
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//マウス。
			Fee.Input.Mouse.GetInstance().Main(true,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			//パーセプトロン。順方向計算。
			this.perceptron.ForwardCalculation();

			//パーセプトロン。バックプロパゲート。
			if(this.backpropagation_flag == true){
				this.perceptron.BackPropagate();
			}

			//表示更新。
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].Update();
			}
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonID a_id)
		{
			switch(a_id){
			case ButtonID.BackPropagation:
				{
					if(this.backpropagation_flag == true){
						this.backpropagation_flag = false;

						this.backpropagation_button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
						this.backpropagation_button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
						this.backpropagation_button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
						this.backpropagation_button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON));
					}else{
						this.backpropagation_flag = true;

						this.backpropagation_button.SetNormalTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON_ACTIVE));
						this.backpropagation_button.SetOnTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON_ACTIVE));
						this.backpropagation_button.SetDownTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON_ACTIVE));
						this.backpropagation_button.SetLockTexture(UnityEngine.Resources.Load<UnityEngine.Texture2D>(Data.Resources.UI_TEXTURE_BUTTON_ACTIVE));
					}
				}break;
			}
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
			return true;
		}

		/** OnDestroy
		*/
		private void OnDestroy()
		{
			this.deleter.DeleteAll();
		}
	}
}

