using System;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        private void AutoKeyboardModeButton_Click(object sender, EventArgs e)
        {
            autoKeyboardDelayMode = !autoKeyboardDelayMode;

            if (autoKeyboardDelayMode)
            {
                AutoKeyboardModeLabel.Text = "Time between inputs (seconds)";
            }
            else
            {
                AutoKeyboardModeLabel.Text = "Inputs per second";
            }
        }

        private void AutoKeyboardInputTextBox_Validated(object sender, EventArgs e)
        {
            if (!AutoKeyboardInputTextBox.Text.All(char.IsDigit) || AutoKeyboardInputTextBox.Text == "")
            {
                AutoKeyboardInputTextBox.Text = "0";
            }

            if (int.Parse(AutoKeyboardInputTextBox.Text) > 60)
            {
                AutoKeyboardInputTextBox.Text = "60";
            }
        }

        private void ToggleAutoKeyboardButton_Click(object sender, EventArgs e)
        {
            if (autoKeyboardEnabled)
            {
                StopAutoKeyboard();
            }
            else
            {
                StartAutoKeyboard();
            }
        }

        private void StartAutoKeyboard()
        {
            AutoKeyboardInputTextBox_Validated(null, null);
            autoKeyboardPatternStep = 0;
            AutoKeyboardPatternTextbox_Validated(null, null);
            if (int.Parse(AutoKeyboardInputTextBox.Text) == 0) { return; }
            autoKeyboardEnabled = true;
            EnableAutoKeyboardUIButtons(false);
            AutoKeyboardStatusLabel.Text = "ON";
            AutoKeyboardStatusLabel.ForeColor = Color.Green;
            ToggleAutoKeyboardButton.FlatAppearance.BorderColor = Color.Green;
            ToggleAutoKeyboardButton.FlatAppearance.BorderSize = 2;

            if (bool.Parse(settings["SameHotkeyForAutoKeyboard"]))
            {
                if (settings["StartAutoKeyboardHotkey"].ToLower() == "already in use" || settings["StartAutoKeyboardHotkey"].ToLower() == "none")
                {
                    ToggleAutoKeyboardButton.Text = "Stop AutoKeyboard";
                }
                else
                {
                    ToggleAutoKeyboardButton.Text = "Stop AutoKeyboard\n(Hotkey: " + settings["StartAutoKeyboardHotkey"] + ")";
                }
            }
            else
            {
                if (settings["StopAutoKeyboardHotkey"].ToLower() == "already in use" || settings["StopAutoKeyboardHotkey"].ToLower() == "none")
                {
                    ToggleAutoKeyboardButton.Text = "Stop AutoKeyboard";
                }
                else
                {
                    ToggleAutoKeyboardButton.Text = "Stop AutoKeyboard\n(Hotkey: " + settings["StopAutoKeyboardHotkey"] + ")";
                }
            }

            if (autoKeyboardDelayMode)
            {
                AutoKeyboardTimer.Interval = (int)Math.Ceiling(1000f * float.Parse(AutoKeyboardInputTextBox.Text));
            }
            else
            {
                AutoKeyboardTimer.Interval = (int)Math.Ceiling(1000f / float.Parse(AutoKeyboardInputTextBox.Text));
            }

            AutoKeyboardTimer.Start();
        }

        private void StopAutoKeyboard()
        {
            autoKeyboardEnabled = false;
            EnableAutoKeyboardUIButtons(true);
            AutoKeyboardStatusLabel.Text = "OFF";
            AutoKeyboardStatusLabel.ForeColor = Color.Red;
            ToggleAutoKeyboardButton.FlatAppearance.BorderColor = Color.Red;
            ToggleAutoKeyboardButton.FlatAppearance.BorderSize = 1;
            AutoKeyboardTimer.Stop();

            if (settings["StartAutoKeyboardHotkey"].ToLower() == "already in use" || settings["StartAutoKeyboardHotkey"].ToLower() == "none")
            {
                ToggleAutoKeyboardButton.Text = "Start AutoKeyboard";
            }
            else
            {
                ToggleAutoKeyboardButton.Text = "Start AutoKeyboard\n(Hotkey: " + settings["StartAutoKeyboardHotkey"] + ")";
            }
        }

        private void AutoKeyboardPatternTextbox_Validated(object sender, EventArgs e)
        {
            // Remove "{" and "}" from the pattern
            AutoKeyboardPatternTextbox.Text = AutoKeyboardPatternTextbox.Text.Replace("{", "").Replace("}", "");

            if (AutoKeyboardPatternSeparatorTextbox.Text != "")
            {
                autoKeyboardPattern = AutoKeyboardPatternTextbox.Text.Split(new char[] { AutoKeyboardPatternSeparatorTextbox.Text[0] }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                Array.Resize(ref autoKeyboardPattern, 1);
                autoKeyboardPattern[0] = AutoKeyboardPatternTextbox.Text;
            }
        }

        private void EnableAutoKeyboardUIButtons(bool enable)
        {
            if (enable == false)
            {
                SettingsPanel.Visible = false;
                HotkeysPanel.Visible = false;
                MacrosPanel.Visible = false;
                AutoClickerPanel.Visible = false;
                AutoKeyboardPanel.Visible = true;
            }

            AutoKeyboardInputTextBox.Enabled = enable;
            AutoKeyboardModeButton.Enabled = enable;
            AutoKeyboardPatternTextbox.Enabled = enable;
            AutoKeyboardPatternSeparatorTextbox.Enabled = enable;
            AutoKeyboardToAutoClickerButton.Enabled = enable;
            AutoKeyboardToMacroButton.Enabled = enable;
            AutoKeyboardToHotkeysButton.Enabled = enable;
            AutoKeyboardToSettingsButton.Enabled = enable;
        }

        private void KeyboardTimer_Tick(object sender, EventArgs e)
        {
            if (autoKeyboardPatternStep >= autoKeyboardPattern.Length)
            {
                autoKeyboardPatternStep = 0;
            }

            try
            {
                SendKeys.Send("{" + autoKeyboardPattern[autoKeyboardPatternStep] + "}");
            }
            catch (Exception)
            {
                try
                {
                    SendKeys.Send(autoKeyboardPattern[autoKeyboardPatternStep]);
                }
                catch (Exception)
                {
                    StopAutoKeyboard();
                }
            }

            autoKeyboardPatternStep++;
        }
    }
}