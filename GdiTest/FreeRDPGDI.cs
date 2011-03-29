using System;
using System.Runtime.InteropServices;

namespace GdiTest
{
	public class FreeRDPGDI : GDI
	{
		static bool available = false;
		static bool initialized = false;
		static FreeRDPGDI instance = null;
		
		public struct Callbacks
		{
			[DllImport("libfreerdpgdi")]
			public static extern IntPtr GetDC();
			
			[DllImport("libfreerdpgdi")]
			public static extern int BitBlt(
			                                 IntPtr hdcDest,
			                                 int nXDest,
			                                 int nYDest,
			                                 int nWidth,
			                                 int nHeight,
			                                 IntPtr hdcSrc,
			                                 int nXSrc,
			                                 int nYSrc,
			                                 System.Int32 dwRop
			                                 );
		}
		
		public FreeRDPGDI ()
		{
			this.init();
		}
		
		public static FreeRDPGDI getInstance()
		{
			if (!initialized)
			{
				instance = new FreeRDPGDI();
				initialized = true;
			}
			
			return instance;
		}
		
		public override void init()
		{
			available = false;
		}
		
		public override bool isAvailable()
		{
			return available;
		}
		
		public override IntPtr GetDC(IntPtr hWnd)
		{
			if (available)
				return Callbacks.GetDC();
			else
				return (IntPtr) null;
		}
		
		public override int ReleaseDC(IntPtr hWnd, IntPtr hDC)
		{
			return 0;
		}
		
		public override IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj)
		{
			return (IntPtr) null;	
		}
		
		public override bool DeleteObject(IntPtr hObject)
		{
			return false;
		}
		
		public override uint GetPixel(IntPtr hdc, int X, int Y)
		{
			return 0;
		}
		
		public override uint SetPixel(IntPtr hdc, int X, int Y, int crColor)
		{
			return 0;
		}
		
		public override bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint)
		{
			return false;
		}
		
		public override bool LineTo(IntPtr hdc, int nXEnd, int nYEnd)
		{
			return false;
		}
		
		public override IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor)
		{
			return (IntPtr) null;
		}
		
		public override int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
		                                 IntPtr hdcSrc, int nXSrc, int nYSrc, System.Int32 dwRop)
		{
			if (available)
				return Callbacks.BitBlt(hdcDest, nXDest, nYDest, nWidth, nHeight, hdcSrc, nXSrc, nYSrc, dwRop);
			else
				return 0;
		}		
	}
}

