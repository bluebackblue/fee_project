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
				---
				"
			);
		}

		/** 削除管理。
		*/
		private Fee.Deleter.Deleter deleter;

		/** bg
		*/
		private Fee.Render2D.Sprite2D bg_sprite;

		/** video_sprite
		*/
		private Fee.Render2D.Sprite2D video_sprite;

		/** video_player
		*/
		private UnityEngine.Video.VideoPlayer video_player;

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

			//フォント。
			Font t_font = Resources.Load<Font>(Data.Resources.FONT);
			if(t_font != null){
				Fee.Render2D.Render2D.GetInstance().SetDefaultFont(t_font);
			}

			//削除管理。
			this.deleter = new Fee.Deleter.Deleter();

			//戻るボタン作成。
			this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP,this.name + ":Return");

			//bg
			{
				this.bg_sprite = Fee.Render2D.Sprite2D.Create(this.deleter,0);
				this.bg_sprite.SetRect(in Fee.Render2D.Config.VIRTUAL_RECT_MAX);
				this.bg_sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				this.bg_sprite.SetTexture(UnityEngine.Texture2D.whiteTexture);
				this.bg_sprite.SetColor(1.0f,0.0f,0.0f,1.0f);
			}
			
			{
				//レンダーテクスチャ。
				UnityEngine.RenderTexture t_render_texture;
				t_render_texture = new RenderTexture(Fee.Render2D.Config.VIRTUAL_W,Fee.Render2D.Config.VIRTUAL_H,0,UnityEngine.RenderTextureFormat.RGB565,UnityEngine.RenderTextureReadWrite.Default);
				t_render_texture.Create();

				//ビデオプレイヤー。
				UnityEngine.GameObject t_gameobject = new GameObject("videoplayer");
				t_gameobject.AddComponent<UnityEngine.Video.VideoPlayer>();
				this.video_player = t_gameobject.GetComponent<UnityEngine.Video.VideoPlayer>();
				this.video_player.renderMode = UnityEngine.Video.VideoRenderMode.RenderTexture;
				this.video_player.isLooping = true;
				this.video_player.playOnAwake = false;
				this.video_player.waitForFirstFrame = true;
				this.video_player.skipOnDrop = true;
				this.video_player.targetTexture = t_render_texture;
				this.video_player.source = UnityEngine.Video.VideoSource.Url;
				this.video_player.url = Fee.File.Path.CreateStreamingAssetsPath(new Fee.File.Path("Test22/video.mp4")).GetPath();

				//表示先。
				this.video_sprite = Fee.Render2D.Sprite2D.Create(this.deleter,1);
				this.video_sprite.SetRect(in Fee.Render2D.Config.VIRTUAL_RECT_MAX);
				this.video_sprite.SetTextureRect(in Fee.Render2D.Config.TEXTURE_RECT_MAX);
				this.video_sprite.SetTexture(t_render_texture);
				this.video_sprite.SetColor(1.0f,1.0f,1.0f,1.0f);
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

			if(Fee.Input.Mouse.GetInstance().left.down == true){
				this.video_player.Play();

				UnityEngine.Debug.Log("Play");
			}else if(Fee.Input.Mouse.GetInstance().right.down == true){
				this.video_player.Pause();

				UnityEngine.Debug.Log("Pause");
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

