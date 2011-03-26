using Gtk;
using Cairo;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GdiTest
{
	public class BitBltDrawingArea : DrawingArea
	{
		[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
		
		private static extern int BitBlt(
		                                 IntPtr hdcDest,
		                                 int nXDest,
		                                 int nYDest,
		                                 int nWidth,
		                                 int nHeight,
		                                 IntPtr hdcSrc,
		                                 int nXSrc,
		                                 int nYSrc,
		                                 System.Int32 dwRop
		                                 );
		
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
				
				System.Drawing.Graphics wg = Gtk.DotNet.Graphics.FromDrawable(this.GdkWindow, true);
				IntPtr dc = wg.GetHdc();
				
				BitBlt(dc, 70, 0, 60, 60, dc, 0, 0, 0xCC0020);
			}
			return true;
		}
	}
}

