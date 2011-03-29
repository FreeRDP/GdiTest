using System;
using Cairo;
using Gtk;

using GdiTest;

public partial class MainWindow : Gtk.Window
{
	DrawingArea testDrawingArea;
	DrawingArea lineToDrawingArea;
	DrawingArea bitBltDrawingArea;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		lineToDrawingArea = new LineToDrawingArea ();
		bitBltDrawingArea = new BitBltDrawingArea ();
		
		testDrawingArea = lineToDrawingArea;
		testFrame.Add(testDrawingArea);
		testFrame.ShowAll();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected virtual void OnTestComboBoxChanged (object sender, System.EventArgs e)
	{
		if (testDrawingArea != null)
		{
			testFrame.Remove(testDrawingArea);
			testDrawingArea = null;
		}
		
		String testSuiteName = testComboBox.ActiveText;
		
		if (testSuiteName.Equals("LineTo"))
		{
			testDrawingArea = lineToDrawingArea;
			testFrame.Add(testDrawingArea);
			testFrame.ShowAll();
		}
		else if (testSuiteName.Equals("BitBlt"))
		{
			testDrawingArea = bitBltDrawingArea;
			testFrame.Add(testDrawingArea);
			testFrame.ShowAll();
		}
	}
}
