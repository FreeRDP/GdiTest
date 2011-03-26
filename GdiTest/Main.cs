using System;
using Cairo;
using Gtk;

namespace GdiTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Gdi32.init();
			Application.Init ();
			MainWindow w = new MainWindow ();
			
			DrawingArea lineDrawingArea = new BitBltDrawingArea ();
	
			Box box = new HBox (true, 0);
			box.Add (lineDrawingArea);
			
			w.Add (box);
			w.Resize (500, 500);
			w.ShowAll ();
			
			Application.Run ();
		}
	}
}
