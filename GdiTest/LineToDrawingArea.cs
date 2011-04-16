using System;
using Cairo;
using Gtk;

namespace GdiTest
{
	public class LineToDrawingArea : TestDrawingArea
	{
		private String dumpText;
		
		public LineToDrawingArea()
		{
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose args)
		{
			using (Context g = Gdk.CairoHelper.Create (args.Window))
			{
				g.LineWidth = 1;
				g.Antialias = Antialias.None;
				
				Win32GDI GDI_Win32 = Win32GDI.getInstance();
				
				if (GDI_Win32.isAvailable())
				{					
					System.Drawing.Graphics wg = Gtk.DotNet.Graphics.FromDrawable(this.GdkWindow, true);
					IntPtr hdc = wg.GetHdc();
					
					int i = 0;
					int n = 27;
					int w = 16;
					int h = 16;
					int k = 12;
					
					Area[] areas = new Area[n];
					Point[] startp = new Point[n];
					Point[] endp = new Point[n];
					int[] rop2 = new int[n - k + 1];
					
					/* Test Case 1: (0,0) -> (15,15) */
					areas[i].X = 0;
					areas[i].Y = 0;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w - 1;
					endp[i].Y =   areas[i].Y + h - 1;
					i++;
					
					/* Test Case 2: (15,15) -> (0,0) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + w - 1;
					startp[i].Y = areas[i].Y + h - 1;
					endp[i].X =   areas[i].X;
					endp[i].Y =   areas[i].Y;
					i++;
					
					/* Test Case 3: (15,0) -> (0,15) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + w - 1;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X;
					endp[i].Y =   areas[i].Y + h - 1;
					i++;
					
					/* Test Case 4: (0,15) -> (15,0) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y + h - 1;
					endp[i].X =   areas[i].X + w - 1;
					endp[i].Y =   areas[i].Y;
					i++;
					
					/* Test Case 5: (0,8) -> (15,8) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y + (h / 2);
					endp[i].X =   areas[i].X + w - 1;
					endp[i].Y =   areas[i].Y + (h / 2);
					i++;
					
					/* Test Case 6: (15,8) -> (0,8) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + w - 1;
					startp[i].Y = areas[i].Y + (h / 2);
					endp[i].X =   areas[i].X;
					endp[i].Y =   areas[i].Y + (h / 2);
					i++;
					
					/* Test Case 7: (8,0) -> (8,15) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + (w / 2);
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + (w / 2);
					endp[i].Y =   areas[i].Y + h - 1;
					i++;
					
					/* Test Case 8: (8,15) -> (8,0) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + (w / 2);
					startp[i].Y = areas[i].Y + h - 1;
					endp[i].X =   areas[i].X + (w / 2);
					endp[i].Y =   areas[i].Y;
					i++;
					
					/* Test Case 9: (4,4) -> (12,12) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + (w / 4);
					startp[i].Y = areas[i].Y + (h / 4);
					endp[i].X =   areas[i].X + 3 * (w / 4);
					endp[i].Y =   areas[i].Y + 3 * (h / 4);
					i++;
					
					/* Test Case 10: (12,12) -> (4,4) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + 3 * (w / 4);
					startp[i].Y = areas[i].Y + 3 * (h / 4);
					endp[i].X =   areas[i].X + (w / 4);
					endp[i].Y =   areas[i].Y + (h / 4);
					i++;
					
					/* Test Case 11: (0,0) -> (+10,+10) */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w + 10;
					endp[i].Y =   areas[i].Y + h + 10;
					i++;
					
					/* Test Case 12: (0,0) -> (16,16), R2_BLACK */
					areas[i].X = 0;
					areas[i].Y = 2 * h;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_BLACK;
					i++;
					
					/* Test Case 13: (0,0) -> (16,16), R2_MERGENOTPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_MERGENOTPEN;
					i++;
					
					/* Test Case 14: (0,0) -> (16,16), R2_MASKNOTPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_MASKNOTPEN;
					i++;
					
					/* Test Case 15: (0,0) -> (16,16), R2_NOTCOPYPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_NOTCOPYPEN;
					i++;
					
					/* Test Case 16: (0,0) -> (16,16), R2_MASKPENNOT */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_MASKPENNOT;
					i++;
					
					/* Test Case 17: (0,0) -> (16,16), R2_NOT */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_NOT;
					i++;
					
					/* Test Case 18: (0,0) -> (16,16), R2_XORPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_XORPEN;
					i++;
					
					/* Test Case 19: (0,0) -> (16,16), R2_NOTMASKPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_NOTMASKPEN;
					i++;
					
					/* Test Case 20: (0,0) -> (16,16), R2_MASKPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_MASKPEN;
					i++;
					
					/* Test Case 21: (0,0) -> (16,16), R2_NOTXORPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_NOTXORPEN;
					i++;
					
					/* Test Case 22: (0,0) -> (16,16), R2_NOP */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_NOP;
					i++;
					
					/* Test Case 23: (0,0) -> (16,16), R2_MERGENOTPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_MERGENOTPEN;
					i++;
					
					/* Test Case 24: (0,0) -> (16,16), R2_COPYPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_COPYPEN;
					i++;
					
					/* Test Case 25: (0,0) -> (16,16), R2_MERGEPENNOT */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_MERGEPENNOT;
					i++;
					
					/* Test Case 26: (0,0) -> (16,16), R2_MERGEPEN */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_MERGEPEN;
					i++;
					
					/* Test Case 27: (0,0) -> (16,16), R2_WHITE */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					rop2[i - k + 1] = GDI.R2_WHITE;
					i++;
					
					for (i = 0; i < k; i++)
					{
						/* Set Clipping Region */
						IntPtr clippingRegion = GDI_Win32.CreateRectRgn(areas[i].X, areas[i].Y, areas[i].X + areas[i].W, areas[i].Y + areas[i].H);
						GDI_Win32.SelectClipRgn(hdc, IntPtr.Zero);
						GDI_Win32.SelectClipRgn(hdc, clippingRegion);
							
						/* Fill Area with White */
						g.Color = new Color(255,255,255);
						Rectangle rect = new Rectangle(areas[i].X, areas[i].Y, areas[i].W, areas[i].H);
						g.Rectangle(rect);
						g.Fill();
						g.Stroke();
							
						/* Render Test Case */
						IntPtr pen = GDI_Win32.CreatePen(1, 1, 0);
						IntPtr oldPen = GDI_Win32.SelectObject(hdc, pen);
						GDI_Win32.MoveToEx(hdc, startp[i].X, startp[i].Y, IntPtr.Zero);
						GDI_Win32.LineTo(hdc, endp[i].X, endp[i].Y);
					
						dumpText += "unsigned char line_to_case_" + (i + 1) + "[" + areas[i].W * areas[i].H + "] = \n";
						dumpText += dumpPixelArea(GDI_Win32, hdc, areas[i].X, areas[i].Y, areas[i].W, areas[i].H) + "\n";
					}
					
					for (i = k; i < n; i++)
					{
						/* Set Clipping Region */
						IntPtr clippingRegion = GDI_Win32.CreateRectRgn(areas[i].X, areas[i].Y, areas[i].X + areas[i].W, areas[i].Y + areas[i].H);
						GDI_Win32.SelectClipRgn(hdc, IntPtr.Zero);
						GDI_Win32.SelectClipRgn(hdc, clippingRegion);
							
						/* Fill Area with White */
						g.Color = new Color(255,255,255);
						Rectangle rect = new Rectangle(areas[i].X, areas[i].Y, areas[i].W, areas[i].H);
						g.Rectangle(rect);
						g.Fill();
						g.Stroke();
							
						/* Render Test Case */
						GDI_Win32.SetROP2(hdc, rop2[i - k + 1]);
						IntPtr pen = GDI_Win32.CreatePen(1, 1, 0);
						IntPtr oldPen = GDI_Win32.SelectObject(hdc, pen);
						GDI_Win32.MoveToEx(hdc, startp[i].X, startp[i].Y, IntPtr.Zero);
						GDI_Win32.LineTo(hdc, endp[i].X, endp[i].Y);
					
						dumpText += "unsigned char line_to_case_" + (i + 1) + "[" + areas[i].W * areas[i].H + "] = \n";
						dumpText += dumpPixelArea(GDI_Win32, hdc, areas[i].X, areas[i].Y, areas[i].W, areas[i].H) + "\n";
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

