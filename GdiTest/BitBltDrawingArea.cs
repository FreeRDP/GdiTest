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
					
					drawBitmap(hdc, 0, 0, bmp_SRC);
					drawBitmap(hdc, 16, 0, bmp_DST);
					drawBitmap(hdc, 32, 0, bmp_PAT);
					
					dumpText += "unsigned char bmp_SRC[" + bmp_SRC.Width * bmp_SRC.Height + "] = \n";
					dumpText += dumpPixelArea(GDI_Win32, hdc, 0, 0, bmp_SRC.Width, bmp_SRC.Height) + "\n";
					
					dumpText += "unsigned char bmp_DST[" + bmp_DST.Width * bmp_DST.Height + "] = \n";
					dumpText += dumpPixelArea(GDI_Win32, hdc, 16, 0, bmp_DST.Width, bmp_DST.Height) + "\n";
					
					dumpText += "unsigned char bmp_PAT[" + bmp_PAT.Width * bmp_PAT.Height + "] = \n";
					dumpText += dumpPixelArea(GDI_Win32, hdc, 32, 0, bmp_PAT.Width, bmp_PAT.Height) + "\n";
					
					//GDI_Win32.BitBlt(dc, 70, 0, 60, 60, dc, 0, 0, GDI.SRCCOPY);
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

