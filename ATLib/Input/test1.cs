using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATLib.Input
{
    public class test1
    {
        public static void RightClick(int x, int y)
        {
            NativeStructs.Input input = new NativeStructs.Input
            {
                type = NativeEnums.SendInputEventType.Mouse,
                mouseInput = new NativeStructs.MouseInput
                {
                    dx = 0,
                    dy = 0,
                    mouseData = 0,
                    dwFlags = NativeEnums.MouseEventFlags.Absolute | NativeEnums.MouseEventFlags.RightDown | NativeEnums.MouseEventFlags.Move,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero,
                },
            };

            var primaryScreen = Screen.PrimaryScreen;
            input.mouseInput.dx = Convert.ToInt32((x - primaryScreen.Bounds.Left) * 65536 / primaryScreen.Bounds.Width);
            input.mouseInput.dy = Convert.ToInt32((y - primaryScreen.Bounds.Top) * 65536 / primaryScreen.Bounds.Height);
            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));
            input.mouseInput.dwFlags = NativeEnums.MouseEventFlags.Absolute | NativeEnums.MouseEventFlags.RightUp | NativeEnums.MouseEventFlags.Move;
            NativeMethods.SendInput(1, ref input, Marshal.SizeOf(input));
        }

        internal static class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern uint SendInput(uint nInputs, ref NativeStructs.Input pInputs, int cbSize);
        }

        internal static class NativeStructs
        {
            [StructLayout(LayoutKind.Sequential)]
            internal struct Input
            {
                public NativeEnums.SendInputEventType type;
                public MouseInput mouseInput;
            }

            [StructLayout(LayoutKind.Sequential)]
            internal struct MouseInput
            {
                public int dx;
                public int dy;
                public uint mouseData;
                public NativeEnums.MouseEventFlags dwFlags;
                public uint time;
                public IntPtr dwExtraInfo;
            }
        }

        internal static class NativeEnums
        {
            internal enum SendInputEventType : int
            {
                Mouse = 0,
                Keyboard = 1,
                Hardware = 2,
            }

            [Flags]
            internal enum MouseEventFlags : uint
            {
                Move = 0x0001,
                LeftDown = 0x0002,
                LeftUp = 0x0004,
                RightDown = 0x0008,
                RightUp = 0x0010,
                MiddleDown = 0x0020,
                MiddleUp = 0x0040,
                XDown = 0x0080,
                XUp = 0x0100,
                Wheel = 0x0800,
                Absolute = 0x8000,
            }
        }
    }
}
