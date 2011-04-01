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
					int n = 3;
					int w = 16;
					int h = 16;
					
					Rect[] rects = new Rect[n];
					
					/* Test Case 1 */
					rects[i].x1 = 0;
					rects[i].y1 = 0;
					rects[i].x2 = w;
					rects[i].y2 = h;
					i++;
					
					/* Test Case 2 */
					rects[i].x1 = w * i + 3;
					rects[i].y1 = 0;
					rects[i].x2 = w * (i + 1) - 3;
					rects[i].y2 = h;
					i++;
					
					/* Test Case 3 */
					rects[i].x1 = w * i;
					rects[i].y1 = 0 + 3;
					rects[i].x2 = w * (i + 1);
					rects[i].y2 = h - 3;
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
						GDI_Win32.Ellipse(hdc, rects[i].x1, rects[i].y1, rects[i].x2, rects[i].y2);
					
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

