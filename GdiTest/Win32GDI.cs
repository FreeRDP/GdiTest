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
			
			[DllImport("gdi32")]
	        public static extern int GetPixel(IntPtr hdc, int X, int Y);
	        
			[DllImport("gdi32")]
	        public static extern int SetPixel(IntPtr hdc, int X, int Y, int crColor);
			
			[DllImport("gdi32")]
			public static extern bool MoveToEx(IntPtr hdc, int X, int Y, IntPtr lpPoint);
			
			[DllImport("gdi32")]
	        public static extern bool LineTo(IntPtr hdc, int nXEnd, int nYEnd);
			
			[DllImport("gdi32")]
			public static extern bool PolylineTo(IntPtr hdc, POINT [] lppt, uint cCount);
			
			[DllImport("gdi32")]
			public static extern IntPtr CreatePen(int fnPenStyle, int nWidth, int crColor);
			
			[DllImport("gdi32")]
			public static extern IntPtr CreateSolidBrush(int crColor);
			
			[DllImport("gdi32")]
			public static extern IntPtr CreatePatternBrush(IntPtr hbmp);
			
			[DllImport("gdi32")]
			public static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, IntPtr lpvBits);
			
			[DllImport("gdi32")]
			public static extern bool Ellipse(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
			
			[DllImport("gdi32")]
			public static extern bool Polygon(IntPtr hdc, POINT [] lpPoints, int nCount);
			
			[DllImport("gdi32")]
			public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
			
			[DllImport("gdi32")]
			public static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);
			
			[DllImport("gdi32")]
			public static extern int SetROP2(IntPtr hdc, int fnDrawMode);

			[DllImport("gdi32")]
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
		
		public System.Drawing.Color GetPixelColor(IntPtr hdc, int X, int Y)
		{
			int pixel = Callbacks.GetPixel(hdc, X, Y);
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
		
		public override bool PolylineTo(IntPtr hdc, POINT [] lppt, uint cCount)
		{
			if (available)
				return Callbacks.PolylineTo(hdc, lppt, cCount);
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
		
		public override bool Ellipse(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
		{
			if (available)
				return Callbacks.Ellipse(hdc, nLeftRect, nTopRect, nRightRect, nBottomRect);
			else
				return false;
		}
		
		public override bool Polygon(IntPtr hdc, POINT [] lpPoints, int nCount)
		{
			if (available)
				return Callbacks.Polygon(hdc, lpPoints, nCount);
			else
				return false;
		}
		
		public override IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
		{
			if (available)
				return Callbacks.CreateRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect);
			else
				return (IntPtr) null;
		}
		
		public override int SelectClipRgn(IntPtr hdc, IntPtr hrgn)
		{
			if (available)
				return Callbacks.SelectClipRgn(hdc, hrgn);
			else
				return 0;
		}
		
		public override IntPtr CreateSolidBrush(int crColor)
		{
			if (available)
				return Callbacks.CreateSolidBrush(crColor);
			else
				return (IntPtr) null;
		}
		
		public override IntPtr CreatePatternBrush(IntPtr hbmp)
		{
			if (available)
				return Callbacks.CreatePatternBrush(hbmp);
			else
				return (IntPtr) null;
		}
		
		public override IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, IntPtr lpvBits)
		{
			if (available)
				return Callbacks.CreateBitmap(nWidth, nHeight, cPlanes, cBitsPerPel, lpvBits);
			else
				return (IntPtr) null;
		}
		
		public override int SetROP2(IntPtr hdc, int fnDrawMode)
		{
			if (available)
				return Callbacks.SetROP2(hdc, fnDrawMode);
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

