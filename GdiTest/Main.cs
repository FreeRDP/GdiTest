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
			
			DrawingArea lineDrawingArea = new BitBltDrawingArea ();
	
			Box box = new HBox (true, 0);
			box.Add (lineDrawingArea);
			
			w.Add (box);
			w.Resize (640, 480);
			w.ShowAll ();
			
			Application.Run ();
		}
	}
}
