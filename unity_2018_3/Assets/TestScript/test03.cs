using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief テスト。
*/


/** test03

	ＵＮＩＶＲＭ。

*/
public class test03 : main_base , Fee.EventPlate.OnOverCallBack_Base
{
	/** 削除管理。
	*/
	private Fee.Deleter.Deleter deleter;

	/** button
	*/
	private Fee.Ui.Button button;

	/** inputfield
	*/
	private Fee.Render2D.InputField2D inputfield;

	/** bg
	*/
	private Fee.Render2D.Sprite2D bg;

	/** bone
	*/
	private HumanBodyBones[] bone_index;
	private Fee.Render2D.Sprite2D[] bone_sprite;
	private Fee.Render2D.Text2D[] bone_name;
	private Fee.EventPlate.Item[] bone_eventplate;

	/** ステータス。
	*/
	private Fee.Render2D.Text2D status;

	/** ロードアイテム。
	*/
	private Fee.File.Item load_item;

	/** ＶＲＭアイテム。
	*/
	private Fee.UniVrm.Item vrm_item;
	private bool vrm_item_load;
	private Fee.UniVrm.Controller vrm_controller;

	/** バイナリ。
	*/
	private byte[] binary;

	/** mycamera
	*/
	private UnityEngine.GameObject mycamera_gameobject;
	private Camera mycamera_camera;

	/** LayerIndex
	*/
	enum LayerIndex
	{
		LayerIndex_Bg = 0,
		LayerIndex_Model = 0,
		LayerIndex_Ui = 1,
	};

	/** drawpriority
	*/
	long drawpriority_bg;
	long drawpriority_mode;
	long drawpriority_ui;
	long drawpriority_ui2;

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
		Fee.Render2D.Render2D.CreateInstance();

		//マウス。インスタンス作成。
		Fee.Input.Mouse.CreateInstance();

		//キー。インスタンス作成。
		Fee.Input.Key.CreateInstance();

		//イベントプレート。インスタンス作成。
		Fee.EventPlate.EventPlate.CreateInstance();

		//ＵＩ。インスタンス作成。
		Fee.Ui.Ui.CreateInstance();

		//ファイル。インスタンス作成。
		Fee.File.File.CreateInstance();

		//ＵＮＩＶＲＭ。インスタンス作成。
		Fee.UniVrm.UniVrm.CreateInstance();

		//削除管理。
		this.deleter = new Fee.Deleter.Deleter();

