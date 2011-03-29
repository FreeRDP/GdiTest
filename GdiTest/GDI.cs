using System;
using System.Runtime.InteropServices;

namespace GdiTest
{
	public abstract class GDI
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
		
		public GDI ()
		{
		}
		
		public abstract void init();
		public abstract bool isAvailable();
		public abstract IntPtr GetDC(IntPtr hWnd);
		public abstract int ReleaseDC(IntPtr hWnd, IntPtr hDC);
		public abstract IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
		public abstract bool DeleteObject(IntPtr hObject);
		public abstract uint GetPixel(IntPtr hdc, int X, int Y);
		public abstract uint SetPixel(IntPtr hdc, int X, int Y, uint crColor);
		public abstract bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);
		public abstract bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);
		public abstract IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor);
		public abstract int BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight,
		                         IntPtr hdcSrc, int nXSrc, int nYSrc, System.Int32 dwRop);
	}
}

