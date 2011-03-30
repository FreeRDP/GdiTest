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
	}
}

