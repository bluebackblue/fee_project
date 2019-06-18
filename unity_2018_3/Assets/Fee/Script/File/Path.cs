

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * https://github.com/bluebackblue/fee/blob/master/LICENSE.txt
 * http://bbbproject.sakura.ne.jp/wordpress/mitlicense
 * @brief ファイル。パス。
*/


/** Fee.File
*/
namespace Fee.File
{
	/** Path

		a_path == "X:/aaaa/bbbb/ccc/dddd" : フルパス
		directorypath == "X:/aaaa/bbbb/ccc/"
		filename == "dddd"

		a_path == "iii/jjj" : 相対パス
		directorypath == "iii/"
		filename == "jjj"

	*/
	public class Path
	{
		/** filename
		*/
		private readonly string filename;

		/** directorypath
		*/
		private readonly string directorypath;

		/** constructor
		*/
		public Path(string a_directorypath,string a_filename)
		{
			//filename
			if(a_filename != null){
				this.filename = a_filename;

				Tool.Assert(System.IO.Path.GetFileName(a_filename) == this.filename);
			}else{
				this.filename = "";
			}

			//directorypath
			if(a_directorypath != null){
				this.directorypath = a_directorypath;

				Tool.Assert(System.IO.Path.GetFileName(a_directorypath) == "");
			}else{
				this.directorypath = "";
			}
		}

		/** constructor
		*/
		public Path(string a_path)
		{
			if(a_path != null){
				//filename
				this.filename = System.IO.Path.GetFileName(a_path);

				//directorypath
				this.directorypath = a_path.Substring(0,a_path.Length - this.filename.Length);
			}else{
				//filename
				this.filename = "";

				//directorypath
				this.directorypath = "";
			}
		}

		/** フルパス。取得。
		*/
		public string GetPath()
		{
			return this.directorypath + this.filename;;
		}

		/** ファイル名。取得。
		*/
		public string GetFileName()
		{
			return this.filename;
		}

		/** ディレクトリパス。取得。
		*/
		public string GetDirectoryPath()
		{
			return this.directorypath;
		}

		/** ファイル名を変更したパス。作成。
		*/
		public Path CreateFileNameChangePath(string a_filename)
		{
			return new Path(this.directorypath,a_filename);
		}

		/** ファイル名を変更したパス。作成。
		*/
		public Path CreateDirectoryPathChangePath(string a_directorypath)
		{
			return new Path(a_directorypath,this.filename);
		}
	}
}

