

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ＵＮＩＶＲＭ。
*/


/** Fee.UniVrm
*/
namespace Fee.UniVrm
{
	/** Controller
	*/
	public class Controller
	{
		/** ControllerType
		*/
		public enum ControllerType
		{
			/** SimpleAnimation
			*/
			SimpleAnimation,

			/** RuntimeAnimatorController
			*/
			RuntimeAnimatorController
		}

		/** raw_context
		*/
		private VRM.VRMImporterContext raw_context;

		/** raw_animator
		*/
		private UnityEngine.Animator raw_animator;

		/** raw_meta
		*/
		private VRM.VRMMeta raw_meta;
 
		/** root_gameobject
		*/
		private UnityEngine.GameObject root_gameobject;

		/** root_transform
		*/
		private UnityEngine.Transform root_transform;

		#if(USE_DEF_FEE_SIMPLEANIMATION)

		/** simpleanimationtion
		*/
		private SimpleAnimation simpleanimationtion;

		#endif

		/** constructor
		*/
		public Controller(Item a_item,ControllerType a_controllertype)
		{
			//raw_context
			this.raw_context = a_item.GetResultContext();

			//raw_animator
			this.raw_animator = this.raw_context.Root.GetComponent<UnityEngine.Animator>();

			//raw_meta
			this.raw_meta = this.raw_context.Root.GetComponent<VRM.VRMMeta>();

			//root_gameobject
			this.root_gameobject = this.raw_context.Root;

			//root_transform
			this.root_transform = this.root_gameobject.GetComponent<UnityEngine.Transform>();

			//simpleanimationtion
			if(a_controllertype == ControllerType.SimpleAnimation){
				#if(USE_DEF_FEE_SIMPLEANIMATION)
				this.simpleanimationtion = this.root_gameobject.AddComponent<SimpleAnimation>();
				#endif
			}
		}

		/** 削除。
		*/
		public void Delete()
		{
			if(this.raw_context != null){
				this.raw_context.Dispose();
			}
		}

		/** メタ情報。取得。
		*/
		public VRM.VRMMeta GetMeta()
		{
			return this.raw_meta;
		}

		/** ボーン位置。取得。
		*/
		public UnityEngine.Transform GetTransform(UnityEngine.HumanBodyBones a_bone)
		{
			return this.raw_animator.GetBoneTransform(a_bone);
		}

		/** 位置。取得。
		*/
		public UnityEngine.Transform GetTransform()
		{
			return this.root_transform;
		}

		/** ゲームオブジェクト。取得。
		*/
		public UnityEngine.GameObject GetGameObject()
		{
			return this.root_gameobject;
		}

		/** ルートモーションの適応フラグ。設定。
		*/
		public void SetApplyRootMotionFlag(bool a_flag)
		{
			this.raw_animator.applyRootMotion = a_flag;
		}

		/** 表示。設定。
		*/
		public void SetVisible(bool a_flag)
		{
			if(this.raw_context != null){
				for(int ii=0;ii<this.raw_context.Meshes.Count;ii++){
					if(this.raw_context.Meshes[ii] != null){
						if(this.raw_context.Meshes[ii].Renderers != null){
							for(int jj=0;jj<this.raw_context.Meshes[ii].Renderers.Count;jj++){
								if(this.raw_context.Meshes[ii].Renderers[jj] != null){
									this.raw_context.Meshes[ii].Renderers[jj].enabled = a_flag;
								}
							}
						}
					}
				}
			}
		}

		/** [RuntimeAnimatorController]ランタイムコントローラ。設定。
		*/
		public void SetRuntimeAnimatorController(UnityEngine.RuntimeAnimatorController a_animator_controller)
		{
			this.raw_animator.runtimeAnimatorController = a_animator_controller;
		}

		/** [SimpleAnimation]モーション。追加。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public void AddMotion_SimpleAnimation(string a_state_name,UnityEngine.AnimationClip a_animetion_clip)
		{
			if(this.simpleanimationtion != null){
				this.simpleanimationtion.AddClip(a_animetion_clip,a_state_name);
			}else{
				Tool.Assert(false);
			}
		}
		#endif
		
		/** [SimpleAnimation]モーション。停止。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public void StopMotion_SimpleAnimation()
		{
			this.simpleanimationtion.Stop();
		}
		#endif

		/** [SimpleAnimation]モーション。停止。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public void StopMotion_SimpleAnimation(string a_state_name)
		{
			this.simpleanimationtion.Stop(a_state_name);
		}
		#endif

		/** [SimpleAnimation]モーション。再生。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public void PlayMotion_SimpleAnimation()
		{
			this.simpleanimationtion.Play();
		}
		#endif

		/** [SimpleAnimation]モーション。再生。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public void PlayMotion_SimpleAnimation(string a_state_name,float a_cross_time,bool a_cross)
		{
			if(this.simpleanimationtion != null){
				if(a_cross == true){
					this.simpleanimationtion.CrossFade(a_state_name,a_cross_time);
				}else{
					this.simpleanimationtion.Stop();
					this.simpleanimationtion.Play(a_state_name);
				}
			}else{
				Tool.Assert(false);
			}
		}
		#endif

		/** [SimpleAnimation]ステータスリスト。取得。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public SimpleAnimation.State GetState_SimpleAnimation(string a_state_name)
		{
			return this.simpleanimationtion.GetState(a_state_name);
		}
		#endif

		/** [SimpleAnimation]正規化時間。取得。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public float GetNormalizedTime_SimpleAnimation(string a_state_name)
		{
			SimpleAnimation.State t_state = this.GetState_SimpleAnimation(a_state_name);
			return t_state.normalizedTime;
		}
		#endif

		/** [SimpleAnimation]時間。取得。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public float GetTime_SimpleAnimation(string a_state_name)
		{
			SimpleAnimation.State t_state = this.GetState_SimpleAnimation(a_state_name);
			return t_state.time;
		}
		#endif

		/** [SimpleAnimation]ブレンド率。取得。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public float GetBlendWeight_SimpleAnimation(string a_state_name)
		{
			SimpleAnimation.State t_state = this.GetState_SimpleAnimation(a_state_name);
			return t_state.weight;
		}
		#endif

		/** [SimpleAnimation]アニメ再生中かどうか。取得。
		*/
		#if(USE_DEF_FEE_SIMPLEANIMATION)
		public bool IsAnime_SimpleAnimation(string a_state_name)
		{
			SimpleAnimation.State t_state = this.GetState_SimpleAnimation(a_state_name);
			return t_state.enabled;
		}
		#endif

		/** [RuntimeAnimatorController]モーション。再生。
		*/
		public void PlayMotion_RuntimeAnimatorController(string a_state)
		{
			this.raw_animator.Play(a_state);
		}

		/** [RuntimeAnimatorController]GetCurrentAnimatorStateInfo
		*/
		public UnityEngine.AnimatorStateInfo GetCurrentAnimatorStateInfo_RuntimeAnimatorController(int a_layer_index)
		{
			return this.raw_animator.GetCurrentAnimatorStateInfo(a_layer_index);
		}

		/** [RuntimeAnimatorController]GetCurrentAnimatorStateInfo
		*/
		public UnityEngine.AnimatorClipInfo[] GetCurrentAnimatorClipInfo_RuntimeAnimatorController(int a_layer_index)
		{
			return this.raw_animator.GetCurrentAnimatorClipInfo(a_layer_index);
		}
	}
}

