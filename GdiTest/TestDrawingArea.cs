using System;
using Cairo;
using Gtk;

namespace GdiTest
{
	public abstract class TestDrawingArea : DrawingArea
	{
		public TestDrawingArea ()
		{
		}
		
		abstract public String dump();
		abstract public String getDumpText();
	}
}

