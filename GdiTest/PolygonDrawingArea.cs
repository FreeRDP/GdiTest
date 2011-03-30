using System;
namespace GdiTest
{
	public class PolygonDrawingArea : TestDrawingArea
	{
		private String dumpText;
		
		public PolygonDrawingArea ()
		{
			dumpText = "";
		}
		
		public override String getDumpText()
		{
			return dumpText;
		}
	}
}

