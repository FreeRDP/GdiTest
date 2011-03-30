using System;
namespace GdiTest
{
	public class PolylineToDrawingArea : TestDrawingArea
	{
		private String dumpText;
		
		public PolylineToDrawingArea ()
		{
			dumpText = "";
		}
		
		public override String getDumpText()
		{
			return dumpText;
		}
	}
}

