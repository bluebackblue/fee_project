

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
	/** Impl
	*/
	namespace Impl
	{
		/** OpenFileNameClass
		*/
		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential,CharSet = System.Runtime.InteropServices.CharSet.Auto)]
		public class OpenFileNameClass
		{
			/** member
			*/
			public int structSize = 0;
			public System.IntPtr dlgOwner = System.IntPtr.Zero;
			public System.IntPtr instance = System.IntPtr.Zero;
			public string filter = null;
			public string customFilter = null;
			public int maxCustFilter = 0;
			public int filterIndex = 0;
			public string file = null;
			public int maxFile = 0;
			public string fileTitle = null;
			public int maxFileTitle = 0;
			public string initialDir = null;
			public string title = null;
			public int flags = 0;
			public short fileOffset = 0;
			public short fileExtension = 0;
			public string defExt = null;
			public System.IntPtr custData = System.IntPtr.Zero;
			public System.IntPtr hook = System.IntPtr.Zero;
			public string templateName = null;
			public System.IntPtr reservedPtr = System.IntPtr.Zero;
			public int reservedInt = 0;
			public int flagsEx = 0;

			/** OFN_***
			*/
			public static readonly int OFN_ALLOWMULTISELECT = 0x00000200;
			public static readonly int OFN_CREATEPROMPT = 0x00000200;
			public static readonly int OFN_DONTADDTORECENT = 0x02000000;
			public static readonly int OFN_ENABLEHOOK = 0x00000020;
			public static readonly int OFN_ENABLEINCLUDENOTIFY = 0x00400000;
			public static readonly int OFN_ENABLESIZING = 0x00800000;
			public static readonly int OFN_ENABLETEMPLATE = 0x00000040;
			public static readonly int OFN_ENABLETEMPLATEHANDLE = 0x00000080;
			public static readonly int OFN_EXPLORER = 0x00080000;
			public static readonly int OFN_EXTENSIONDIFFERENT = 0x00000400;
			public static readonly int OFN_FILEMUSTEXIST = 0x00001000;
			public static readonly int OFN_FORCESHOWHIDDEN = 0x10000000;
			public static readonly int OFN_HIDEREADONLY = 0x00000004;
			public static readonly int OFN_LONGNAMES = 0x00200000;
			public static readonly int OFN_NOCHANGEDIR = 0x00000008;
			public static readonly int OFN_NODEREFERENCELINKS = 0x00100000;
			public static readonly int OFN_NOLONGNAMES = 0x00040000;
			public static readonly int OFN_NONETWORKBUTTON = 0x00020000;
			public static readonly int OFN_NOREADONLYRETURN = 0x00008000;
			public static readonly int OFN_NOTESTFILECREATE = 0x00010000;
			public static readonly int OFN_NOVALIDATE = 0x00000100;
			public static readonly int OFN_OVERWRITEPROMPT = 0x00000002;
			public static readonly int OFN_PATHMUSTEXIST = 0x00000800;
			public static readonly int OFN_READONLY = 0x00000001;
			public static readonly int OFN_SHAREAWARE = 0x00004000;
			public static readonly int OFN_SHOWHELP = 0x00000010;

			/** constructor
			*/
			public OpenFileNameClass()
			{
				this.structSize = System.Runtime.InteropServices.Marshal.SizeOf(this);

			}

			/** GetOpenFileName
			*/
			[System.Runtime.InteropServices.DllImport("Comdlg32.dll",SetLastError = true,ThrowOnUnmappableChar = true,CharSet = System.Runtime.InteropServices.CharSet.Auto)]
			public static extern bool GetOpenFileName([System.Runtime.InteropServices.In,System.Runtime.InteropServices.Out]OpenFileNameClass lpOfn);
		}
	}

	/** OpenFileDialog_Comdlg32
	*/
	public class OpenFileDialog_Comdlg32
	{
		/** Open
		*/
		public static string Open(string a_title,string a_extension)
		{
			string t_filename = "";

			try{
				Impl.OpenFileNameClass t_dialog = new Impl.OpenFileNameClass();

				t_dialog.filter = a_extension;
				t_dialog.file = new string('\0',4096);
				t_dialog.maxFile = t_dialog.file.Length;
				t_dialog.fileTitle = new string('\0',256);
				t_dialog.maxFileTitle = t_dialog.fileTitle.Length;
				t_dialog.title = a_title;
				t_dialog.flags = Impl.OpenFileNameClass.OFN_EXPLORER | Impl.OpenFileNameClass.OFN_FILEMUSTEXIST | Impl.OpenFileNameClass.OFN_PATHMUSTEXIST | Impl.OpenFileNameClass.OFN_NOCHANGEDIR;

				if(Impl.OpenFileNameClass.GetOpenFileName(t_dialog) == true){
					t_filename = t_dialog.file;
				}

				t_dialog = null;
			}catch(System.Exception /*t_exception*/){
				t_filename = "";
			}
			
			return t_filename;
		}
	}
}

