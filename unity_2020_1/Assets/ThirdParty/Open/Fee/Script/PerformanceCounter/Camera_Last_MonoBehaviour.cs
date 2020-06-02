

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief パフォーマンスカウンター。カメラ。
*/


/** Fee.PerformanceCounter
*/
namespace Fee.PerformanceCounter
{
	/** Camera_Last_MonoBehaviour
	*/
	[UnityEngine.DefaultExecutionOrder(Config.EXECUTIONORDER_FIRST)]
	public class Camera_Last_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** FixedUpdate
		*/
		private void FixedUpdate()
		{
			PerformanceCounter.GetInstance().Unity_FixedUpdate_Last();
		}

		/** Update
		*/
		private void Update()
		{
			PerformanceCounter.GetInstance().Unity_Update_Last();
		}

		/** LateUpdate
		*/
		private void LateUpdate()
		{
			PerformanceCounter.GetInstance().Unity_LateUpdate_Last();
		}

		/** OnPostRender
		*/
		private void OnPostRender()
		{
			PerformanceCounter.GetInstance().Unity_Render_Last();
			PerformanceCounter.GetInstance().Draw();
		}
	}
}

