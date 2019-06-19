

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief ２Ｄ描画。ＵＩ描画カメラ。
*/


/** Fee.Render2D
*/
namespace Fee.Render2D
{
	/** MonoBehaviour_Camera_UI
	*/
	public class MonoBehaviour_Camera_UI : UnityEngine.MonoBehaviour
	{
		/** index
		*/
		public int index;

		/** log
		*/
		public int log_text_start_index;
		public int log_text_end_index;
		public int log_inputfield_start_index;
		public int log_inputfield_end_index;

		/** mycamera
		*/
		public UnityEngine.Camera mycamera;

		/** cameradepth
		*/
		public float cameradepth;

		/** constructor
		*/
		public MonoBehaviour_Camera_UI()
		{
			//index
			this.index = -1;

			//log
			this.log_text_start_index = -1;
			this.log_text_end_index = -1;
			this.log_inputfield_start_index = -1;
			this.log_inputfield_end_index = -1;

			//mycamera
			this.mycamera = null;

			//cameradepth
			this.cameradepth = 0.0f;
		}

		/** SetActive
		*/
		public void SetActive(bool a_flag)
		{
			this.mycamera.enabled = a_flag;
		}

		/**  デプスクリアーの設定。
		*/
		public void SetDepthClear(bool a_flag)
		{
			if(a_flag == true){
				this.mycamera.clearFlags = UnityEngine.CameraClearFlags.Depth;
			}else{
				this.mycamera.clearFlags = UnityEngine.CameraClearFlags.Nothing;
			}
		}
	}
}

