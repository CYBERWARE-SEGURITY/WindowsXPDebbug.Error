using System;
using System.Runtime.InteropServices;
using System.Threading;
using static Vanara.PInvoke.User32;
using static Vanara.PInvoke.Gdi32;
using System.Windows.Forms;
using System.Drawing;
using Vanara.PInvoke;

namespace Project1
{
    public class Cargas
    {
        static int w = Screen.PrimaryScreen.Bounds.Width;
        static int h = Screen.PrimaryScreen.Bounds.Height;
        static int iconSize = 50;
        static double spiralSpeed = 1;

        public static void Cls()
        {
            for (int num = 0; num < 10; num++)
            {
                InvalidateRect(IntPtr.Zero, null, true);
                Thread.Sleep(10);
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDC(IntPtr hWnd);


        public static void Pay1()
        {
            int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;

            while(true)
            {
                for (int i = 1; i <= 7; i++)
                {
                    var hdc = GetDC(IntPtr.Zero);
                    var Brush = CreateSolidBrush(0xF0FFFF);
                    SelectObject(hdc, Brush);
                    PatBlt(hdc, 0, 0, x, y, RasterOperationMode.PATINVERT);
                    DeleteObject(Brush);
                    DeleteDC(hdc);

                    Thread.Sleep(200);
                }

                for (int o = 1; o <= 5; o++)
                {
                    var hdc = GetDC(IntPtr.Zero);
                    var Brush = CreateSolidBrush(0xF0FFFF);
                    SelectObject(hdc, Brush);
                    PatBlt(hdc, 0, 0, x, y, RasterOperationMode.PATINVERT);
                    DeleteObject(Brush);
                    DeleteDC(hdc);

                    Thread.Sleep(200);
                }

                for (int e = 1; e <= 2; e++)
                {
                    var hdc = GetDC(IntPtr.Zero);
                    var Brush = CreateSolidBrush(0xF0FFFF);
                    SelectObject(hdc, Brush);
                    PatBlt(hdc, 0, 0, x, y, RasterOperationMode.PATINVERT);
                    DeleteObject(Brush);
                    DeleteDC(hdc);

                    Thread.Sleep(800);
                }
            }
        }

        public static void Pay2()
        {
            var dc = GetDC(IntPtr.Zero);
            Random rand = new Random();
            while (true)
            {
                int x = rand.Next(Screen.PrimaryScreen.Bounds.Width);
                int y = rand.Next(Screen.PrimaryScreen.Bounds.Height);

                int x2 = rand.Next(Screen.PrimaryScreen.Bounds.Width);
                int y2 = rand.Next(Screen.PrimaryScreen.Bounds.Height);
                using (Graphics g = Graphics.FromHdc(dc))
                {
                    g.DrawImage(Project1.Properties.Resources.Erro, x, y);
                    g.DrawImage(Project1.Properties.Resources.Sla, x2, y2);
                }
                Thread.Sleep(100);
            }
        }

        public static void Pay3()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            var dcMem = CreateCompatibleDC(hdc);

            try
            {
                Icon errorIcon = SystemIcons.Error;
                Bitmap iconBitmap = errorIcon.ToBitmap();
                IntPtr hBitmap = iconBitmap.GetHbitmap();
                var oldBitmap = SelectObject(dcMem, hBitmap);

                double[] angles = { 0, 0, 0, 0 };
                double[] radii = { 0, 0, 0, 0 };
                Point[] centers = {
                new Point(w / 4, h / 4),
                new Point(3 * w / 4, h / 4),
                new Point(w / 4, 3 * h / 4),
                new Point(3 * w / 4, 3 * h / 4)
            };

                while (true)
                {
                    BitBlt(hdc, 0, 0, w, h, IntPtr.Zero, 0, 0, RasterOperationMode.SRCCOPY);

                    for (int i = 0; i < 4; i++)
                    {
                        double x = centers[i].X + radii[i] * Math.Cos(angles[i]) - iconSize / 2;
                        double y = centers[i].Y + radii[i] * Math.Sin(angles[i]) - iconSize / 2;

                        using (Graphics g = Graphics.FromHdc(hdc))
                        {
                            g.DrawImage(iconBitmap, (float)x, (float)y, iconSize, iconSize);
                        }

                        angles[i] += spiralSpeed;
                        radii[i] += 0.5;
                    }

                    Thread.Sleep(30);
                }
            }
            finally
            {
                DeleteDC(dcMem);
                ReleaseDC(IntPtr.Zero, hdc);
            }

        }

        public static void Pay4()
        {
            Random r;
            int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;
            int left = Screen.PrimaryScreen.Bounds.Left, right = Screen.PrimaryScreen.Bounds.Right, top = Screen.PrimaryScreen.Bounds.Top, bottom = Screen.PrimaryScreen.Bounds.Bottom;
            uint[] rndclr = { 0x1B4BE8, 0x7C8D0F, 0xF51D1D, 0x21BDFF };
            POINT[] lppoint = new POINT[3];

            while (true)
            {
                r = new Random();
                var hdc = GetDC(IntPtr.Zero);
                var hwnd = GetDesktopWindow();
                var dc = GetWindowDC(hwnd);
                var mhdc = CreateCompatibleDC(hdc);
                var hbit = CreateCompatibleBitmap(hdc, x, y);
                var holdbit = SelectObject(mhdc, hbit);
                lppoint[0].X = (left + 50) + 0;
                lppoint[0].Y = (top - 50) + 0;
                lppoint[1].X = (right + 50) + 0;
                lppoint[1].Y = (top + 50) + 0;
                lppoint[2].X = (left - 50) + 0;
                lppoint[2].Y = (bottom - 50) + 0;
                PlgBlt(hdc, lppoint, hdc, left - 20, top - 20, (right - left) + 40, (bottom - top) + 40, IntPtr.Zero, 0, 0);
                StretchBlt(hdc, 25, 25, x - 50, y - 50, hdc, 0, 0, x, y, RasterOperationMode.SRCCOPY);
                StretchBlt(dc, 0, y, x, -y, dc, 0, 0, x, y, RasterOperationMode.SRCCOPY);
                DeleteDC(hdc);
                Thread.Sleep(50);

            }
        }

        public static void Pay5()
        {
            Random r;
            int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;
            int left = Screen.PrimaryScreen.Bounds.Left, right = Screen.PrimaryScreen.Bounds.Right, top = Screen.PrimaryScreen.Bounds.Top, bottom = Screen.PrimaryScreen.Bounds.Bottom;
            uint[] rndclr = { 0x990000, 0x0900FF, 0x39C14B, 0x21BDFF };
            POINT[] lppoint = new POINT[3];

            while (true)
            {
                r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                var mhdc = CreateCompatibleDC(hdc);
                var hbit = CreateCompatibleBitmap(hdc, x, y);
                var holdbit = SelectObject(mhdc, hbit);
                var Brush = CreateSolidBrush(rndclr[1]);
                SelectObject(hdc, Brush);
                BitBlt(hdc, 0, 0, x, y, hdc, 0, 0, RasterOperationMode.MERGECOPY);
                DeleteObject(Brush);
                DeleteDC(hdc);
                Thread.Sleep(1000);
            }
        }

        public static void Pay6()
        {
            var hwnd = GetDesktopWindow();
            var hdc = GetWindowDC(hwnd);

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            while(true)
            {
                for (int i = 1; i <= 20; i++)
                {
                    // Calculando a nova largura e altura
                    int newWidth = screenWidth + (i + 50);  // Aumenta gradualmente a largura
                    int newHeight = screenHeight + (i + 50); // Aumenta gradualmente a altura

                    // Calculando a posição para centralizar a imagem
                    int xDest = (screenWidth - newWidth) / 2;
                    int yDest = (screenHeight - newHeight) / 2;

                    StretchBlt(hdc, xDest, yDest, newWidth, newHeight, hdc, 0, 0, screenWidth, screenHeight, RasterOperationMode.SRCCOPY);

                    // Atraso para criar o efeito gradativo
                    Thread.Sleep(10);

                    Cls();
                }
            }

            // Liberar o contexto de dispositivo (DC)
            ReleaseDC(hwnd, hdc);
        }
    

        public static void Pay7()
        {
            Random r;
            int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;
            int left = Screen.PrimaryScreen.Bounds.Left, right = Screen.PrimaryScreen.Bounds.Right, top = Screen.PrimaryScreen.Bounds.Top, bottom = Screen.PrimaryScreen.Bounds.Bottom;
            uint[] rndclr = { 0x1B4BE8, 0x7C8D0F, 0xF51D1D, 0x21BDFF };
            POINT[] lppoint = new POINT[3];

            while (true)
            {
                r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                var mhdc = CreateCompatibleDC(hdc);
                var hbit = CreateCompatibleBitmap(hdc, x, y);
                var holdbit = SelectObject(mhdc, hbit);
                var Brush = CreateSolidBrush(rndclr[3]);
                int rand = r.Next(x);
                BitBlt(hdc, rand, r.Next(-4, 4), r.Next(100), y, hdc, rand, 0, RasterOperationMode.SRCCOPY);
                DeleteDC(hdc);
            }
        }

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateHatchBrush(int iHatch, uint Color);
        public static void Pay8()
        {
            Random r;
            int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;
            int left = Screen.PrimaryScreen.Bounds.Left, right = Screen.PrimaryScreen.Bounds.Right, top = Screen.PrimaryScreen.Bounds.Top, bottom = Screen.PrimaryScreen.Bounds.Bottom;
            uint[] rndclr = { 0x1B4BE8, 0x7C8D0F, 0xF51D1D, 0x21BDFF };
            POINT[] lppoint = new POINT[3];

            while (true)
            {
                r = new Random();
                IntPtr hdc = GetDC(IntPtr.Zero);
                var mhdc = CreateCompatibleDC(hdc);
                var hbit = CreateCompatibleBitmap(hdc, x, y);
                var holdbit = SelectObject(mhdc, hbit);
                var Brush = CreateHatchBrush(r.Next(4), 0);
                SetBkColor(hdc, rndclr[r.Next(rndclr.Length)]);
                SelectObject(hdc, Brush);
                PatBlt(hdc, 0, 0, x, y, RasterOperationMode.PATINVERT);
                DeleteObject(Brush);
                DeleteDC(hdc);
                Thread.Sleep(100);
            }
        }
        private const int AC_SRC_OVER = 0x00;
        private const int BI_RGB = 0;
        private const uint DIB_RGB_COLORS = 0;

        public static void Pay9()
        {
            IntPtr dc = GetDC(IntPtr.Zero);
            var dcCopy = CreateCompatibleDC(dc);
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmpi.bmiHeader);
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = -h; // Corrigir para orientação correta
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = BI_RGB;

            IntPtr bits;
            var hBitmap = CreateDIBSection(dc, ref bmpi, DIB_RGB_COLORS, out bits, IntPtr.Zero, 0);
            SelectObject(dcCopy, hBitmap);

            Random random = new Random();
            Point[] particles = new Point[1000];
            Color[] colors = new Color[1000];

            // Inicializa as partículas e suas cores
            for (int i = 0; i < 1000; i++)
            {
                particles[i] = new Point(random.Next(0, w), random.Next(0, h));
                colors[i] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }

            while (true)
            {
                // Copia a tela atual para o DC compatível
                StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, RasterOperationMode.SRCCOPY);

                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    for (int i = 0; i < 1000; i++)
                    {
                        particles[i].Y += 50;
                        if (particles[i].Y > h)
                        {
                            particles[i].Y = 0;
                            particles[i].X = random.Next(0, w);
                            colors[i] = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                        }

                        // Desenha a partícula
                        for (int dx = -50 / 2; dx < 50 / 2; dx++)
                        {
                            for (int dy = -50 / 2; dy < 50 / 2; dy++)
                            {
                                int x = particles[i].X + dx;
                                int y = particles[i].Y + dy;

                                if (x >= 0 && x < w && y >= 0 && y < h)
                                {
                                    int index = y * w + x;
                                    rgbquad[index].rgbRed = colors[i].R;
                                    rgbquad[index].rgbGreen = colors[i].G;
                                    rgbquad[index].rgbBlue = colors[i].B;
                                    rgbquad[index].rgbReserved = 100;
                                }
                            }
                        }
                    }
                }

                BLENDFUNCTION blendFunction = new BLENDFUNCTION
                {
                    BlendOp = AC_SRC_OVER,
                    BlendFlags = 0,
                    SourceConstantAlpha = 255,
                    AlphaFormat = 1
                };

                AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, blendFunction);

                Thread.Sleep(10);
            }
        }

        public static void Pay10()
        {
            while (true)
            {
                var hwnd = GetDesktopWindow();
                var hdc = GetWindowDC(hwnd);
                int x = Screen.PrimaryScreen.Bounds.Width;
                int y = Screen.PrimaryScreen.Bounds.Height;

                StretchBlt(hdc, -50, -60, x + 100, y + 100, hdc, 0, 0, x, y, RasterOperationMode.SRCCOPY);

                ReleaseDC(hwnd, hdc);
            }

        }

        public static void Pay11()
        {
            
        }

    }
}
