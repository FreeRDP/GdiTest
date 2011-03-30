using System;
using Cairo;
using Gtk;

using GdiTest;

public partial class MainWindow : Gtk.Window
{
	TestDrawingArea testDrawingArea;
	TestDrawingArea bitBltDrawingArea;
	TestDrawingArea ellipseDrawingArea;
	TestDrawingArea polygonDrawingArea;
	TestDrawingArea lineToDrawingArea;
	TestDrawingArea polylineToDrawingArea;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build();
		bitBltDrawingArea = new BitBltDrawingArea();
		ellipseDrawingArea = new EllipseDrawingArea();
		polygonDrawingArea = new PolygonDrawingArea();
		lineToDrawingArea = new LineToDrawingArea();
		polylineToDrawingArea = new PolylineToDrawingArea();
		
		testDrawingArea = bitBltDrawingArea;
		testFrame.Add(testDrawingArea);
		testFrame.ShowAll();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
	
	protected virtual void OnTestComboBoxChanged (object sender, System.EventArgs e)
	{
		if (testDrawingArea != null)
		{
			testFrame.Remove(testDrawingArea);
			testDrawingArea = null;
			dumpTextView.Buffer.Text = "";
		}
		
		String testSuiteName = testComboBox.ActiveText;
		
		if (testSuiteName.Equals("BitBlt"))
			testDrawingArea = bitBltDrawingArea;
		else if (testSuiteName.Equals("Ellipse"))
			testDrawingArea = ellipseDrawingArea;
		else if (testSuiteName.Equals("Polygon"))
			testDrawingArea = polygonDrawingArea;
		else if (testSuiteName.Equals("LineTo"))
			testDrawingArea = lineToDrawingArea;
		else if (testSuiteName.Equals("PolylineTo"))
			testDrawingArea = polylineToDrawingArea;
		
		if (testDrawingArea != null)
		{
			testFrame.Add(testDrawingArea);
			testFrame.ShowAll();
		}
	}
	protected virtual void OnDumpButtonClicked (object sender, System.EventArgs e)
	{
		String dumpText = testDrawingArea.getDumpText();
		dumpTextView.Buffer.Text = dumpText;
	}
	
	
}
