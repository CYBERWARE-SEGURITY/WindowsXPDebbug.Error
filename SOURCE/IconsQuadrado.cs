using System;
using System.Drawing;
using static Vanara.PInvoke.Kernel32;
using static Vanara.PInvoke.Gdi32;
using static Vanara.PInvoke.User32;
using System.Windows.Forms;

namespace Project1
{
    public class IconsQuadrado
    {
        static void RotateX(ref VERTEX vtx, float angle)
        {
            float newY = (float)(Math.Cos(angle) * vtx.y - Math.Sin(angle) * vtx.z);
            float newZ = (float)(Math.Sin(angle) * vtx.y + Math.Cos(angle) * vtx.z);
            vtx.y = newY;
            vtx.z = newZ;
        }

        static void RotateY(ref VERTEX vtx, float angle)
        {
            float newX = (float)(Math.Cos(angle) * vtx.x + Math.Sin(angle) * vtx.z);
            float newZ = (float)(-Math.Sin(angle) * vtx.x + Math.Cos(angle) * vtx.z);
            vtx.x = newX;
            vtx.z = newZ;
        }

        static void RotateZ(ref VERTEX vtx, float angle)
        {
            float newX = (float)(Math.Cos(angle) * vtx.x - Math.Sin(angle) * vtx.y);
            float newY = (float)(Math.Sin(angle) * vtx.x + Math.Cos(angle) * vtx.y);
            vtx.x = newX;
            vtx.y = newY;
        }

        static void DrawEdge(IntPtr dc, IntPtr icon, int x0, int y0, int x1, int y1, int r)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = -Math.Abs(y1 - y0);

            int sx = (x0 < x1) ? 1 : -1;
            int sy = (y0 < y1) ? 1 : -1;

            int error = dx + dy;
            int i = 0;

            while (true)
            {
                if (i == 0)
                {
                    DrawIcon(dc, x0, y0, icon);
                    i = 10;
                }
                else
                {
                    i--;
                }

                if (x0 == x1 && y0 == y1)
                    break;

                int e2 = 2 * error;

                if (e2 >= dy)
                {
                    if (x0 == x1)
                        break;

                    error += dy;
                    x0 += sx;
                }

                if (e2 <= dx)
                {
                    if (y0 == y1)
                        break;

                    error += dx;
                    y0 += sy;
                }
            }
        }
        static void DrawEdge(Graphics graphics, Icon icon, int x0, int y0, int x1, int y1, int r)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = -Math.Abs(y1 - y0);

            int sx = (x0 < x1) ? 1 : -1;
            int sy = (y0 < y1) ? 1 : -1;

            int error = dx + dy;
            int i = 0;

