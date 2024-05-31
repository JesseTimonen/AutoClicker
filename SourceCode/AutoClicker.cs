using System;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace AutoClicker
{
    public partial class AutoClickerForm
    {
        private void AutoClickerModeButton_Click(object sender, EventArgs e)
        {
            autoClickerDelayMode = !autoClickerDelayMode;

            if (autoClickerDelayMode)
            {
                AutoClickerModeLabel.Text = "Time between clicks (seconds)";
            }
            else
            {
                AutoClickerModeLabel.Text = "Clicks per second";
            }
        }

        private void AutoClickerInputTextBox_Validated(object sender, EventArgs e)
        {
            if (!AutoClickerInputTextBox.Text.All(char.IsDigit) || AutoClickerInputTextBox.Text == "")
            {
                AutoClickerInputTextBox.Text = "0";
            }

            if (int.Parse(AutoClickerInputTextBox.Text) > 60)
            {
                AutoClickerInputTextBox.Text = "60";
            }
        }

        private void ToggleAutoClickerButton_Click(object sender, EventArgs e)
        {
            if (autoClickerEnabled)
            {
                StopAutoClicker();
            }
            else
            {
                StartAutoClicker();
            }
        }

        private void StartAutoClicker()
        {
            AutoClickerInputTextBox_Validated(null, null);
            if (int.Parse(AutoClickerInputTextBox.Text) == 0) { return; }
            autoClickerEnabled = true;
            EnableAutoClickerUIButtons(false);
            AutoClickerStatusLabel.Text = "ON";
            AutoClickerStatusLabel.ForeColor = Color.Green;
            ToggleAutoClickerButton.FlatAppearance.BorderColor = Color.Green;
            ToggleAutoClickerButton.FlatAppearance.BorderSize = 2;

            if (bool.Parse(settings["SameHotkeyForAutoClicker"]))
            {
                if (settings["StartAutoClickerHotkey"].ToLower() == "already in use" || settings["StartAutoClickerHotkey"].ToLower() == "none")
                {
                    ToggleAutoClickerButton.Text = "Stop AutoClicker";
                }
                else
                {
                    ToggleAutoClickerButton.Text = "Stop AutoClicker\n(Hotkey: " + settings["StartAutoClickerHotkey"] + ")";
                }
            }
            else
            {
                if (settings["StopAutoClickerHotkey"].ToLower() == "already in use" || settings["StopAutoClickerHotkey"].ToLower() == "none")
                {
                    ToggleAutoClickerButton.Text = "Stop AutoClicker";
                }
                else
                {
                    ToggleAutoClickerButton.Text = "Stop AutoClicker\n(Hotkey: " + settings["StopAutoClickerHotkey"] + ")";
                }
            }

            if (autoClickerDelayMode)
            {
                AutoClickerTimer.Interval = (int)Math.Ceiling(1000f * float.Parse(AutoClickerInputTextBox.Text));
            }
            else
            {
                AutoClickerTimer.Interval = (int)Math.Ceiling(1000f / float.Parse(AutoClickerInputTextBox.Text));
            }

            if (limitAutoClicker)
            {
                LimitAutoClickerProgressTitle.Visible = true;
                LimitAutoClickerProgressLabel.Visible = true;
                LimitAutoClickerProgressLabel.Text = "";

                if (LimitAutoClickerTypeHours.Checked)
                {
                    limitAutoClickerTime = int.Parse(LimitAutoClickerTextbox.Text) * 60 * 60;
                }
                else if (LimitAutoClickerTypeMinutes.Checked)
                {
                    limitAutoClickerTime = int.Parse(LimitAutoClickerTextbox.Text) * 60;
                }
                else
                {
                    limitAutoClickerTime = int.Parse(LimitAutoClickerTextbox.Text);
                }
            }

            if (bool.Parse(settings["DelayAutoClicker"]))
            {
                if (!autoClickerDelayMode) { AutoClickerDelayCounter = (int)Math.Ceiling(1000f / AutoClickerTimer.Interval); }
            }

            autoClickerActiveTime = 0;
            mouseLastPosition = new int[] { Cursor.Position.X, Cursor.Position.Y };
            autoClickerClickWhileMoving = bool.Parse(settings["ClickWhileMoving"]);
            AutoClickerTimer.Start();
        }

        private void StopAutoClicker()
        {
            autoClickerEnabled = false;
            EnableAutoClickerUIButtons(true);
            AutoClickerStatusLabel.Text = "OFF";
            AutoClickerStatusLabel.ForeColor = Color.Red;
            ToggleAutoClickerButton.FlatAppearance.BorderColor = Color.Red;
            ToggleAutoClickerButton.FlatAppearance.BorderSize = 1;
            AutoClickerTimer.Stop();

            if (limitAutoClicker)
            {
                LimitAutoClickerProgressTitle.Visible = false;
                LimitAutoClickerProgressLabel.Visible = false;
            }

            if (settings["StartAutoClickerHotkey"].ToLower() == "already in use" || settings["StartAutoClickerHotkey"].ToLower() == "none")
            {
                ToggleAutoClickerButton.Text = "Start AutoClicker";
            }
            else
            {
                ToggleAutoClickerButton.Text = "Start AutoClicker\n(Hotkey: " + settings["StartAutoClickerHotkey"] + ")";
            }
        }

        private void EnableAutoClickerUIButtons(bool enable)
        {
            if (enable == false)
            {
                SettingsPanel.Visible = false;
                HotkeysPanel.Visible = false;
                MacrosPanel.Visible = false;
                AutoKeyboardPanel.Visible = false;
                AutoClickerPanel.Visible = true;
            }

            LimitAutoClickerButton.Enabled = enable;
            LimitAutoClickerTextbox.Enabled = enable;
            LimitAutoClickerTypeSeconds.Enabled = enable;
            LimitAutoClickerTypeMinutes.Enabled = enable;
            LimitAutoClickerTypeHours.Enabled = enable;
            LimitAutoClickerTypeClicks.Enabled = enable;
            AutoClickerToMacroButton.Enabled = enable;
            AutoClickerToAutoKeyboardButton.Enabled = enable;
            AutoClickerToHotkeysButton.Enabled = enable;
            AutoClickerToSettingsButton.Enabled = enable;
            AutoClickerModeButton.Enabled = enable;
            AutoClickerInputTextBox.Enabled = enable;
        }

        private void LimitAutoClickerButton_Click(object sender, EventArgs e)
        {
            limitAutoClicker = !limitAutoClicker;

            if (limitAutoClicker)
            {
                LimitAutoClickerButton.Text = "Enabled";
                LimitAutoClickerButton.ForeColor = Color.Green;
                LimitAutoClickerButton.FlatAppearance.BorderColor = Color.Green;
                LimitAutoClickerPanel.Visible = true;
            }
            else
            {
                LimitAutoClickerButton.Text = "Disabled";
                LimitAutoClickerButton.ForeColor = Color.Red;
                LimitAutoClickerButton.FlatAppearance.BorderColor = Color.Red;
                LimitAutoClickerPanel.Visible = false;
            }
        }

        private void LimitAutoClickerTextbox_Validated(object sender, EventArgs e)
        {
            if (!LimitAutoClickerTextbox.Text.All(char.IsDigit) || LimitAutoClickerTextbox.Text == "")
            {
                LimitAutoClickerTextbox.Text = "0";
            }
        }

        private void AutoClickerTimer_Tick(object sender, EventArgs e)
        {
            if (autoClickerClickWhileMoving)
            {
                if (AutoClickerDelayCounter <= 0)
                {
                    LeftMouseClick(Cursor.Position.X, Cursor.Position.Y);
                    if (LimitAutoClickerTypeClicks.Checked) { autoClickerActiveTime++; }
                }
                else
                {
                    AutoClickerDelayCounter--;
                }
            }
            else if (!autoClickerDelayMode)
            {
                AutoClickerDelayCounter--;

                if (AutoClickerDelayCounter <= 0 && mouseLastPosition[0] == Cursor.Position.X && mouseLastPosition[1] == Cursor.Position.Y)
                {
                    LeftMouseClick(Cursor.Position.X, Cursor.Position.Y);
                    if (LimitAutoClickerTypeClicks.Checked) { autoClickerActiveTime++; }
                }
                else
                {
                    if (AutoClickerDelayCounter <= 0)
                    {
                        mouseLastPosition = new int[] { Cursor.Position.X, Cursor.Position.Y };
                        AutoClickerDelayCounter = (int)Math.Ceiling(1000f / AutoClickerTimer.Interval);
                    }
                }
            }
            else
            {
                if (AutoClickerTimer.Interval == 999)
                {
                    if (mouseLastPosition[0] == Cursor.Position.X && mouseLastPosition[1] == Cursor.Position.Y)
                    {
                        LeftMouseClick(Cursor.Position.X, Cursor.Position.Y);
                        if (LimitAutoClickerTypeClicks.Checked) { autoClickerActiveTime++; }

                        if (AutoClickerInputTextBox.Text == "1")
                        {
                            AutoClickerTimer.Interval = 999;
                        }
                        else
                        {
                            AutoClickerTimer.Interval = (int)Math.Ceiling(1000 * (float.Parse(AutoClickerInputTextBox.Text) - 1));
                        }
                    }
                    else
                    {
                        mouseLastPosition = new int[] { Cursor.Position.X, Cursor.Position.Y };
                    }
                }
                else
                {
                    mouseLastPosition = new int[] { Cursor.Position.X, Cursor.Position.Y };
                    AutoClickerTimer.Interval = 999;
                }
            }

            if (limitAutoClicker && (AutoClickerTimer.Interval != 999 || AutoClickerInputTextBox.Text == "1"))
            {
                if (!LimitAutoClickerTypeClicks.Checked)
                {
                    if (autoClickerDelayMode)
                    {
                        autoClickerActiveTime += float.Parse(AutoClickerInputTextBox.Text);
                    }
                    else
                    {
                        autoClickerActiveTime += 1 / float.Parse(AutoClickerInputTextBox.Text);
                    }
                }

                if (LimitAutoClickerTypeClicks.Checked)
                {
                    LimitAutoClickerProgressLabel.Text = autoClickerActiveTime + " / " + limitAutoClickerTime + " clicks";
                }
                else if (LimitAutoClickerTypeSeconds.Checked)
                {
                    LimitAutoClickerProgressLabel.Text = Math.Floor(autoClickerActiveTime) + " / " + limitAutoClickerTime + " seconds";
                }
                else if (LimitAutoClickerTypeMinutes.Checked)
                {
                    LimitAutoClickerProgressLabel.Text = Math.Floor(autoClickerActiveTime) + " / " + limitAutoClickerTime + " seconds (" + limitAutoClickerTime / 60 + " minutes)";
                }
                else if (LimitAutoClickerTypeHours.Checked)
                {
                    LimitAutoClickerProgressLabel.Text = Math.Floor(autoClickerActiveTime) + " / " + limitAutoClickerTime + " seconds (" + limitAutoClickerTime / 3600 + " hours)";
                }

                if (limitAutoClickerTime <= autoClickerActiveTime)
                {
                    StopAutoClicker();
                    return;
                }
            }
        }
    }
}
