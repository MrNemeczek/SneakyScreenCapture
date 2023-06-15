using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;

namespace SneakySS
{
    internal class Program
    {
        //import shit
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        static extern int GetAsyncKeyState(int i);

        [DllImport("user32.dll")]
        internal static extern bool SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        static int WM_SYSCOMMAND = 0x0112;
        static int SC_CLOSE = 0xF060;

        const int SM_CXSCREEN = 0;//width
        const int SM_CYSCREEN = 1;//height

        static void Main(string[] args)
        {
            SendMessage(Process.GetCurrentProcess().MainWindowHandle, WM_SYSCOMMAND, SC_CLOSE, 0);

            int width = GetSystemMetrics(SM_CXSCREEN);
            int height = GetSystemMetrics(SM_CYSCREEN);
            int screenShotCount = 1;

            while (true)
            {
                Thread.Sleep(5);

                //192 - tylda
                int keyState = GetAsyncKeyState(192);

                if (keyState == 32769)
                {
                    using (var bitmap = new Bitmap(width, height))
                    {
                        using (var graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.CopyFromScreen(Point.Empty, Point.Empty, bitmap.Size);
                        }

                        string filePath = $"screeny/screenshot{screenShotCount}.png";
                        bitmap.Save(filePath);

                        screenShotCount++;
                    }
                }
            }
        }
    }
}