		//戻るボタン作成。
		this.CreateReturnButton(this.deleter,(Fee.Render2D.Render2D.MAX_LAYER - 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP);

		//drawpriority
		this.drawpriority_bg = (int)LayerIndex.LayerIndex_Bg * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;
		this.drawpriority_mode = (int)LayerIndex.LayerIndex_Model * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;
		this.drawpriority_ui = (int)LayerIndex.LayerIndex_Ui * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;
		this.drawpriority_ui2 = ((int)LayerIndex.LayerIndex_Ui + 1) * Fee.Render2D.Render2D.DRAWPRIORITY_STEP;

		{
			//button
			this.button = new Fee.Ui.Button(this.deleter,this.drawpriority_ui,this.CallBack_Click,0);
			this.button.SetRect(130,10,50,50);
			this.button.SetTexture(Resources.Load<Texture2D>("button"));
			this.button.SetText("Load");

			//inputfield
			this.inputfield = new Fee.Render2D.InputField2D(this.deleter,this.drawpriority_ui);
			this.inputfield.SetRect(130 + 50 + 10,10,700,50);
			this.inputfield.SetText("https://bbbproject.sakura.ne.jp/www/project_webgl/fee/StreamingAssets/nana.vrmx");
			this.inputfield.SetMultiLine(false);

			//ステータス。
			this.status = new Fee.Render2D.Text2D(this.deleter,this.drawpriority_ui);
			this.status.SetRect(100,100,0,0);

			//bone
			{
				this.bone_index = new HumanBodyBones[]{
					HumanBodyBones.Hips,
					HumanBodyBones.LeftUpperLeg,
					HumanBodyBones.RightUpperLeg,
					HumanBodyBones.LeftLowerLeg,
					HumanBodyBones.RightLowerLeg,
					HumanBodyBones.LeftFoot,
					HumanBodyBones.RightFoot,
					HumanBodyBones.Spine,
					HumanBodyBones.Chest,
					HumanBodyBones.Neck,
					HumanBodyBones.Head,
					HumanBodyBones.LeftShoulder,
					HumanBodyBones.RightShoulder,
					HumanBodyBones.LeftUpperArm,
					HumanBodyBones.RightUpperArm,
					HumanBodyBones.LeftLowerArm,
					HumanBodyBones.RightLowerArm,
					HumanBodyBones.LeftHand,
					HumanBodyBones.RightHand,
					HumanBodyBones.LeftToes,
					HumanBodyBones.RightToes,
					HumanBodyBones.LeftEye,
					HumanBodyBones.RightEye,
					HumanBodyBones.Jaw,
					HumanBodyBones.LeftThumbProximal,
					HumanBodyBones.LeftThumbIntermediate,
					HumanBodyBones.LeftThumbDistal,
					HumanBodyBones.LeftIndexProximal,
					HumanBodyBones.LeftIndexIntermediate,
					HumanBodyBones.LeftIndexDistal,
					HumanBodyBones.LeftMiddleProximal,
					HumanBodyBones.LeftMiddleIntermediate,
					HumanBodyBones.LeftMiddleDistal,
					HumanBodyBones.LeftRingProximal,
					HumanBodyBones.LeftRingIntermediate,
					HumanBodyBones.LeftRingDistal,
					HumanBodyBones.LeftLittleProximal,
					HumanBodyBones.LeftLittleIntermediate,
					HumanBodyBones.LeftLittleDistal,
					HumanBodyBones.RightThumbProximal,
					HumanBodyBones.RightThumbIntermediate,
					HumanBodyBones.RightThumbDistal,
					HumanBodyBones.RightIndexProximal,
					HumanBodyBones.RightIndexIntermediate,
					HumanBodyBones.RightIndexDistal,
					HumanBodyBones.RightMiddleProximal,
					HumanBodyBones.RightMiddleIntermediate,
					HumanBodyBones.RightMiddleDistal,
					HumanBodyBones.RightRingProximal,
					HumanBodyBones.RightRingIntermediate,
					HumanBodyBones.RightRingDistal,
					HumanBodyBones.RightLittleProximal,
					HumanBodyBones.RightLittleIntermediate,
					HumanBodyBones.RightLittleDistal,
					HumanBodyBones.UpperChest,
				};
				this.bone_sprite = new Fee.Render2D.Sprite2D[this.bone_index.Length];
				this.bone_name = new Fee.Render2D.Text2D[this.bone_index.Length];
				this.bone_eventplate = new Fee.EventPlate.Item[this.bone_index.Length];

				for(int ii=0;ii<this.bone_index.Length;ii++){
					this.bone_sprite[ii] = new Fee.Render2D.Sprite2D(this.deleter,this.drawpriority_ui + ii);
					this.bone_sprite[ii].SetTexture(Resources.Load<Texture2D>("maru"));
					this.bone_sprite[ii].SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
					this.bone_sprite[ii].SetRect(0,0,50,20);
					this.bone_sprite[ii].SetMaterialType(Fee.Render2D.Config.MaterialType.Alpha);
					this.bone_sprite[ii].SetVisible(false);

					this.bone_name[ii] = new Fee.Render2D.Text2D(this.deleter,this.drawpriority_ui + ii);
					this.bone_name[ii].SetText(this.bone_index[ii].ToString());
					this.bone_name[ii].SetRect(0,0,0,0);
					this.bone_name[ii].SetVisible(false);

					this.bone_eventplate[ii] = new Fee.EventPlate.Item(this.deleter,Fee.EventPlate.EventType.Button,this.drawpriority_ui + ii);
					this.bone_eventplate[ii].SetOnOverCallBack(this);
					this.bone_eventplate[ii].SetOnOverCallBackValue(ii);
					this.bone_eventplate[ii].SetRect(0,0,50,20);
					this.bone_eventplate[ii].SetEnable(false);
				}
			}
		}

		//bg
		{
			this.bg = new Fee.Render2D.Sprite2D(this.deleter,this.drawpriority_bg);
			this.bg.SetTexture(Texture2D.whiteTexture);
			this.bg.SetTextureRect(ref Fee.Render2D.Render2D.TEXTURE_RECT_MAX);
			this.bg.SetRect(ref Fee.Render2D.Render2D.VIRTUAL_RECT_MAX);
			this.bg.SetColor(0.1f,0.1f,0.1f,1.0f);
		}

		//load_item
		this.load_item = null;

		//vrm
		this.vrm_item = null;
		this.vrm_item_load = false;
		this.vrm_controller = null;

		//binary
		this.binary = null;

		//カメラ。
		this.mycamera_gameobject = GameObject.Find("Main Camera");
		this.mycamera_camera = this.mycamera_gameobject.GetComponent<Camera>();
		if(this.mycamera_camera != null){
			//クリアしない。
			this.mycamera_camera.clearFlags = CameraClearFlags.Depth;

			//モデルだけを表示。
			this.mycamera_camera.cullingMask = (1 << LayerMask.NameToLayer("Model"));

			//デプスを２Ｄ描画の合わせる。
			this.mycamera_camera.depth = Fee.Render2D.Render2D.GetInstance().GetCameraAfterDepth((int)LayerIndex.LayerIndex_Model);
		}
	}

