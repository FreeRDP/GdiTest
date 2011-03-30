using System;
using Cairo;
using Gtk;

namespace GdiTest
{
	public class LineToDrawingArea : TestDrawingArea
	{		
		private bool rendered;
		private String dumpText;
		
		public LineToDrawingArea()
		{
			rendered = false;
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
					int n = 10;
					int w = 16;
					int h = 16;
					
					Area[] areas = new Area[n];
					Point[] startp = new Point[n];
					Point[] endp = new Point[n];
					
					/* Test Case 1 */
					areas[i].X = 0;
					areas[i].Y = 0;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + h;
					i++;
					
					/* Test Case 2 */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + w;
					startp[i].Y = areas[i].Y + h;
					endp[i].X =   areas[i].X;
					endp[i].Y =   areas[i].Y;
					i++;
					
					/* Test Case 3 */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + w;
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X;
					endp[i].Y =   areas[i].Y + h;
					i++;
					
					/* Test Case 4 */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y + h;
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y;
					i++;
					
					/* Test Case 5 */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X;
					startp[i].Y = areas[i].Y + (h / 2);
					endp[i].X =   areas[i].X + w;
					endp[i].Y =   areas[i].Y + (h / 2);
					i++;
					
					/* Test Case 6 */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + w;
					startp[i].Y = areas[i].Y + (h / 2);
					endp[i].X =   areas[i].X;
					endp[i].Y =   areas[i].Y + (h / 2);
					i++;
					
					/* Test Case 7 */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + (w / 2);
					startp[i].Y = areas[i].Y;
					endp[i].X =   areas[i].X + (w / 2);
					endp[i].Y =   areas[i].Y + h;
					i++;
					
					/* Test Case 8 */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + (w / 2);
					startp[i].Y = areas[i].Y + h;
					endp[i].X =   areas[i].X + (w / 2);
					endp[i].Y =   areas[i].Y;
					i++;
					
					/* Test Case 9 */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + (w / 4);
					startp[i].Y = areas[i].Y + (h / 4);
					endp[i].X =   areas[i].X + 3 * (w / 4);
					endp[i].Y =   areas[i].Y + 3 * (h / 4);
					i++;
					
					/* Test Case 10 */
					areas[i].X = areas[i - 1].X + areas[i - 1].W;
					areas[i].Y = areas[i - 1].Y;
					areas[i].W = w;
					areas[i].H = h;
					startp[i].X = areas[i].X + 3 * (w / 4);
					startp[i].Y = areas[i].Y + (h / 4);
					endp[i].X =   areas[i].X + (w / 4);
					endp[i].Y =   areas[i].Y + 3 * (h / 4);
					i++;
					
					for (i = 0; i < n; i++)
					{
						if (!rendered)
						{
							/* Fill Area with White */
							g.Color = new Color(255,255,255);
							Rectangle rect = new Rectangle(areas[i].X, areas[i].Y, areas[i].W, areas[i].H);
							g.Rectangle(rect);
							g.Fill();
							g.Stroke();
						}
							
						/* Render Test Case */
						IntPtr pen = GDI_Win32.CreatePen(1, 1, 0);
						IntPtr oldPen = GDI_Win32.SelectObject(hdc, pen);
						GDI_Win32.MoveToEx(hdc, startp[i].X, startp[i].Y, IntPtr.Zero);
						GDI_Win32.LineTo(hdc, endp[i].X, endp[i].Y);
					
						dumpText += "unsigned char line_to_case_" + (i + 1) + "[" + areas[i].W * areas[i].H + "] = \n";
						dumpText += this.dump(hdc, areas[i].X, areas[i].Y, areas[i].W, areas[i].H) + "\n";
					}
				}
			}
			
			return true;
		}
		
		public String dump(IntPtr hdc, int X, int Y, int W, int H)
		{
			String text = "";
			
			Win32GDI GDI_Win32 = Win32GDI.getInstance();
			
			if (GDI_Win32.isAvailable())
			{
				text += "{\n";
				for (int y = Y; y < Y + H; y++)
				{
					text += "\t\"";
					for (int x = X; x < X + W; x++)
					{
						int p = GDI_Win32.GetPixel(hdc, x, y);
						
						if (p == 0)
							text += "\\x00";
						else
							text += "\\xFF";
					}
					text += "\"\n";
				}
				text += "};\n";
			}
				
			return text;
		}
		
		public override String getDumpText()
		{
			return dumpText;
		}
	}
}

