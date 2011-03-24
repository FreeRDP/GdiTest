using System;
using Gtk;
using Cairo;

namespace GdiTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow w = new MainWindow ();
			
			DrawingArea a = new CairoGraphic ();
	
			Box box = new HBox (true, 0);
			box.Add (a);
			w.Add (box);
			w.Resize (500, 500);
			w.ShowAll ();
			
			Application.Run ();
		}
	}
}

public class CairoGraphic : DrawingArea
{
	protected override bool OnExposeEvent (Gdk.EventExpose args)
	{
		using (Context g = Gdk.CairoHelper.Create (args.Window))
		{
			g.Antialias = Antialias.None;
			g.LineWidth = 4;
			
			g.Color = new Color(1,0,0);
			g.MoveTo (10, 10);
			g.LineTo (110, 10);
			g.Stroke ();
			
			g.Color = new Color(0,1,0);
			g.MoveTo (10, 10);
			g.LineTo (10, 110);
			g.Stroke ();
			
			g.Color = new Color(0,0,1);
			g.MoveTo (10, 10);
			g.LineTo (110, 110);
			g.Stroke ();
			
			g.Color = new Color(0,0,0);
			Rectangle rect = new Rectangle(210, 10, 260, 110);
			g.Rectangle(rect);
			g.Stroke ();
		}
		return true;
	}
}
