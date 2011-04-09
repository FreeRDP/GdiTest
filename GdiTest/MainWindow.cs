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
		FillTestComboBox(testComboBox);
	}
	
	private void FillTestComboBox(Gtk.ComboBox cb)
	{
		cb.Clear();
		CellRendererText cell = new CellRendererText();
		cb.PackStart(cell, false);
		cb.AddAttribute(cell, "text", 0);
		ListStore store = new ListStore(typeof (string));
		cb.Model = store;
    
		store.AppendValues("BitBlt");
		store.AppendValues("Ellipse");
		store.AppendValues("Polygon");
		store.AppendValues("LineTo");
		store.AppendValues("PolylineTo");
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
		
		if (testSuiteName.StartsWith("BitBlt"))
			testDrawingArea = bitBltDrawingArea;
		else if (testSuiteName.StartsWith("Ellipse"))
			testDrawingArea = ellipseDrawingArea;
		else if (testSuiteName.StartsWith("Polygon"))
			testDrawingArea = polygonDrawingArea;
		else if (testSuiteName.StartsWith("LineTo"))
			testDrawingArea = lineToDrawingArea;
		else if (testSuiteName.StartsWith("PolylineTo"))
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
