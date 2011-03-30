using System;
using Cairo;
using Gtk;

namespace GdiTest
{
	public struct Area
	{
		public int X;
		public int Y;
		public int W;
		public int H;
	}
	
	public abstract class TestDrawingArea : DrawingArea
	{	
		public TestDrawingArea ()
		{
		}
		
		abstract public String getDumpText();
		
		public String dumpPixelArea(GDI gdi, IntPtr hdc, int X, int Y, int W, int H)
		{
			String text = "";
			
			if (gdi.isAvailable())
			{
				text += "{\n";
				for (int y = Y; y < Y + H; y++)
				{
					text += "\t\"";
					for (int x = X; x < X + W; x++)
					{
						int p = gdi.GetPixel(hdc, x, y);
						
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
	}
}

