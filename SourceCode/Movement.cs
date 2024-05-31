using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        // Import necessary user32.dll methods for dragging functionality
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")] private static extern bool ReleaseCapture();

        // Handle the MouseDown event for dragging the form
        private void MoveHandler(MouseEventArgs args)
        {
            // Allows you to drag the application by holding down left mouse button
            if (args.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        // Allows you to drag the application my holding down left mouse button
        private void MacroManagerForm_MouseDown(object sender, MouseEventArgs args)
        {
            MoveHandler(args);
        }

        private void ApplicationTitle_MouseDown(object sender, MouseEventArgs args)
        {
            MoveHandler(args);
        }
    }
}