	/** [Button_Base]コールバック。クリック。
	*/
	private void CallBack_Click(int a_id)
	{
		if((this.load_item == null)&&(this.binary == null)){
			GameObject t_model = GameObject.Find("Model");
			if(t_model != null){
				GameObject.Destroy(t_model);
			}

			#if(true)
			this.load_item = Fee.File.File.GetInstance().RequestDownLoadBinaryFile(new Fee.File.Path(this.inputfield.GetText()),null);
			#else
			this.load_item = Fee.File.File.GetInstance().RequestLoadStreamingAssetsBinaryFile("nana.vrmx");
			#endif
		}
	}

	/** [Fee.EventPlateOnOverCallBack_Base]イベントプレートに入場。
	*/
	public void OnOverEnter(int a_value)
	{
		this.bone_name[a_value].SetDrawPriority(this.drawpriority_ui2);
		this.bone_name[a_value].SetColor(1.0f,0.0f,0.0f,1.0f);
	}

	/** [Fee.EventPlateOnOverCallBack_Base]イベントプレートから退場。
	*/
	public void OnOverLeave(int a_value)
	{
		this.bone_name[a_value].SetDrawPriority(this.drawpriority_ui + a_value);
		this.bone_name[a_value].SetColor(1.0f,1.0f,1.0f,1.0f);
	}

