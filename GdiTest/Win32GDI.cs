using System;
using System.Runtime.InteropServices;

namespace GdiTest
{
	public class Win32GDI : GDI
	{
		static bool available = false;
		static bool initialized = false;
		static Win32GDI instance = null;
		
		public class Callbacks
		{
	        [DllImport("user32.dll")]
	        public static extern IntPtr GetDC(IntPtr hWnd);
			
	        [DllImport("user32.dll")]
	        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
	        
			[DllImport("gdi32.dll")]
	        public static extern int GetPixel(IntPtr hdc, int X, int Y);
	        
			[DllImport("gdi32.dll")]
	        public static extern int SetPixel(IntPtr hdc, int X, int Y, int crColor);
			
			[DllImport("gdi32.dll")]
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
		
		public Win32GDI ()
		{
			this.init();
		}
		
		public static Win32GDI getInstance()
		{
			if (!initialized)
			{
				instance = new Win32GDI();
				initialized = true;
			}
			
			return instance;
		}
		
		public override void init()
		{
			int p = (int) Environment.OSVersion.Platform;
			
			if ((p == 4) || (p == 6) || (p == 128)) {
				available = false;
			} else {
				available = true;
			}
		}
		
		public override bool isAvailable()
		{
			return available;
		}
		
		public override IntPtr GetDC(IntPtr hWnd)
		{
			if (available)
				return Callbacks.GetDC(hWnd);
			else
				return (IntPtr) null;
		}
		
		public override int ReleaseDC(IntPtr hWnd, IntPtr hDC)
		{
			if (available)
				return Callbacks.ReleaseDC(hWnd, hDC);
			else
				return 0;
		}
		
		public override int GetPixel(IntPtr hdc, int X, int Y)
		{
			if (available)
				return Callbacks.GetPixel(hdc, X, Y);
			else
				return 0;
		}
		
		public override int SetPixel(IntPtr hdc, int X, int Y, int crColor)
		{
			if (available)
				return Callbacks.SetPixel(hdc, X, Y, crColor);
			else
				return 0;
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

