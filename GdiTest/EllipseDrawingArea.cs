using Gtk;
using Cairo;
using System;
using System.Drawing;

namespace GdiTest
{
	public class EllipseDrawingArea : TestDrawingArea
	{
		String dumpText;
		
		public EllipseDrawingArea ()
		{
			dumpText = "";
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			using (Context cg = Gdk.CairoHelper.Create (args.Window))
			{	
				Win32GDI GDI_Win32 = Win32GDI.getInstance();
				
				if (GDI_Win32.isAvailable())
				{
					System.Drawing.Graphics wg = Gtk.DotNet.Graphics.FromDrawable(this.GdkWindow, true);
					IntPtr hdc = wg.GetHdc();
					
					int i = 0;
					int n = 1;
					int w = 16;
					int h = 16;
					
					Area[] areas = new Area[n];
					
					areas[i].X = 0;
					areas[i].Y = 0;
					areas[i].W = w;
					areas[i].H = h;
					i++;
					
					/* Fill Area with White */
					cg.Color = new Cairo.Color(255,255,255);
					Cairo.Rectangle rect = new Cairo.Rectangle(0, 0, n * w, h);
					cg.Rectangle(rect);
					cg.Fill();
					cg.Stroke();
					
					IntPtr blackSolidBrush = GDI_Win32.CreateSolidBrush(0);
					IntPtr oldBrush = GDI_Win32.SelectObject(hdc, blackSolidBrush);
					
					for (i = 0; i < n; i++)
					{
						GDI_Win32.Ellipse(hdc, areas[i].X, areas[i].Y, areas[i].W, areas[i].H);
					
						dumpText += "unsigned char ellipse_case_" + (i + 1) + "[" + w * h + "] = \n";
						dumpText += dumpPixelArea(GDI_Win32, hdc, i * w, 0, w, h) + "\n";
					}
				}
			}
			return true;
		}
		
		public override string getDumpText ()
		{
			return dumpText;
		}
	}
}

