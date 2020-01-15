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

		/** prefablist
		*/
		private Common.PrefabList prefablist;

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

			/** line
			*/
			Fee.Ui.Line2D[] line;

			/** constructor
			*/
			public Item(Common.PrefabList a_prefablist,Fee.Deleter.Deleter a_deleter,int a_layer_index,int a_node_index,Fee.Perceptron.Node a_node)
			{
				int t_size = 30;
				int t_offset_x = 100;
				int t_offset_y = 100;
				int t_alignment_x = 100;
				int t_alignment_y = 60;

				{
					int t_x = t_offset_x + t_alignment_x * a_layer_index;
					int t_y = t_offset_y + t_alignment_y * a_node_index;
					

					//sprite_bg
					this.sprite_bg = Fee.Render2D.Sprite2D.Create(a_deleter,1);
					this.sprite_bg.SetTexture(UnityEngine.Texture2D.whiteTexture);
					this.sprite_bg.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
					this.sprite_bg.SetRect(t_x,t_y,t_size,t_size);
					this.sprite_bg.SetColor(1.0f,1.0f,1.0f,1.0f);

					//sprite
					this.sprite = Fee.Render2D.Sprite2D.Create(a_deleter,2);
					this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
					this.sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
					this.sprite.SetRect(t_x + 2,t_y + 2,t_size - 4,t_size - 4);
					this.sprite.SetColor(1.0f,1.0f,1.0f,1.0f);

					//eventplate
					this.eventplate = new Fee.EventPlate.Item(a_deleter,Fee.EventPlate.EventType.Button,1);
					this.eventplate.SetRect(t_x,t_y,t_size,t_size);
					this.eventplate.SetOnEventPlateOver<Fee.Perceptron.Node>(this,a_node);

					//text
					this.text = a_prefablist.CreateText(a_deleter,1);
					this.text.SetText("");
					this.text.SetVisible(false);
					this.text.SetRect(t_x + 20,t_y - 20,0,0);
					this.text.SetColor(1.0f,0.2f,0.2f,1.0f);

					//line
					this.line = new Fee.Ui.Line2D[a_node.link_list.Count];
					for(int ii=0;ii<a_node.link_list.Count;ii++){
						this.line[ii] = Fee.Ui.Line2D.Create(a_deleter,3);
						this.line[ii].SetSize(2);
						int t_x_to = t_offset_x + t_alignment_x * a_node.link_list[ii].node_to.layer_parent.layer_index;
						int t_y_to = t_offset_y + t_alignment_y * a_node.link_list[ii].node_to.node_index;
						this.line[ii].SetRect(new Fee.Geometry.Rect2D_A<int>(t_x + t_size / 2,t_y + t_size / 2,t_x_to + t_size / 2,t_y_to + t_size / 2));
					}
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

				for(int ii=0;ii<this.node.link_list.Count;ii++){
					int t_size = (int)UnityEngine.Mathf.Abs(this.node.link_list[ii].weight);
					this.line[ii].SetSize(t_size);

					if(this.node.link_list[ii].weight > 0.0f){
						this.line[ii].SetColor(1.0f,0.0f,0.0f,1.0f);
					}else{
						this.line[ii].SetColor(0.0f,0.0f,1.0f,1.0f);
					}
				}

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

					//stringbuilder
					System.Text.StringBuilder t_stringbuilder = new System.Text.StringBuilder(64);

					t_stringbuilder.Append("value : " + this.node.value.ToString() + "\n");
					t_stringbuilder.Append("error : " + this.node.error.ToString() + "\n");
					t_stringbuilder.Append("\n");

					for(int ii=0;ii<this.node.link_list.Count;ii++){
						t_stringbuilder.Append(this.node.link_list[ii].weight.ToString());
						t_stringbuilder.Append("\n");
					}
					this.text.SetText(t_stringbuilder.ToString());
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
			//プラットフォーム。インスタンス作成。
			Fee.Platform.Platform.CreateInstance();

			//タスク。インスタンス作成。
			Fee.TaskW.TaskW.CreateInstance();

			//パフォーマンスカウンター。インスタンス作成。
			Fee.PerformanceCounter.Config.LOG_ENABLE = true;
			Fee.PerformanceCounter.PerformanceCounter.CreateInstance();

			//関数呼び出し。
			Fee.Function.Function.SetMonoBehaviour(this);

			//２Ｄ描画。インスタンス作成。
			Fee.Render2D.Config.FIRSTGLCAMERA_CLEAR_RENDERTEXTURE = true;
			Fee.Render2D.Config.LOG_ENABLE = true;
			Fee.Render2D.Config.ReCalcWH();
			Fee.Render2D.Render2D.CreateInstance();

			//マウス。インスタンス作成。
			Fee.Input.Config.LOG_ENABLE = true;
			Fee.Input.Mouse.CreateInstance();

			//イベントプレート。
			Fee.EventPlate.Config.LOG_ENABLE = true;
			Fee.EventPlate.EventPlate.CreateInstance();

			//ＵＩ。インスタンス作成。
			Fee.Ui.Config.LOG_ENABLE = false;
			Fee.Ui.Ui.CreateInstance();

			//プレハブリスト。
			{
				this.prefablist = new Common.PrefabList();
				this.prefablist.LoadFontList();
				this.prefablist.LoadTextureList();
			}

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont("FONT"));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//パーセプトロン。
			this.perceptron = new Fee.Perceptron.Perceptron(4,4,0,2);

			//backpropagation
			{
				int t_x = 100;
				int t_y = 50;
				int t_w = 150;
				int t_h = 30;

				//backpropagation_button
				this.backpropagation_button = this.prefablist.CreateButton(this.deleter,0);
				this.backpropagation_button.SetOnButtonClick(this,ButtonID.BackPropagation);
				this.backpropagation_button.SetRect(t_x,t_y,t_w,t_h);
				this.backpropagation_button.SetText("BackPropagation");

				//backpropagation_flag
				this.backpropagation_flag = false;
			}

			//表示。
			this.list = new List<Item>();
			
			//計算レイヤー。
			for(int t_layerindex=0;t_layerindex<this.perceptron.layer_list.Count;t_layerindex++){
				for(int t_node_index=0;t_node_index<this.perceptron.layer_list[t_layerindex].node_list.Count;t_node_index++){
					Fee.Perceptron.Node t_node = this.perceptron.layer_list[t_layerindex].node_list[t_node_index];

					//乱数初期化。
					for(int ll=0;ll<t_node.link_list.Count;ll++){
						if(t_node.link_list[ll].node_to.is_bias == true){
							//接続先がバイアス。
							t_node.link_list[ll].weight = 0.0f;
						}else{
							t_node.link_list[ll].weight = UnityEngine.Random.Range(-1.0f,1.0f);
						}
					}

					//表示アイテム。
					this.list.Add(new Item(this.prefablist,this.deleter,t_layerindex,t_node_index,t_node));
				}
			}

			//教師レイヤー。
			{
				int xx = this.perceptron.layer_list.Count + 1;
				for(int yy=0;yy<this.perceptron.layer_teacher.node_list.Count;yy++){
					this.list.Add(new Item(this.prefablist,this.deleter,xx,yy,this.perceptron.layer_teacher.node_list[yy]));
				}
			}
		}

		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_Before();

			//マウス。
			Fee.Input.Mouse.GetInstance().Main(this.is_focus,Fee.Render2D.Render2D.GetInstance());

			//イベントプレート。
			Fee.EventPlate.EventPlate.GetInstance().Main(in Fee.Input.Mouse.GetInstance().cursor.pos);

			//ＵＩ。
			Fee.Ui.Ui.GetInstance().Main();

			if(this.backpropagation_flag == true){

				//パーセプトロン。学習。
				for(int t_loop=0;t_loop<50;t_loop++){
					float t_in_1 = 0.0f;
					float t_in_2 = 0.0f;
					float t_teacher = 0.0f;

					switch(UnityEngine.Random.Range(0,4)){
					case 0:
						{
							t_in_1 = 0.0f;
							t_in_2 = 0.0f;
							t_teacher = 0.0f;
						}break;
					case 1:
						{
							t_in_1 = 0.0f;
							t_in_2 = 1.0f;
							t_teacher = 1.0f;
						}break;
					case 2:
						{
							t_in_1 = 1.0f;
							t_in_2 = 0.0f;
							t_teacher = 1.0f;
						}break;
					case 3:
						{
							t_in_1 = 1.0f;
							t_in_2 = 1.0f;
							t_teacher = 0.0f;
						}break;
					}

					this.perceptron.layer_list[0].node_list[0].value = t_in_1;
					this.perceptron.layer_list[0].node_list[1].value = t_in_1;
					this.perceptron.layer_list[0].node_list[2].value = t_in_2;
					this.perceptron.layer_list[0].node_list[3].value = t_in_2;
					this.perceptron.layer_teacher.node_list[0].value = t_teacher;
					this.perceptron.layer_teacher.node_list[1].value = t_teacher;

					//計算。
					this.perceptron.ForwardCalculation();

					//学習。
					this.perceptron.BackPropagation();
				}
			}else{
				//パーセプトロン。順方向計算。
				this.perceptron.ForwardCalculation();
			}

			//表示更新。
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].Update();
			}

			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_After();
		}

		/** Update
		*/
		private void Update()
		{
			//２Ｄ描画。
			Fee.Render2D.Render2D.GetInstance().Main_PreDraw();
		}

		/** [Fee.Ui.OnButtonClick_CallBackInterface]クリック。
		*/
		public void OnButtonClick(ButtonID a_id)
		{
			this.backpropagation_flag ^= true;

			if(this.backpropagation_flag == true){
				this.prefablist.SetButtonActive(this.backpropagation_button,true);
			}else{
				this.prefablist.SetButtonActive(this.backpropagation_button,false);
			}
		}

		/** 削除前。
		*/
		public override bool PreDestroy(bool a_first)
		{
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

