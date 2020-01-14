

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief インスタンス作成。フォントリスト。
*/


/** Fee.Instantiate
*/
namespace Fee.Instantiate
{
	/** FontList_MonoBehaviour
	*/
	public class FontList_MonoBehaviour : UnityEngine.MonoBehaviour
	{
		/** tag_list
		*/
		[UnityEngine.SerializeField]
		public string[] tag_list;

		/** font_list
		*/
		[UnityEngine.SerializeField]
		public UnityEngine.Font[] font_list;
	}
}

