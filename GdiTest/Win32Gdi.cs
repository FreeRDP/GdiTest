using System;
using System.Runtime.InteropServices;

namespace GdiTest
{
	public class Win32Gdi : Gdi
	{
		static bool available = false;
		static bool initialized = false;
		static Win32Gdi instance = null;
		
		public class NativeGdi
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
		
		public Win32Gdi ()
		{
			this.init();
		}
		
		public static Win32Gdi getInstance()
		{
			if (!initialized)
			{
				instance = new Win32Gdi();
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
				return NativeGdi.GetDC(hWnd);
			else
				return (IntPtr) null;
		}
		
		public override int ReleaseDC(IntPtr hWnd, IntPtr hDC)
		{
			if (available)
				return NativeGdi.ReleaseDC(hWnd, hDC);
			else
				return 0;
		}
		
		public override int GetPixel(IntPtr hdc, int X, int Y)
		{
			if (available)
				return NativeGdi.GetPixel(hdc, X, Y);
			else
				return 0;
		}
		
		public override int SetPixel(IntPtr hdc, int X, int Y, int crColor)
		{
			if (available)
				return NativeGdi.SetPixel(hdc, X, Y, crColor);
			else
				return 0;
		}
		
		public override int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
		                                 IntPtr hdcSrc, int nXSrc, int nYSrc, System.Int32 dwRop)
		{
			if (available)
				return NativeGdi.BitBlt(hdcDest, nXDest, nYDest, nWidth, nHeight, hdcSrc, nXSrc, nYSrc, dwRop);
			else
				return 0;
		}
	}
}

