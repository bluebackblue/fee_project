

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief データ。データアイテム。
*/


/** Fee.Data
*/
namespace Fee.Data
{
	/** PathType
	*/
	public enum PathType
	{
		/** Resources_Texture
		*/
		Resources_Texture,

		/** Resources_Text
		*/
		Resources_Text,
	}

	/** ListItem
	*/
	public class ListItem
	{
		/** datatype
		*/
		public PathType datatype;

		/** path
		*/
		public Fee.File.Path path;

		/** constructor
		*/
		public ListItem(PathType a_datatype,Fee.File.Path a_path)
		{
			//datatype
			this.datatype = a_datatype;

			//path
			this.path = a_path;
		}
	}
}