            while (true)
            {
                if (i == 0)
                {
                    graphics.DrawIcon(icon, x0, y0);
                    i = 10;
                }
                else
                {
                    i--;
                }

                if (x0 == x1 && y0 == y1)
                    break;

                int e2 = 2 * error;

                if (e2 >= dy)
                {
                    if (x0 == x1)
                        break;

                    error += dy;
                    x0 += sx;
                }

                if (e2 <= dx)
                {
                    if (y0 == y1)
                        break;

                    error += dx;
                    y0 += sy;
                }
            }
        }

        enum IconType : uint
        {
            IDI_ERROR = 32513,
            IDI_WARNING = 32515,
            IDI_INFORMATION = 32516,
            IDI_APPLICATION = 32512
        }

        static int w = Screen.PrimaryScreen.Bounds.Width;
        static int h = Screen.PrimaryScreen.Bounds.Height;

        public static void CuboDeIcons()
        {
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                float size = (w + h) / 10;
                int cx = (int)size;
                int cy = (int)size;
                int xdv = 100;
                int ydv = 100;
                float angleX = 13;
                float angleY = 3;
                float angleZ = 3;
                int d = 70;

                VERTEX[] vtx = new VERTEX[]
                {
            new VERTEX { x = size, y = 0, z = 0 },
            new VERTEX { x = size, y = size, z = 0 },
            new VERTEX { x = 0, y = size, z = 0 },
            new VERTEX { x = 0, y = 0, z = 0 },
            new VERTEX { x = size, y = 0, z = size },
            new VERTEX { x = size, y = size, z = size },
            new VERTEX { x = 0, y = size, z = size },
            new VERTEX { x = 0, y = 0, z = size },
            new VERTEX { x = size - d, y = d, z = d },
            new VERTEX { x = size - d, y = size - d, z = d },
            new VERTEX { x = d, y = size - d, z = d },
            new VERTEX { x = d, y = d, z = d },
            new VERTEX { x = size - d, y = d, z = size - d },
            new VERTEX { x = size - d, y = size - d, z = size - d },
            new VERTEX { x = d, y = size - d, z = size - d },
            new VERTEX { x = d, y = d, z = size - d }
                };

                EDGE[] edges = new EDGE[]
                {
            new EDGE { vtx0 = 0, vtx1 = 1 },
            new EDGE { vtx0 = 1, vtx1 = 2 },
            new EDGE { vtx0 = 2, vtx1 = 3 },
            new EDGE { vtx0 = 3, vtx1 = 0 },
            new EDGE { vtx0 = 0, vtx1 = 4 },
            new EDGE { vtx0 = 1, vtx1 = 5 },
            new EDGE { vtx0 = 2, vtx1 = 6 },
            new EDGE { vtx0 = 3, vtx1 = 7 },
            new EDGE { vtx0 = 4, vtx1 = 5 },
            new EDGE { vtx0 = 5, vtx1 = 6 },
            new EDGE { vtx0 = 6, vtx1 = 7 },
            new EDGE { vtx0 = 7, vtx1 = 4 },
            new EDGE { vtx0 = 8, vtx1 = 9 },
            new EDGE { vtx0 = 9, vtx1 = 10 },
            new EDGE { vtx0 = 10, vtx1 = 11 },
            new EDGE { vtx0 = 11, vtx1 = 8 },
            new EDGE { vtx0 = 8, vtx1 = 12 },
            new EDGE { vtx0 = 9, vtx1 = 13 },
            new EDGE { vtx0 = 10, vtx1 = 14 },
            new EDGE { vtx0 = 11, vtx1 = 15 },
            new EDGE { vtx0 = 12, vtx1 = 13 },
            new EDGE { vtx0 = 13, vtx1 = 14 },
            new EDGE { vtx0 = 14, vtx1 = 15 },
            new EDGE { vtx0 = 15, vtx1 = 12 },
            new EDGE { vtx0 = 0, vtx1 = 8 },
            new EDGE { vtx0 = 10, vtx1 = 9 },
            new EDGE { vtx0 = 2, vtx1 = 10 },
            new EDGE { vtx0 = 3, vtx1 = 11 },
            new EDGE { vtx0 = 4, vtx1 = 12 },
            new EDGE { vtx0 = 5, vtx1 = 13 },
            new EDGE { vtx0 = 6, vtx1 = 14 },
            new EDGE { vtx0 = 7, vtx1 = 15 }
                };

                int totvtx = vtx.Length;
                int totedg = edges.Length;
                IconType currentIcon = IconType.IDI_ERROR; // Começa com o ícone de erro

                while (true)
                {
                    // Alterna o tipo de ícone a cada iteração
                    switch (currentIcon)
                    {
                        case IconType.IDI_ERROR:
                            currentIcon = IconType.IDI_WARNING;
                            break;
                        case IconType.IDI_WARNING:
                            currentIcon = IconType.IDI_INFORMATION;
                            break;
                        case IconType.IDI_INFORMATION:
                            currentIcon = IconType.IDI_APPLICATION;
                            break;
                        case IconType.IDI_APPLICATION:
                            currentIcon = IconType.IDI_ERROR;
                            break;
                    }

                    Icon iconToDraw = SystemIcons.Error;
                    switch (currentIcon)
                    {
                        case IconType.IDI_ERROR:
                            iconToDraw = SystemIcons.Error;
                            break;
                        case IconType.IDI_WARNING:
                            iconToDraw = SystemIcons.Warning;
                            break;
                        case IconType.IDI_INFORMATION:
                            iconToDraw = SystemIcons.Information;
                            break;
                        case IconType.IDI_APPLICATION:
                            iconToDraw = SystemIcons.Application;
                            break;
                    }

                    for (int i = 0; i < totvtx; i++)
                    {
                        RotateX(ref vtx[i], angleX);
                        RotateY(ref vtx[i], angleY);
                        RotateZ(ref vtx[i], angleZ);
                    }

                    for (int i = 0; i < totedg; i++)
                    {
                        DrawEdge(graphics, iconToDraw, (int)vtx[edges[i].vtx0].x + cx, (int)vtx[edges[i].vtx0].y + cy,
                                 (int)vtx[edges[i].vtx1].x + cx, (int)vtx[edges[i].vtx1].y + cy, 20);
                    }

                    cx += xdv;
                    cy += ydv;

                    if (cx > w - (size / 2) || cx < -(size / 2))
                        xdv *= -1;

                    if (cy > h - (size / 2) || cy < -(size / 2))
                        ydv *= -1;
                }
            }
        }


        struct VERTEX
        {
            public float x;
            public float y;
            public float z;
        }

        struct EDGE
        {
            public int vtx0;
            public int vtx1;
        }
    }
}
