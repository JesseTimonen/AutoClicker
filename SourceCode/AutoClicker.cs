using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoClicker
{
    public partial class AutoClicker : Form
    {
        private KeyHandler keyHandler;
        private bool isActive = false;
        private string mode = "mouse";
        private bool isFastMode = true;
        private double speed;
        private int iterationCount = 0;
        private int timeCount = 0;
        private int step;
        string[] pattern;


        public AutoClicker()
        {
            InitializeComponent();
            this.FormClosing += AutoClicker_FormClosing;
            keyHandler = new KeyHandler(Keys.PageDown, this);
            keyHandler.Register();
        }


        private void AutoClicker_FormClosing(Object sender, FormClosingEventArgs e)
        {
            keyHandler.Unregister();
        }


        // =================================== RESETS =================================== \\
        private void ResetStatistics()
        {
            timeCount = 0;
            iterationCount = 0;
            timeLabel.Text = "Time: 0 seconds";
            iterationLabel.Text = "Iterations: 0";
        }


        private void UpdateTexts()
        {
            if (mode == "mouse")
            {
                if (isFastMode)
                {
                    speedLabel.Text = "Clicks / Second";
                }
                else
                {
                    speedLabel.Text = "Time between clicks";
                }
            }
            else
            {
                if (isFastMode)
                {
                    speedLabel.Text = "Iterations / Second";
                }
                else
                {
                    speedLabel.Text = "Time between iterations";
                }
            }
        }


        // =================================== BUTTONS =================================== \\
        private void startButton_Click(object sender, EventArgs e)
        {
            Start();
        }


        private void endButton_Click(object sender, EventArgs e)
        {
            End();
        }


        private void mouseButton_Click(object sender, EventArgs e)
        {
            patternLabel.Visible = false;
            patternTextbox.Visible = false;
            patternSeperatorLabel.Visible = false;
            patternSeperatorTextbox.Visible = false;
            mode = "mouse";

            UpdateTexts();
            ResetStatistics();
            End();
        }


        private void keyboardButton_Click(object sender, EventArgs e)
        {
            patternLabel.Visible = true;
            patternTextbox.Visible = true;
            patternSeperatorLabel.Visible = true;
            patternSeperatorTextbox.Visible = true;
            mode = "keyboard";

            UpdateTexts();
            ResetStatistics();
            End();
        }


        private void modeButton_Click(object sender, EventArgs e)
        {
            isFastMode = !isFastMode;

            UpdateTexts();
            ResetStatistics();
        }


        // =================================== HOTKEYS =================================== \\
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
            {
                HandleHotkey();
            }
            base.WndProc(ref m);
        }


        private void HandleHotkey()
        {
            if (isActive)
            {
                End();
            }
            else
            {
                Start();
            }
        }


        // ==================================== TIMERS =================================== \\
        private void timeTimer_Tick(object sender, EventArgs e)
        {
            timeCount++;
            timeLabel.Text = $"Time: {timeCount.ToString()} seconds";
        }


        private void iterationTimer_Tick(object sender, EventArgs e)
        {
            iterationCount++;
            iterationLabel.Text = $"Iterations: {iterationCount.ToString()}";

            if (mode == "mouse")
            {
                LeftMouseClick(Cursor.Position.X, Cursor.Position.Y);
            }
            else
            {
                step++;
                if (step >= pattern.Length){ step = 0; }

                try
                {
                    SendKeys.Send("{" + pattern[step] + "}");
                }
                catch
                {
                    try
                    {
                        SendKeys.Send(pattern[step]);
                    }
                    catch
                    {
                        End();
                    }
                }
            }
        }


        // ==================================== START ==================================== \\
        private void Start()
        {
            try
            {
                speed = Convert.ToDouble(speedTextbox.Text.Replace('.', ','));

                if (speed <= 0) return;
                speed = (speed > 60) ? 60 : speed;

                speedTextbox.Text = speed.ToString();
            }
            catch (Exception)
            {
                speedTextbox.Text = "";
                return;
            }

            if (mode == "keyboard" && patternTextbox.Text == "")
            {
                return;
            }


            startButton.Enabled = false;
            modeButton.Enabled = false;
            endButton.Enabled = true;
            iterationTimer.Enabled = true;
            timeTimer.Enabled = true;
            isActive = true;
            statusLabel.ForeColor = Color.Green;
            statusLabel.Text = "ON";
            ResetStatistics();


            // Set the timer intervals using user's time settings
            if (isFastMode)
            {
                iterationTimer.Interval = (int)Math.Ceiling(1000 / speed);
            }
            else
            {
                iterationTimer.Interval = (int)Math.Ceiling(1000 * speed);
            }

            // Additional keyboard initialization
            if (mode == "keyboard")
            {
                // Remove "{" and "}" from the pattern
                patternTextbox.Text = patternTextbox.Text.Replace("{", "").Replace("}", "");

                // Get Pattern Seperator
                if (patternSeperatorTextbox.Text != "")
                {
                    pattern = patternTextbox.Text.Split(new char[] { patternSeperatorTextbox.Text[0] }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    Array.Resize(ref pattern, 1);
                    pattern[0] = patternTextbox.Text;
                }

                // Reset Pattern counter
                step = -1;
            }
        }


        // ===================================== END ===================================== \\
        private void End()
        {
            startButton.Enabled = true;
            modeButton.Enabled = true;
            endButton.Enabled = false;
            iterationTimer.Enabled = false;
            timeTimer.Enabled = false;
            isActive = false;
            statusLabel.ForeColor = Color.Red;
            statusLabel.Text = "OFF";
        }


        // ================================= MOUSE CLICK ================================= \\
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }
    }


    // ================================ HOTKEY HANDLER =============================== \\
    public static class Constants
    {
        public const int WM_HOTKEY_MSG_ID = 0x0312;
    }


    public class KeyHandler
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int key;
        private IntPtr hWnd;
        private int id;

        public KeyHandler(Keys key, Form form)
        {
            this.key = (int)key;
            hWnd = form.Handle;
            id = GetHashCode();
        }

        public override int GetHashCode()
        {
            return key ^ hWnd.ToInt32();
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, 0, key);
        }

        public bool Unregister()
        {
            return UnregisterHotKey(hWnd, id);
        }
    }
}