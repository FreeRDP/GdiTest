using System;
using System.Runtime.InteropServices;

namespace GdiTest
{
	public class Gdi32
	{
		public static System.Int32 SRCCOPY =		0x00CC0020; /* D = S */
		public static System.Int32 SRCPAINT =		0x00EE0086; /* D = S | D */
		public static System.Int32 SRCAND =			0x008800C6; /* D = S & D */
		public static System.Int32 SRCINVERT =		0x00660046; /* D = S ^ D */
		public static System.Int32 SRCERASE	=		0x00440328; /* D = S & ~D */
		public static System.Int32 NOTSRCCOPY =		0x00330008; /* D = ~S */
		public static System.Int32 NOTSRCERASE =	0x001100A6; /* D = ~S & ~D */
		public static System.Int32 MERGECOPY =		0x00C000CA; /* D = S & P */
		public static System.Int32 MERGEPAINT =		0x00BB0226; /* D = ~S | D */
		public static System.Int32 PATCOPY =		0x00F00021; /* D = P */
		public static System.Int32 PATPAINT =		0x00FB0A09; /* D = D | (P | ~S) */
		public static System.Int32 PATINVERT =		0x005A0049; /* D = P ^ D */
		public static System.Int32 DSTINVERT =		0x00550009; /* D = ~D */
		public static System.Int32 BLACKNESS =		0x00000042; /* D = 0 */
		public static System.Int32 WHITENESS =		0x00FF0062; /* D = 1 */
		public static System.Int32 DSPDxax =		0x00E20746; /* D = (S & P) | (~S & D) */
		public static System.Int32 SPna =			0x000C0324;	/* D = S & ~P */
		
		public class NativeGdi32
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
		
		public static IntPtr GetDC(IntPtr hWnd)
		{
			if (win32)
				return NativeGdi32.GetDC(hWnd);
			else
				return (IntPtr) null;
		}
		
		public static int ReleaseDC(IntPtr hWnd, IntPtr hDC)
		{
			if (win32)
				return NativeGdi32.ReleaseDC(hWnd, hDC);
			else
				return 0;
		}
		
		public static int GetPixel(IntPtr hdc, int X, int Y)
		{
			if (win32)
				return NativeGdi32.GetPixel(hdc, X, Y);
			else
				return 0;
		}
		
		public static int SetPixel(IntPtr hdc, int X, int Y, int crColor)
		{
			if (win32)
				return NativeGdi32.SetPixel(hdc, X, Y, crColor);
			else
				return 0;
		}
		
		public static int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
		                                 IntPtr hdcSrc, int nXSrc, int nYSrc, System.Int32 dwRop)
		{
			if (win32)
				return NativeGdi32.BitBlt(hdcDest, nXDest, nYDest, nWidth, nHeight, hdcSrc, nXSrc, nYSrc, dwRop);
			else
				return 0;
		}
	}
}

