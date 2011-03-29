using System;
using Cairo;
using Gtk;

namespace GdiTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow w = new MainWindow ();
			Application.Run ();
		}
	}
}
