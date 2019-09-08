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
	public class test19 : MainBase
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

		/** perceptron
		*/
		private Fee.Perceptron.Perceptron perceptron;

		/** Item
		*/
		private class Item
		{
			/** sprite
			*/
			Fee.Render2D.Sprite2D sprite;

			/** node
			*/
			Fee.Perceptron.Node node;

			/** constructor
			*/
			public Item(Fee.Deleter.Deleter a_deleter,int a_x,int a_y,Fee.Perceptron.Node a_node)
			{
				//sprite
				{
					int t_x = 100 + 50 * a_x;
					int t_y = 100 + 50 * a_y;
					this.sprite = new Fee.Render2D.Sprite2D(a_deleter,1);
					this.sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
					this.sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
					this.sprite.SetRect(t_x,t_y,30,30);
					this.sprite.SetColor(1.0f,1.0f,1.0f,1.0f);
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

			this.list = new List<Item>();
			for(int xx=0;xx<this.perceptron.layer_list.Count;xx++){
				for(int yy=0;yy<this.perceptron.layer_list[xx].node_list.Count;yy++){
					this.list.Add(new Item(this.deleter,xx,yy,this.perceptron.layer_list[xx].node_list[yy]));
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

			//順方向計算。
			this.perceptron.ForwardCalculation();

			//更新。
			for(int ii=0;ii<this.list.Count;ii++){
				this.list[ii].Update();
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

