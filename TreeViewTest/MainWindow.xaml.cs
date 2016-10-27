using System.Windows;
using System.Collections.Generic;

namespace TreeViewTest
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			List<TestClass_1> TList = new List<TestClass_1>();

			for (int i = 0; i < 10; i++)
				TList.Add(new TestClass_1());

			DataContext = TList;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		//	List<TestClass_1> TList = new List<TestClass_1>();

		//	for (int i = 0; i < 10; i++)
		//		TList.Add(new TestClass_1());

		//	TreeView_Test.ItemsSource = TList;
		}
	}
}
