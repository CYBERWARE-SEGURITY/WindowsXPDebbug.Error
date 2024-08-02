using System;
using System.Runtime.InteropServices;
using static Vanara.PInvoke.Gdi32;
using System.Windows.Forms;
using Vanara.PInvoke;
using static Vanara.PInvoke.Kernel32;
using static Vanara.PInvoke.User32;

namespace Project1
{
    public class CargaVoid
    {
        public static void EffectWaveInteractive()
        {
            var dc = GetDC(IntPtr.Zero);
            var dcCopy = CreateCompatibleDC(dc);
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = -h;
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = 0; // BI_RGB

            IntPtr bits;
            var bmp = CreateDIBSection(dc, ref bmpi, 0, out bits, IntPtr.Zero, 0);
            var oldBmp = SelectObject(dcCopy, bmp);

            Random rand = new Random();
            double time = 0;

            while (true)
            {
                POINT mousePoint;
                GetCursorPos(out mousePoint);
                ScreenToClient(IntPtr.Zero, ref mousePoint);
                int mouseX = mousePoint.X;
                int mouseY = mousePoint.Y;

                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    for (int x = 0; x < w; x++)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            int index = y * w + x;

                            double waveX = Math.Sin((x + time) * 0.05 + mouseX * 0.01) * 200 + 170;
                            double waveY = Math.Cos((y + time) * 0.05 + mouseY * 0.01) * 200 + 170;

                            rgbquad[index].rgbRed = (byte)waveX;
                            rgbquad[index].rgbGreen = (byte)waveY;
                            rgbquad[index].rgbBlue = (byte)((waveX * waveY) / 100);
                            rgbquad[index].rgbReserved = 0;
                        }
                    }
                }

                StretchBlt(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, RasterOperationMode.SRCCOPY);

                time += 0.1;
                Sleep(30);
            }

            SelectObject(dcCopy, oldBmp);
            ReleaseDC(IntPtr.Zero, dc);
            ReleaseDC(IntPtr.Zero, dcCopy);
        }
    }
}
