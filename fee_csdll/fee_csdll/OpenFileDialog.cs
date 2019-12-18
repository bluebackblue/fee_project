

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
	/** OpenFileDialog
	*/
    public class OpenFileDialog
    {
		/** Open
		*/
		public static string Open()
		{
			string t_filename = "";

			try{

				/*using(*/System.Windows.Forms.OpenFileDialog t_dialog = new System.Windows.Forms.OpenFileDialog()/*)*/;{

					t_dialog.Reset();

					//t_dialog.Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*" ;
					t_dialog.AutoUpgradeEnabled = true;
					t_dialog.Title = "ファイルを開く";
					t_dialog.ValidateNames = true;

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

					t_dialog.Dispose();
				}

			}catch(System.Exception e){
				t_filename = e.Message;
			}

			return t_filename;
		}
    }
}

