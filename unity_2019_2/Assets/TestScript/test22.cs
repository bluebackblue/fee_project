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
	/** test22
	*/
	public class test22 : MainBase
	{
		/** CreateStatus
		*/
		public static TestStatus CreateStatus()
		{
			return new TestStatus(
				"test22",
				"test22",

				@"
				ビデオ
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** prefablist
		*/
		private Common.PrefabList prefablist;

		/** bg
		*/
		private Fee.Render2D.Sprite2D bg_sprite;

		/** video
		*/
		private Fee.Video.Item video_item_1;
		private Fee.Video.Item video_item_2;
		private Fee.Video.Item video_item_3;
		private Fee.Render2D.Sprite2D video_sprite_1;
		private Fee.Render2D.Sprite2D video_sprite_2;
		private Fee.Render2D.Sprite2D video_sprite_3;
		private Fee.Render2D.Text2D video_text_1;
		private Fee.Render2D.Text2D video_text_2;
		private Fee.Render2D.Text2D video_text_3;

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
			Fee.Ui.Config.LOG_ENABLE = true;
			Fee.Ui.Ui.CreateInstance();

			//ムービー。インスタンス作成。
			Fee.Video.Video.CreateInstance();

			//プレハブリスト。
			{
				this.prefablist = new Common.PrefabList();
				this.prefablist.LoadFontList();
				this.prefablist.LoadTextureList();
				this.prefablist.LoadVideoClipList();
			}

			//フォント。
			Fee.Render2D.Render2D.GetInstance().SetDefaultFont(this.prefablist.GetFont("FONT"));

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.prefablist,this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//bg
			{
				this.bg_sprite = Fee.Render2D.Sprite2D.Create(this.deleter,0);
				this.bg_sprite.SetRect(in Fee.Render2D.Config.VIRTUAL_RECT_MAX);
				this.bg_sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				this.bg_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
				this.bg_sprite.SetColor(0.2f,0.0f,0.0f,1.0f);
			}

			Fee.File.Path t_path = Fee.File.Path.CreateStreamingAssetsPath("Test22/video.mp4",Fee.File.Path.SEPARATOR);

			//video_texture
			this.video_item_1 = Fee.Video.Item.Create(this.deleter,Fee.Render2D.Config.VIRTUAL_W,Fee.Render2D.Config.VIRTUAL_H,t_path);
			this.video_item_1.SetLoop(false);

			this.video_item_2 = Fee.Video.Item.Create(this.deleter,Fee.Render2D.Config.VIRTUAL_W,Fee.Render2D.Config.VIRTUAL_H,t_path);
			this.video_item_2.SetLoop(false);

			this.video_item_3 = Fee.Video.Item.Create(this.deleter,Fee.Render2D.Config.VIRTUAL_W,Fee.Render2D.Config.VIRTUAL_H,t_path);
			this.video_item_3.SetLoop(false);

			//表示先。
			this.video_sprite_1 = Fee.Render2D.Sprite2D.Create(this.deleter,1);
			this.video_sprite_1.SetRect(100,100,300,300);
			this.video_sprite_1.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			this.video_sprite_1.SetTexture(this.video_item_1.GetRenderTexture());
			this.video_sprite_1.SetColor(1.0f,1.0f,1.0f,1.0f);

			//表示先。
			this.video_sprite_2 = Fee.Render2D.Sprite2D.Create(this.deleter,2);
			this.video_sprite_2.SetRect(300,200,300,300);
			this.video_sprite_2.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			this.video_sprite_2.SetTexture(this.video_item_2.GetRenderTexture());
			this.video_sprite_2.SetColor(1.0f,1.0f,1.0f,1.0f);

			//表示先。
			this.video_sprite_3 = Fee.Render2D.Sprite2D.Create(this.deleter,3);
			this.video_sprite_3.SetRect(500,300,300,300);
			this.video_sprite_3.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
			this.video_sprite_3.SetTexture(this.video_item_3.GetRenderTexture());
			this.video_sprite_3.SetColor(1.0f,1.0f,1.0f,1.0f);


			this.video_text_1 = this.prefablist.CreateText(this.deleter,1);
			this.video_text_1.SetRect(this.video_sprite_1.GetX(),this.video_sprite_1.GetY() - 30,0,0);

			this.video_text_2 = this.prefablist.CreateText(this.deleter,1);
			this.video_text_2.SetRect(this.video_sprite_2.GetX(),this.video_sprite_2.GetY() - 30,0,0);

			this.video_text_3 = this.prefablist.CreateText(this.deleter,1);
			this.video_text_3.SetRect(this.video_sprite_3.GetX(),this.video_sprite_3.GetY() - 30,0,0);
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

			if(Fee.Input.Mouse.GetInstance().left.down == true){
				this.video_item_1.Play();
				this.video_item_2.Play();
				this.video_item_3.Play();
			}else if(Fee.Input.Mouse.GetInstance().right.down == true){
				this.video_item_1.Pause();
				this.video_item_2.Pause();
				this.video_item_3.Pause();
			}

			this.video_text_1.SetText(this.video_item_1.GetFrame().ToString() + " " + this.video_item_1.GetFrameMax().ToString() + " " + this.video_item_1.GetTime().ToString("0.000") + " " + this.video_item_1.GetFrameRate().ToString("0.000")+ " " + this.video_item_1.IsPrepared().ToString() + " " + this.video_item_1.IsPlay().ToString() + " " + this.video_item_1.IsPause().ToString());
			this.video_text_2.SetText(this.video_item_2.GetFrame().ToString() + " " + this.video_item_2.GetFrameMax().ToString() + " " + this.video_item_2.GetTime().ToString("0.000") + " " + this.video_item_2.GetFrameRate().ToString("0.000")+ " " + this.video_item_2.IsPrepared().ToString() + " " + this.video_item_2.IsPlay().ToString() + " " + this.video_item_2.IsPause().ToString());
			this.video_text_3.SetText(this.video_item_3.GetFrame().ToString() + " " + this.video_item_3.GetFrameMax().ToString() + " " + this.video_item_3.GetTime().ToString("0.000") + " " + this.video_item_3.GetFrameRate().ToString("0.000")+ " " + this.video_item_3.IsPrepared().ToString() + " " + this.video_item_3.IsPlay().ToString() + " " + this.video_item_3.IsPause().ToString());

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

