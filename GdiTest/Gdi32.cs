using System;
using System.Runtime.InteropServices;

namespace GdiTest
{
	public class Gdi32
	{		
		[DllImport("gdi32.dll")]
		
		private static extern int BitBlt(
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
		
		static bool win32 = false;
		
		public Gdi32 ()
		{
		}
		
		public static void init()
		{
			int p = (int) Environment.OSVersion.Platform;
			
			if ((p == 4) || (p == 6) || (p == 128)) {
				win32 = false;
			} else {
				win32 = true;
			}
		}
		
		public static void setWin32(bool pWin32)
		{
			win32 = pWin32;
		}
		
		public static bool getWin32()
		{
			return win32;
		}
			
		public static int _BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
		                                 IntPtr hdcSrc, int nXSrc, int nYSrc, System.Int32 dwRop)
		{
			if (win32)
				return BitBlt(hdcDest, nXDest, nYDest, nWidth, nHeight, hdcSrc, nXSrc, nYSrc, dwRop);
			else
				return -1;
		}
	}
}