	/** FixedUpdate
	*/
	private void FixedUpdate()
	{
		//マウス。
		Fee.Input.Mouse.GetInstance().Main(Fee.Render2D.Render2D.GetInstance());

		//キー。
		Fee.Input.Key.GetInstance().Main();

		//イベントプレート。
		Fee.EventPlate.EventPlate.GetInstance().Main(Fee.Input.Mouse.GetInstance().pos.x,Fee.Input.Mouse.GetInstance().pos.y);

		//ＵＩ。
		Fee.Ui.Ui.GetInstance().Main();

		//ファイル。
		Fee.File.File.GetInstance().Main();

		//ＵＮＩＶＲＭ。
		Fee.UniVrm.UniVrm.GetInstance().Main();

		if(this.load_item != null){
			if(this.load_item.IsBusy() == true){
				//ダウンロード中。
				this.status.SetText("Load : " + this.load_item.GetResultProgressDown().ToString());

				//キャンセル。
				if(this.IsChangeScene() == true){
					this.load_item.Cancel();
				}
			}else{
				//ダウンロード完了。
				if(this.load_item.GetResultType() == Fee.File.Item.ResultType.Binary){
					this.status.SetText("Load : Fix");
					this.binary = this.load_item.GetResultBinary();
				}else{
					this.status.SetText("Load : Error");
				}
				this.load_item = null;
			}
		}
		
		if(this.vrm_item_load == true){
			if(this.vrm_item != null){
				if(this.vrm_item.IsBusy() == true){
					//ロード中。
					this.status.SetText("LoavVrm : " + this.vrm_item.GetResultProgress().ToString());

					//キャンセル。
					if(this.IsChangeScene() == true){
						this.vrm_item.Cancel();
					}
				}else{
					//ロード完了。
					if(this.vrm_item.GetResultType() == Fee.UniVrm.Item.ResultType.Context){
						this.status.SetText("LoavVrm : Fix");

						//レイヤー。設定。
						//this.vrm_item.SetLayer("Model");

						//表示。設定。
						this.vrm_controller = this.vrm_item.CreateRuntimeAnimatorController();
						this.vrm_controller.SetVisible(true);

						//アニメータコントローラ。設定。
						this.vrm_controller.SetRuntimeAnimatorController(Resources.Load<RuntimeAnimatorController>("Anime/AnimatorController"));

					}else{
						this.status.SetText("LoavVrm : Error");
					}
					this.vrm_item_load = false;
				}
			}
		}

		if(this.binary != null){
			this.status.SetText("Create : size = " + this.binary.Length.ToString());
			{
				if(this.vrm_item != null){
					this.vrm_item.Delete();
					this.vrm_item = null;
				}

				this.vrm_item = Fee.UniVrm.UniVrm.GetInstance().Request(this.binary);
				this.vrm_item_load = true;
			}
			this.binary = null;
		}

		//マウスイベント。
		if(Fee.Input.Mouse.GetInstance().left.down == true){
			if(this.vrm_controller != null){
				//Animator.StringToHash("Base Layer.standing_walk_forward_inPlace")
				this.vrm_controller.PlayMotion_RuntimeAnimatorController("standing_walk_forward_inPlace");
			}
		}else if(Fee.Input.Key.GetInstance().enter.down == true){
			if(this.vrm_controller != null){
				//if(this.vrm_item.IsAnimeEnable() == true){
				//	this.vrm_item.SetAnimeEnable(false);
				//}else{
				//	this.vrm_item.SetAnimeEnable(true);
				//}
			}
		}

		//カメラを回す。
		if(this.vrm_controller != null){
			//if(this.vrm_item.IsAnimeEnable() == true){
				if(this.mycamera_gameobject != null){
					float t_time = Time.realtimeSinceStartup / 3;
					Vector3 t_position = new Vector3(Mathf.Sin(t_time) * 2.0f,1.0f,Mathf.Cos(t_time) * 2.0f);
					Transform t_camera_transform = this.mycamera_gameobject.GetComponent<Transform>();
					t_camera_transform.position = t_position;
					t_camera_transform.LookAt(new Vector3(0.0f,1.0f,0.0f));
				}
			//}
		}

		int t_none_index = 0;

		for(int ii=0;ii<this.bone_sprite.Length;ii++){

			Transform t_transcorm_hand = null;
			if(this.vrm_controller != null){
				if(this.mycamera_camera != null){
					t_transcorm_hand = this.vrm_controller.GetTransform(this.bone_index[ii]);
				}
			}

			if(t_transcorm_hand != null){
				//位置。
				Vector3 t_position = t_transcorm_hand.position;

				//スクリーン座標計算。
				{
					int t_x;
					int t_y;
					Fee.Render2D.Render2D.GetInstance().WorldToVirtualScreen(this.mycamera_camera,ref t_position,out t_x,out t_y);
					this.bone_sprite[ii].SetVisible(true);
					this.bone_sprite[ii].SetX(t_x - this.bone_sprite[ii].GetW()/2);
					this.bone_sprite[ii].SetY(t_y - this.bone_sprite[ii].GetH()/2);

					this.bone_name[ii].SetVisible(true);
					this.bone_name[ii].SetRect(t_x,t_y,0,0);

					this.bone_eventplate[ii].SetEnable(true);
					this.bone_eventplate[ii].SetX(t_x - this.bone_eventplate[ii].GetW()/2);
					this.bone_eventplate[ii].SetY(t_y - this.bone_eventplate[ii].GetH()/2);
				}
			}else{
				this.bone_sprite[ii].SetVisible(false);
				this.bone_eventplate[ii].SetEnable(false);

				this.bone_name[ii].SetVisible(true);
				this.bone_name[ii].SetRect(Fee.Render2D.Render2D.VIRTUAL_W - 150,100 + t_none_index * 20,0,0);
				t_none_index++;
			}
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

