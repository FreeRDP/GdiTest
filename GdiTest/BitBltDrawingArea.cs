using Gtk;
using Cairo;
using System;
using System.Drawing;

namespace GdiTest
{
	public class BitBltDrawingArea : DrawingArea
	{		
		public BitBltDrawingArea ()
		{
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			using (Context cg = Gdk.CairoHelper.Create (args.Window))
			{
				cg.Antialias = Antialias.None;
				cg.LineWidth = 4;
				
				cg.Color = new Cairo.Color(1,0,0);
				cg.MoveTo (10, 10);
				cg.LineTo (110, 10);
				cg.Stroke ();
				
				cg.Color = new Cairo.Color(0,1,0);
				cg.MoveTo (10, 10);
				cg.LineTo (10, 110);
				cg.Stroke ();
				
				cg.Color = new Cairo.Color(0,0,1);
				cg.MoveTo (10, 10);
				cg.LineTo (110, 110);
				cg.Stroke ();
				
				Win32GDI GDI_Win32 = Win32GDI.getInstance();
				
				if (GDI_Win32.isAvailable())
				{
					System.Drawing.Graphics wg = Gtk.DotNet.Graphics.FromDrawable(this.GdkWindow, true);
					IntPtr dc = wg.GetHdc();
				
					GDI_Win32.BitBlt(dc, 70, 0, 60, 60, dc, 0, 0, GDI.SRCCOPY);
				}
				
				FreeRDPGDI GDI_FreeRDP = FreeRDPGDI.getInstance();
				
				if (GDI_FreeRDP.isAvailable())
				{	
					GDI_FreeRDP.GetDC((IntPtr) null);
				}
			}
			return true;
		}
	}
}

