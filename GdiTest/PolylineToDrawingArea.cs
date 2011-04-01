using Gtk;
using Cairo;
using System;
using System.Drawing;

namespace GdiTest
{
	public class PolylineToDrawingArea : TestDrawingArea
	{
		private String dumpText;
		
		public PolylineToDrawingArea ()
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
					int n = 2;
					int w = 16;
					int h = 16;
					
					Area[] areas = new Area[n];
					GDI.POINT[][] points = new GDI.POINT[n][];
					
					/* Test Case 1 */
					areas[i].X = 0;
					areas[i].Y = 0;
					areas[i].W = w;
					areas[i].H = h;
					points[i] = new GDI.POINT[4];
					points[i][0].X = 0;
					points[i][0].Y = 15;
					points[i][1].X = 8;
					points[i][1].Y = 0;
					points[i][2].X = 15;
					points[i][2].Y = 15;
					points[i][3].X = 0;
					points[i][3].Y = 15;
					i++;
					
					/* Test Case 2 */
					areas[i].X = 0;
					areas[i].Y = 0;
					areas[i].W = w;
					areas[i].H = h;
					points[i] = new GDI.POINT[5];
					points[i][0].X = w * i + 3;
					points[i][0].Y = 0 + 3;
					points[i][1].X = w * (i + 1) - 3;
					points[i][1].Y = 0 + 3;
					points[i][2].X = w * (i + 1) - 3;
					points[i][2].Y = h - 3;
					points[i][3].X = w * i + 3;
					points[i][3].Y = h - 3;
					points[i][4].X = w * i + 3;
					points[i][4].Y = 0 + 3;
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
						GDI_Win32.MoveToEx(hdc, points[i][0].X, points[i][0].Y, IntPtr.Zero);
						GDI_Win32.PolylineTo(hdc, points[i], (uint) points[i].Length);
					
						dumpText += "unsigned char polyline_to_case_" + (i + 1) + "[" + w * h + "] = \n";
						dumpText += dumpPixelArea(GDI_Win32, hdc, i * w, 0, w, h) + "\n";
					}
				}
			}
			return true;
		}
		
		public override String getDumpText()
		{
			return dumpText;
		}
	}
}

