using System;
using Cairo;
using Gtk;

using GdiTest;

public partial class MainWindow : Gtk.Window
{
	TestDrawingArea testDrawingArea;
	TestDrawingArea lineToDrawingArea;
	TestDrawingArea bitBltDrawingArea;
	TestDrawingArea ellipseDrawingArea;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		lineToDrawingArea = new LineToDrawingArea();
		bitBltDrawingArea = new BitBltDrawingArea();
		ellipseDrawingArea = new EllipseDrawingArea();
		
		testDrawingArea = lineToDrawingArea;
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
		else if (testSuiteName.Equals("Ellipse"))
		{
			testDrawingArea = ellipseDrawingArea;
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
