

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * @brief オープンファイルダイアログ
*/


/** FeeCsDll
*/
namespace FeeCsDll
{
	/** OpenFileDialog_SystemWindowsForms
	*/
    public class OpenFileDialog_SystemWindowsForms
    {
		/** Open
		*/
		public static string Open()
		{
			string t_filename = "";

			try{
				using(System.Windows.Forms.OpenFileDialog t_dialog = new System.Windows.Forms.OpenFileDialog()){

					t_dialog.Filter = "すべてのファイル (*.*)|*.*" ;
					t_dialog.Title = "ファイルを開く";

					System.Windows.Forms.DialogResult t_result = t_dialog.ShowDialog();

					switch(t_result){
					case System.Windows.Forms.DialogResult.OK:
						{
							t_filename = t_dialog.FileName;
						}break;
					default:
						{
						}break;
					}
				}
			}catch(System.Exception /*t_exception*/){
				t_filename = "";
			}

			return t_filename;
		}
    }
}

