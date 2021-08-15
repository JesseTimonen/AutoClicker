using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoClicker
{
    class KeyHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int key;
        private IntPtr hWnd;
        private int ID;

        public KeyHandler(Keys key, int id, Form form)
        {
            this.key = (int)key;
            hWnd = form.Handle;
            ID = id;
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, ID, 0, key);
        }

        public bool Unregister()
        {
            return UnregisterHotKey(hWnd, ID);
        }

        public static Keys ConvertToKey(string input)
        {
            switch (input.ToLower())
            {
                case "a": return Keys.A;
                case "b": return Keys.B;
                case "c": return Keys.C;
                case "d": return Keys.D;
                case "e": return Keys.E;
                case "f": return Keys.F;
                case "g": return Keys.G;
                case "h": return Keys.H;
                case "i": return Keys.I;
                case "j": return Keys.J;
                case "k": return Keys.K;
                case "l": return Keys.L;
                case "m": return Keys.M;
                case "n": return Keys.N;
                case "o": return Keys.O;
                case "p": return Keys.P;
                case "q": return Keys.Q;
                case "r": return Keys.R;
                case "s": return Keys.S;
                case "t": return Keys.T;
                case "u": return Keys.U;
                case "v": return Keys.V;
                case "w": return Keys.W;
                case "x": return Keys.X;
                case "y": return Keys.Y;
                case "z": return Keys.Z;
                case "0": return Keys.D0;
                case "1": return Keys.D1;
                case "2": return Keys.D2;
                case "3": return Keys.D3;
                case "4": return Keys.D4;
                case "5": return Keys.D5;
                case "6": return Keys.D6;
                case "7": return Keys.D7;
                case "8": return Keys.D8;
                case "9": return Keys.D9;
                case "numpad0": return Keys.NumPad0;
                case "numpad1": return Keys.NumPad1;
                case "numpad2": return Keys.NumPad2;
                case "numpad3": return Keys.NumPad3;
                case "numpad4": return Keys.NumPad4;
                case "numpad5": return Keys.NumPad5;
                case "numpad6": return Keys.NumPad6;
                case "numpad7": return Keys.NumPad7;
                case "numpad8": return Keys.NumPad8;
                case "numpad9": return Keys.NumPad9;
                case "space": return Keys.Space;
                case "enter": return Keys.Enter;
                case "tab": return Keys.Tab;
                case "backspace": return Keys.Back;
                case "capslock": return Keys.CapsLock;
                case "scroll lock": return Keys.Scroll;
                case "pageup": return Keys.PageUp;
                case "pagedown": return Keys.PageDown;
                case "up": return Keys.Up;
                case "down": return Keys.Down;
                case "left": return Keys.Left;
                case "right": return Keys.Right;
                case "home": return Keys.Home;
                case "insert": return Keys.Insert;
                case "numlock": return Keys.NumLock;
                case "none": return Keys.None;
                default: return Keys.None;
            }
        }

        public static Keys ConvertToFKey(string input)
        {
            switch (input.ToLower())
            {
                case "f1": return Keys.F1;
                case "f2": return Keys.F2;
                case "f3": return Keys.F3;
                case "f4": return Keys.F4;
                case "f5": return Keys.F5;
                case "f6": return Keys.F6;
                case "f7": return Keys.F7;
                case "f8": return Keys.F8;
                case "f9": return Keys.F9;
                case "f10": return Keys.F10;
                case "f11": return Keys.F11;
                default: return Keys.None;
            }
        }
    }
}