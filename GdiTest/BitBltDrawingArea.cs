using Gtk;
using Cairo;
using System;
using System.Drawing;

namespace GdiTest
{
	public class BitBltDrawingArea : TestDrawingArea
	{
		String dumpText;
		
		public BitBltDrawingArea ()
		{
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
				
					TestData data = new TestData();
					
					for (int i = 0; i < data.bmp_SRC.GetLength(0) / 2; i++)
					{
						for (int j = 0; j < data.bmp_SRC.GetLength(1); j++)
						{
							uint p = 0;
							
							if (data.bmp_SRC[i, j] == 0)
								p = 0;
							else
								p = 0xFFFFFFFF;
							
							GDI_Win32.SetPixel(hdc, i, j, p);
						}
					}
					
					//GDI_Win32.BitBlt(dc, 70, 0, 60, 60, dc, 0, 0, GDI.SRCCOPY);
				}
			}
			return true;
		}
		
		public override String dump()
		{
			String text = "";
			
			Win32GDI GDI_Win32 = Win32GDI.getInstance();
				
			if (GDI_Win32.isAvailable())
			{					
				System.Drawing.Graphics wg = Gtk.DotNet.Graphics.FromDrawable(this.GdkWindow, true);
				IntPtr hdc = wg.GetHdc();
				
				for (int y = 0; y < 16; y++)
				{
					for (int x = 0; x < 16; x++)
					{
						System.Drawing.Color color = GDI_Win32.GetPixelColor(hdc, x, y);
						text += String.Format("0x{0:X}, ", color.ToArgb());
					}
					text += "\n";
				}
			}
				
			return text;
		}
		
		public override String getDumpText ()
		{
			return dumpText;
		}
	}
}

