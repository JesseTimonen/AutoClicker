using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)][return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private static LowLevelMouseProc mouseProc = HookCallback;
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr hookID = IntPtr.Zero;
        private const int MOUSE_EVENT_LEFT_DOWN = 0x02;
        private const int MOUSE_EVENT_LEFT_UP = 0x04;
        private const int MOUSE_EVENT_RIGHT_DOWN = 0x08;
        private const int MOUSE_EVENT_RIGHT_UP = 0x10;

        private enum MouseMessages
        {
            WM_MOUSEMOVE = 0x0200,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_MOUSEWHEEL = 0x020A
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT point;
            public IntPtr dwExtraInfo;
            public uint mouseData;
            public uint flags;
            public uint time;
        }


        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentModule = currentProcess.MainModule)
            {
                return SetWindowsHookEx(14, proc, GetModuleHandle(currentModule.ModuleName), 0);
            }
        }


        private void ActivateMouseHook()
        {
            hookID = SetHook(mouseProc);
        }


        private void DeactivateMouseHook()
        {
            UnhookWindowsHookEx(hookID);
        }


        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                if (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
                {
                    placeholderNewRecordedMacroInstructions = timerStep + "-" + "Left" + "-" + hookStruct.point.x + "-" + hookStruct.point.y + ",";
                }
                else if (MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam)
                {
                    placeholderNewRecordedMacroInstructions = timerStep + "-" + "Right" + "-" + hookStruct.point.x + "-" + hookStruct.point.y + ",";
                }
                else if (MouseMessages.WM_LBUTTONUP == (MouseMessages)wParam || MouseMessages.WM_RBUTTONUP == (MouseMessages)wParam)
                {
                    newRecordedMacroInstructions += placeholderNewRecordedMacroInstructions;
                }
            }

            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }


        private void MoveMouseToPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }


        private void LeftMouseClick(int x, int y)
        {
            mouse_event(MOUSE_EVENT_LEFT_DOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_LEFT_UP, x, y, 0, 0);
        }


        private void RightMouseClick(int x, int y)
        {
            mouse_event(MOUSE_EVENT_RIGHT_DOWN, x, y, 0, 0);
            mouse_event(MOUSE_EVENT_RIGHT_UP, x, y, 0, 0);
        }
    }
}