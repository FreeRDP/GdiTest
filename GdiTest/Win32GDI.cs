using System;
using System.Runtime.InteropServices;

namespace GdiTest
{
	public class Win32GDI : GDI
	{
		static bool available = false;
		static bool initialized = false;
		static Win32GDI instance = null;
		
		public struct Callbacks
		{
	        [DllImport("user32.dll")]
	        public static extern IntPtr GetDC(IntPtr hWnd);
			
	        [DllImport("user32.dll")]
	        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
	        
			[DllImport("gdi32")]
			public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

			[DllImport("gdi32")]
			public static extern bool DeleteObject(IntPtr hObject);
			
			[DllImport("gdi32.dll")]
	        public static extern uint GetPixel(IntPtr hdc, int X, int Y);
	        
			[DllImport("gdi32.dll")]
	        public static extern uint SetPixel(IntPtr hdc, int X, int Y, int crColor);
			
			[DllImport("gdi32")]
			public static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);
			
			[DllImport("gdi32.dll")]
	        public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);
			
			[DllImport("gdi32")]
			public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor);
			
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
		
		public override IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj)
		{
			if (available)
				return Callbacks.SelectObject(hdc, hgdiobj);
			else
				return (IntPtr) null;	
		}
		
		public override bool DeleteObject(IntPtr hObject)
		{
			if (available)
				return Callbacks.DeleteObject(hObject);
			else
				return false;
		}
		
		public override uint GetPixel(IntPtr hdc, int X, int Y)
		{
			if (available)
				return Callbacks.GetPixel(hdc, X, Y);
			else
				return 0;
		}
		
		public override uint SetPixel(IntPtr hdc, int X, int Y, int crColor)
		{
			if (available)
				return Callbacks.SetPixel(hdc, X, Y, crColor);
			else
				return 0;
		}
		
		public System.Drawing.Color GetPixelColor(int x, int y)
		{
			IntPtr hdc = GetDC(IntPtr.Zero);
			uint pixel = Callbacks.GetPixel(hdc, x, y);
			ReleaseDC(IntPtr.Zero, hdc);
			System.Drawing.Color color = System.Drawing.Color.FromArgb((int)(pixel & 0x000000FF),
			                             (int)(pixel & 0x0000FF00) >> 8,
			                             (int)(pixel & 0x00FF0000) >> 16);
			return color;
      }
		
		public override bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint)
		{
			if (available)
				return Callbacks.MoveToEx(hdc, X, Y, lpPoint);
			else
				return false;
		}
		
		public override bool LineTo(IntPtr hdc, int nXEnd, int nYEnd)
		{
			if (available)
				return Callbacks.LineTo(hdc, nXEnd, nYEnd);
			else
				return false;
		}
		
		public override IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor)
		{
			if (available)
				return Callbacks.CreatePen(fnPenStyle, nWidth, crColor);
			else
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

