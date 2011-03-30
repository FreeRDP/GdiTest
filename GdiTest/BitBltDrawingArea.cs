using Gtk;
using Cairo;
using System;
using System.Drawing;

namespace GdiTest
{
	public class BitBltDrawingArea : TestDrawingArea
	{
		Bitmap bmp_SRC;
		Bitmap bmp_DST;
		Bitmap bmp_PAT;
		String dumpText;
		
		public BitBltDrawingArea ()
		{
			String cwd = Environment.CurrentDirectory;
			bmp_SRC = new Bitmap(cwd + "\\..\\..\\resources\\bmp_SRC.bmp");
			bmp_DST = new Bitmap(cwd + "\\..\\..\\resources\\bmp_DST.bmp");
			bmp_PAT = new Bitmap(cwd + "\\..\\..\\resources\\bmp_PAT.bmp");
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
					
					Area aSrc = new Area();
					Area aDst = new Area();
					Area aPat = new Area();
					
					aSrc.X = 0;
					aSrc.Y = 0;
					aSrc.W = 16;
					aSrc.H = 16;
					
					aDst.X = 16;
					aDst.Y = 0;
					aDst.W = 16;
					aDst.H = 16;
					
					aPat.X = 32;
					aPat.Y = 0;
					aPat.W = 8;
					aPat.H = 8;
					
					drawBitmap(hdc, aSrc.X, aSrc.Y, bmp_SRC);
					drawBitmap(hdc, aDst.X, aDst.Y, bmp_DST);
					drawBitmap(hdc, aPat.X, aPat.Y, bmp_PAT);
					
					dumpText += "unsigned char bmp_SRC[" + bmp_SRC.Width * bmp_SRC.Height + "] = \n";
					dumpText += dumpPixelArea(GDI_Win32, hdc, aSrc.X, aSrc.Y, bmp_SRC.Width, bmp_SRC.Height) + "\n";
					
					dumpText += "unsigned char bmp_DST[" + bmp_DST.Width * bmp_DST.Height + "] = \n";
					dumpText += dumpPixelArea(GDI_Win32, hdc, aDst.X, aDst.Y, bmp_DST.Width, bmp_DST.Height) + "\n";
					
					dumpText += "unsigned char bmp_PAT[" + bmp_PAT.Width * bmp_PAT.Height + "] = \n";
					dumpText += dumpPixelArea(GDI_Win32, hdc, aPat.X, aPat.Y, bmp_PAT.Width, bmp_PAT.Height) + "\n";
					
					int i = 0;
					int n = 11;
					int w = 16;
					int h = 16;
					
					/* Fill Area with White */
					cg.Color = new Cairo.Color(255,255,255);
					Cairo.Rectangle rect = new Cairo.Rectangle(0, 32, n * w, h);
					cg.Rectangle(rect);
					cg.Fill();
					cg.Stroke();
					
					Area[] areas = new Area[n];
					
					/* Test Case 1: SRCCOPY */
					areas[i].X = 0;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.SRCCOPY);
					i++;
					
					/* Test Case 2: BLACKNESS */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.BLACKNESS);
					i++;
					
					/* Test Case 3: WHITENESS */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.WHITENESS);
					i++;
					
					/* Test Case 4: SRCAND */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.SRCAND);
					i++;
					
					/* Test Case 5: SRCPAINT */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.SRCPAINT);
					i++;
					
					/* Test Case 6: SRCINVERT */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.SRCINVERT);
					i++;
					
					/* Test Case 7: SRCERASE */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.SRCERASE);
					i++;
					
					/* Test Case 8: NOTSRCCOPY */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.NOTSRCCOPY);
					i++;
					
					/* Test Case 9: NOTSRCERASE */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.NOTSRCERASE);
					i++;
					
					/* Test Case 10: DSTINVERT */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.DSTINVERT);
					i++;
					
					/* Test Case 11: SPna */
					areas[i].X = areas[i - 1].X + w;
					areas[i].Y = 32;
					areas[i].W = w;
					areas[i].H = h;
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aDst.X, aDst.Y, GDI.SRCCOPY);
					GDI_Win32.BitBlt(hdc, areas[i].X, areas[i].Y, aSrc.W, aSrc.H, hdc, aSrc.X, aSrc.Y, GDI.SPna);
					i++;
				}
			}
			return true;
		}
		
		public void drawBitmap(IntPtr hdc, int X, int Y, Bitmap bmp)
		{
			Win32GDI GDI_Win32 = Win32GDI.getInstance();
			
			for (int y = Y; y < Y + bmp.Height; y++)
			{
				for (int x = X; x < X + bmp.Width; x++)
				{
					int p = 0;
					System.Drawing.Color pixel = bmp.GetPixel(x - X, y - Y);
							
					if (pixel.R == 0 && pixel.G == 0 && pixel.B == 0)
						p = 0;
					else
						p = 0xFFFFFF;
							
					GDI_Win32.SetPixel(hdc, x, y, p);
				}
			}
		}
		
		public override String getDumpText ()
		{
			return dumpText;
		}
	}
}

