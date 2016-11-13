using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Collections.Generic;

namespace CodeGenerater.Translation.UI.WPF
{
	public partial class MainWindow : Window
	{
		#region Constructor
		public MainWindow()
		{
			InitializeComponent();
		}
		#endregion

		#region Event Handler
		private void Window_DragEnter(object sender, DragEventArgs e)
		{
			var @do = e.Data as DataObject;

			if (@do.ContainsFileDropList())
				if ((from f in GetOnlyFilePath(@do.GetFileDropList().Cast<string>())
					 select LoadedAssembly.IsLoadable(f)).Count((@this) => @this == true) > 0)
					e.Effects = DragDropEffects.Copy;
				else
					e.Effects = DragDropEffects.None;
			else
				e.Effects = DragDropEffects.None;

			e.Handled = true;
		}

		private void Window_Drop(object sender, DragEventArgs e)
		{
			var @do = e.Data as DataObject;
			var VM = App.Current.FindResource("MainViewModel") as MainViewModel;

			foreach (var each in GetOnlyFilePath(@do.GetFileDropList().Cast<string>()))
				if (LoadedAssembly.IsLoadable(each))
				{
					VM.MyTranslator.LoadAssembly(new LoadedAssembly(FileToBytes(each)));
				}

			e.Handled = true;
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			var VM = App.Current.FindResource("MainViewModel") as MainViewModel;

			VM.MyTranslator.Save();
		}
		#endregion

		#region Helper
		bool IsDirectory(string FilePath)
		{
			return File.GetAttributes(FilePath).HasFlag(FileAttributes.Directory);
		}

		IEnumerable<string> GetOnlyFilePath(IEnumerable<string> PathCollection)
		{
			foreach (var each in PathCollection)
			{
				if (IsDirectory(each))
					foreach (var file in GetOnlyFilePath(Directory.GetFiles(each)))
						yield return file;
				else
					yield return each;
			}
		}

		byte[] FileToBytes(string Path)
		{
			Stream S = null;

			try
			{
				S = File.OpenRead(Path);
				byte[] Bytes = new byte[S.Length];

				S.Read(Bytes, 0, (int)S.Length);
				S.Close();

				return Bytes;
			}
			finally
			{
				S.Close();
			}
		}
		#endregion
	}
}