using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ATLib.Input
{
    public class HookDll
    {
        private KBDLLHOOKSTRUCT kbdllhs;
        private IntPtr iHookHandle = IntPtr.Zero;
        private GCHandle _hookProcHandle;
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int hookid, HookProc pfnhook, IntPtr hinst, int threadid);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wparam, IntPtr lparam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyMemory(ref KBDLLHOOKSTRUCT Source, IntPtr Destination, int Length);

        private const int WH_KEYBOARD = 13;

        public void DisableKBDHook()
        {
            try
            {
                if (iHookHandle != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(iHookHandle);
                }
                _hookProcHandle.Free();
                iHookHandle = IntPtr.Zero;
            }
            catch
            {
                return;
            }
        }
        public void EnableKBDHook()
        {
            HookProc hookProc = new HookProc(KBDDelegate);
            _hookProcHandle = GCHandle.Alloc(hookProc);
            iHookHandle = SetWindowsHookEx(WH_KEYBOARD, hookProc, GetModuleHandle("HookDll.dll"), 0);
            if (iHookHandle == IntPtr.Zero)
            {
                throw new System.Exception("错误,钩子失败!");
            }
        }
        public IntPtr KBDDelegate(int iCode, IntPtr wParam, IntPtr lParam)
        {
            kbdllhs = new KBDLLHOOKSTRUCT();
            CopyMemory(ref kbdllhs, lParam, 20);

            //结果就在这里了^_^
            int iHookCode = kbdllhs.vkCode;
            DisableKBDHook();
            EnableKBDHook();
            return CallNextHookEx(iHookHandle, iCode, wParam, lParam);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct KBDLLHOOKSTRUCT
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }
}
