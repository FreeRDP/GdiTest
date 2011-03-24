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
			
			DrawingArea lineDrawingArea = new LineDrawingArea ();
	
			Box box = new HBox (true, 0);
			box.Add (lineDrawingArea);
			
			w.Add (box);
			w.Resize (500, 500);
			w.ShowAll ();
			
			Application.Run ();
		}
	}
}
